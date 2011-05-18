namespace ButterflyNet.Parser.Scopes {
	public class DefinitionTermScope : BlockScope {
		public override bool ManuallyClosing { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenDefinitionTerm();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseDefinitionTerm();
		}
	}
}