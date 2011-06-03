using System;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser {
	/// <summary>
	/// Helper class for accessing scope types
	/// </summary>
	public static class ScopeTypeCache {
		public static readonly Type Strong = typeof(StrongScope);
		public static readonly Type Emphasis = typeof(EmphasisScope);
		public static readonly Type Underline = typeof(UnderlineScope);
		public static readonly Type StrikeThrough = typeof(StrikeThroughScope);
		public static readonly Type Teletype = typeof(TeletypeScope);
		public static readonly Type Small = typeof(SmallScope);
		public static readonly Type Big = typeof(BigScope);
		public static readonly Type Raw = typeof(UnparsedScope);
		public static readonly Type Unescaped = typeof(UnescapedScope);
		public static readonly Type Link = typeof(LinkScope);
		public static readonly Type Module = typeof(ModuleScope);
		public static readonly Type Macro = typeof(MacroScope);
		public static readonly Type LineBreak = typeof(LineBreakScope);
 
		public static readonly Type HorizontalRuler = typeof(HorizontalRulerScope);
		public static readonly Type Paragraph = typeof(ParagraphScope);
		public static readonly Type Blockquote = typeof(BlockquoteScope);
		public static readonly Type OrderedList = typeof(OrderedListScope);
		public static readonly Type UnorderedList = typeof(UnorderedListScope);
		public static readonly Type ListItem = typeof(ListItemScope);
		public static readonly Type Preformatted = typeof(PreformattedScope);
		public static readonly Type PreformattedLine = typeof(PreformattedLineScope);
		public static readonly Type Table = typeof(TableScope);
		public static readonly Type TableRow = typeof(TableRowScope);
		public static readonly Type TableRowLine = typeof(TableRowLineScope);
		public static readonly Type TableHeader = typeof(TableHeaderScope);
		public static readonly Type TableCell = typeof(TableCellScope);
		public static readonly Type DefinitionList = typeof(DefinitionListScope);
		public static readonly Type DefinitionTerm = typeof(DefinitionTermScope);
		public static readonly Type Definition = typeof(DefinitionScope);
		public static readonly Type MultiLineDefinition = typeof(MultiLineDefinitionScope);
		public static readonly Type Header = typeof(HeaderScope);
	}
}