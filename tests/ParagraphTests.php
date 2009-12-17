<?php

	require_once 'ButterflyTestCase.php';

	class ParagraphTests extends ButterflyTestCase {
		
		public function testParagraphBeginsAtStartOfLine() {
			$wikitext = <<<WIKI
foo
WIKI;

			$expected = <<<HTML
<p>foo</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testMultipleParagraphsAndSingleLineBreaksDoNotEndParagraph() {
			$wikitext = <<<WIKI
foo

bar
baz

bat
WIKI;

			$expected = <<<HTML
<p>foo</p>

<p>barbaz</p>

<p>bat</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
	
	}

?>