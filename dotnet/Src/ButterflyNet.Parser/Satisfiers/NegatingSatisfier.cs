namespace ButterflyNet.Parser.Satisfiers {

	public class NegatingSatisfier<T> : NegatingSatisfier where T : ISatisfier, new() {
		public NegatingSatisfier() : base(new T()) { }
	}

	public class NegatingSatisfier : ISatisfier {
		private readonly ISatisfier satisfierToNegate;

		public NegatingSatisfier(ISatisfier satisfierToNegate) {
			this.satisfierToNegate = satisfierToNegate;
		}

		public bool IsSatisfiedBy(ParseContext context) {
			return !satisfierToNegate.IsSatisfiedBy(context);
		}
	}
}