namespace ButterflyNet.Parser.Scopes {
	public class UnderlineScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenUnderlinedText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseUnderlinedText();
		}
	}
}