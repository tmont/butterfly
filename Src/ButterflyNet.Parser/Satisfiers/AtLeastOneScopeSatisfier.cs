namespace ButterflyNet.Parser.Satisfiers {
	public class AtLeastOneScopeSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return !context.Scopes.IsEmpty();
		}
	}
}