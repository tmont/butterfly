<?php

	require_once 'ButterflyTestCase.php';

	class TableTests extends ButterflyTestCase {
	
		public function testTableHeadersAndNoScoping() {
			$wikitext = <<<WIKI
|! foo |! bar |
| baz | bat |
WIKI;

			$expected = <<<HTML
<table>
  <tr>
    <th> foo </th>
    <th> bar </th>
  </tr>
  <tr>
    <td> baz </td>
    <td> bat </td>
  </tr>
</table>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		public function testRowScoping() {
			$wikitext = <<<WIKI
|{! foo | bar
| bat }|
| baz | bat |
WIKI;

			$expected = <<<HTML
<table>
  <tr>
    <th> foo </th>
    <td> bar</td>
    <td> bat </td>
  </tr>
  <tr>
    <td> baz </td>
    <td> bat </td>
  </tr>
</table>

HTML;
			
			$this->assertEquals($expected, $this->butterfly->toHtml($wikitext, true));
		}
		
		
		
	}

?>