var EMPTY = function() {};

function ButterflyAnalyzer(writer) {
	this.writer = writer || new AppendStringWriter();
}

ButterflyAnalyzer.prototype = {
	writer: null,
	
	flush: function() {
		this.writer && this.writer.flush();
	},
	
	onStart: EMPTY,
	onEnd: EMPTY,
	
	writeAndEscape: function(text) {},
	writeUnescapedString: function(text) {},
	
	openStrongText: EMPTY,
	closeStrongText: EMPTY,
	
	openEmphasizedText: EMPTY,
	closeEmphasizedText: EMPTY,
	
	openHeader: function(depth) {},
	closeHeader: function(depth) {},
	
	openParagraph: EMPTY,
	closeParagraph: EMPTY,
	
	openUnderlinedText: EMPTY,
	closeUnderlinedText: EMPTY,
	
	openStrikeThroughText: EMPTY,
	closeStrikeThroughText: EMPTY,
	
	openTeletypeText: EMPTY,
	closeTeletypeText: EMPTY,
	
	openHorizontalRuler: EMPTY,
	closeHorizontalRuler: EMPTY,
	
	openSmallText: EMPTY,
	closeSmallText: EMPTY,
	
	openBigText: EMPTY,
	closeBigText: EMPTY,
	
	openLink: function(url, baseUrl) {},
	closeLink: EMPTY,
	
	openModule: function(module) {},
	closeModule: function(module) {},
	
	openBlockquote: EMPTY,
	closeBlockquote: EMPTY,
	
	openOrderedList: EMPTY,
	closeOrderedList: EMPTY,
	
	openUnorderedList: EMPTY,
	closeUnorderedList: EMPTY,
	
	openListItem: EMPTY,
	closeListItem: EMPTY,
	
	openPreformatted: function(language) {},
	closePreformatted: function(language) {},
	
	openPreformattedLine: EMPTY,
	closePreformattedLine: EMPTY,
	
	openTable: EMPTY,
	closeTable: EMPTY,
	
	openTableRow: EMPTY,
	closeTableRow: EMPTY,
	
	openTableRowLine: EMPTY,
	closeTableRowLine: EMPTY,
	
	openTableCell: EMPTY,
	closeTableCell: EMPTY,
	
	openTableHeader: EMPTY,
	closeTableHeader: EMPTY,
	
	openDefinitionList: EMPTY,
	closeDefinitionList: EMPTY,
	
	openDefinitionTerm: EMPTY,
	closeDefinitionTerm: EMPTY,
	
	openDefinition: EMPTY,
	closeDefinition: EMPTY,
	
	openMacro: function(macro) {},
	closeMacro: function(macro) {},
	
	openMultiLineDefinition: EMPTY,
	closeMultiLineDefinition: EMPTY,
	
	openLineBreak: EMPTY,
	closeLineBreak: EMPTY,
	
	openUnparsed: EMPTY,
	closeUnparsed: EMPTY
};