namespace ButterflyNet.Parser.Scopes {
	public class PreformattedLineScope : BlockScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenPreformattedLine();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.ClosePreformattedLine();
		}
	}
}