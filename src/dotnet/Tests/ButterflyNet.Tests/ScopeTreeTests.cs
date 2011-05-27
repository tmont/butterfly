using System.Linq;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ScopeTreeTests : WikiToHtmlTest {

		[Test]
		public void Should_generate_nested_scope_tree() {
			var parser = new ButterflyParser { ModuleFactory = new ActivatorFactory<IButterflyModule>(new NamedTypeRegistry<IButterflyModule>()) }
				.LoadDefaultStrategies(new DefaultParseStrategyFactory());

			var tree = parser.Parse("foo __bar ''baz ==lulz=='' --oh hai!--__").ScopeTree;

			/*
			 * <p>
			 *   <strong>
			 *	   <em>
			 *	     <tt>
			 *	   <ins>
			*/

			Assert.That(tree.Count, Is.EqualTo(5));
			Assert.That(tree.Nodes.Count(), Is.EqualTo(1));

			var p = tree.Nodes.First();
			Assert.That(p.Scope.GetType(), Is.EqualTo(ScopeTypeCache.Paragraph));
			Assert.That(p.Depth, Is.EqualTo(1));
			Assert.That(p.Parent, Is.Null);
			Assert.That(p.Count, Is.EqualTo(4));
			Assert.That(p.Children.Count(), Is.EqualTo(1));

			var strong = p.Children.First();
			Assert.That(strong.Scope.GetType(), Is.EqualTo(ScopeTypeCache.Strong));
			Assert.That(strong.Parent, Is.EqualTo(p));
			Assert.That(strong.Depth, Is.EqualTo(2));
			Assert.That(strong.Count, Is.EqualTo(3));
			Assert.That(strong.Children.Count(), Is.EqualTo(2));

			var em = strong.Children.First();
			Assert.That(em.Scope.GetType(), Is.EqualTo(ScopeTypeCache.Emphasis));
			Assert.That(em.Parent, Is.EqualTo(strong));
			Assert.That(em.Depth, Is.EqualTo(3));
			Assert.That(em.Count, Is.EqualTo(1));
			Assert.That(em.Children.Count(), Is.EqualTo(1));

			var ins = strong.Children.Last();
			Assert.That(ins.Scope.GetType(), Is.EqualTo(ScopeTypeCache.Underline));
			Assert.That(ins.Parent, Is.EqualTo(strong));
			Assert.That(ins.Depth, Is.EqualTo(3));
			Assert.That(ins.Count, Is.EqualTo(0));
			Assert.That(ins.Children.Count(), Is.EqualTo(0));

			var tt = em.Children.First();
			Assert.That(tt.Scope.GetType(), Is.EqualTo(ScopeTypeCache.Teletype));
			Assert.That(tt.Parent, Is.EqualTo(em));
			Assert.That(tt.Depth, Is.EqualTo(4));
			Assert.That(tt.Count, Is.EqualTo(0));
			Assert.That(tt.Children.Count(), Is.EqualTo(0));
		}

	}
}