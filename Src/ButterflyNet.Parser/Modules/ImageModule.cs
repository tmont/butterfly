using System;
using System.IO;
using System.Text;

namespace ButterflyNet.Parser.Modules {
	public class ImageModule : IButterflyModule {
		public string Url { get; set; }
		public string Alt { get; set; }
		public string Title { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public void Render(TextWriter writer) {
			if (string.IsNullOrEmpty(Url)) {
				throw new ModuleException("For images, the URL must be specified");
			}

			if (string.IsNullOrEmpty(Alt)) {
				Alt = Url;
			}
			if (string.IsNullOrEmpty(Title)) {
				Title = Url;
			}

			var otherAttributes = new StringBuilder(25);
			if (Width > 0 || Height > 0) {
				otherAttributes.Append(" ");
				if (Width > 0) {
					otherAttributes.Append(string.Format("width=\"{0}\" ", Width));
				}
				if (Height > 0) {
					otherAttributes.Append(string.Format("height=\"{0}\"", Height));
				}
			}

			if (!Url.IsExternalUrl()) {
				Url = "/media/images/" + Url;
			}

			writer.Write(String.Format("<img src=\"{0}\" alt=\"{1}\" title=\"{2}\"{3} />", Url, Alt, Title, otherAttributes.ToString().TrimEnd(' ')));
		}
	}
}