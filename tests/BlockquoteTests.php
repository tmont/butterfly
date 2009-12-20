<?php

	require_once 'ButterflyTestCase.php';

	class BlockquoteTests extends ButterflyTestCase {
	
		public function testBlockquoteScoping() {
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
Hello there, this is <p>a blockquote that should be terminated by a blockquote closer</p>
</div></blockquote>

<blockquote><div>
Here is another.</div></blockquote>

<blockquote><div>
<p>here is one that is terminated by virtue of eof</p>
</div></blockquote>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
	
	}

?>