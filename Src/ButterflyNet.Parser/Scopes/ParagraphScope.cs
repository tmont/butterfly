namespace ButterflyNet.Parser.Scopes {
	public class ParagraphScope : ScopeBase {
		public override ScopeLevel Level { get { return ScopeLevel.Block; } }
		public override bool ClosesOnContext { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenParagraph();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseParagraph();
		}
	}
}