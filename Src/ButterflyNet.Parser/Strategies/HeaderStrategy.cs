using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class HeaderStrategy : ScopeDrivenStrategy {
		public HeaderStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
			AddSatisfier(new ExactCharMatchSatisfier("!"));
		}

		protected override void DoExecute(ParseContext context) {
			var depth = 1;
			while (context.Input.Peek() == '!') {
				depth++;
				context.Input.Read();
			}

			context.Input.SeekToNonWhitespace(); //ignore spaces/tabs
			context.UpdateCurrentChar();
			OpenScope(new HeaderScope(depth), context);
		}
	}
}