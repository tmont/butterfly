using System.Text.RegularExpressions;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("[!")]
	public class NoWikiStrategy : ScopeDrivenStrategy {
		private static readonly Regex nowikiTextRegex = new Regex(@"(.*?)](?!])", RegexOptions.Singleline);
		
		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new NoWikiScope(), context);

			var match = nowikiTextRegex.Match(context.Input.PeekSubstring);
			if (!match.Success) {
				throw new ParseException("NoWiki scope never closes");
			}

			var text = match.Groups[1].Value.Replace("]]", "]");
			context.Input.Read(match.Value.Length);
			context.UpdateCurrentChar();
			context.Analyzer.WriteAndEscape(text);
			CloseCurrentScope(context);
		}
	}
}