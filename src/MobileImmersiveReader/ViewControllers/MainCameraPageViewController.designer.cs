// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MobileImmersiveReader
{
    [Register("MainCameraPageViewController")]
    partial class MainCameraPageViewController
    {
        [Outlet]
        UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

        [Outlet]
        UIKit.UILabel DirectionsLabel { get; set; }

        [Outlet]
        UIKit.UIView StatusLabelGroup { get; set; }

        [Outlet]
        UIKit.UILabel StatusMessageLabel { get; set; }

        [Outlet]
        WebKit.WKWebView WebView { get; set; }

        [Action("AddButtonPressed:")]
        partial void AddButtonPressed(Foundation.NSObject sender);

        [Action("LibraryButtonPressed:")]
        partial void LibraryButtonPressed(Foundation.NSObject sender);

        void ReleaseDesignerOutlets()
        {
            if (ActivityIndicator != null)
            {
                ActivityIndicator.Dispose();
                ActivityIndicator = null;
            }

            if (DirectionsLabel != null)
            {
                DirectionsLabel.Dispose();
                DirectionsLabel = null;
            }

            if (StatusMessageLabel != null)
            {
                StatusMessageLabel.Dispose();
                StatusMessageLabel = null;
            }

            if (StatusLabelGroup != null)
            {
                StatusLabelGroup.Dispose();
                StatusLabelGroup = null;
            }

            if (WebView != null)
            {
                WebView.Dispose();
                WebView = null;
            }
        }
    }
}
