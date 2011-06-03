namespace ButterflyNet.Parser.Scopes {
	public class LinkScope : InlineScope {
		public LinkScope(string url, string baseUrl) {
			Url = url;
			BaseUrl = baseUrl;
		}

		public string Url { get; private set; }
		public string BaseUrl { get; private set; }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenLink(Url, BaseUrl);
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseLink();
		}
	}
}