using System;

namespace MobileImmersiveReader
{
    public static partial class Help
    {

#error Your API Keys are missing. Follow the guide in the ReadMe.md to create your Azure resources, replace the keys and endpoints here, and comment out this error to continue.
        public static class PrivateKeys
        {
            public const string ImmersiveReaderKey = "";
            //example for West US Immersive Reader Azure Resource, found in the Overview tab of the resource: https://westus.api.cognitive.microsoft.com/sts/v1.0/issuetoken 
            public const string ImmersiveReaderEndpoint = "";

            public const string CognitiveServicesKey = "";
            //example for West US Cognitive Services Azure Resource, found in the Overview tab of the resource: https://westus2.api.cognitive.microsoft.com/
            public const string CognitiveServicesEndpoint = "";
        }
    }
}
