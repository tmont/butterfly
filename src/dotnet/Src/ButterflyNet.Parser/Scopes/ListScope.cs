namespace ButterflyNet.Parser.Scopes {
	public abstract class ListScope : BlockScope {
		public override bool CanNestText { get { return false; } }
	}
}