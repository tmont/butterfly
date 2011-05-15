using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class InScopeStackSatisfier : ISatisfier {
		private readonly Type[] types;

		public InScopeStackSatisfier(params Type[] types) {
			this.types = types;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return context.Scopes.ContainsType(types);
		}
	}
}