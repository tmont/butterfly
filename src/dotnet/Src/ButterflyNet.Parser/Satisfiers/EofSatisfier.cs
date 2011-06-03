namespace ButterflyNet.Parser.Satisfiers {
	public class EofSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return context.Input.IsEof;
		}
	}
}