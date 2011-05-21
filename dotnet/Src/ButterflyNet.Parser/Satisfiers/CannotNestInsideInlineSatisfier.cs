using System.Linq;

namespace ButterflyNet.Parser.Satisfiers {
	public class CannotNestInsideInlineSatisfier : ISatisfier {
		public bool IsSatisfiedBy(ParseContext context) {
			return !context.Scopes.Any(scope => scope.Level == ScopeLevel.Inline);
		}
	}
}