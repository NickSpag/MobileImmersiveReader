using System;
using System.Runtime.CompilerServices;

namespace MobileImmersiveReader
{
    public static partial class Help
    {
        public static class Log
        {
            public static void Message(string message, [CallerMemberName] string memberName = "") =>
                WriteMessage(message, memberName, false);

            public static void Exception(Exception ex, [CallerMemberName] string memberName = "") =>
                WriteMessage(ex.Message, memberName, true);

            public static void Error(string message, [CallerMemberName] string memberName = "") =>
                WriteMessage(message, memberName, true);

            private static void WriteMessage(string message, string callingMember, bool error)
            {
                var messageTypeText = error ? "ERROR" : "MESSAGE";

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"LOG {messageTypeText}: Origin memeber {callingMember}");
                System.Diagnostics.Debug.WriteLine($"       CONTENT: {message}");
#else
                System.Console.WriteLine($"LOG {messageTypeText}: Origin memeber {callingMember}");
                System.Console.WriteLine($"       CONTENT: {message}");
#endif
            }
        }
    }
}
