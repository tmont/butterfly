namespace ButterflyNet.Parser.Satisfiers {
	public class DependentSatisfier : ISatisfier {
		private readonly IParseStrategy strategy;

		public DependentSatisfier(IParseStrategy strategy) {
			this.strategy = strategy;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return strategy.IsSatisfiedBy(context);
		}
	}

	public sealed class DependentSatisfier<T> : DependentSatisfier where T : IParseStrategy, new() {
		public DependentSatisfier() : base(new T()) { }
	}
}