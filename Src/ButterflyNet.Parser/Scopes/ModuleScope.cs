namespace ButterflyNet.Parser.Scopes {
	public class ModuleScope : InlineScope {
		public ModuleScope(IButterflyModule module) {
			Module = module;
		}

		public IButterflyModule Module { get; private set; }

		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenModule(Module);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseModule(Module);
		}
	}
}