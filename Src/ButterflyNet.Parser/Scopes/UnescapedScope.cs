using System.Collections.Generic;

namespace ButterflyNet.Parser.Scopes {
	public class UnescapedScope : InlineScope {
		public override void Open(IEnumerable<ButterflyAnalyzer> analyzers) { }
		public override void Close(IEnumerable<ButterflyAnalyzer> analyzers) { }
	}
}