namespace ButterflyNet.Parser.Scopes {
	public class SmallScope : InlineScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenSmallText();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseSmallText();
		}
	}
}