namespace ButterflyNet.Parser.Satisfiers {
	public class CannotNestInsideModuleSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return !context.Scopes.ContainsType(ScopeTypeCache.Module);
		}
	}
}