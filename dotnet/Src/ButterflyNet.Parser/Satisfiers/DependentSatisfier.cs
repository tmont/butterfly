namespace ButterflyNet.Parser.Satisfiers {
	public class DependentSatisfier : ISatisfier {
		private readonly ParseStrategy strategy;

		public DependentSatisfier(ParseStrategy strategy) {
			this.strategy = strategy;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return strategy.IsSatisfiedBy(context);
		}
	}

	public sealed class DependentSatisfier<T> : DependentSatisfier where T : ParseStrategy, new() {
		public DependentSatisfier() : base(new T()) { }
	}
}