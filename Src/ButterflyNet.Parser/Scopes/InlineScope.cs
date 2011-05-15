namespace ButterflyNet.Parser.Scopes {
	public abstract class InlineScope : ScopeBase {
		public sealed override ScopeLevel Level { get { return ScopeLevel.Inline; } }
		public sealed override bool ManuallyClosing { get { return true; } }
	}
}