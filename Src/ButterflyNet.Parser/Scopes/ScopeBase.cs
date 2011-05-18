namespace ButterflyNet.Parser.Scopes {
	public abstract class ScopeBase : IScope {
		public abstract ScopeLevel Level { get; }
		public virtual bool ManuallyClosing { get { return false; } }
		public virtual bool CanNestParagraph { get { return false; } }
		public virtual bool CanNestText { get { return true; } }

		public virtual void Open(ButterflyAnalyzer analyzers) { }
		public virtual void Close(ButterflyAnalyzer analyzers) { }

		public override string ToString() {
			return string.Format("Scope(Type={0}, Level={1})", GetType().GetFriendlyName(false), Level);
		}
	}
}