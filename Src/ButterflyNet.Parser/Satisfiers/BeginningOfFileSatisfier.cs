namespace ButterflyNet.Parser.Satisfiers {
	public sealed class BeginningOfFileSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return context.Input.IsStartOfFile;
		}
	}
}