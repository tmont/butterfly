namespace ButterflyNet.Parser.Satisfiers {
	public class DependentSatisfier : ISatisfier {
		private readonly ParseStrategyBase strategy;

		public DependentSatisfier(ParseStrategyBase strategy) {
			this.strategy = strategy;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return strategy.IsSatisfiedBy(context);
		}
	}

	public sealed class DependentSatisfier<T> : DependentSatisfier where T : ParseStrategyBase, new() {
		public DependentSatisfier() : base(new T()) { }
	}
}