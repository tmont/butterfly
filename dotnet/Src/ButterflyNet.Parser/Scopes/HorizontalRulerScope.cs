namespace ButterflyNet.Parser.Scopes {
	public class HorizontalRulerScope : BlockScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenHorizontalRuler();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseHorizontalRuler();
		}
	}
}