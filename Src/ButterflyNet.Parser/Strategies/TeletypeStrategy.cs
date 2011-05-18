using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {

	[TokenTransformer("==")]
	public abstract class TeletypeStrategy : InlineStrategy {
		protected override sealed Type Type { get { return ScopeTypeCache.Teletype; } }
	}

	public class OpenTeletypeStrategy : TeletypeStrategy {
		public OpenTeletypeStrategy() {
			AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new TeletypeScope(), context);
		}
	}

	public class CloseTeletypeStrategy : TeletypeStrategy {
		public CloseTeletypeStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}