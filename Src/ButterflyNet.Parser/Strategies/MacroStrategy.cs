using System;
using System.Collections.Generic;
using System.Diagnostics;
using ButterflyNet.Parser.Scopes;
namespace ButterflyNet.Parser.Strategies {
	[TokenTransformer(Token)]
	public class MacroStrategy : FunctionalStrategy {
		public const string Token = "[::";

		public MacroStrategy() {
			SetEventDelegates();
		}

		private void SetEventDelegates() {
			var startIndex = -1;

			BeforeExecute += context => {
				startIndex = context.Input.Index - (Token.Length - 1);
			};

			AfterScopeCloses += (scope, context) => {
				Debug.Assert(startIndex >= 0);

				//replace the macro declaration with its value
				var value = ((MacroScope)scope).Macro.GetValue();
				var endIndex = context.Input.Index + 1;
				context.Input.Replace(startIndex, endIndex, value);// = beginning + value + end;
				context.Input.Seek(startIndex);
				startIndex = -1;
			};
		}

		public override int Priority {
			get {
				//must be less than ModuleStrategy
				return DefaultPriority - 2;
			}
		}

		protected override Type Type { get { return ScopeTypeCache.Macro; } }

		protected override IScope CreateScope(string name, IDictionary<string, string> data, ParseContext context) {
			var macro = context.MacroFactory.Create(name);
			macro.Load(data);
			return new MacroScope(macro);
		}
	}
}