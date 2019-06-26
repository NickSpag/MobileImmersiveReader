## MobileImmersiveReader
A Xamarin app using Azure Computer Vision's Read API and the Azure Immersive Reader's javascript sdk.


### Background:
The [Immersive Reader](https://azure.microsoft.com/en-us/services/cognitive-services/immersive-reader/) is an Azure Cognitive Service for developers who want to embed inclusive capabilities into their apps for enhancing text reading and comprehension for users regardless of age or ability. It's largely designed, from what it seems, to be added to web properties to easily extend their inclusive functionality. I wanted to try expanding the capabilities a little by integrating a "Take Photo" functionality. 

### App:
This is a Xamarin.iOS sample application. It uploads an image of a book page or text, either taken with the camera or picked from the gallery, to [Azure Computer Vision](https://azure.microsoft.com/en-us/services/cognitive-services/computer-vision/) to be read. The resulting text is built in to a makeshift html page that loads and launches the Javascript sdk for the Immersive Reader, which is pushed in to a WKWebView.

Since it's being demo'd on a phone with a retina screen and there aren't yet options to granularly control the UI of the reader, the controls are a bit small. I'd need to play with increasing the web view's zoom/scale/etc, as well as loading the reader with preset options if that functionality becomes available in the future. But it's a fun example in the meantime:

Using this app: You'll need an [Azure subscription](https://azure.microsoft.com/en-us/free/) with an Immersive Reader resource, and a Cognitive Services resource. Once those are created, place the keys and endpoint urls in the respective string decelerations in the [Help/PrivateKeys.cs](https://github.com/NickSpag/MobileImmersiveReader/blob/master/src/MobileImmersiveReader/Help/PrivateKeys.cs) file.

![An animated gif of the app showing the gallery button being pressed, a photo of a book page being picked, an "Analyzing" notification, and ending with the immersive reader showing that book's text](https://raw.githubusercontent.com/NickSpag/MobileImmersiveReader/master/docs/exampleGifpt1.gif)
![An animated gif of the app where the immersive reader's options are shown. Enables line focus item and a gray overlay on the reader obscures only a few lines. Enables the sylables option and the reader's words split in to sylables. Enables the noun highlighting and the nouns in the reader's page turn purple. Increases the text size and the reader's text gets substantially larger](https://raw.githubusercontent.com/NickSpag/MobileImmersiveReader/master/docs/exampleGifpt2.gif)

### License
Provided open source under MIT

