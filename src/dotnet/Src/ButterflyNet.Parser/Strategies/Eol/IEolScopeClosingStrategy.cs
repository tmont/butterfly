using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public interface IEolScopeClosingStrategy {
		Type ScopeType { get; }
		bool ShouldClose(ParseContext context);
	}
}