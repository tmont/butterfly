namespace ButterflyNet.Parser.Scopes {
	public class EmphasisScope : InlineScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenEmphasizedText();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseEmphasizedText();
		}
	}
}