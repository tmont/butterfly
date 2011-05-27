using System.IO;

namespace ButterflyNet.Parser {
	public interface IButterflyModule {
		void Render(TextWriter writer);
	}
}