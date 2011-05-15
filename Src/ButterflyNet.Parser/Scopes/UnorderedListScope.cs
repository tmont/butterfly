namespace ButterflyNet.Parser.Scopes {
	public class UnorderedListScope : ListScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenUnorderedList();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseUnorderedList();
		}
	}
}