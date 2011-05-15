namespace ButterflyNet.Parser.Scopes {
	public class BlockquoteScope : BlockScope {
		public override bool CanNestParagraph { get { return true; } }
		public override bool ManuallyClosing { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenBlockquote();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseBlockquote();
		}
	}
}