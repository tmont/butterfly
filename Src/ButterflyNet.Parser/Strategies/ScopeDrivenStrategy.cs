using System;
using System.Linq;

namespace ButterflyNet.Parser.Strategies {
	public abstract class ScopeDrivenStrategy : ParseStrategyBase {
		private readonly IParseStrategy bufferingStrategy;

		public event Action<IScope, ParseContext> BeforeScopeOpens;
		public event Action<IScope, ParseContext> AfterScopeOpens;
		public event Action<IScope, ParseContext> BeforeScopeCloses;
		public event Action<IScope, ParseContext> AfterScopeCloses;

		protected ScopeDrivenStrategy(IParseStrategy bufferingStrategy) {
			this.bufferingStrategy = bufferingStrategy;

			BeforeScopeOpens += CreateParagraphIfNecessary;
			BeforeScopeOpens += EmptyBufferAndCloseParagraph;
			AfterScopeOpens += UpdateScopeTreeAfterOpen;
			BeforeScopeCloses += EmptyBufferOnClose;
			AfterScopeCloses += UpdateScopeTreeAfterClose;
		}

		internal ScopeDrivenStrategy() : this(WriteStringStrategy.Instance) { }

		#region Event delegates
		private void CreateParagraphIfNecessary(IScope scope, ParseContext context) {
			if (scope.GetType() == ScopeTypeCache.Paragraph) {
				return;
			}

			if (scope.Level == ScopeLevel.Inline) {
				new OpenParagraphStrategy().ExecuteIfSatisfied(context);
			} else {
				bufferingStrategy.ExecuteIfSatisfied(context);
			}
		}

		private void EmptyBufferOnClose(IScope scope, ParseContext context) {
			if (!bufferingStrategy.IsSatisfiedBy(context)) {
				return;
			}

			var preEmptyCount = context.Scopes.Count;
			bufferingStrategy.Execute(context);
			if (context.Scopes.Count > preEmptyCount) {
				while (context.Scopes.Count > preEmptyCount) {
					CloseCurrentScope(context);
				}
			}
		}

		private void EmptyBufferAndCloseParagraph(IScope scope, ParseContext context) {
			if (scope.GetType() != ScopeTypeCache.Paragraph) {
				var preBufferScopeCount = context.Scopes.Count;
				BufferingStrategy.ExecuteIfSatisfied(context);
				if (scope.Level == ScopeLevel.Block) {
					//we don't want to nest block level scopes inside a paragraph
					switch (context.Scopes.Count - preBufferScopeCount) {
						case 0:
							break;
						case 1:
							if (context.Scopes.Peek().GetType() != ScopeTypeCache.Paragraph) {
								throw new NotSupportedException("Buffering strategy created a non-paragraph scope");
							}

							CloseCurrentScope(context);
							break;
						default:
							throw new NotSupportedException("Buffering strategy created more than one scope while emptying the buffer");
					}
				}
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

		protected virtual IParseStrategy BufferingStrategy { get { return bufferingStrategy; } }
	}
}