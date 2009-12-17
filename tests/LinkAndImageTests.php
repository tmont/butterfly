<?php

	require_once 'ButterflyTestCase.php';

	class LinkAndImageTests extends ButterflyTestCase {
	
		public function testWikiLinksWithNoText() {
			$wikitext = <<<WIKI
[foo]

this is a [wiki] link
WIKI;

			$expected = <<<HTML
<p><a class="wiki" href="/foo">foo</a></p>

<p>this is a <a class="wiki" href="/wiki">wiki</a> link</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testWikiLinksWithText() {
			$wikitext = <<<WIKI
[foo|bar]

this is a [wiki|hello there!] link
WIKI;

			$expected = <<<HTML
<p><a class="wiki" href="/foo">bar</a></p>

<p>this is a <a class="wiki" href="/wiki">hello there!</a> link</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testExternalLinksWithNoText() {
			$wikitext = <<<WIKI
[http://example.com]

this is a [http://example.com] link
WIKI;

			$expected = <<<HTML
<p><a class="external" href="http://example.com">http://example.com</a></p>

<p>this is a <a class="external" href="http://example.com">http://example.com</a> link</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testExternalLinksWithText() {
			$wikitext = <<<WIKI
[http://example.com|foo]

this is a [http://example.com|bar] link
WIKI;

			$expected = <<<HTML
<p><a class="external" href="http://example.com">foo</a></p>

<p>this is a <a class="external" href="http://example.com">bar</a> link</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testLinksWithEmbeddedStyles() {
			$wikitext = <<<WIKI
this is a [http://example.com|__bold__ link]
WIKI;

			$expected = <<<HTML
<p>this is a <a class="external" href="http://example.com"><strong>bold</strong> link</a></p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testImageWithNoProperties() {
			$wikitext = <<<WIKI
[image:http://example.com/]

[image:http://example.com/||]]]
WIKI;

			$expected = <<<HTML
<p><img src="http://example.com/" /></p>

<p><img src="http://example.com/|]" /></p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testImageWithProperties() {
			$wikitext = <<<WIKI
[image:http://example.com/|width=200|height=100|alt=[foo||bar]]]
WIKI;

			$expected = <<<HTML
<p><img src="http://example.com/" width="200px" height="100px" alt="[foo|bar]" /></p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
		
	}

?>