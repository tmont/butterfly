using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class EofStrategy : ScopeDrivenStrategy {

		public EofStrategy() {
			AddSatisfier<EofSatisfier>();
		}

		protected override void Execute(ParseContext context) {
			CloseNonManuallyClosingScopes(context);
			context.ExecuteNext = true;
		}

		private void CloseNonManuallyClosingScopes(ParseContext context) {
			var emptiedBuffer = false;

			while (!context.Scopes.IsEmpty()) {
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