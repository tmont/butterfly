﻿using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("(+")]
	public sealed class OpenBigStrategy : InlineStrategy {
		protected override void DoExecute(ParseContext context) {
			OpenScope(new BigScope(), context);
		}
	}
}