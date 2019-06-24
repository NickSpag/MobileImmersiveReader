using System.Collections.Generic;
using System.Text;

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace MobileImmersiveReader
{
    public static partial class Help
    {
        public static class CognitiveServicesTextReader
        {
            //TODO: Break in to chunks for larger results. stores all the content in one for now
            public static string GenerateJavascriptArray(IList<TextRecognitionResult> results)
            {
                var builder = new StringBuilder();

                foreach (var result in results)
                {
                    foreach (var line in result.Lines)
                    {
                        builder.Append(line.Text + " ");
                        System.Console.WriteLine(line.Text);
                    }
                }

                //probably isn't exhaustive of all the escaping that will need to be done
                builder.Replace("\'", "\\" + "\'");
                builder.Replace("\"", "\\" + "\"");

                return builder.ToString();
            }
        }
    }
}
