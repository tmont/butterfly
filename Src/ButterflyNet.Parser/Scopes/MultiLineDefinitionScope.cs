namespace ButterflyNet.Parser.Scopes {
	public class MultiLineDefinitionScope : BlockScope {
		public override bool CanNestParagraph { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenMultiLineDefinition();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseMultiLineDefinition();
		}
	}
}