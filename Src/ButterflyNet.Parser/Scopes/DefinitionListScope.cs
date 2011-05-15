namespace ButterflyNet.Parser.Scopes {
	public class DefinitionListScope : BlockScope {
		public override bool ManuallyClosing { get { return false; } }
		public override bool ClosesOnContext { get { return true; } }
		public override bool CanNestText { get { return false; } }

		public override bool ShouldClose(IScope nextScope) {
			var nextType = nextScope.GetType();
			return nextType != ScopeTypeCache.DefinitionTerm
				&& nextType != ScopeTypeCache.Definition
				&& nextType != ScopeTypeCache.MultiLineDefinition;
		}

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenDefinitionList();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseDefinitionList();
		}
	}
}