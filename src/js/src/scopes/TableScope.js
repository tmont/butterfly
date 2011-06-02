function TableRowLineScope() {
	this.type = ScopeTypeCache.tableRowLine;
	this.level = ScopeLevel.block;
	this.open = function(analyzer) {
		analyzer.openTableRowLine();
	};
	this.close = function(analyzer) {
		analyzer.closeTableRowLine();
	};
}

extend(Scope, TableRowLineScope);

function TableRowScope() {
	this.type = ScopeTypeCache.tableRow;
	this.level = ScopeLevel.block;
	this.open = function(analyzer) {
		analyzer.openTableRow();
	};
	this.close = function(analyzer) {
		analyzer.closeTableRow();
	};
}

extend(Scope, TableRowScope);

function TableScope() {
	this.type = ScopeTypeCache.table;
	this.level = ScopeLevel.block;
	this.open = function(analyzer) {
		analyzer.openTable();
	};
	this.close = function(analyzer) {
		analyzer.closeTable();
	};
}

extend(Scope, TableScope);

function TableCellScope() {
	this.type = ScopeTypeCache.tableCell;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	this.open = function(analyzer) {
		analyzer.openTableCell();
	};
	this.close = function(analyzer) {
		analyzer.closeTableCell();
	};
}

extend(Scope, TableCellScope);

function TableHeaderScope() {
	this.type = ScopeTypeCache.tableHeader;
	this.level = ScopeLevel.block;
	this.canNestText = true;
	this.open = function(analyzer) {
		analyzer.openTableHeader();
	};
	this.close = function(analyzer) {
		analyzer.closeTableHeader();
	};
}

extend(Scope, TableHeaderScope);