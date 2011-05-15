namespace ButterflyNet.Parser.Scopes {
	public class UnderlineScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenUnderlinedText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseUnderlinedText();
		}
	}
}