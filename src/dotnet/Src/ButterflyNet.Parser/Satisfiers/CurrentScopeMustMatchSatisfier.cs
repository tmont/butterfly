using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class CurrentScopeMustMatchSatisfier : ISatisfier {
		private readonly Type scopeType;

		public CurrentScopeMustMatchSatisfier(Type scopeType) {
			this.scopeType = scopeType;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return !context.Scopes.IsEmpty() && context.Scopes.Peek().GetType() == scopeType;
		}
	}
}