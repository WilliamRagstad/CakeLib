using System;
using System.Collections.Generic;
using System.Text;
using Console = EzConsole.EzConsole;

namespace CakeLang
{
    internal static class ErrorManager
    {
        internal class GeneralError : Exception { public GeneralError(string message, string type = "ERROR") : base('[' + type + "] " + message) { } }
        internal class IOError : GeneralError { public IOError(string message) : base(message, "IO ERROR") { } }
        internal class InjectError : GeneralError { public InjectError(string message) : base(message, "INJECTION ERROR") { } }
    }
}
