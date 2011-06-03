namespace ButterflyNet.Parser {
	public interface IScope {
		ScopeLevel Level { get; }
		bool CanNestParagraph { get; }
		bool CanNestText { get; }
		void Open(ButterflyAnalyzer analyzers);
		void Close(ButterflyAnalyzer analyzer);
	}
}