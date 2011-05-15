using System;

namespace ButterflyNet.Parser.Scopes {
	public class LinkScope : InlineScope {
		public string Url { get; private set; }

		public LinkScope(string url) {
			Url = url;
		}

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenLink(Url);
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseLink();
		}
	}
}