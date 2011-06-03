namespace ButterflyNet.Parser.Scopes {
	public abstract class InlineScope : ScopeBase {
		public sealed override ScopeLevel Level { get { return ScopeLevel.Inline; } }
	}
}