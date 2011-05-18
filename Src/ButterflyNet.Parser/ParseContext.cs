using System.Collections.Generic;
using System.Text;

namespace ButterflyNet.Parser {
	public sealed class ParseContext {
		public ParseContext(
			ButterflyStringReader input, 
			ButterflyAnalyzer analyzer, 
			INamedFactory<IButterflyModule> moduleFactory, 
			INamedFactory<IButterflyMacro> macroFactory
		) {
			Input = input;
			Scopes = new Stack<IScope>();
			Analyzer = analyzer;
			ModuleFactory = moduleFactory;
			MacroFactory = macroFactory;
			ScopeTree = new ScopeTree();
			Buffer = new StringBuilder();
			ExecuteNext = true;
		}

		public int CurrentChar { get; set; }
		public ButterflyStringReader Input { get; private set; }
		public Stack<IScope> Scopes { get; private set; }
		public ButterflyAnalyzer Analyzer { get; private set; }
		public ScopeTree ScopeTree { get; private set; }
		public ScopeTreeNode CurrentNode { get; set; }
		public StringBuilder Buffer { get; private set; }

		/// <summary>
		/// Gets or sets whether to execute the next strategy in the chain. Defaults to true.
		/// </summary>
		public bool ExecuteNext { get; set; }

		public INamedFactory<IButterflyModule> ModuleFactory { get; private set; }
		public INamedFactory<IButterflyMacro> MacroFactory { get; private set; }
	}
}