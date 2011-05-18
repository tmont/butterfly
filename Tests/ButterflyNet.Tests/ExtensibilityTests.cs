using System;
using System.IO;
using ButterflyNet.Parser.Satisfiers;
using ButterflyNet.Parser.Scopes;
using ButterflyNet.Parser.Strategies;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ExtensibilityTests {
		[Test]
		public void Should_parse_with_custom_strategy() {
			var parser = new ButterflyParser()
				.LoadDefaultStrategies(new DefaultParseStrategyFactory())
				.AddStrategy<OpenBlinkStrategy>()
				.AddStrategy<CloseBlinkStrategy>();

			parser.Analyzer = new BlinkAnalyzer(new StringWriter());

			Assert.That(parser.ParseAndReturn("(((lulz)))").Replace("\n", ""), Is.EqualTo("<p><blink>lulz</blink></p>"));
		}

		[Test]
		public void Can_replace_strategy() {
			var parser = new ButterflyParser()
				.LoadDefaultStrategies(new DefaultParseStrategyFactory())
				.RemoveStrategy<OpenStrongStrategy>()
				.RemoveStrategy<CloseStrongStrategy>()
				.AddStrategy<CustomOpenStrongStrategy>()
				.AddStrategy<CustomCloseStrongStrategy>();

			Assert.That(parser.ParseAndReturn("??__lulz__??").Replace("\n", ""), Is.EqualTo("<p><strong>__lulz__</strong></p>"));
		}

		#region Custom strong strategy
		public abstract class CustomStrongStrategy : InlineStrategy {
			protected CustomStrongStrategy() {
				AddSatisfier(new ExactCharMatchSatisfier("??"));
			}

			protected override sealed Type Type { get { return ScopeTypeCache.Strong; } }
		}

		public class CustomOpenStrongStrategy : CustomStrongStrategy {
			public CustomOpenStrongStrategy() {
				AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
			}

			protected override void DoExecute(ParseContext context) {
				OpenScope(new StrongScope(), context);
			}
		}

		public class CustomCloseStrongStrategy : CustomStrongStrategy {
			public CustomCloseStrongStrategy() {
				AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
			}

			protected override void DoExecute(ParseContext context) {
				CloseCurrentScope(context);
			}
		}
		#endregion

		#region Blink strategy ftw
		public abstract class BlinkStrategy : InlineStrategy {
			protected override sealed Type Type { get { return typeof(BlinkScope); } }
		}

		public class BlinkScope : InlineScope {
			public override void Open(ButterflyAnalyzer analyzer) {
				var blinkAnalyzer = analyzer as BlinkAnalyzer;
				if (blinkAnalyzer == null) {
					return;
				}

				blinkAnalyzer.OpenBlink();
			}

			public override void Close(ButterflyAnalyzer analyzer) {
				var blinkAnalyzer = analyzer as BlinkAnalyzer;
				if (blinkAnalyzer == null) {
					return;
				}

				blinkAnalyzer.CloseBlink();
			}
		}

		public class OpenBlinkStrategy : BlinkStrategy {
			public OpenBlinkStrategy() {
				AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
				AddSatisfier(new ExactCharMatchSatisfier("((("));

			}
			protected override void DoExecute(ParseContext context) {
				OpenScope(new BlinkScope(), context);
			}
		}

		public class CloseBlinkStrategy : BlinkStrategy {
			public CloseBlinkStrategy() {
				AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
				AddSatisfier(new ExactCharMatchSatisfier(")))"));
			}

			protected override void DoExecute(ParseContext context) {
				CloseCurrentScope(context);
			}
		}

		public class BlinkAnalyzer : HtmlAnalyzer {
			public BlinkAnalyzer(TextWriter writer) : base(writer) { }

			public void OpenBlink() {
				Writer.Write("<blink>");
			}

			public void CloseBlink() {
				Writer.Write("</blink>");
			}
		}
		#endregion
	}
}