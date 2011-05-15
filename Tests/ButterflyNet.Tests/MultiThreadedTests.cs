using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {

	[TestFixture]
	public class MultiThreadedTests {
		[Test]
		public void Should_allow_multithreaded_parsing() {
			const string wikitext = "foo bar __lulz__ foo bar __lulz__ foo bar __lulz__ foo bar __lulz__";
			const string expected = "<p>foo bar <strong>lulz</strong> foo bar <strong>lulz</strong> foo bar <strong>lulz</strong> foo bar <strong>lulz</strong></p>";

			Parallel.For(0, 100, i => Assert.That(ParseString(wikitext), Is.EqualTo(expected), string.Format("Thread {0} failed", i)));
		}

		private static string ParseString(string wikitext) {
			return new ButterflyParser()
				.AddAnalyzer(new HtmlAnalyzer(new StringWriter()))
				.LoadDefaultStrategies(new DefaultParseStrategyFactory())
				.ParseAndReturn(wikitext);
		}
	}
}