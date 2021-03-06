﻿namespace ButterflyNet.Parser.Scopes {
	public class MultiLineDefinitionScope : BlockScope {
		public override bool CanNestParagraph { get { return true; } }

		public override void Open(ButterflyAnalyzer analyzer) {
			analyzer.OpenMultiLineDefinition();
		}

		public override void Close(ButterflyAnalyzer analyzer) {
			analyzer.CloseMultiLineDefinition();
		}
	}
}