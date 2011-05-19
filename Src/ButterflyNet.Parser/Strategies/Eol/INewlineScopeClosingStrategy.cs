using System;

namespace ButterflyNet.Parser.Strategies.Eol {
	public interface INewlineScopeClosingStrategy {
		Type ScopeType { get; }
		bool ShouldClose(ParseContext context);
	}
}