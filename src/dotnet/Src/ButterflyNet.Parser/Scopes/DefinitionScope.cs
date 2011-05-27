namespace ButterflyNet.Parser.Scopes {
	public class DefinitionScope : BlockScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenDefinition();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseDefinition();
		}
	}
}