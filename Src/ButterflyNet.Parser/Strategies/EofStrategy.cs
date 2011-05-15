using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class EofStrategy : ScopeDrivenStrategy {

		public EofStrategy() {
			AddSatisfier<EofSatisfier>();
		}

		protected override bool Scopable { get { return false; } }

		protected override void Execute(ParseContext context) {
			CloseNonManuallyClosingScopes(context);
			context.ExecuteNext = true;
		}

		private void CloseNonManuallyClosingScopes(ParseContext context) {
			var emptiedBuffer = false;

			while (!context.Scopes.IsEmpty()) {
				var scope = context.Scopes.Peek();
				if (scope.ManuallyClosing) {
					throw new ParseException(string.Format("The scope \"{0}\" must be manually closed", scope.GetType().GetFriendlyName(false)));
				}

				if (!emptiedBuffer && WriteStringStrategy.Instance.IsSatisfiedBy(context)) {
					WriteStringStrategy.Instance.Execute(context);
					emptiedBuffer = true;
				}

				CloseCurrentScope(context);
			}

			if (!emptiedBuffer && WriteStringStrategy.Instance.IsSatisfiedBy(context)) {
				WriteStringStrategy.Instance.Execute(context);
				CloseNonManuallyClosingScopes(context);
			}
		}
	}
}