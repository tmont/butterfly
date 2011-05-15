namespace ButterflyNet.Parser.Scopes {
	public class BigScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenBigText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseBigText();
		}
	}
}