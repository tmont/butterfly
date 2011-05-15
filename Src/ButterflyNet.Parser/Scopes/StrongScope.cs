namespace ButterflyNet.Parser.Scopes {
	public class StrongScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenStrongText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseStrongText();
		}
	}
}