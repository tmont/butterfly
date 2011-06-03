namespace ButterflyNet.Parser.Scopes {
	public class ParagraphScope : ScopeBase {
		public override ScopeLevel Level { get { return ScopeLevel.Block; } }
		
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenParagraph();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseParagraph();
		}
	}
}