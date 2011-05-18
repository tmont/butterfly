using System.Text;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class OpenPreformattedStrategy : ScopeDrivenStrategy, ITokenProvider {

		public OpenPreformattedStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted)));
		}

		protected override void DoExecute(ParseContext context) {
			//read to end of line to get the language, if applicable
			var languageBuilder = new StringBuilder();
			context.AdvanceInput();
			while (context.CurrentChar != '\n' && context.CurrentChar != ButterflyStringReader.NoValue) {
				languageBuilder.Append((char)context.CurrentChar);
				context.AdvanceInput();
			}

			OpenScope(new PreformattedScope(languageBuilder.ToString()), context);
		}

		public string Token { get { return "{{{"; } }
	}
}