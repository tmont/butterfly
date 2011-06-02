var macroTests = function() {
	var parser;
	
	function FooMacro() {
		FooMacro.$parent.call(this);
		
		this.getValue = function() {
			return "bar";
		};
	}
	
	function BarMacro() {
		BarMacro.$parent.call(this);
		
		this.getValue = function() {
			return "__bold__";
		};
	}
	
	extend(Macro, FooMacro);
	extend(Macro, BarMacro);
	
	return {
		setup: function() {
			parser = new Butterfly.Parser();
			parser.loadDefaultStrategies();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Macro_tests() {
			return [
				function Should_update_input_with_macro_value() {
					parser.macroFactory = new DefaultMacroFactory({ foo: FooMacro });
					
					var result = parser.parse("[::foo] foo [::foo]");
					var text = parser.flushAndReturn();
					
					Assert.that(result.markup, Is.equalTo("bar foo bar"));
					Assert.that(trimLf(text), Is.equalTo("<p>bar foo bar</p>"));
				},
				
				function Should_parse_markup_in_macros() {
					parser.macroFactory = new DefaultMacroFactory({ bar: BarMacro });
					
					var result = parser.parse("[::bar] lol [::bar]");
					var text = parser.flushAndReturn();
					
					Assert.that(result.markup, Is.equalTo("__bold__ lol __bold__"));
					Assert.that(trimLf(text), Is.equalTo("<p><strong>bold</strong> lol <strong>bold</strong></p>"));
				},
				
				function Should_show_current_timestamp() {
					Assert.that(trimLf(parser.parseAndReturn("[::timestamp]")), Is.regexMatch(/^<p>\d{4}-\d\d-\d\d \d\d:\d\d:\d\dZ<\/p>$/));
				},
				
				function Should_show_local_timestamp() {
					Assert.that(trimLf(parser.parseAndReturn("[::timestamp|format=local]")), Is.regexMatch(/^<p>\d{4}-\d\d-\d\d \d\d:\d\d:\d\d<\/p>$/));
				}
				
				
			];
		}
	};
}();

Jarvis.run(macroTests);