namespace ButterflyNet.Parser.Scopes {
	public class ListItemScope : BlockScope {
		public ListItemScope(int depth) {
			Depth = depth;
		}

		public int Depth { get; private set; }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenListItem();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseListItem();
		}
	}
}