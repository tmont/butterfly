using System.Text.RegularExpressions;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("[!")]
	public class UnparsedStrategy : ScopeDrivenStrategy {
		private static readonly Regex unparsedRegex = new Regex(@"(.*?)](?!])", RegexOptions.Singleline);
		
		public override int Priority { get { return DefaultPriority - 1; } }

		protected override void DoExecute(ParseContext context) {
			OpenScope(new UnparsedScope(), context);

			var match = unparsedRegex.Match(context.Input.PeekSubstring);
			if (!match.Success) {
				throw new ParseException("Unparsed scope never closes");
			}

			var text = match.Groups[1].Value.Replace("]]", "]");
			context.Input.Read(match.Value.Length);
			context.UpdateCurrentChar();
			context.Analyzer.WriteAndEscape(text);
			CloseCurrentScope(context);
		}
	}
}