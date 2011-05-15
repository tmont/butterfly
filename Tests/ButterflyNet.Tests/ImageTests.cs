using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ImageTests : WikiToHtmlTest {

		[Test]
		public void Should_parse_image() {
			Convert("[:image|url=foo.png]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_external_image() {
			Convert("[:image|url=http://example.com/foo.png]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"http://example.com/foo.png\" alt=\"http://example.com/foo.png\" title=\"http://example.com/foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_width() {
			Convert("[:image|url=foo.png|width=100]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" width=\"100\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_height() {
			Convert("[:image|url=foo.png|height=100]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" height=\"100\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_custom_alt_text() {
			Convert("[:image|url=foo.png|alt=oh hai!]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"oh hai!\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_multiple_options() {
			Convert("[:image|url=foo.png|alt=oh hai!|width=100|height=234]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"oh hai!\" title=\"foo.png\" width=\"100\" height=\"234\" /></p>");
		}

		[Test]
		public void Should_allow_open_bracket_inside_image() {
			Convert("[:image|url=foo.png|alt=foo[]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo[\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_allow_escaped_close_bracket_inside_image() {
			Convert("[:image|url=foo.png|alt=foo]]]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo]\" title=\"foo.png\" /></p>");
		}

		[Test]
		public void Should_parse_image_with_title() {
			Convert("[:image|url=foo.png|title=lulz]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><img src=\"/media/images/foo.png\" alt=\"foo.png\" title=\"lulz\" /></p>");
		}
	}
}