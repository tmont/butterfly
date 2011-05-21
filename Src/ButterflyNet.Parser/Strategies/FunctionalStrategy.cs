using System;
using System.Collections.Generic;
using System.Text;

namespace ButterflyNet.Parser.Strategies {
	public abstract class FunctionalStrategy : InlineStrategy {
		protected override sealed void DoExecute(ParseContext context) {
			var peek = context.Input.Peek();
			var functionNameBuilder = new StringBuilder();
			while (peek != ButterflyStringReader.NoValue && peek != '|' && peek != ']') {
				functionNameBuilder.Append((char)context.Input.Read());
				peek = context.Input.Peek();
			}

			var closer = context.Input.Read(); //| or ]
			var name = functionNameBuilder.ToString();

			var data = new Dictionary<string, string>();

			if (closer != ']') {
				//read module options
				peek = context.Input.Peek();

				var optionStringBuilder = new StringBuilder();
				while (peek != ButterflyStringReader.NoValue) {
					if (peek == ']') {
						context.Input.Read();
						if (context.Input.Peek() != ']') {
							//module closer
							break;
						}

						//fallthrough: output a literal "]"
					} else if (peek == '|') {
						context.Input.Read();
						if (context.Input.Peek() != '|') {
							ParseOptions(optionStringBuilder.ToString(), data);
							optionStringBuilder.Clear();
						}

						//fallthrough: output a literal "|"
					}

					optionStringBuilder.Append((char)context.Input.Read());
					peek = context.Input.Peek();
				}

				//handle the last option
				if (optionStringBuilder.Length > 0) {
					ParseOptions(optionStringBuilder.ToString(), data);
				}
			}

			context.UpdateCurrentChar();
			OpenAndCloseScope(CreateScope(name, data, context), context);
		}

		protected virtual void OpenAndCloseScope(IScope scope, ParseContext context) {
			OpenScope(scope, context);
			CloseCurrentScope(context);
		}

		protected abstract IScope CreateScope(string name, IDictionary<string, string> data, ParseContext context);

		private static void ParseOptions(string optionString, IDictionary<string, string> data) {
			var equalsIndex = Math.Max(0, optionString.IndexOf('='));
			data[optionString.Substring(0, equalsIndex)] = optionString.Substring(Math.Min(equalsIndex + 1, optionString.Length));
		}
	}
}