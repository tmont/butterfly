using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public abstract class StrikeThroughStrategy : InlineStrategy {
		protected StrikeThroughStrategy() {
			AddSatisfier(new ExactCharMatchSatisfier("---"));
		}
		
		public override sealed int Priority { get { return DefaultPriority - 1; } }
		protected override sealed Type Type { get { return ScopeTypeCache.StrikeThrough; } }
	}

	public class OpenStrikeThroughStrategy : StrikeThroughStrategy {
		public OpenStrikeThroughStrategy() {
			AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new StrikeThroughScope(), context);
		}
	}

	public class CloseStrikeThroughStrategy : StrikeThroughStrategy {
		public CloseStrikeThroughStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}