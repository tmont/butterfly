using System;

namespace ButterflyNet.Parser {
	public interface IParseStrategy : ISatisfier {
		int Priority { get; }
		void Execute(ParseContext context);
		event Action<ParseContext> BeforeExecute;
		event Action<ParseContext> AfterExecute;
	}
}