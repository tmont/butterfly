namespace ButterflyNet.Parser.Scopes {
	public class StrongScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenStrongText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseStrongText();
		}
	}
}