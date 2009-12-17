<?php

	require_once 'ButterflyTestCase.php';

	class PreformattedTests extends ButterflyTestCase {
	
		public function testPreformattedStartsIfFirstCharIsSpace() {
			$wikitext = <<<WIKI
 this line begins with a space
WIKI;

			$expected = <<<HTML
<pre>this line begins with a space</pre>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testPreformattedScoping() {
			$wikitext = <<<WIKI
{{{this is some preformatted


text}}}

{{{hi there, this should be
automatically closed
WIKI;

			$expected = <<<HTML
<pre>this is some preformatted


text</pre>

<pre>hi there, this should be
automatically closed</pre>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
	}

?>