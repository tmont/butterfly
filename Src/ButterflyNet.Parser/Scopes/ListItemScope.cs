namespace ButterflyNet.Parser.Scopes {
	public class ListItemScope : BlockScope {
		public override bool ManuallyClosing { get { return false; } }
		public override bool CloseOnSingleLineBreak { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenListItem();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseListItem();
		}
	}
}