namespace ButterflyNet.Parser.Scopes {
	public class TableScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTable();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTable();
		}
	}

	public class TableRowScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableRow();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableRow();
		}
	}

	public class TableRowLineScope : BlockScope {
		public override bool CanNestText { get { return false; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableRowLine();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableRowLine();
		}
	}

	public class TableCellScope : BlockScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableCell();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableCell();
		}
	}

	public class TableHeaderScope : BlockScope {
		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenTableHeader();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseTableHeader();
		}
	}
}