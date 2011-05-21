using System.Collections.Generic;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer("[:")]
	public class ModuleStrategy : FunctionalStrategy {
		public override int Priority {
			get {
				//must be less than OpenLinkStrategy
				return DefaultPriority - 1;
			}
		}

		protected override IScope CreateScope(string name, IDictionary<string, string> data, ParseContext context) {
			var module = context.ModuleFactory.Create(name);
			module.Load(data);

			return new ModuleScope(module);
		}
	}
}