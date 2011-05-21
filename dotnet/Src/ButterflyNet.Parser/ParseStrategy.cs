using System;
using System.Collections.Generic;
using System.Linq;
using ButterflyNet.Parser.Strategies;

namespace ButterflyNet.Parser {

	public abstract class ParseStrategy : ISatisfier {
		public const int DefaultPriority = 100;
		public event Action<ParseContext> BeforeExecute;
		public event Action<ParseContext> AfterExecute;

		private readonly IList<ISatisfier> satisfiers = new List<ISatisfier>();

		protected ParseStrategy() {
			var tokenAttribute = GetType().GetAttributes<TokenTransformerAttribute>().FirstOrDefault();
			if (tokenAttribute != null) {
				AddSatisfier(new ExactCharMatchSatisfier(tokenAttribute.Token));
				BeforeExecute += AdvanceInputForToken(tokenAttribute.Token);
			}
		}

		#region event delegates
		private static Action<ParseContext> AdvanceInputForToken(string token) {
			return context => {
				if (token.Length <= 1) {
					return;
				}

				context.AdvanceInput(token.Length - 1);
			};
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

		protected ParseStrategy AddSatisfier(Func<ParseContext, bool> satisfyingFunction) {
			AddSatisfier(new LambdaDrivenSatisfier(satisfyingFunction));
			return this;
		}

		protected ParseStrategy AddSatisfier<T>() where T : ISatisfier, new() {
			AddSatisfier(new T());
			return this;
		}

		protected ParseStrategy AddSatisfier(ISatisfier satisfier) {
			satisfiers.Add(satisfier);
			return this;
		}

		protected IEnumerable<ISatisfier> SatisfactionChain { get { return satisfiers; } }
		#endregion

		public virtual int Priority { get { return DefaultPriority; } }

		public bool IsSatisfiedBy(ParseContext context) {
			return SatisfactionChain.All(satisfier => satisfier.IsSatisfiedBy(context));
		}

		public void Execute(ParseContext context) {
			if (BeforeExecute != null) {
				BeforeExecute.Invoke(context);
			}

			DoExecute(context);

			if (AfterExecute != null) {
				AfterExecute.Invoke(context);
			}
		}

		protected abstract void DoExecute(ParseContext context);
	}
}