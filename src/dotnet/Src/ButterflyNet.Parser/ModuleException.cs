using System;

namespace ButterflyNet.Parser {
	public class ModuleException : Exception {
		public ModuleException(string message, Exception innerException = null) : base(message, innerException) { }
	}
}