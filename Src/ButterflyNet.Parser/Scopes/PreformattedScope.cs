namespace ButterflyNet.Parser.Scopes {
	public class PreformattedScope : BlockScope {
		private readonly string language;

		public PreformattedScope(string language) {
			this.language = language;
		}

		public override bool ManuallyClosing { get { return true; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenPreformatted(language);
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.ClosePreformatted(language);
		}
	}
}