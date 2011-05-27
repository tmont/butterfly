namespace ButterflyNet.Parser.Satisfiers {
	public class StartOfLineSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return context.Input.IsStartOfLine;
		}
	}
}