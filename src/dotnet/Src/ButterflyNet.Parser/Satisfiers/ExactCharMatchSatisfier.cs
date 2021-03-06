﻿namespace ButterflyNet.Parser.Strategies {
	public class ExactCharMatchSatisfier : ISatisfier {
		private readonly string chars;

		public ExactCharMatchSatisfier(string chars) {
			this.chars = chars;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			if (chars.Length == 1) {
				return chars[0] == context.CurrentChar;
			}

			return (char)context.CurrentChar + context.Input.Peek(chars.Length - 1) == chars;
		}
	}
}