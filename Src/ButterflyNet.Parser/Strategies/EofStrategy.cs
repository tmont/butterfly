using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public sealed class EofStrategy : ScopeDrivenStrategy {

		public EofStrategy() {
			AddSatisfier<EofSatisfier>();
		}

		protected override void DoExecute(ParseContext context) {
			while (!context.Scopes.IsEmpty()) {
				CloseCurrentScope(context);
			}
		}
	}
}