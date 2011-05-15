namespace ButterflyNet.Parser.Scopes {
	public class TeletypeScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTeletypeText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTeletypeText();
		}
	}
}