namespace ButterflyNet.Parser.Scopes {
	public class TeletypeScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTeletypeText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTeletypeText();
		}
	}
}