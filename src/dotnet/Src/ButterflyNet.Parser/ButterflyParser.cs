﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ButterflyNet.Parser {

	internal interface IParserOptions {
		string LocalLinkBaseUrl { get; }
		string LocalImageBaseUrl { get; }
		ButterflyAnalyzer Analyzer { get; }
		INamedFactory<IButterflyMacro> MacroFactory { get; }
		INamedFactory<IButterflyModule> ModuleFactory { get; }
	}

	public class ButterflyParser : IParserOptions {
		private readonly ICollection<ParseStrategy> strategies = new Collection<ParseStrategy>();

		public ButterflyParser() {
			Analyzer = new HtmlAnalyzer();
			LocalLinkBaseUrl = "/";
			LocalImageBaseUrl = "/";
		}

		public ButterflyAnalyzer Analyzer { get; set; }
		public string LocalLinkBaseUrl { get; set; }
		public string LocalImageBaseUrl { get; set; }

		public IEnumerable<ParseStrategy> Strategies { get { return strategies; } }
		public INamedFactory<IButterflyModule> ModuleFactory { get; set; }
		public INamedFactory<IButterflyMacro> MacroFactory { get; set; }

		#region add and remove stuff
		public ButterflyParser AddStrategy(ParseStrategy strategy) {
			strategies.Add(strategy);
			return this;
		}

		public ButterflyParser AddStrategy<T>() where T : ParseStrategy, new() {
			strategies.Add(new T());
			return this;
		}

		public ButterflyParser RemoveStrategy<T>() where T : ParseStrategy {
			strategies
				.OfType<T>()
				.ToList()
				.ForEach(strategy => RemoveStrategy(strategy));

			return this;
		}

		public ButterflyParser RemoveStrategy(ParseStrategy strategy) {
			strategies.Remove(strategy);
			return this;
		}
		#endregion

		public ParseResult Parse(string wikitext) {
			var context = new ParseContext(new ButterflyStringReader(wikitext ?? string.Empty), this);

			var orderedStrategies = Strategies.OrderBy(strategy => strategy.Priority);
			bool eofHandled;

			context.Analyzer.OnStart();

			do {
				context.AdvanceInput();
				eofHandled = context.Input.IsEof;

				var strategy = orderedStrategies.Where(s => s.IsSatisfiedBy(context)).FirstOrDefault();
				if (strategy == null) {
					throw new ParseException(string.Format(
						"No strategy found for {0}",
						context.CurrentChar == ButterflyStringReader.NoValue ? "<EOF>" : ((char)context.CurrentChar).ToString())
					);
				}

				strategy.Execute(context);
			} while (!eofHandled);

			if (!context.Scopes.IsEmpty()) {
				throw new ParseException("Scopes that need to be manually closed were not closed");
			}

			context.Analyzer.OnEnd();

			return new ParseResult(context.ScopeTree, context.Input.Value);
		}
	}
}