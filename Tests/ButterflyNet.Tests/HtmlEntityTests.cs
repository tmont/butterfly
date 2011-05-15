using ButterflyNet.Parser.Modules;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class HtmlEntityTests : WikiToHtmlTest {

		[SetUp]
		public override void SetUp() {
			base.SetUp();
			Parser.ModuleFactory = new ActivatorFactory<IButterflyModule>(new NamedTypeRegistry<IButterflyModule>().RegisterType<HtmlEntityModule>("entity"));
		}

		[Test]
		public void Should_display_named_html_entity() {
			Assert.That(Parser.ParseAndReturn("[:entity|value=hellip]"), Is.EqualTo("<p>&hellip;</p>"));
		}

		[Test]
		public void Should_display_numbered_html_entity() {
			Assert.That(Parser.ParseAndReturn("[:entity|value=#8567]"), Is.EqualTo("<p>&#8567;</p>"));
		}

		[Test]
		[ExpectedException(typeof(ModuleException), ExpectedMessage = "The \"value\" property must be set to a valid HTML entity")]
		public void Should_require_value_property() {
			Parser.Parse("[:entity]");
		}

		[Test]
		[ExpectedException(typeof(ModuleException), ExpectedMessage = "The \"value\" property must be set to a valid HTML entity")]
		public void Should_not_allow_empty_entity_value() {
			Parser.Parse("[:entity|value=]");
		}

		[Test]
		[ExpectedException(typeof(ModuleException), ExpectedMessage = "\"foo!\" is not a valid HTML entity")]
		public void Should_validate_entity_value() {
			Parser.Parse("[:entity|value=foo!]");
		}
	}
}