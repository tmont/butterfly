using System;

namespace ButterflyNet.Parser.Satisfiers {
	public class OpenNonNestableInlineScopeSatisfier : ISatisfier {
		private readonly ISatisfier innerSatisfier;

		public OpenNonNestableInlineScopeSatisfier(Type type) {
			innerSatisfier = new NegatingSatisfier(new InScopeStackSatisfier(type));
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return innerSatisfier.IsSatisfiedBy(context);
		}
	}
}