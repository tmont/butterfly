using System;
using System.Text;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class OpenLinkStrategy : InlineStrategy {
		public OpenLinkStrategy() {
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Link)));
			AddSatisfier(new ExactCharMatchSatisfier("["));
		}

		protected override void DoExecute(ParseContext context) {
			var peek = context.Input.Peek();
			var urlBuilder = new StringBuilder();
			while (peek != ButterflyStringReader.NoValue && peek != '|' && peek != ']') {
				urlBuilder.Append((char)context.Input.Read());
				peek = context.Input.Peek();
			}

			var closer = context.Input.Read(); //| or ]
			var url = urlBuilder.ToString();

			OpenScope(new Scopes.LinkScope(url), context);

			if (closer == ']') {
				//the text is the same as the URL, so we can close the scope immediately
				context.Analyzer.WriteAndEscape(url);
				CloseCurrentScope(context);
			}
		}

		protected override Type Type { get { return ScopeTypeCache.Link; } }
	}
}