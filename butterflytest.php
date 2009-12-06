<?php

	require_once 'PHPUnit/Framework.php';
	require_once 'butterfly.php';

	class ButterflyTest extends PHPUnit_Framework_TestCase {
		
		private $butterfly;
		
		public function setUp() {
			$this->butterfly = new Butterfly();
		}
		
		public function testNormalize() {
			$this->assertEquals("foo\nbar\n", $this->butterfly->normalize("fo\ro\r\nbar\r\n"));
		}
		
		public function testHeader() {
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
		
		
		
		public function testList1() {
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
		
		public function testList2() {
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
		
		public function testList3() {
			$wikitext = <<<WIKI
**foo
WIKI;

			$this->setExpectedException('Exception', 'New lists cannot be nested');
			$this->butterfly->toHtml($wikitext, true);
		}
		
		public function testList4() {
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
		
		public function testList5() {
			$wikitext = <<<WIKI
*foo
***foo
WIKI;

			$this->setExpectedException('Exception', 'New lists cannot skip levels');
			$this->butterfly->toHtml($wikitext, true);
		}
		
		
		
		public function testParagraph1() {
			$wikitext = <<<WIKI
foo
WIKI;

			$expected = <<<HTML
<p>foo</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testParagraph2() {
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
		
		
		
		public function testDefinitionList1() {
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
		
		public function testDefinitionList2() {
			$wikitext = <<<WIKI
:foo
WIKI;

			$this->setExpectedException('Exception', 'Cannot have a definition without a definition term');
			$this->butterfly->toHtml($wikitext, true);
		}
		
	}

?>