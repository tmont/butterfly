namespace ButterflyNet.Parser.Scopes {
	public class StrikeThroughScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenStrikeThroughText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseStrikeThroughText();
		}
	}
}