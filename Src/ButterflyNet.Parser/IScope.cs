using System.Collections.Generic;

namespace ButterflyNet.Parser {
	public interface IScope {
		ScopeLevel Level { get; }
		bool ManuallyClosing { get; }
		bool CloseOnSingleLineBreak { get; }
		bool ClosesOnContext { get; }
		bool CanNestParagraph { get; }
		bool CanNestText { get; }
		void Open(IEnumerable<ButterflyAnalyzer> analyzers);
		void Close(IEnumerable<ButterflyAnalyzer> analyzers);
		bool ShouldClose(IScope nextScope);
	}
}