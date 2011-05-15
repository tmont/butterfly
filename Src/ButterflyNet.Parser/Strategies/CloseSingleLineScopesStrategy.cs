using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseSingleLineScopesStrategy : ScopeDrivenStrategy {
		public CloseSingleLineScopesStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier<AtLeastOneScopeSatisfier>();
		}

		public override int Priority { get { return int.MinValue; } }

		protected override void Execute(ParseContext context) {
			var emptiedBuffer = false;

			while (!context.Scopes.IsEmpty()) {
				var scope = context.Scopes.Peek();
				if (!scope.CloseOnSingleLineBreak) {
					break;
				}

				if (!emptiedBuffer) {
					WriteStringStrategy.Instance.ExecuteIfSatisfied(context);
					emptiedBuffer = true;
				}

				CloseCurrentScope(context);
			}

			context.ExecuteNext = true;
		}
	}
}