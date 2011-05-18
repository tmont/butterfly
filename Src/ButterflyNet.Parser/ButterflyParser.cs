using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ButterflyNet.Parser {
	public class ButterflyParser {
		private readonly ICollection<IParseStrategy> strategies = new Collection<IParseStrategy>();

		public ButterflyParser() {
			Analyzer = new HtmlAnalyzer(new StringWriter());
		}

		public ButterflyAnalyzer Analyzer { get; set; }
		public IEnumerable<IParseStrategy> Strategies { get { return strategies; } }
		public INamedFactory<IButterflyModule> ModuleFactory { get; set; }
		public INamedFactory<IButterflyMacro> MacroFactory { get; set; }

		#region add and remove stuff
		public ButterflyParser AddStrategy(IParseStrategy strategy) {
			strategies.Add(strategy);
			return this;
		}

		public ButterflyParser AddStrategy<T>() where T : IParseStrategy, new() {
			strategies.Add(new T());
			return this;
		}

		public ButterflyParser RemoveStrategy<T>() where T : IParseStrategy {
			strategies
				.OfType<T>()
				.ToList()
				.ForEach(strategy => RemoveStrategy(strategy));

			return this;
		}

		public ButterflyParser RemoveStrategy(IParseStrategy strategy) {
			strategies.Remove(strategy);
			return this;
		}
		#endregion

		public ParseResult Parse(string wikitext) {
			var context = new ParseContext(
				new ButterflyStringReader(wikitext ?? string.Empty),
				Analyzer,
				ModuleFactory ?? new ActivatorFactory<IButterflyModule>(new NamedTypeRegistry<IButterflyModule>().LoadDefaults()),
				MacroFactory ?? new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().LoadDefaults())
			);
			var orderedStrategies = Strategies.OrderBy(strategy => strategy.Priority);
			bool eofHandled;

			do {
				context.AdvanceInput();
				eofHandled = context.Input.IsEof;

				var strategy = orderedStrategies.Where(s => s.IsSatisfiedBy(context)).FirstOrDefault();
				if (strategy != null) {
					strategy.Execute(context);
				}
			} while (!eofHandled);

			return new ParseResult(context.ScopeTree, context.Input.Value);
		}
	}
}