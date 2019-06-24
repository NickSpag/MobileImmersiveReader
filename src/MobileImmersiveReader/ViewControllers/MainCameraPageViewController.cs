using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

using Foundation;
using UIKit;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace MobileImmersiveReader
{
    public partial class MainCameraPageViewController : UIViewController
    {
        private bool notificationShowing = false;
        private ComputerVisionClient computerVisionClient;
        private HttpClient httpClient;
        private Task<string> retreiveTokenTask;

        public MainCameraPageViewController(IntPtr handle) : base(handle)
        {
            httpClient = new HttpClient();
            var credentials = new ApiKeyServiceClientCredentials(Help.PrivateKeys.CognitiveServicesKey);

            computerVisionClient = new ComputerVisionClient(credentials, httpClient, false);
            computerVisionClient.Endpoint = Help.PrivateKeys.CognitiveServicesEndpoint;
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            retreiveTokenTask = Task.Run(Help.ImmersiveReader.GetTokenAsync);

            //warm up webview to avoid first load delay penalty
            WebView.LoadHtmlString("", NSBundle.MainBundle.BundleUrl);

        }

        #region User Actions
        async partial void LibraryButtonPressed(NSObject sender)
        {
            await Help.Permissions.RequestIfNotGranted(Permission.Photos);

            if (await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()) is MediaFile photoFromLibrary)
            {
                await ReadPhoto(photoFromLibrary);
            }
        }

        async partial void AddButtonPressed(NSObject sender)
        {
            await Help.Permissions.RequestIfNotGranted(Permission.Camera);

            var options = new StoreCameraMediaOptions()
            {
                AllowCropping = true,
                PhotoSize = PhotoSize.Full
            };

            if (await CrossMedia.Current.TakePhotoAsync(options) is MediaFile photo)
            {
                await ReadPhoto(photo);
            }
        }
        #endregion

        private void ToggleDirectionsVisibility(bool showing)
        {
            InvokeOnMainThread(() =>
            {
                DirectionsLabel.Hidden = !showing;
            });
        }

        private void ShowActivityNotification(string message)
        {
            if (!notificationShowing)
            {
                InvokeOnMainThread(() =>
                {
                    ToggleNotificationVisibility(true);
                    StatusMessageLabel.Text = message;
                });
            }
            else
            {
                InvokeOnMainThread(() => StatusMessageLabel.Text = message);
            }
        }

        private void HideActivityNotification()
        {
            if (notificationShowing)
            {
                InvokeOnMainThread(() => ToggleNotificationVisibility(false));
            }
        }

        private void ToggleNotificationVisibility(bool showing)
        {
            InvokeOnMainThread(() =>
            {
                notificationShowing = showing;

                if (showing)
                {
                    ActivityIndicator.StartAnimating();
                }
                else
                {
                    ActivityIndicator.StopAnimating();
                }

                StatusLabelGroup.Hidden = !showing;
            });
        }

        private async Task ReadPhoto(MediaFile photo)
        {
            ShowActivityNotification("Analyzing photo.");

            if (await AnalyzePhoto(photo) is IList<TextRecognitionResult> recognitionResults)
            {
                ShowActivityNotification("Immersing...");

                var token = await retreiveTokenTask;

                var textContent = Help.CognitiveServicesTextReader.GenerateJavascriptArray(recognitionResults);
                var html = Help.ImmersiveReader.GeneratePageHtml(textContent, token);

                ToggleDirectionsVisibility(false);

                WebView.LoadHtmlString(html, NSBundle.MainBundle.BundleUrl);

                //TODO: Use WKWebView's delegate to get more granular control of loading. Leave "Immersing" notification as overly until finished loading the html body's onload script
                await Task.Delay(TimeSpan.FromSeconds(1));
                HideActivityNotification();
            }
            else
            {
                ShowActivityNotification("There was a problem!");

                await Task.Delay(TimeSpan.FromSeconds(2));

                ToggleDirectionsVisibility(true);
                ToggleNotificationVisibility(false);
            }
        }

        private async Task<IList<TextRecognitionResult>> AnalyzePhoto(MediaFile photo)
        {
            try
            {
                var stream = photo.GetStream();

                var headers = await computerVisionClient.BatchReadFileInStreamAsync(stream);
                var operationPath = new Uri(headers.OperationLocation);
                var operationId = operationPath.Segments[operationPath.Segments.Length - 1];

                for (int i = 0; i < 5; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(3));

                    var httpRequestResponse = await computerVisionClient.GetReadOperationResultWithHttpMessagesAsync(operationId);

                    if (httpRequestResponse.Response.IsSuccessStatusCode)
                    {
                        var result = httpRequestResponse.Body;

                        switch (result.Status)
                        {
                            case TextOperationStatusCodes.NotStarted:
                            case TextOperationStatusCodes.Running:
                                break;
                            case TextOperationStatusCodes.Succeeded:
                                return result.RecognitionResults;
                            case TextOperationStatusCodes.Failed:
                            default:
                                Help.Log.Error("CogServices Error Code Returned");
                                return null;
                        }
                    }

                    if (i == 4)
                    {
                        Help.Log.Error("CogServices Timeout");
                    }
                }
            }
            catch (Exception ex)
            {
                Help.Log.Exception(ex);
            }

            return null;
        }
    }
}
