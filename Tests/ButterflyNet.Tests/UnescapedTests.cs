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
			Parser.Parse("__bold [@&amp;]__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><strong>bold &amp;</strong></p>");
		}

		[Test]
		public void Should_create_paragraph_if_unescaped_is_not_at_start_of_line() {
			Parser.Parse("hello [@&amp;] there");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>hello &amp; there</p>");
		}

		[Test]
		public void Should_write_multiline_unescaped_string() {
			Parser.Parse(@"[@lolz
lolz
<foo&amp;>
]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>lolzlolz<foo&amp;></p>");
		}

	}
}