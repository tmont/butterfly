using System;
using System.Linq;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies.Eol {

	public abstract class ListScopeClosingStrategy : IEolScopeClosingStrategy {
		public abstract Type ScopeType { get; }

		public bool ShouldClose(ParseContext context) {
			if (context.Input.Peek() != '*' && context.Input.Peek() != '#') {
				return true;
			}

			var currentDepth = GetListItem(context).Depth;

			return ShouldClose(currentDepth, GetDepthOfNextListItem(context));
		}

		protected abstract bool ShouldClose(int currentDepth, int nextDepth);

		protected abstract ListItemScope GetListItem(ParseContext context);

		private static int GetDepthOfNextListItem(ParseContext context) {
			var depth = 1;
			var peek = context.Input.Peek(2).Last();
			while (peek == '*' || peek == '#') {
				peek = context.Input.Peek(++depth + 1).Last();
			}

			return depth;
		}
	}

	public sealed class ListItemClosingStrategy : ListScopeClosingStrategy {
		public override Type ScopeType { get { return ScopeTypeCache.ListItem; } }

		protected override bool ShouldClose(int currentDepth, int nextDepth) {
			return nextDepth <= currentDepth;
		}

		protected override ListItemScope GetListItem(ParseContext context) {
			return context.Scopes.Peek() as ListItemScope;
		}
	}

	public sealed class ListClosingStrategy : ListScopeClosingStrategy {
		private readonly Type scopeType;

		public ListClosingStrategy(Type scopeType) {
			if (!typeof(ListScope).IsAssignableFrom(scopeType)) {
				throw new ArgumentException("scopeType must be assignable to ListScope");
			}

			this.scopeType = scopeType;
		}

		public override Type ScopeType {
			get { return scopeType; }
		}

		protected override bool ShouldClose(int currentDepth, int nextDepth) {
			return nextDepth < currentDepth;
		}

		protected override ListItemScope GetListItem(ParseContext context) {
			var lastNode = context.ScopeTree.GetMostRecentNode(context.Scopes.Count);
			if (lastNode == null || !(lastNode.Scope is ListItemScope)) {
#if DEBUG
				//this shouldn't ever happen if the list parsing is performed correctly
				throw new InvalidOperationException("Encountered list with no list items");
#else
				return new ListItemScope(1);
#endif
			}

			return (ListItemScope)lastNode.Scope;
		}
	}
}