using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class StringExtensionTests {
		

		[Test]
		public void Http_urls_as_external_urls() {
			Assert.That("http://example.com/".IsExternalUrl(), Is.True);
		}

		[Test]
		public void Https_urls_as_external_urls() {
			Assert.That("https://example.com/".IsExternalUrl(), Is.True);
		}

		[Test]
		public void Fvn_urls_as_external_urls() {
			Assert.That("svn://example.com/".IsExternalUrl(), Is.True);
		}

		[Test]
		public void Ftp_urls_as_external_urls() {
			Assert.That("ftp://example.com/".IsExternalUrl(), Is.True);
		}

		[Test]
		public void Mailto_urls_are_not_external() {
			Assert.That("mailto:foo@bar.com".IsExternalUrl(), Is.False);
		}

		[Test]
		public void Data_urls_are_not_external() {
			Assert.That("data:image/png;base64,asdfasdf=".IsExternalUrl(), Is.False);
		}

		[Test]
		public void Should_not_recognize_external_url_within_local_url_as_external() {
			Assert.That("/foo/http://example.com".IsExternalUrl(), Is.False);
		}
	}
}