namespace ButterflyNet.Parser.Scopes {
	public class UnorderedListScope : ListScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenUnorderedList();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseUnorderedList();
		}
	}
}