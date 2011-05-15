using System.IO;

namespace ButterflyNet.Parser.Modules {
	public class CategoryModule : IButterflyModule {
		public string Name { get; set; }
		public string DisplayName { get; set; }

		public void Render(TextWriter writer) { }
	}
}