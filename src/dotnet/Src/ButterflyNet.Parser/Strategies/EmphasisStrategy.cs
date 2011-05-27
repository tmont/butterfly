using System;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {

	[TokenTransformer("''")]
	public abstract class EmphasisStrategy : InlineStrategy {
		protected Type Type { get { return ScopeTypeCache.Emphasis; } }
	}

	public class OpenEmphasisStrategy : EmphasisStrategy {
		public OpenEmphasisStrategy() {
			AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new EmphasisScope(), context);
		}
	}

	public class CloseEmphasisStrategy : EmphasisStrategy {
		public CloseEmphasisStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}
	}
}