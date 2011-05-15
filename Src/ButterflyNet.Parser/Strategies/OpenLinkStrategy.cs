using System;
using System.Text;
using ButterflyNet.Parser.Satisfiers;

namespace ButterflyNet.Parser.Strategies {
	public class OpenLinkStrategy : InlineStrategy, ITokenProvider {
		public OpenLinkStrategy() {
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Link)));
		}

		protected override void Execute(ParseContext context) {
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
				context.Analyzers.Walk(converter => converter.WriteAndEscapeString(url));
				CloseCurrentScope(context);
			}
		}

		protected override Type Type { get { return ScopeTypeCache.Link; } }

		public string Token { get { return "["; } }
	}
}