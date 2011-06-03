namespace ButterflyNet.Parser.Scopes {
	public class HeaderScope : BlockScope {
		public HeaderScope(int depth) {
			Depth = depth;
		}

		public int Depth { get; private set; }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenHeader(Depth);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseHeader(Depth);
		}
	}
}