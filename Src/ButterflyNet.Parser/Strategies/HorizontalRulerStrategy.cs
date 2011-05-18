using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(@"^----(?=(?:\n|$))", RegexMatch = true)]
	public class HorizontalRulerStrategy : ScopeDrivenStrategy {
		public HorizontalRulerStrategy() {
			AddSatisfier<StartOfLineSatisfier>();
		}

		//needs to be less than strike through, which is three dashes in a row
		public override int Priority { get { return DefaultPriority - 2; } }

		protected override void DoExecute(ParseContext context) {
			context.Input.Read(3);
			context.UpdateCurrentChar();

			OpenScope(new HorizontalRulerScope(), context);
			CloseCurrentScope(context);
		}
	}
}