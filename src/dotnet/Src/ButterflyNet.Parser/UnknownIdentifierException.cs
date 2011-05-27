using System;

namespace ButterflyNet.Parser {
	public class UnknownIdentifierException : Exception {
		public UnknownIdentifierException(string identifier) : base(string.Format("Could not locate type for identifier \"{0}\"", identifier)) { }
	}
}