using System.Linq;
using System.Text;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class ListStrategy : ScopeDrivenStrategy {
		public ListStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new OneOfSeveralTokensSatisfier('*', '#'));
		}

		protected override void Execute(ParseContext context) {
			var peek = context.Input.Peek();

			var listText = new StringBuilder(((char)context.CurrentChar).ToString(), 3);
			while (peek == '*' || peek == '#') {
				listText.Append((char)context.Input.Read());
				peek = context.Input.Peek();
			}

			//ignore whitespace after * or #
			context.Input.SeekToNonWhitespace();
			context.UpdateCurrentChar();

			HandleList(listText.ToString(), context);
		}

		private static bool IsList(IScope scope) {
			var scopeType = scope.GetType();
			return scopeType == ScopeTypeCache.UnorderedList || scopeType == ScopeTypeCache.OrderedList;
		}

		private void HandleList(string listStart, ParseContext context) {
			var depth = listStart.Length;
			var newScope = listStart.Last() == '*' ? new UnorderedListScope() : (IScope)new OrderedListScope();

			//get the number of opened lists
			var openLists = context.Scopes.Where(IsList).Count();

			if (openLists == 0) {
				//new list
				if (depth > 1) {
					throw new ParseException("Cannot start a list with a depth greater than one");
				}

				OpenScope(newScope, context);
			} else {
				//a list has already been opened
				var difference = depth - openLists;

				if (difference < 0) {
					//close lists of a higher depth
					while (difference < 0) {
						CloseScopeUntil(context, ScopeTypeCache.UnorderedList, ScopeTypeCache.OrderedList);
						CloseCurrentScope(context);
						difference++;
					}
				} else if (difference == 1) {
					//start a new list at a higher depth
					OpenScope(newScope, context);
				} else if (difference > 1) {
					throw new ParseException(string.Format(
						"Nested lists cannot skip levels: expected a depth of less than or equal to {0} but got {1}", 
						openLists + 1, 
						depth
					));
				}

				//verify that the list types match
				var currentListScope = context.Scopes.First(IsList);
				if (currentListScope.GetType() != newScope.GetType()) {
					throw new ParseException(string.Format(
						"Expected list of type {0} but got {1}", 
						currentListScope.GetName(),
						newScope.GetName()
					));
				}
			}

			OpenScope(new ListItemScope(depth), context);
		}
	}

	public class CloseListStrategy : ScopeDrivenStrategy, ITokenProvider {
		public CloseListStrategy() {
			AddSatisfier(new CurrentScopeMustMatchSatisfier(ScopeTypeCache.ListItem));
			AddSatisfier<NextLineIsNotAList>();
		}

		public override int Priority {
			get {
				return int.MinValue;
			}
		}

		protected override void Execute(ParseContext context) {
			CloseCurrentScope(context);
			context.ExecuteNext = true;
		}

		public string Token { get { return "\n"; } }

		private class NextLineIsNotAList : ISatisfier {
			public bool IsSatisfiedBy(ParseContext context) {
				var currentScope = context.Scopes.Peek() as ListItemScope;
				if (currentScope == null) {
					return false;
				}

				if (context.Input.Peek() != '*' && context.Input.Peek() != '#') {
					return true;
				}

				//get depth of next list item
				var depth = 1;
				var peek = context.Input.Peek(2).Last();
				while (peek == '*' || peek == '#') {
					peek = context.Input.Peek(++depth + 1).Last();
				}

				return depth <= currentScope.Depth;
			}
		}
	}
}