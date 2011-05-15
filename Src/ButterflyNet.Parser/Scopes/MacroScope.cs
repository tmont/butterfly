namespace ButterflyNet.Parser.Scopes {
	public class MacroScope : InlineScope {
		public MacroScope(IButterflyMacro macro) {
			Macro = macro;
		}

		public IButterflyMacro Macro { get; private set; }
		public override bool CanNestText { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenMacro(Macro);
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseMacro(Macro);
		}
	}
}