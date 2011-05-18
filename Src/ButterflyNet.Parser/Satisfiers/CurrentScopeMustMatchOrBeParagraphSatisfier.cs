using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterflyNet.Parser.Satisfiers {
	public class CurrentScopeMustMatchOrBeParagraphSatisfier : ISatisfier {
		private readonly IEnumerable<Type> scopeTypes;

		public CurrentScopeMustMatchOrBeParagraphSatisfier(params Type[] scopeTypes) {
			this.scopeTypes = scopeTypes.Concat(new[] { ScopeTypeCache.Paragraph });
		}

		public bool IsSatisfiedBy(ParseContext context) {
			var currentScope = context.Scopes.PeekOrDefault();
			return currentScope != null && scopeTypes.Contains(currentScope.GetType());
		}
	}
}