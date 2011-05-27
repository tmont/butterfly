using System.IO;
using System.Text;

namespace ButterflyNet.Parser.Modules {
	public class LinkModule : IButterflyModule {
		public string Href { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public string Class { get; set; }
		public string Id { get; set; }
		public string Title { get; set; }

		public void Render(TextWriter writer) {
			if (string.IsNullOrEmpty(Href) && string.IsNullOrEmpty(Name)) {
				throw new ModuleException("To use the link module, you must specify one of href or name");
			}

			var attributes = new StringBuilder(30);
			if (!string.IsNullOrEmpty(Href)) {
				attributes.Append(string.Format("href=\"{0}\" ", Href));
			}
			if (!string.IsNullOrEmpty(Name)) {
				attributes.Append(string.Format("name=\"{0}\" ", Name));
			}
			if (!string.IsNullOrEmpty(Class)) {
				attributes.Append(string.Format("class=\"{0}\" ", Class));
			}
			if (!string.IsNullOrEmpty(Id)) {
				attributes.Append(string.Format("id=\"{0}\" ", Id));
			}
			if (!string.IsNullOrEmpty(Title)) {
				attributes.Append(string.Format("title=\"{0}\"", Title));
			}

			writer.Write(string.Format("<a {0}>{1}</a>", attributes.ToString().TrimEnd(' '), Text));
		}
	}
}