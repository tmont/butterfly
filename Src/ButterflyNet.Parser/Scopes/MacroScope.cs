namespace ButterflyNet.Parser.Scopes {
	public class MacroScope : InlineScope {
		public MacroScope(IButterflyMacro macro) {
			Macro = macro;
		}

		public IButterflyMacro Macro { get; private set; }
		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenMacro(Macro);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseMacro(Macro);
		}
	}
}