using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("%%%")]
	public sealed class LineBreakStrategy : InlineStrategy {
		protected override void DoExecute(ParseContext context) {
			OpenScope(new LineBreakScope(), context);
			CloseCurrentScope(context);
		}
	}
}