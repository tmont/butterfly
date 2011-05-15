namespace ButterflyNet.Parser.Scopes {
	public class TableScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTable();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTable();
		}
	}

	public class TableRowScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableRow();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableRow();
		}
	}

	public class TableRowLineScope : BlockScope {
		public override bool CanNestText { get { return false; } }
		public override bool CloseOnSingleLineBreak { get { return true; } }
		public override bool ManuallyClosing { get { return false; } }

		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableRowLine();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableRowLine();
		}
	}

	public class TableCellScope : BlockScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableCell();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableCell();
		}
	}

	public class TableHeaderScope : BlockScope {
		protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableHeader();
		}

		protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableHeader();
		}
	}
}