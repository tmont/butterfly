using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("--")]
	public abstract class UnderlineStrategy : InlineStrategy {
		protected Type Type { get { return ScopeTypeCache.Underline; } }
	}

	public class OpenUnderlineStrategy : UnderlineStrategy {
		public OpenUnderlineStrategy() {
			AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new UnderlineScope(), context);
		}
	}

	public class CloseUnderlineStrategy : UnderlineStrategy {
		public CloseUnderlineStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}