using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseTableRowStrategy : ScopeDrivenStrategy {
		public CloseTableRowStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.TableRow));
			AddSatisfier(new ExactCharMatchSatisfier("}|"));
		}

		protected override void DoExecute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.TableRow);
			CloseCurrentScope(context);
		}
	}
}