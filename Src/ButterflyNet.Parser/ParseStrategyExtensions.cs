namespace ButterflyNet.Parser {
	public static class ParseStrategyExtensions {
		public static void ExecuteIfSatisfied(this IParseStrategy strategy, ParseContext context) {
			if (strategy.IsSatisfiedBy(context)) {
				strategy.Execute(context);
			}
		}
	}
}