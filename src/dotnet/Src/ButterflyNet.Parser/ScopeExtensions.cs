namespace ButterflyNet.Parser {
	public static class ScopeExtensions {
		public static string GetName(this IScope scope) {
			return scope.GetType().GetFriendlyName(false).Replace("Scope", "");
		}
	}
}