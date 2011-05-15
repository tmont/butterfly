namespace ButterflyNet.Parser.Scopes {
	public class HeaderScope : BlockScope {
		public HeaderScope(int depth) {
			Depth = depth;
		}

		public int Depth { get; private set; }
		public override bool CloseOnSingleLineBreak { get { return true; } }
		public override bool ManuallyClosing { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenHeader(Depth);
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseHeader(Depth);
		}
	}
}