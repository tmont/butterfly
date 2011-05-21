using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public abstract class WikiToHtmlTest {
		protected ButterflyParser Parser { get; private set; }

		protected static void AssertWithNoRegardForLineBreaks(string actual, string text) {
			Assert.That(actual.Replace("\n", ""), Is.EqualTo(text));
		}

		[SetUp]
		public virtual void SetUp() {
			Parser = new ButterflyParser().LoadDefaultStrategies();
		}
	}
}
