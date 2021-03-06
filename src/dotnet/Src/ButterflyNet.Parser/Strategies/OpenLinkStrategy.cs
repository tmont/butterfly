﻿using System.Text;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("[")]
	public class OpenLinkStrategy : InlineStrategy {
		public OpenLinkStrategy() {
			AddSatisfier(new NegatingSatisfier(new InScopeStackSatisfier(ScopeTypeCache.Link)));
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

			OpenScope(new LinkScope(url, context.LocalLinkBaseUrl), context);

			if (closer == ']') {
				//the text is the same as the URL, so we can close the scope immediately
				context.Analyzer.WriteAndEscape(url);
				CloseCurrentScope(context);
			}
		}
	}
}