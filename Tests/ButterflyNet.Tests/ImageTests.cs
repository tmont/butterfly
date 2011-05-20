using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ImageTests : WikiToHtmlTest {

		[Test]
		public void Should_parse_image() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_external_image() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=http://example.com/foo.png]"), "<p><img src=\"http://example.com/foo.png\" alt=\"http://example.com/foo.png\" title=\"http://example.com/foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_width() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|width=100]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" width=\"100\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_height() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|height=100]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" height=\"100\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_custom_alt_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|alt=oh hai!]"), "<p><img src=\"/media/images/foo.png\" alt=\"oh hai!\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_multiple_options() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|alt=oh hai!|width=100|height=234]"), "<p><img src=\"/media/images/foo.png\" alt=\"oh hai!\" title=\"foo.png\" width=\"100\" height=\"234\" /></p>");
		}

		[Test]
		public void Should_allow_open_bracket_inside_image() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|alt=foo[]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo[\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_allow_escaped_close_bracket_inside_image() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|alt=foo]]]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo]\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_title() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[:image|url=foo.png|title=lulz]"), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"lulz\" /></p>");
		}

		[Test]
		[ExpectedException(typeof(ModuleException), ExpectedMessage = "For images, the URL must be specified")]
		public void Should_require_url() {
			Parser.Parse("[:image]");
		}
	}
}