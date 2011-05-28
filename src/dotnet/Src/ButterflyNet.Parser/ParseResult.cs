namespace ButterflyNet.Parser {
	public class ParseResult {
		public ScopeTree ScopeTree { get; private set; }
		public string Markup { get; private set; }

		public ParseResult(ScopeTree scopeTree, string markup) {
			ScopeTree = scopeTree;
			Markup = markup;
		}
	}
}