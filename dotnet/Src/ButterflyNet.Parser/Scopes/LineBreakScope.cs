namespace ButterflyNet.Parser.Scopes {
	public class LineBreakScope : InlineScope {

		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenLineBreak();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseLineBreak();
		}
	}
}