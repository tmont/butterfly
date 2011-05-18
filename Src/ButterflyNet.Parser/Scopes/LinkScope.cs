using System;

namespace ButterflyNet.Parser.Scopes {
	public class LinkScope : InlineScope {
		public string Url { get; private set; }

		public LinkScope(string url) {
			Url = url;
		}

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenLink(Url);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseLink();
		}
	}
}