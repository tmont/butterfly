namespace ButterflyNet.Parser.Satisfiers {
	public class NextCharacterIsNotTheSameSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return context.Input.Peek() != context.CurrentChar;
		}
	}
}