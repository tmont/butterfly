using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class BeginningOfFileStrategy : ParseStrategyBase {
		public BeginningOfFileStrategy() {
			AddSatisfier<BeginningOfFileSatisfier>();
		}

		/// <remarks> Should always be executed first </remarks>
		public override int Priority { get { return int.MinValue; } }

		protected override bool Scopable {
			get {
				return false;
			}
		}

		protected override void Execute(ParseContext context) {
			context.Analyzers.Walk(converter => converter.OnStart());
			context.ExecuteNext = true;
		}
	}
}