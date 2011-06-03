using ButterflyNet.Parser.Strategies;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class UnescapedTests : WikiToHtmlTest {

		[SetUp]
		public override void SetUp() {
			base.SetUp();
			Parser
				.AddStrategy<OpenUnescapedStrategy>()
				.AddStrategy<CloseUnescapedStrategy>();
		}

		[Test]
		public void Should_write_unescaped_string_inside_scope() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__bold [@&amp;]__"), "<p><strong>bold &amp;</strong></p>");
		}

		[Test]
		public void Should_create_paragraph_if_unescaped_is_not_at_start_of_line() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("hello [@&amp;] there"), "<p>hello &amp; there</p>");
		}

		[Test]
		public void Should_write_multiline_unescaped_string() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[@lolz\nlolz\n<foo&amp;>\n]"), "<p>lolzlolz<foo&amp;></p>");
		}

		[Test]
		public void Should_allow_escaped_closing_brackets() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[@lulz]]]"), "<p>lulz]</p>");
		}

	}
}