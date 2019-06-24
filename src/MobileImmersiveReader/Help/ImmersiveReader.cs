using System.Threading.Tasks;

namespace MobileImmersiveReader
{
    public static partial class Help
    {
        public static class ImmersiveReader
        {
            private const string immersiveReaderSdkUrl = "https://contentstorage.onenote.office.net/onenoteltir/immersivereadersdk/immersive-reader-sdk.preview.js";

            public static async Task<string> GetTokenAsync()
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Help.PrivateKeys.ImmersiveReaderKey);
                    using (var response = await client.PostAsync(Help.PrivateKeys.ImmersiveReaderEndpoint, null))
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }

            private static string LocalScripts(string token, string content)
            {
                return
                "<script type='text/javascript'>" +
                "function launchImmersiveReader() {" +
                    "const content = {" +
                        "title: 'ImmersiverReader'," +
                        "chunks: [ {" +
                            $"content: '{content}'" +
                        "}]" +
                    "};" +
                    "" +
                    $"const token = '{token}';" +
                    "ImmersiveReader.launchAsync(token, null, content);" +
                "}" +
                "</script>";
            }

            public static string ContentHtml(string scripts)
            {
                return
                "<html>" +
                "<head>" +
                    "<meta charset='utf-8'>" +
                    $"<script type='text/javascript' src='{immersiveReaderSdkUrl}'></script>" +
                "</head>" +
                "" +
                "<body onload='launchImmersiveReader()'>" +
                    "<div class='immersive-reader-button' data-button-style='iconAndText' onclick='launchImmersiveReader()'></div>" +
                    scripts +
                "</body>" +
                "</html>";
            }

            public static string GeneratePageHtml(string content, string token)
            {
                var scripts = LocalScripts(token, content);
                return ContentHtml(scripts);
            }
        }
    }
}

