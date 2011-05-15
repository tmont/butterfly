namespace ButterflyNet.Parser.Scopes {
	public class ModuleScope : InlineScope {
		public ModuleScope(IButterflyModule module) {
			Module = module;
		}

		public IButterflyModule Module { get; private set; }

		public override bool CanNestText { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenModule(Module);
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseModule(Module);
		}
	}
}