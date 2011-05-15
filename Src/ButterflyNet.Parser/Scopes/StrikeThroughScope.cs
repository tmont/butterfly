namespace ButterflyNet.Parser.Scopes {
	public class StrikeThroughScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenStrikeThroughText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseStrikeThroughText();
		}
	}
}