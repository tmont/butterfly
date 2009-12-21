<?php

	require_once 'ButterflyTestCase.php';

	class DefinitionListTests extends ButterflyTestCase {
		
		public function testDefaultDefinitionList() {
			$wikitext = <<<WIKI
;foo
:bar
;baz
:bat

;boo
:biz
WIKI;

			$expected = <<<HTML
<dl>
<dt>foo</dt>
<dd>bar</dd>
<dt>baz</dt>
<dd>bat</dd>
</dl>

<dl>
<dt>boo</dt>
<dd>biz</dd>
</dl>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testCannotHaveDefinitionWithoutTerm() {
			$wikitext = <<<WIKI
:foo
WIKI;

			$this->setExpectedException('Exception', 'Cannot have a definition without a definition term');
			$this->butterfly->toHtml($wikitext, true);
		}
		
	}

?>