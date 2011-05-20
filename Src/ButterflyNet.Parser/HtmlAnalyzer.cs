using System;
using System.IO;
using System.Web;

namespace ButterflyNet.Parser {
	public class HtmlAnalyzer : ButterflyAnalyzer {

		public HtmlAnalyzer(TextWriter writer = null) : base(writer) { }

		public SyntaxHighlightingLibrary SyntaxHighlighter { get; set; }

		public override void OpenStrongText() {
			Writer.Write("<strong>");
		}

		public override void CloseStrongText() {
			Writer.Write("</strong>");
		}

		public override void OpenEmphasizedText() {
			Writer.Write("<em>");
		}

		public override void CloseEmphasizedText() {
			Writer.Write("</em>");
		}

		public override void OpenTeletypeText() {
			Writer.Write("<tt>");
		}

		public override void CloseTeletypeText() {
			Writer.Write("</tt>");
		}

		public override void OpenHeader(int depth) {
			depth = Math.Min(6, depth);
			Writer.Write("<h" + depth + ">");
		}

		public override void CloseHeader(int depth) {
			depth = Math.Min(6, depth);
			Writer.Write("</h" + depth + ">\n");
		}

		public override void WriteAndEscape(string text) {
			Writer.Write(HttpUtility.HtmlEncode(text));
		}

		public override void WriteAndEscape(char c) {
			Writer.Write(HttpUtility.HtmlEncode(c));
		}

		public override void WriteUnescapedString(string text) {
			Writer.Write(text);
		}

		public override void WriteUnescapedChar(char c) {
			Writer.Write(c);
		}

		public override void CloseParagraph() {
			Writer.Write("</p>\n");
		}

		public override void OpenUnderlinedText() {
			Writer.Write("<ins>");
		}

		public override void CloseUnderlinedText() {
			Writer.Write("</ins>");
		}

		public override void OpenStrikeThroughText() {
			Writer.Write("<del>");
		}

		public override void CloseStrikeThroughText() {
			Writer.Write("</del>");
		}

		public override void OpenHorizontalRuler() {
			Writer.Write("<hr />\n");
		}

		public override void OpenSmallText() {
			Writer.Write("<small>");
		}

		public override void CloseSmallText() {
			Writer.Write("</small>");
		}

		public override void OpenBigText() {
			Writer.Write("<big>");
		}

		public override void CloseBigText() {
			Writer.Write("</big>");
		}

		public override void OpenParagraph() {
			Writer.Write("<p>");
		}

		public override void OpenLink(string url, string baseUrl) {
			var cls = "class=\"external\" ";
			if (!url.IsExternalUrl()) {
				cls = "";
				url = baseUrl + url;
			}

			Writer.Write(string.Format("<a {1}href=\"{0}\">", HttpUtility.HtmlEncode(url), cls));
		}

		public override void CloseLink() {
			Writer.Write("</a>");
		}

		public override void OpenModule(IButterflyModule module) {
			module.Render(Writer);
		}

		public override void OpenBlockquote() {
			Writer.Write("<blockquote>");
		}

		public override void CloseBlockquote() {
			Writer.Write("</blockquote>\n");
		}

		public override void OpenOrderedList() {
			Writer.Write("<ol>\n");
		}

		public override void CloseOrderedList() {
			Writer.Write("</ol>\n");
		}

		public override void OpenUnorderedList() {
			Writer.Write("<ul>\n");
		}

		public override void CloseUnorderedList() {
			Writer.Write("</ul>\n");
		}

		public override void OpenListItem() {
			Writer.Write("<li>");
		}

		public override void CloseListItem() {
			Writer.Write("</li>\n");
		}

		public override void OpenPreformatted(string language) {
			var cls = string.Empty;
			if (!string.IsNullOrWhiteSpace(language)) {
				switch (SyntaxHighlighter) {
					case SyntaxHighlightingLibrary.Sunlight:
						cls = string.Format(" class=\"sunlight-highlight-{0}\"", language);
						break;
					case SyntaxHighlightingLibrary.SyntaxHighlighter:
						cls = string.Format(" class=\"brush: {0}\"", language);
						break;
					case SyntaxHighlightingLibrary.Prettify:
						cls = " class=\"prettyprint\"";
						break;
					case SyntaxHighlightingLibrary.HighlightJs:
						cls = string.Format(" class=\"language-{0}\"", language);
						break;
				}
			}

			Writer.Write(string.Format("<pre{0}>", cls));
		}

		public override void ClosePreformatted(string language) {
			Writer.Write("</pre>\n");
		}

		public override void OpenPreformattedLine() {
			Writer.Write("<pre>");
		}

		public override void ClosePreformattedLine() {
			ClosePreformatted(null);
		}

		public override void OpenTable() {
			Writer.Write("<table>\n");
		}

		public override void CloseTable() {
			Writer.Write("</table>\n");
		}

		public override void OpenTableRow() {
			Writer.Write("<tr>\n");
		}

		public override void CloseTableRow() {
			Writer.Write("</tr>\n");
		}

		public override void OpenTableRowLine() {
			OpenTableRow();
		}

		public override void CloseTableRowLine() {
			CloseTableRow();
		}

		public override void OpenTableHeader() {
			Writer.Write("<th>");
		}

		public override void CloseTableHeader() {
			Writer.Write("</th>\n");
		}

		public override void OpenTableCell() {
			Writer.Write("<td>");
		}

		public override void CloseTableCell() {
			Writer.Write("</td>\n");
		}

		public override void OpenDefinitionList() {
			Writer.Write("<dl>");
		}

		public override void CloseDefinitionList() {
			Writer.Write("</dl>\n");
		}

		public override void OpenDefinitionTerm() {
			Writer.Write("<dt>");
		}

		public override void CloseDefinitionTerm() {
			Writer.Write("</dt>\n");
		}

		public override void OpenDefinition() {
			Writer.Write("<dd>");
		}

		public override void CloseDefinition() {
			Writer.Write("</dd>\n");
		}

		public override void CloseMultiLineDefinition() {
			CloseDefinition();
		}

		public override void OpenMultiLineDefinition() {
			OpenDefinition();
		}

		public override void OpenLineBreak() {
			Writer.Write("<br />");
		}

	}
}