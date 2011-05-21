using System.Collections.Generic;

namespace ButterflyNet.Parser {
	public sealed class ParseContext : IParserOptions {
		internal ParseContext(ButterflyStringReader input, IParserOptions options) {
			Input = input;
			Scopes = new Stack<IScope>();
			LocalLinkBaseUrl = options.LocalLinkBaseUrl;
			LocalImageBaseUrl = options.LocalImageBaseUrl;
			Analyzer = options.Analyzer ?? new HtmlAnalyzer();
			ModuleFactory = options.ModuleFactory ?? new DefaultModuleFactory(LocalImageBaseUrl, new NamedTypeRegistry<IButterflyModule>().LoadDefaults());
			MacroFactory = options.MacroFactory ?? new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().LoadDefaults());
			ScopeTree = new ScopeTree();
		}

		public string LocalLinkBaseUrl { get; private set; }
		public string LocalImageBaseUrl { get; private set; }

		public int CurrentChar { get; set; }
		public ButterflyStringReader Input { get; private set; }
		public Stack<IScope> Scopes { get; private set; }
		public ButterflyAnalyzer Analyzer { get; private set; }
		public ScopeTree ScopeTree { get; private set; }
		public ScopeTreeNode CurrentNode { get; set; }

		public INamedFactory<IButterflyModule> ModuleFactory { get; private set; }
		public INamedFactory<IButterflyMacro> MacroFactory { get; private set; }
	}
}