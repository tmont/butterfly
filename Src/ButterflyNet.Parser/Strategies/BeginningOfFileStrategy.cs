using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class BeginningOfFileStrategy : ParseStrategy {
		public BeginningOfFileStrategy() {
			AddSatisfier<BeginningOfFileSatisfier>();
		}

		/// <remarks> Should always be executed first </remarks>
		public override int Priority { get { return int.MinValue; } }

		protected override void DoExecute(ParseContext context) {
			context.Analyzer.OnStart();
		}
	}
}