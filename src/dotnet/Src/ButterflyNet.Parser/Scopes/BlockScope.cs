namespace ButterflyNet.Parser.Scopes {
	public abstract class BlockScope : ScopeBase {
		public override sealed ScopeLevel Level { get { return ScopeLevel.Block; } }
	}
}