<?php

	require_once 'ButterflyTestCase.php';

	class OtherTests extends ButterflyTestCase {
		
		public function testNormalize() {
			$this->assertEquals("foo\nbar\n", $this->butterfly->normalize("fo\ro\r\nbar\r\n"));
		}
		
		public function testHorizontalRuler() {
			$wikitext = <<<WIKI
----
WIKI;

			$expected = <<<HTML
<hr />

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
	}

?>