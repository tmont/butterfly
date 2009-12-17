<?php

	require_once 'ButterflyTestCase.php';

	class EscapeTests extends ButterflyTestCase {
	
		public function testInlineEscaping() {
			$wikitext = <<<WIKI
[!escaped __hello__ [hi]]]

how about some __[![inline]] es''capage'']__?
WIKI;

			$expected = <<<HTML
escaped __hello__ [hi]
<p>how about some <strong>[inline] es''capage''</strong>?</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
	}

?>