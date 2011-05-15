namespace ButterflyNet.Parser.Scopes {
	public class PreformattedLineScope : BlockScope {
		public override bool CloseOnSingleLineBreak { get { return true; } }
		public override bool ManuallyClosing { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenPreformattedLine();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.ClosePreformattedLine();
		}
	}
}