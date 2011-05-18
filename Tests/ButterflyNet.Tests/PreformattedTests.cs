using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class PreformattedTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_language() {
			const string text = @"{{{javascript
lulz}}}";

			Assert.That(Parser.ParseAndReturn(text).TrimEnd(), Is.EqualTo("<pre class=\"sunlight-highlight-javascript\">lulz</pre>"));
		}

		[Test]
		[ExpectedException(typeof(ParseException))]
		public void Should_not_get_in_infinite_loop_while_parsing_language() {
			Parser.Parse(@"{{{");
		}

		[Test]
		public void Should_keep_linebreaks_in_preformatted() {
			const string text = @"{{{

lulz

}}}";
			Assert.That(Parser.ParseAndReturn(text).TrimEnd(), Is.EqualTo("<pre>\nlulz\n\n</pre>"));
		}

		[Test]
		public void Should_allow_formatting_inside_preformatted() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("{{{\n__bold__}}}"), "<pre><strong>bold</strong></pre>");
		}

		[Test]
		public void Should_not_be_able_to_nest_preformatted_blocks() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("{{{\nnot {{{nested}}}}}}"), "<pre>not {{{nested</pre><p>}}}</p>");
		}

		[Test]
		public void Should_parse_preformatted_line() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(" text"), "<pre>text</pre>");
		}

		[Test]
		public void Should_parse_preformatted_line_and_close_on_line_break() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(" text\nlol"), "<pre>text</pre><p>lol</p>");
		}
	}
}