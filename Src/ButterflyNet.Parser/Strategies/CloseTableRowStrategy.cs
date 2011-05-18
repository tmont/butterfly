using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class CloseTableRowStrategy : ScopeDrivenStrategy, ITokenProvider {
		public CloseTableRowStrategy() {
			AddSatisfier(new InScopeStackSatisfier(ScopeTypeCache.TableRow));
		}

		protected override void DoExecute(ParseContext context) {
			CloseScopeUntil(context, ScopeTypeCache.TableRow);
			CloseCurrentScope(context);
		}

		public string Token { get { return "}|"; } }
	}
}