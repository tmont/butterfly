using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class CharacterSatisfier : ISatisfier {
		private readonly string characters;

		public CharacterSatisfier(char character) : this(new string(character, 1)) { }

		public CharacterSatisfier(string characters) {
			if (string.IsNullOrEmpty(characters)) {
				throw new ArgumentNullException("characters");
			}

			this.characters = characters;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			if (characters.Length == 1) {
				return context.CurrentChar == characters[0];
			}

			return characters == (char)context.CurrentChar + context.Input.Peek(characters.Length - 1);
		}
	}
}