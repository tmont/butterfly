using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("}|")]
	public class CloseTableRowStrategy : ScopeDrivenStrategy {
		public CloseTableRowStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.TableRow));
		}

		protected override void DoExecute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.TableRow);
			CloseCurrentScope(context);
		}
	}
}