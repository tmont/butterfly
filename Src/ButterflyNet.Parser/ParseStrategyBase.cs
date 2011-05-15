using System;
using System.Collections.Generic;
using System.Linq;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser {
	public abstract class ParseStrategyBase : IParseStrategy {
		public const int DefaultPriority = 100;
		public event Action<ParseContext> BeforeExecute;
		public event Action<ParseContext> AfterExecute;

		protected ParseStrategyBase() {
			InitializeSatisfiers();

			BeforeExecute += CancelNextStrategy;
			BeforeExecute += VerifyPreExecutionChain;
			BeforeExecute += AdvanceInputForTokenProvider;
		}

		private void InitializeSatisfiers() {
			var tokenProvider = this as ITokenProvider;
			if (tokenProvider != null) {
				AddSatisfier(new CharacterSatisfier(tokenProvider.Token));
			}

			if (Scopable) {
				AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.NoWiki, ScopeTypeCache.Unescaped)));
			}
		}

		#region event delegates
		private static void CancelNextStrategy(ParseContext context) {
			context.ExecuteNext = false;
		}

		private void VerifyPreExecutionChain(ParseContext context) {
			var failingSatisfier = PreExecuteChain.SkipWhile(satisfier => satisfier.IsSatisfiedBy(context)).FirstOrDefault();
			if (failingSatisfier != null) {
				throw new ParseException(string.Format("The pre-execute satisifer {0} failed", failingSatisfier.GetType().Name));
			}
		}

		private void AdvanceInputForTokenProvider(ParseContext context) {
			var tokenProvider = this as ITokenProvider;
			if (tokenProvider != null && tokenProvider.Token.Length > 1) {
				context.AdvanceInput(tokenProvider.Token.Length - 1);
			}
		}
		#endregion

		#region can't get no satisfaction

		private class LambdaDrivenSatisfier : ISatisfier {
			private readonly Func<ParseContext, bool> func;

			public LambdaDrivenSatisfier(Func<ParseContext, bool> func) {
				this.func = func;
			}

			public bool IsSatisfiedBy(ParseContext context) {
				return func(context);
			}
		}

		private readonly IList<ISatisfier> satisfiers = new List<ISatisfier>();
		private readonly IList<ISatisfier> preExecuteSatisfiers = new List<ISatisfier>();

		protected ParseStrategyBase AddSatisfier(Func<ParseContext, bool> satisfyingFunction) {
			AddSatisfier(new LambdaDrivenSatisfier(satisfyingFunction));
			return this;
		}

		protected ParseStrategyBase AddSatisfier<T>() where T : ISatisfier, new() {
			AddSatisfier(new T());
			return this;
		}

		protected ParseStrategyBase AddSatisfier(ISatisfier satisfier) {
			satisfiers.Add(satisfier);
			return this;
		}

		protected ParseStrategyBase AddPreExecuteSatisfier<T>() where T : ISatisfier, new() {
			AddPreExecuteSatisfier(new T());
			return this;
		}

		protected ParseStrategyBase AddPreExecuteSatisfier(ISatisfier satisfier) {
			preExecuteSatisfiers.Add(satisfier);
			return this;
		}

		protected IEnumerable<ISatisfier> SatisfactionChain { get { return satisfiers; } }
		protected IEnumerable<ISatisfier> PreExecuteChain { get { return preExecuteSatisfiers; } }
		#endregion

		/// <summary>
		/// Indicates that this strategy creates scopes or something 
		/// </summary>
		protected virtual bool Scopable { get { return true; } }
		public virtual int Priority { get { return DefaultPriority; } }

		public bool IsSatisfiedBy(ParseContext context) {
			return SatisfactionChain.All(satisfier => satisfier.IsSatisfiedBy(context));
		}

		void IParseStrategy.Execute(ParseContext context) {
			if (BeforeExecute != null) {
				BeforeExecute.Invoke(context);
			}

			Execute(context);

			if (AfterExecute != null) {
				AfterExecute.Invoke(context);
			}
		}

		protected abstract void Execute(ParseContext context);
	}
}