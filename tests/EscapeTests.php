<?php

	require_once 'ButterflyTestCase.php';

	class EscapeTests extends ButterflyTestCase {
	
		public function testInlineEscaping() {
			$wikitext = <<<WIKI
[!escaped __hello__ [hi]]]

how about some __[![inline]] es''capage'']__?
WIKI;

			$expected = <<<HTML
<p>escaped __hello__ [hi]</p>

<p>how about some <strong>[inline] es&#039;&#039;capage&#039;&#039;</strong>?</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
	}

?>