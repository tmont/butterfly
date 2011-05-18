using System;
using System.Linq;

namespace ButterflyNet.Parser.Strategies {
	public abstract class ScopeDrivenStrategy : ParseStrategy {
		public event Action<IScope, ParseContext> BeforeScopeOpens;
		public event Action<IScope, ParseContext> AfterScopeOpens;
		public event Action<IScope, ParseContext> BeforeScopeCloses;
		public event Action<IScope, ParseContext> AfterScopeCloses;

		protected ScopeDrivenStrategy() {
			BeforeScopeOpens += CreateParagraphIfNecessary;
			AfterScopeOpens += UpdateScopeTreeAfterOpen;
			AfterScopeCloses += UpdateScopeTreeAfterClose;
		}

		#region Event delegates
		private static void CreateParagraphIfNecessary(IScope scope, ParseContext context) {
			if (scope.GetType() == ScopeTypeCache.Paragraph) {
				return;
			}

			if (scope.Level == ScopeLevel.Inline) {
				new OpenParagraphStrategy().ExecuteIfSatisfied(context);
			}
		}

		private static void UpdateScopeTreeAfterOpen(IScope scope, ParseContext context) {
			var newNode = new ScopeTreeNode(scope);

			if (context.CurrentNode != null) {
				context.CurrentNode.AddChild(newNode);
			} else {
				context.ScopeTree.AddNode(newNode);
			}

			context.CurrentNode = newNode;
		}

		private static void UpdateScopeTreeAfterClose(IScope scope, ParseContext context) {
			context.CurrentNode = context.CurrentNode.Parent;
		}
		#endregion

		protected void OpenScope(IScope scope, ParseContext context) {
			if (BeforeScopeOpens != null) {
				BeforeScopeOpens.Invoke(scope, context);
			}

			context.Scopes.Push(scope);
			scope.Open(context.Analyzer);

			if (AfterScopeOpens != null) {
				AfterScopeOpens.Invoke(scope, context);
			}
		}

		protected void CloseScopeUntil(ParseContext context, params Type[] scopeTypes) {
			while (!context.Scopes.IsEmpty()) {
				var scope = context.Scopes.Peek();
				if (scopeTypes.Contains(scope.GetType())) {
					break;
				}

				CloseCurrentScope(context);
			}
		}

		protected void CloseCurrentScope(ParseContext context) {
			var currentScope = context.Scopes.PeekOrDefault();
			if (currentScope == null) {
				throw new ParseException("Stack is empty, unable to find scope to close");
			}

			if (BeforeScopeCloses != null) {
				BeforeScopeCloses.Invoke(currentScope, context);
			}

			if (!ReferenceEquals(context.Scopes.PeekOrDefault(), currentScope)) {
				throw new ParseException("ScopeDrivenStrategy.BeforeScopeCloses invocations created an inconsistent state");
			}

			//cannot pop scope until after BeforeScopeCloses is invoked; among other things, it screws up paragraph creation logic
			context.Scopes.Pop();
			currentScope.Close(context.Analyzer);

			if (AfterScopeCloses != null) {
				AfterScopeCloses.Invoke(currentScope, context);
			}
		}

	}
}