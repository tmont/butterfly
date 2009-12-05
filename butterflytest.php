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
		
		public function testList() {
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
		
	}

?>