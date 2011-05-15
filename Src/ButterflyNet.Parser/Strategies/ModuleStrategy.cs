using System;
using System.Collections.Generic;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class ModuleStrategy : FunctionalStrategy {
		public override int Priority {
			get {
				//must be less than OpenLinkStrategy
				return DefaultPriority - 1;
			}
		}

		protected override Type Type { get { return ScopeTypeCache.Module; } }

		protected override IScope CreateScope(string name, IDictionary<string, string> data, ParseContext context) {
			var module = context.ModuleFactory.Create(name);
			module.Load(data);

			return new ModuleScope(module);
		}

		public override string Token { get { return "[:"; } }
	}
}