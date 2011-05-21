using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class HeaderTests : WikiToHtmlTest {

		[Test]
		public void Should_parse_headers_with_depth_one_through_six() {
			const string text = @"! one
!! two
!!! three
!!!! four
!!!!! five
!!!!!! six";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<h1>one</h1><h2>two</h2><h3>three</h3><h4>four</h4><h5>five</h5><h6>six</h6>");
		}
		
		[Test]
		public void Should_treat_depth_greater_than_six_as_six() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("!!!!!!! lol"), "<h6>lol</h6>");
		}

		[Test]
		public void Should_allow_formatting_in_header() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("! lol __bold and ''italic''__"), "<h1>lol <strong>bold and <em>italic</em></strong></h1>");
		}

		[Test]
		public void Should_not_create_header_if_inline_scope_is_not_closed() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__foo\n!header__"), "<p><strong>foo!header</strong></p>");
		}

	}
}