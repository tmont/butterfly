namespace ButterflyNet.Parser.Scopes {
	public class OrderedListScope : ListScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenOrderedList();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseOrderedList();
		}
	}
}