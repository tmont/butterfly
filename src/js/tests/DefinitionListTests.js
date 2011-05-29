var definitionListTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Definition_list_tests() {
			return [
				function Should_parse_definition_list() {
					Assert.that(trimLf(parser.parseAndReturn(";term\n:definition")), Is.equalTo("<dl><dt>term</dt><dd>definition</dd></dl>"));
				},
				
				function Should_allow_formatting_in_definitions() {
					Assert.that(trimLf(parser.parseAndReturn(";__te''rm''__\n:definition")), Is.equalTo("<dl><dt><strong>te<em>rm</em></strong></dt><dd>definition</dd></dl>"));
				},
				
				function Should_allow_multiple_paragraphs_inside_definition() {
					Assert.that(
						trimLf(parser.parseAndReturn(";term\n:{definition\n\nthat spans\n\nmultiple lines}:")), 
						Is.equalTo("<dl><dt>term</dt><dd><p>definition</p><p>that spans</p><p>multiple lines</p></dd></dl>")
					);
				},
				
				function Should_allow_formatting_in_multiline_definition() {
					Assert.that(trimLf(parser.parseAndReturn(";term\n:{__definition__}:")), Is.equalTo("<dl><dt>term</dt><dd><p><strong>definition</strong></p></dd></dl>"));
				},
				
				function Should_not_create_list_if_inline_scope_is_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("__foo\n;term__")), Is.equalTo("<p><strong>foo;term</strong></p>"));
				}
			];
		}
	};
}();

Jarvis.run(definitionListTests);