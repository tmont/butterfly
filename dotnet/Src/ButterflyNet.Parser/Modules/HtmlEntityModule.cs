using System.IO;
using System.Text.RegularExpressions;

namespace ButterflyNet.Parser.Modules {
	public class HtmlEntityModule : IButterflyModule {
		private static readonly Regex entityValidator = new Regex("^[a-zA-Z0-9#]+$");
		public string Value { get; set; }

		public void Render(TextWriter writer) {
			if (string.IsNullOrEmpty(Value)) {
				throw new ModuleException("The \"value\" property must be set to a valid HTML entity");
			}

			if (!entityValidator.IsMatch(Value)) {
				throw new ModuleException(string.Format("\"{0}\" is not a valid HTML entity", Value));
			}

			writer.Write(string.Format("&{0};", Value));
		}
	}
}