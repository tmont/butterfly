using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class InvokeConvertersOnEofStrategy : ParseStrategyBase {
		public InvokeConvertersOnEofStrategy() {
			AddSatisfier<EofSatisfier>();
		}

		protected override bool Scopable {
			get {
				return false;
			}
		}

		/// <remarks> Should always be executed last </remarks>
		public override int Priority { get { return int.MaxValue; } }

		protected override void Execute(ParseContext context) {
			context.Analyzers.Walk(converter => converter.OnEnd());
			context.ExecuteNext = true;
		}
	}
}