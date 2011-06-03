using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("__")]
	public abstract class StrongStrategy : InlineStrategy {
		protected Type Type { get { return ScopeTypeCache.Strong; } }
	}

	public class OpenStrongStrategy : StrongStrategy {
		public OpenStrongStrategy() {
			AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new StrongScope(), context);
		}
	}

	public class CloseStrongStrategy : StrongStrategy {
		public CloseStrongStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}