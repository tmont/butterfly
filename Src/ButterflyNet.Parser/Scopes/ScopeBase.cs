using System.Collections.Generic;

namespace ButterflyNet.Parser.Scopes {
	public abstract class ScopeBase : IScope {
		public abstract ScopeLevel Level { get; }
		public virtual bool ManuallyClosing { get { return false; } }
		public virtual bool CloseOnSingleLineBreak { get { return false; } }
		public virtual bool ClosesOnContext { get { return false; } }
		public virtual bool CanNestParagraph { get { return false; } }
		public virtual bool CanNestText { get { return true; } }

		public virtual void Open(IEnumerable<ButterflyAnalyzer> analyzers) {
			analyzers.Walk(OpenAndAnalyze);
		}

		public virtual void Close(IEnumerable<ButterflyAnalyzer> analyzers) {
			analyzers.Walk(CloseAndAnalyze);
		}

		public virtual bool ShouldClose(IScope nextScope) {
			return false;
		}

		protected virtual void OpenAndAnalyze(ButterflyAnalyzer analyzer) { }

		protected virtual void CloseAndAnalyze(ButterflyAnalyzer analyzer) { }

		public override string ToString() {
			return string.Format("Scope(Type={0}, Level={1})", GetType().GetFriendlyName(false), Level);
		}
	}
}