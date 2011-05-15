using System.Collections.Generic;

namespace ButterflyNet.Parser.Scopes {
	public class NoWikiScope : InlineScope {
		public override void Open(IEnumerable<ButterflyAnalyzer> analyzers) { }
		public override void Close(IEnumerable<ButterflyAnalyzer> analyzers) { }
	}
}