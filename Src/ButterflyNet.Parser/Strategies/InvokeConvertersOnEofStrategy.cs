using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class InvokeConvertersOnEofStrategy : ParseStrategyBase {
		public InvokeConvertersOnEofStrategy() {
			AddSatisfier<EofSatisfier>();
		}

		/// <remarks> Should always be executed last </remarks>
		public override int Priority { get { return int.MaxValue; } }

		protected override void Execute(ParseContext context) {
			context.Analyzer.OnEnd();
			context.ExecuteNext = true;
		}
	}
}