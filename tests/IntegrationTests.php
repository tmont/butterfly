<?php

	require_once 'ButterflyTestCase.php';

	class IntegrationTests extends ButterflyTestCase {
	
	
		private function getDataDir() {
			return dirname(__FILE__) . DIRECTORY_SEPARATOR . 'data' . DIRECTORY_SEPARATOR;
		}
		
		private function loadData($prefix, &$wikitext, &$expected) {
			$this->assertFileExists($this->getDataDir() . $prefix . '.wiki');
			$this->assertFileExists($this->getDataDir() . $prefix . '.html');
			
			$wikitext = file_get_contents($this->getDataDir() . $prefix . '.wiki');
			$expected = file_get_contents($this->getDataDir() . $prefix . '.html');
		}
		
		public function testIntegration() {
			$this->loadData('integration', $wikitext, $expected);
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testNestedInlineScopes() {
			$wikitext = <<<WIKI
Lorem __ip''s---u--m d(-o(-l(+or s(-i(+t am==et, c==o+)-)n+)s-)e-)c--t---e''t__ur adipisicing elit, sed do 
eiusmod tempor incididunt ut labore et dolore magna aliqua.
WIKI;
			
			$expected = <<<HTML
<p>Lorem <strong>ip<em>s<del>u<ins>m d<small>o<small>l<big>or s<small>i<big>t am<tt>et, c</tt>o</big></small>n</big>s</small>e</small>c</ins>t</del>e</em>t</strong>ur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>

HTML;

			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testNestedScopesInBlockquote() {
			$this->loadData('blockquote', $wikitext, $expected);
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testNestedScopesInPreformatted() {
			$wikitext = <<<WIKI
{{{Hello
__world
and such


hello__
world}}}
WIKI;
			
			$expected = <<<HTML
<pre>Hello
<strong>world
and such


hello</strong>
world</pre>

HTML;

			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
			
		}
		
	}

?>