namespace ButterflyNet.Parser.Scopes {
	public class EmphasisScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenEmphasizedText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseEmphasizedText();
		}
	}
}