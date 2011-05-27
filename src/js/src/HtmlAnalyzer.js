var SyntaxHighlightingLibrary = {
	sunlight: "sunlight",
	syntaxHighlighter: "SyntaxHighlighter",
	prettify: "prettify",
	highlightJs: "highlightjs",
	none: "none"
};

function htmlEncode(text) {
	return text.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/'/g, "&#39;");
}

function isExternalUrl(url) {
	return /^[^\/]:/.test(url || "");
}

function HtmlAnalyzer(writer) {
	HtmlAnalyzer.$parent.call(this, writer);
	
	function convertToString(value) {
		if (typeof(text) === "number") {
			text = String.fromCharCode(text);
		}
		
		return text;
	}
	
	this.syntaxHighlighter = SyntaxHighlightingLibrary.sunlight;
	
	this.openStrongText = function() { this.writer.write("<strong>"); };
	this.closeStrongText = function() { this.writer.write("</strong>"); };
	
	this.openEmphasizedText = function() { this.writer.write("<em>"); };
	this.closeEmphasizedText = function() { this.writer.write("</em>"); };
	
	this.openTeletypeText = function() { this.writer.write("<tt>"); };
	this.closeTeletypeText = function() { this.writer.write("</tt>"); };
	
	this.openHeader = function(depth) { this.writer.write("<h" + Math.min(6, depth) + ">"); };
	this.closeHeader = function(depth) { this.writer.write("</h" + Math.min(6, depth) + ">\n"); };
	
	this.writeAndEscape = function(text) { this.writer.write(htmlEncode(convertToString(text))); };
	this.writeUnescapedString = function(text) { this.writer.write(convertToString(text)); };
	
	this.openParagraph = function() { this.writer.write("<p>"); };
	this.closeParagraph = function() { this.writer.write("</p>\n"); };
	
	this.openUnderlinedText = function() { this.writer.write("<ins>"); };
	this.closeUnderlinedText = function() { this.writer.write("</ins>"); };
	
	this.openStrikeThroughText = function() { this.writer.write("<del>"); };
	this.closeStrikeThroughText = function() { this.writer.write("</del>"); };
	
	this.openHorizontalRuler = function() { this.writer.write("<hr />\n"); };
	
	this.openSmallText = function() { this.writer.write("<small>"); };
	this.closeSmallText = function() { this.writer.write("</small>"); };
	
	this.openBigText = function() { this.writer.write("<big>"); };
	this.closeBigText = function() { this.writer.write("</big>"); };
	
	this.openLink = function(url, baseUrl) {
		var cls = "class=\"external\" ";
		if (!isExternalUrl(url)) {
			cls = "";
			url = baseUrl + url;
		}
		
		this.writer.write("<a " + cls + "href=\"" + htmlEncode(url) + "\">");
	};
	this.closeLink = function() { this.writer.write("</a>"); };
	
	this.openModule = function(module) { module.render(buffer); };
	
	this.openBlockquote = function() { this.writer.write("<blockquote>"); };
	this.closeBlockquote = function() { this.writer.write("</blockquote>\n"); };
	
	this.openOrderedList = function() { this.writer.write("<ol>\n"); };
	this.closeOrderedList = function() { this.writer.write("</ol>\n"); };
	
	this.openUnorderedList = function() { this.writer.write("<ul>\n"); };
	this.closeUnorderedList = function() { this.writer.write("</ul>\n"); };
	
	this.openListItem = function() { this.writer.write("<li>"); };
	this.closeListItem = function() { this.writer.write("</li>\n"); };
	
	this.openPreformatted = function(language) {
		var cls = "",
			className;
		if (language) {
			switch (this.syntaxHighlighter) {
				case SyntaxHighlightingLibrary.sunlight:
					className = "sunlight-highlight-" + language;
					break;
				case SyntaxHighlightingLibrary.syntaxHighlighter:
					className = "brush: " + language;
					break;
				case SyntaxHighlightingLibrary.prettify:
					className = "prettyprint";
					break;
				case SyntaxHighlightingLibrary.highlightJs:
					className = "language-" + language;
					break;
			}
			
			cls = " class=\"" + className + "\"";
		}
		
		this.writer.write("<pre" + cls + ">");
	};
	
	this.closePreformatted = function(language) { this.writer.write("</pre>\n"); };
	
	this.openPreformattedLine = function(language) { this.writer.write("<pre>"); };
	this.closePreformattedLine = this.closePreformatted;
	
	this.openTable = function() { this.writer.write("<table>\n"); };
	this.closeTable = function() { this.writer.write("</table>\n"); };
	
	this.openTableRow = function() { this.writer.write("<tr>\n"); };
	this.closeTableRow = function() { this.writer.write("</tr>\n"); };
	
	this.openTableRowLine = this.openTableRow;
	this.closeTableRowLine = this.closeTableRow;
	
	this.openTableHeader = function() { this.writer.write("<th>"); };
	this.closeTableHeader = function() { this.writer.write("</th>\n"); };
	
	this.openTableCell = function() { this.writer.write("<td>"); };
	this.closeTableCell = function() { this.writer.write("</td>\n"); };
	
	this.openDefinitionList = function() { this.writer.write("<dl>\n"); };
	this.closeDefinitionList = function() { this.writer.write("</dl>\n"); };
	
	this.openDefinitionTerm = function() { this.writer.write("<dt>"); };
	this.closeDefinitionTerm = function() { this.writer.write("</dt>\n"); };
	
	this.openDefinition = function() { this.writer.write("<dd>"); };
	this.closeDefinition = function() { this.writer.write("</dd>\n"); };
	
	this.closeMultiLineDefinition = this.openDefinition;
	this.closeMultiLineDefinition = this.closeDefinition;
	
	this.openLineBreak = function() { this.writer.write("<br />"); };
}

extend(ButterflyAnalyzer, HtmlAnalyzer);


