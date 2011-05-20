using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ButterflyNet.Parser.Strategies;

namespace ButterflyNet.Parser {
	public static class ParserExtensions {
		/// <summary>
		/// Convenience method for quickly getting the textual result of a previous parse analysis
		/// </summary>
		public static string FlushAndReturn(this ButterflyParser parser) {
			var data = parser.Analyzer.Writer.ToString();
			parser.Analyzer.Flush();
			return data;
		}

		/// <summary>
		/// Convenience method for quickly getting the textual result of a parse analysis.
		/// This method will throw if there isn't exactly one analyzer registered with the parser.
		/// </summary>
		public static string ParseAndReturn(this ButterflyParser parser, string wikitext) {
			parser.Parse(wikitext);
			return parser.FlushAndReturn();
		}

		/// <summary>
		/// Loads the default parse strategies (everything from the <c>ButterflyNet.Parser.Strategies</c> namespace)
		/// </summary>
		public static ButterflyParser LoadDefaultStrategies(this ButterflyParser parser, IParseStrategyFactory strategyFactory) {
			var strongType = typeof(StrongStrategy);
			var types = Assembly
				.GetAssembly(strongType)
				.GetTypes()
				.Where(type => type.Namespace == strongType.Namespace && !type.HasAttribute<NonDefaultAttribute>() && !type.HasAttribute<ExcludeAttribute>());

			return parser.LoadStrategiesFromTypes(strategyFactory, types);
		}

		/// <summary>
		/// Loads all <see cref="ParseStrategy"/> implementations found in the specified assembly
		/// </summary>
		public static ButterflyParser LoadStrategiesFromAssembly(this ButterflyParser parser, IParseStrategyFactory strategyFactory, Assembly assembly) {
			return parser.LoadStrategiesFromTypes(strategyFactory, assembly.GetTypes().Where(type => !type.HasAttribute<ExcludeAttribute>()));
		}

		private static ButterflyParser LoadStrategiesFromTypes(this ButterflyParser parser, IParseStrategyFactory strategyFactory, IEnumerable<Type> types) {
			var strategyInterface = typeof(ParseStrategy);
			types = types.Where(type => type.IsClass && !type.IsAbstract && strategyInterface.IsAssignableFrom(type));

			types.Walk(type => parser.AddStrategy(strategyFactory.Create(type)));

			return parser;
		}
	}
}