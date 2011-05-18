using System;
using System.Collections.Generic;
using System.Diagnostics;
using ButterflyNet.Parser.Scopes;

namespace ButterflyNet.Parser.Strategies {
	public class MacroStrategy : FunctionalStrategy {
		private const string Token = "[::";

		public MacroStrategy() {
			SetEventDelegates();
			AddSatisfier(new ExactCharMatchSatisfier(Token));
		}

		private void SetEventDelegates() {
			int startIndex = -1;

			BeforeExecute += context => {
				startIndex = context.Input.Index - (Token.Length - 1);
			};

			AfterScopeCloses += (scope, context) => {
				Debug.Assert(startIndex >= 0);

				//replace the macro declaration with its value
				var value = ((MacroScope)scope).Macro.GetValue();
				var endIndex = context.Input.Index + 1;
				//var beginning = context.Input.Value.Substring(0, startIndex + context.MacroOffset);
				//var end = endIndex + context.MacroOffset < context.Input.Value.Length ? context.Input.Value.Substring(endIndex + context.MacroOffset) : string.Empty;

				context.Input.Replace(startIndex, endIndex, value);// = beginning + value + end;
				context.Input.Seek(startIndex);

				//context.MacroOffset += value.Length - (endIndex - startIndex);
				startIndex = -1;

				//context.Parser.PartialParse(context.InputAfterMacros, endIndex, endIndex + value.Length, context.Scopes);
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