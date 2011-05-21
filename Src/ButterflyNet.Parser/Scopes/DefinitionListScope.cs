namespace ButterflyNet.Parser.Scopes {
	public class DefinitionListScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenDefinitionList();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseDefinitionList();
		}
	}
}