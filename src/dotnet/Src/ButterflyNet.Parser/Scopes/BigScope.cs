namespace ButterflyNet.Parser.Scopes {
	public class BigScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenBigText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseBigText();
		}
	}
}