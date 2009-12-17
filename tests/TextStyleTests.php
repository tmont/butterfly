<?php

	require_once 'ButterflyTestCase.php';

	class TextStyleTests extends ButterflyTestCase {
	
		public function testBold() {
			$wikitext = <<<WIKI
__foo__

_foo and so__me fo__o.
WIKI;

			$expected = <<<HTML
<p><strong>foo</strong></p>

<p>_foo and so<strong>me fo</strong>o.</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testEmphasis() {
			$wikitext = <<<WIKI
''foo''

'foo and so''me fo''o.
WIKI;

			$expected = <<<HTML
<p><em>foo</em></p>

<p>'foo and so<em>me fo</em>o.</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testUnderline() {
			$wikitext = <<<WIKI
--foo--

-foo and so--me fo--o.
WIKI;

			$expected = <<<HTML
<p><ins>foo</ins></p>

<p>-foo and so<ins>me fo</ins>o.</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testStrikeThrough() {
			$wikitext = <<<WIKI
---foo---

-foo and so---me fo---o.
WIKI;

			$expected = <<<HTML
<p><del>foo</del></p>

<p>-foo and so<del>me fo</del>o.</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testSmall() {
			$wikitext = <<<WIKI
(-foo-)

(foo (-sm(-all-)-)
WIKI;

			$expected = <<<HTML
<p><small>foo</small></p>

<p>(foo <small>sm<small>all</small></small></p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testBig() {
			$wikitext = <<<WIKI
(+foo+)

(foo (+bi(+g+)+)
WIKI;

			$expected = <<<HTML
<p><big>foo</big></p>

<p>(foo <big>bi<big>g</big></big></p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
	}

?>