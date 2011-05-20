using System;
using System.IO;

namespace ButterflyNet.Parser {

	public abstract class ButterflyAnalyzer {
		protected ButterflyAnalyzer(TextWriter writer = null) {
			Writer = writer ?? new StringWriter();
		}

		public TextWriter Writer { get; private set; }

		public void Flush() {
			Writer.Flush();
		}

		public virtual void OnStart() { }
		public virtual void OnEnd() { }

		public virtual void WriteAndEscape(string text) { }
		public virtual void WriteAndEscape(char c) { }
		public virtual void WriteUnescapedString(string text) { }
		public virtual void WriteUnescapedChar(char c) { }

		public virtual void OpenStrongText() { }
		public virtual void CloseStrongText() { }

		public virtual void OpenEmphasizedText() { }
		public virtual void CloseEmphasizedText() { }

		public virtual void OpenHeader(int depth) { }
		public virtual void CloseHeader(int depth) { }

		public virtual void OpenParagraph() { }
		public virtual void CloseParagraph() { }

		public virtual void OpenUnderlinedText() { }
		public virtual void CloseUnderlinedText() { }

		public virtual void OpenStrikeThroughText() { }
		public virtual void CloseStrikeThroughText() { }

		public virtual void OpenTeletypeText() { }
		public virtual void CloseTeletypeText() { }

		public virtual void OpenHorizontalRuler() { }
		public virtual void CloseHorizontalRuler() { }

		public virtual void OpenSmallText() { }
		public virtual void CloseSmallText() { }

		public virtual void OpenBigText() { }
		public virtual void CloseBigText() { }

		public virtual void OpenLink(string url) { }
		public virtual void CloseLink() { }

		public virtual void OpenModule(IButterflyModule module) { }
		public virtual void CloseModule(IButterflyModule module) { }

		public virtual void OpenBlockquote() { }
		public virtual void CloseBlockquote() { }

		public virtual void OpenOrderedList() { }
		public virtual void CloseOrderedList() { }

		public virtual void OpenUnorderedList() { }
		public virtual void CloseUnorderedList() { }

		public virtual void OpenListItem() { }
		public virtual void CloseListItem() { }

		public virtual void OpenPreformatted(string language) { }
		public virtual void ClosePreformatted(string language) { }

		public virtual void OpenPreformattedLine() { }
		public virtual void ClosePreformattedLine() { }

		public virtual void OpenTable() { }
		public virtual void CloseTable() { }

		public virtual void OpenTableRow() { }
		public virtual void CloseTableRow() { }

		public virtual void OpenTableRowLine() { }
		public virtual void CloseTableRowLine() { }

		public virtual void OpenTableCell() { }
		public virtual void CloseTableCell() { }

		public virtual void OpenTableHeader() { }
		public virtual void CloseTableHeader() { }

		public virtual void OpenDefinitionList() { }
		public virtual void CloseDefinitionList() { }

		public virtual void OpenDefinitionTerm() { }
		public virtual void CloseDefinitionTerm() { }

		public virtual void OpenDefinition() { }
		public virtual void CloseDefinition() { }

		public virtual void OpenMacro(IButterflyMacro macro) { }
		public virtual void CloseMacro(IButterflyMacro macro) { }

		public virtual void OpenMultiLineDefinition() { }
		public virtual void CloseMultiLineDefinition() { }

		public virtual void OpenLineBreak() {}
		public virtual void CloseLineBreak() {}
	}
}