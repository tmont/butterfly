namespace ButterflyNet.Parser {
	public class ParseResult {
		public ScopeTree ScopeTree { get; private set; }
		public string WikiText { get; private set; }

		public ParseResult(ScopeTree scopeTree, string wikitext) {
			ScopeTree = scopeTree;
			WikiText = wikitext;
		}
	}
}