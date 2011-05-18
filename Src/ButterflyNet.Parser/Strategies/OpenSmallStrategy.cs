using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("(-")]
	public sealed class OpenSmallStrategy : InlineStrategy {
		protected override void DoExecute(ParseContext context) {
			OpenScope(new SmallScope(), context);
		}

		protected override Type Type { get { return ScopeTypeCache.Small; } }
	}
}