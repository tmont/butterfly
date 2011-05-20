namespace ButterflyNet.Parser.Scopes {
	public class BlockquoteScope : BlockScope {
		public override bool CanNestParagraph { get { return true; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenBlockquote();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseBlockquote();
		}
	}
}