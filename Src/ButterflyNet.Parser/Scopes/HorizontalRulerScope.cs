namespace ButterflyNet.Parser.Scopes {
	public class HorizontalRulerScope : BlockScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenHorizontalRuler();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseHorizontalRuler();
		}
	}
}