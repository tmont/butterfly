﻿using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("<<")]
	public class OpenBlockquoteStrategy : ScopeDrivenStrategy {
		public OpenBlockquoteStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier<CannotNestInsideInlineSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			OpenScope(new BlockquoteScope(), context);
		}
	}
}