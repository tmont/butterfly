<?php

	require_once 'ButterflyTestCase.php';

	class HeaderTests extends ButterflyTestCase {
		
		public function testHeadersOneThroughSix() {
			$wikitext = <<<WIKI
!foo1
!!foo2
!!!foo3
!!!!foo4
!!!!!foo5
!!!!!!foo6
!!!!!!!foo7
WIKI;

			$expected = <<<HTML
<h1>foo1</h1>
<h2>foo2</h2>
<h3>foo3</h3>
<h4>foo4</h4>
<h5>foo5</h5>
<h6>foo6</h6>
<h6>foo7</h6>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testHeadersAboveSixShouldDefaultToSix() {
			$wikitext = <<<WIKI
!!!!!!!foo7
WIKI;

			$expected = <<<HTML
<h6>foo7</h6>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testHeaderGetsClosedProperlyOnDoubleLineBreak() {
			$wikitext = <<<WIKI
!Test


WIKI;

			$expected = <<<HTML
<h1>Test</h1>


HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
	}

?>