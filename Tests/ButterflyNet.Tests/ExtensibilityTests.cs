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
				.AddStrategy<CloseBlinkStrategy>()
				.AddAnalyzer(new BlinkAnalyzer(new StringWriter()));

			Assert.That(parser.ParseAndReturn("(((lulz)))"), Is.EqualTo("<p><blink>lulz</blink></p>"));
		}

		[Test]
		public void Can_replace_strategy() {
			var parser = new ButterflyParser()
				.LoadDefaultStrategies(new DefaultParseStrategyFactory())
				.RemoveStrategy<OpenStrongStrategy>()
				.RemoveStrategy<CloseStrongStrategy>()
				.AddStrategy<CustomOpenStrongStrategy>()
				.AddStrategy<CustomCloseStrongStrategy>()
				.AddAnalyzer(new HtmlAnalyzer(new StringWriter()));

			Assert.That(parser.ParseAndReturn("??__lulz__??"), Is.EqualTo("<p><strong>__lulz__</strong></p>"));
		}

		#region Custom strong strategy
		public abstract class CustomStrongStrategy : InlineStrategy, ITokenProvider {
			public string Token { get { return "??"; } }
			protected override sealed Type Type { get { return ScopeTypeCache.Strong; } }
		}

		public class CustomOpenStrongStrategy : CustomStrongStrategy {
			public CustomOpenStrongStrategy() {
				AddSatisfier(new OpenNonNestableInlineScopeSatisfier(Type));
			}

			protected override void Execute(ParseContext context) {
				OpenScope(new StrongScope(), context);
			}
		}

		public class CustomCloseStrongStrategy : CustomStrongStrategy {
			public CustomCloseStrongStrategy() {
				AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
			}

			protected override void Execute(ParseContext context) {
				CloseCurrentScope(context);
			}
		}
		#endregion

		#region Blink strategy ftw
		public abstract class BlinkStrategy : InlineStrategy, ITokenProvider {
			protected override sealed Type Type { get { return typeof(BlinkScope); } }
			public abstract string Token { get; }
		}

		public class BlinkScope : InlineScope {
			protected override void OpenAndAnalyze(ButterflyAnalyzer analyzer) {
				var blinkAnalyzer = analyzer as BlinkAnalyzer;
				if (blinkAnalyzer == null) {
					return;
				}

				blinkAnalyzer.OpenBlink();
			}

			protected override void CloseAndAnalyze(ButterflyAnalyzer analyzer) {
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

			}
			protected override void Execute(ParseContext context) {
				OpenScope(new BlinkScope(), context);
			}

			public override string Token { get { return "((("; } }
		}

		public class CloseBlinkStrategy : BlinkStrategy {
			public CloseBlinkStrategy() {
				AddSatisfier(new CurrentScopeMustMatchSatisfier(Type));
			}

			protected override void Execute(ParseContext context) {
				CloseCurrentScope(context);
			}

			public override string Token { get { return ")))"; } }
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