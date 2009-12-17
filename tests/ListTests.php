<?php

	require_once 'ButterflyTestCase.php';

	class ListTests extends ButterflyTestCase {
		
		public function testUnorderedListWithNestedUnorderedList() {
			$wikitext = <<<WIKI
*foo
**bar
WIKI;

			$expected = <<<HTML
<ul>
  <li>foo</li>
  <ul>
    <li>bar</li>
  </ul>
</ul>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testOrderedListWithNestedUnorderedList() {
			$wikitext = <<<WIKI
#foo
#*bar
WIKI;

			$expected = <<<HTML
<ol>
  <li>foo</li>
  <ul>
    <li>bar</li>
  </ul>
</ol>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testCannotStartListNested() {
			$wikitext = <<<WIKI
**foo
WIKI;

			$this->setExpectedException('Exception', 'New lists cannot be nested');
			$this->butterfly->toHtml($wikitext, true);
		}
		
		public function testComplexUnorderedList() {
			$wikitext = <<<WIKI
*foo
*bar
**baz
***bat
**bart
***boo
*foobar
*faz
WIKI;

			$expected = <<<HTML
<ul>
  <li>foo</li>
  <li>bar</li>
  <ul>
    <li>baz</li>
    <ul>
      <li>bat</li>
    </ul>
    <li>bart</li>
    <ul>
      <li>boo</li>
    </ul>
  </ul>
  <li>foobar</li>
  <li>faz</li>
</ul>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testCannotSkipLevels() {
			$wikitext = <<<WIKI
*foo
***foo
WIKI;

			$this->setExpectedException('Exception', 'New lists cannot skip levels');
			$this->butterfly->toHtml($wikitext, true);
		}
		
	}

?>