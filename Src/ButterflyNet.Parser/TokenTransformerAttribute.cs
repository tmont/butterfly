using System;

namespace ButterflyNet.Parser {
	public sealed class TokenTransformerAttribute : Attribute {
		public TokenTransformerAttribute(string token) {
			Token = token;
		}

		public string Token { get; private set; }

		public bool RegexMatch { get; set; }
	}
}