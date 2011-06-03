var scopeTreeTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Scope_tree_tests() {
			return [
				function Should_generate_nested_scope_tree() {
					var tree = parser.parse("foo __bar ''baz ==lulz=='' --oh hai!--__").scopeTree;
					
					Assert.that(tree.count(), Is.equalTo(5));
					Assert.that(tree.nodes.length, Is.equalTo(1));
					
					var p = tree.nodes[0];
					Assert.that(p.scope, Has.property("type").equalTo("paragraph"));
					Assert.that(p.depth(), Is.equalTo(1));
					Assert.that(p, Has.property("parent").NULL);
					Assert.that(p.count(), Is.equalTo(4));
					Assert.that(p.children, Has.property("length").equalTo(1));
					
					var strong = p.children[0];
					Assert.that(strong.scope, Has.property("type").equalTo("strong"));
					Assert.that(strong.depth(), Is.equalTo(2));
					Assert.that(strong, Has.property("parent").identicalTo(p));
					Assert.that(strong.count(), Is.equalTo(3));
					Assert.that(strong.children, Has.property("length").equalTo(2));
					
					var em = strong.children[0];
					Assert.that(em.scope, Has.property("type").equalTo("emphasis"));
					Assert.that(em.depth(), Is.equalTo(3));
					Assert.that(em, Has.property("parent").identicalTo(strong));
					Assert.that(em.count(), Is.equalTo(1));
					Assert.that(em.children, Has.property("length").equalTo(1));
					
					var ins = strong.children[1];
					Assert.that(ins.scope, Has.property("type").equalTo("underline"));
					Assert.that(ins.depth(), Is.equalTo(3));
					Assert.that(ins, Has.property("parent").identicalTo(strong));
					Assert.that(ins.count(), Is.equalTo(0));
					Assert.that(ins, Has.property("children").empty);
					
					var tt = em.children[0];
					Assert.that(tt.scope, Has.property("type").equalTo("teletype"));
					Assert.that(tt.depth(), Is.equalTo(4));
					Assert.that(tt, Has.property("parent").identicalTo(em));
					Assert.that(tt.count(), Is.equalTo(0));
					Assert.that(tt, Has.property("children").empty);
				}
			];
		}
	};
}();

Jarvis.run(scopeTreeTests);