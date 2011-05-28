function Framework_tests() {
	return [
		function Stack_tests() {
			return [
				function Should_be_indexable_like_an_array() {
					var stack = new Stack();
					stack[0] = "foo";
					
					Assert.that(stack, Has.property(0).equalTo("foo"));
				},
				
				function Should_return_the_most_recently_pushed_element_without_removing_it() {
					var stack = new Stack();
					stack.push("foo");
					
					Assert.that(stack.peek(), Is.equalTo("foo"));
					Assert.that(stack, Has.property(0).equalTo("foo"));
				},
				
				function Should_return_undefined_if_peeking_at_an_empty_stack() {
					var stack = new Stack();
					
					Assert.that(stack.isEmpty(), Is.identicalTo(true));
					Assert.that(stack.peek(), Is.undefined);
				},
				
				function Stack_should_contain_type() {
					var stack = new Stack();
					stack.push({ type: "foo" });
					stack.push({ type: "bar" });
					
					Assert.that(stack.containsType("foo"), Is.equalTo(true));
					Assert.that(stack.containsType("bar"), Is.equalTo(true));
					Assert.that(stack.containsType("lol", "bar"), Is.equalTo(true));
					Assert.that(stack.containsType("lol"), Is.equalTo(false));
				}
			];
		},
		
		function AppendStringWriter_tests() {
			return [
				function Flush_should_reset_internal_buffer() {
					var writer = new AppendStringWriter();
					writer.write("foo bar");
					
					Assert.that(writer.toString(), Is.equalTo("foo bar"));
					
					writer.flush();
					
					Assert.that(writer.toString(), Is.empty);
				},
				
				function Writing_a_number_should_convert_it_to_string() {
					var writer = new AppendStringWriter();
					writer.write(5);
					writer.write(6);
					
					Assert.that(writer.toString(), Is.identicalTo("56"));
				}
			];
		},
		
		function Event_tests() {
			return [
				function Should_stack_callbacks_and_call_them_in_order() {
					var tracker = { f: false, g: false, h: false };
					
					function f() {
						Assert.that(tracker, Has.property("g").equalTo(false));
						Assert.that(tracker, Has.property("h").equalTo(false));
						tracker.f = true;
					}
					
					function g() {
						Assert.that(tracker, Has.property("f").equalTo(true));
						Assert.that(tracker, Has.property("h").equalTo(false));
						tracker.g = true;
					}
					
					function h() {
						Assert.that(tracker, Has.property("f").equalTo(true));
						Assert.that(tracker, Has.property("g").equalTo(true));
						tracker.h = true;
					}
					
					var event = new Event();
					event.attach(f);
					event.attach(g);
					event.attach(h);
					
					event.fire();
					
					Assert.that(tracker, Has.property("f").equalTo(true));
					Assert.that(tracker, Has.property("g").equalTo(true));
					Assert.that(tracker, Has.property("h").equalTo(true));
				},
				
				function Should_pass_all_arguments_to_callback() {
					var tracker = false;
					function f(arg1, arg2) {
						Assert.that(arg1, Is.equalTo("foo"));
						Assert.that(arg2, Is.equalTo("bar"));
						tracker = true;
					}
					
					var event = new Event();
					event.attach(f);
					event.fire("foo", "bar");
					
					Assert.that(tracker, Is.equalTo(true));
				},
				
				function Callbacks_should_be_called_in_the_context_of_the_event_instance() {
					var tracker = false;
					var event = new Event();
					
					function f() {
						Assert.that(this, Is.identicalTo(event));
						tracker = true;
					}
					
					event.attach(f);
					event.fire();
					
					Assert.that(tracker, Is.equalTo(true));
				}
			];
		}
	];
};

Jarvis.run(Framework_tests);