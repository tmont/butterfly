namespace ButterflyNet.Parser.Scopes {
	public class DefinitionScope : BlockScope {
		public override bool ManuallyClosing { get { return false; } }
		public override bool CloseOnSingleLineBreak { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenDefinition();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseDefinition();
		}
	}
}