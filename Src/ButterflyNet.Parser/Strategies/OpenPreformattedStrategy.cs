using System.Text;
using System.Text.RegularExpressions;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {

	public abstract class PreformattedStrategyBase : ScopeDrivenStrategy {
		protected PreformattedStrategyBase() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Preformatted)));
		}

		protected string GetLanguage(ParseContext context) {
			var languageBuilder = new StringBuilder();
			context.AdvanceInput();
			while (context.CurrentChar != '\n' && context.CurrentChar != ButterflyStringReader.NoValue) {
				languageBuilder.Append((char)context.CurrentChar);
				context.AdvanceInput();
			}

			return languageBuilder.ToString();
		}
	}

	[TokenTransformer("{{{")]
	public class OpenPreformattedStrategy : PreformattedStrategyBase {
		protected override void DoExecute(ParseContext context) {
			OpenScope(new PreformattedScope(GetLanguage(context)), context);
		}
	}

	[TokenTransformer("{{{{")]
	public class PreformattedCodeStrategy : PreformattedStrategyBase {
		private static readonly Regex codeRegex = new Regex(@"^(.*?)\}\}\}\}", RegexOptions.Singleline);

		public override int Priority { get { return base.Priority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new PreformattedScope(GetLanguage(context)), context);

			//this strategy ignores any formatting
			//read until }}}}
			var match = codeRegex.Match(context.Input.PeekSubstring);
			if (!match.Success) {
				throw new ParseException("Preformatted scope never closes");
			}

			var text = match.Groups[1].Value;
			context.Input.Read(match.Value.Length);
			context.UpdateCurrentChar();
			context.Analyzer.WriteAndEscape(text);
			CloseCurrentScope(context);
		}
	}
}