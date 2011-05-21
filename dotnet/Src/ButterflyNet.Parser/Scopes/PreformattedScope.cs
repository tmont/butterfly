namespace ButterflyNet.Parser.Scopes {
	public class PreformattedScope : BlockScope {
		private readonly string language;

		public PreformattedScope(string language) {
			this.language = language;
		}

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenPreformatted(language);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.ClosePreformatted(language);
		}
	}
}