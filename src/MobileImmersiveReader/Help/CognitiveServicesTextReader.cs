using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace MobileImmersiveReader
{
    public static partial class Help
    {
        public static class CognitiveServicesTextReader
        {
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

                builder.Replace("\'", "\\" + "\'");
                builder.Replace("\"", "\\" + "\"");

                return builder.ToString();
            }
        }
    }
}
