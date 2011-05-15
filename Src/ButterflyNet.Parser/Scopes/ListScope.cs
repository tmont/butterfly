namespace ButterflyNet.Parser.Scopes {
	public abstract class ListScope : BlockScope {
		public override bool ManuallyClosing { get { return false; } }
		public override bool ClosesOnContext { get { return true; } }
		public override bool CanNestText { get { return false; } }

		public override bool ShouldClose(IScope nextScope) {
			var nextType = nextScope.GetType();
			return nextType != ScopeTypeCache.ListItem
				&& nextType != ScopeTypeCache.OrderedList
				&& nextType != ScopeTypeCache.UnorderedList;
		}
	}
}