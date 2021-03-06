﻿using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(":{")]
	public class OpenMultiLineDefinitionStrategy : ScopeDrivenStrategy {
		public OpenMultiLineDefinitionStrategy() {
			AddSatisfier<DependentSatisfier<DefinitionStrategy>>();
		}

		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new MultiLineDefinitionScope(), context);
		}
	}

	[TokenTransformer("}:")]
	public class CloseMultiLineDefinitionStrategy : ScopeDrivenStrategy {
		public CloseMultiLineDefinitionStrategy() {
			AddSatisfier(new CurrentScopeMustMatchOrBeParagraphSatisfier(ScopeTypeCache.MultiLineDefinition));
		}

		protected override void DoExecute(ParseContext context) {
			CloseParagraphIfNecessary(context);
			CloseCurrentScope(context);
		}
	}
}