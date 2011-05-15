using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public abstract class InlineStrategy : ScopeDrivenStrategy {
		protected InlineStrategy() {
			AddSatisfier<CannotNestInsideModuleSatisfier>();
		}

		protected abstract Type Type { get; }
	}
}