namespace ButterflyNet.Parser.Scopes {
	public class OrderedListScope : ListScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenOrderedList();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseOrderedList();
		}
	}
}