﻿using System;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("]")]
	public class CloseLinkStrategy : InlineStrategy {
		public CloseLinkStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.Link));
		}

		protected override void DoExecute(ParseContext context) {
			CloseCurrentScope(context);
		}

		protected override Type Type { get { return ScopeTypeCache.Link; } }
	}
}