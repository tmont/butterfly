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
		
		
		
		public function testBlockquote1() {
			$wikitext = <<<WIKI
<<Hello there, this is 
a blockquote that should be terminated 
by a blockquote closer
>>

<<Here is another.>>

<<
here is one that is terminated by virtue 
of eof
WIKI;

			$expected = <<<HTML
<blockquote><div>
Hello there, this is a blockquote that should be terminated by a blockquote closer</div></blockquote>

<blockquote><div>
Here is another.</div></blockquote>

<blockquote><div>
here is one that is terminated by virtue of eof</div></blockquote>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
		
		public function testPreformatted1() {
			$wikitext = <<<WIKI
 this line begins with a space
WIKI;

			$expected = <<<HTML
<pre>this line begins with a space</pre>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testPreformatted2() {
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
		
		
		
		public function testHorizontalRuler() {
			$wikitext = <<<WIKI
----
WIKI;

			$expected = <<<HTML
<hr />

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
		
		public function testEscape1() {
			$wikitext = <<<WIKI
[!escaped __hello__ [hi]]]

how about some __[![inline]] es''capage'']__?
WIKI;

			$expected = <<<HTML
escaped __hello__ [hi]
<p>how about some <strong>[inline] es''capage''</strong>?</p>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
		
		public function testLink1() {
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
		
		public function testLink2() {
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
		
		public function testLink3() {
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
		
		public function testLink4() {
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
		
	}

?>