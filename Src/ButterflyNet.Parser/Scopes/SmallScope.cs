namespace ButterflyNet.Parser.Scopes {
	public class SmallScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenSmallText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseSmallText();
		}
	}
}