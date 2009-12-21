<?php

	class Butterfly {
		
		private $wikitext;
		private $index;
		private $scopeStack;
		private $isStartOfLine;
		
		private static $scopes = array(
			//name                       opener              closer
			//block level
			'paragraph'        => array('<p>',               '</p>'),
			'unorderedlist'    => array('<ul>',              '</ul>'),
			'orderedlist'      => array('<ol>',              '</ol>'),
			'listitem'         => array('<li>',              '</li>'),
			'deflist'          => array('<dl>',              '</dl>'),
			'defterm'          => array('<dt>',              '</dt>'),
			'defdef'           => array('<dd>',              '</dd>'),
			'header'           => array('<h{level}>',        '</h{level}>'),
			'table'            => array('<table>',           '</table>'),
			'tablecell'        => array('<td>',              '</td>'),
			'tablerowline'     => array('<tr>',              '</tr>'),
			'tablerow'         => array('<tr>',              '</tr>'),
			'tableheader'      => array('<th>',              '</th>'),
			'preformatted'     => array('<pre>',             '</pre>'),
			'preformattedline' => array('<pre>',             '</pre>'),
			'blockquote'       => array('<blockquote><div>', '</div></blockquote>'),
			
			//inline
			'strong'           => array('<strong>',          '</strong>'),
			'emphasis'         => array('<em>',              '</em>'),
			'strikethrough'    => array('<del>',             '</del>'),
			'underline'        => array('<ins>',             '</ins>'),
			'small'            => array('<small>',           '</small>'),
			'big'              => array('<big>',             '</big>'),
			'teletype'         => array('<tt>',              '</tt>'),
			'link'             => array('<a class="{class}" href="{href}">', '</a>'),
			
			'escape'           => array(null,                 null)
		);
		
		private static $blockScopes = array(
			'paragraph', 'unorderedlist', 'orderedlist', 'listitem', 
			'header', 'table', 'tablecell', 'tablerow', 'tableheader', 
			'preformatted', 'blockquote', 'deflist', 'defterm', 'defdef',
			'preformattedline', 'tablerowline'
		);
		
		private static $inlineScopes = array(
			'strong', 'emphasis', 'strikethrough', 'underline',
			'small', 'big', 'teletype', 'link', 'escape'
		);
		
		private static $manuallyClosableScopes = array(
			'blockquote', 'preformatted', 'escape', 'tablerow'
		);
		
		public function __construct() {
			$this->init();
		}
		
		private function init($wikitext = '') {
			$this->scopeStack = array();
			$this->wikitext = $wikitext;
			$this->index = -1;
			$this->isStartOfLine = true;
		}
		
		public function __get($key) {
			if ($key === 'nextScope') {
				return $this->scopePeek();
			}
		}
		
		public function normalize($string) {
			return str_replace("\r", '', $string);
		}
		
		private function scopePush($type, $level = null) {
			$this->scopeStack[] = array('type' => $type, 'nesting_level' => $level);
		}
		
		private function scopePeek() {
			return count($this->scopeStack) ? end($this->scopeStack) : null;
		}
		
		private function scopePop() {
			return count($this->scopeStack) ? array_pop($this->scopeStack) : null;
		}
		
		private function out($string) {
			echo $string;
		}

		public static function escape($string, $charset = 'utf-8') {
			return htmlentities($string, ENT_QUOTES, $charset);
		}
		
		public function toHtml($wikitext, $return = false) {
			$this->init($wikitext);
			
			if ($return) {
				ob_start();
			}
			
			while (($text = $this->read()) !== null) {
				if ($this->isInScopeStack('escape')) {
					if ($text === ']') {
						if ($this->peek() === ']') {
							//to output a literal "]" precede it with another "]"
							$this->read();
						} else {
							$this->closeScopeUntil('escape');
							continue;
						}
					}
					
					$this->out($text);
					continue;
				}
				
				if ($this->isStartOfLine && $this->handleStartOfLine($text)) {
					continue;
				}
				
				$this->handleChar($text);
			}
			
			//close orphaned scopes
			$this->emptyScopeStack();
			
			if ($return) {
				return ob_get_clean();
			}
		}
		
		private function handleChar($text) {
			switch ($text) {
				case "\n":
					$this->handleNewLine();
					break;
				case '[': //open link, open image, open escape
					if ($this->peek() === '!') {
						$this->read();
						$this->openScope('escape');
					} else {
						//this is a link or an image
						$this->handleLinkOrImage();
					}
					break;
				case ']':
					if ($this->isInScopeStack('link')) {
						$this->closeScopeUntil('link');
					} else {
						$this->out($text);
					}
					break;
				case '|':
					if ($this->isInScopeStack('table')) {
						$this->handleTableCell();
					} else {
						$this->out($text);
					}
					break;
				case '_': //bold
					if ($this->peek() === '_') {
						$this->read();
						$this->openOrCloseUnnestableScope('strong');
					} else {
						$this->out($text);
					}
					break;
				case '\'': //emphasis
					if ($this->peek() === '\'') {
						$this->read();
						$this->openOrCloseUnnestableScope('emphasis');
					} else {
						$this->out($text);
					}
					break;
				case '-': //underline, strikethrough, small closer
					if ($this->peek() === '-') {
						$this->read();
						if ($this->peek() === '-') {
							$this->read();
							$this->openOrCloseUnnestableScope('strikethrough');
						} else {
							$this->openOrCloseUnnestableScope('underline');
						}
					} else if ($this->peek() === ')' && $this->nextScope['type'] === 'small') {
						$this->read();
						$this->closeScopeOfType('small');
					} else {
						$this->out($text);
					}
					break;
				case '=': //teletype
					if ($this->peek() === '=') {
						$this->read();
						$this->openOrCloseUnnestableScope('teletype');
					} else {
						$this->out($text);
					}
					break;
				case '(': //small opener, big opener
					if ($this->peek() === '-') {
						$this->read();
						$this->openScope('small');
					} else if ($this->peek() === '+') {
						$this->read();
						$this->openScope('big');
					} else {
						$this->out($text);
					}
					break;
				case '+': //big closer
					if ($this->peek() === ')' && $this->nextScope['type'] === 'big') {
						$this->read();
						$this->closeScopeOfType('big');
					} else {
						$this->out($text);
					}
					break;
				case '}': //preformatted block closer, tablerow closer
					if ($this->peek() === '|' && $this->isInScopeStack('tablerow')) {
						$this->read();
						$this->closeScopeUntil('tablerow');
					} else if ($this->peek(2) === '}}' && $this->isInScopeStack('preformatted')) {
						$this->read(2);
						$this->closeScopeUntil('preformatted');
					} else {
						$this->out($text);
					}
					break;
				case '>': //blockquote closer
					if ($this->peek() === '>' && $this->isInScopeStack('blockquote')) {
						$this->read();
						$this->closeScopeUntil('blockquote');
					} else {
						$this->out($text);
					}
					break;
				default:
					$this->out($text);
					break;
			}
		}
		
		private function handleStartOfLine($text) {
			$continue = true;
			$createParagraph = false;
			switch ($text) {
				case '!': //header
					while ($this->peek() === '!') {
						$text .= $this->read();
					}
					
					$level = min(6, strlen($text));
					$this->openScope('header', $level, $level);
					break;
				case '*': //unordered list
				case '#': //ordered list
					$peek = $this->peek();
					while ($peek === '*' || $peek === '#') {
						$text .= $this->read();
						$peek = $this->peek();
					}
					
					$this->handleListItem($text);
					break;
				case ';': //definition term
					if (!$this->isInScopeStack('deflist')) {
						$this->openScope('deflist');
					}
					
					$this->openScope('defterm');
					break;
				case ':': //definition
					if (!$this->isInScopeStack('deflist')) {
						$this->throwException(new Exception('Cannot have a definition without a definition term'));
					}
					
					$this->openScope('defdef');
					break;
				case '<': //blockquote opener
					if ($this->peek() === '<') {
						$this->read();
						$this->openScope('blockquote');
					} else {
						$this->out($text);
					}
					break;
				case ' ': //preformatted line
					$this->openScope('preformattedline');
					break;
				case '{': //preformatted block
					if ($this->peek(2) === '{{') {
						$this->read(2);
						$this->openScope('preformatted');
					} else {
						$continue = false;
						$createParagraph = true;
					}
					break;
				case '|': //tables!
					$this->handleTableCell();
					break;
				case '-': //horizontal ruler
					if ($this->peek(4) === "---" || $this->peek(4) === "---\n") {
						$this->read(3);
						$this->out("<hr />\n");
					} else {
						$continue = false;
						$createParagraph = true;
					}
					break;
				case '[':
					$continue = false;
					$createParagraph = ($this->peek() !== '!' && $this->peek() !== ':') ? true : false;
					break;
				case "\n":
					$createParagraph = false; //to prevent empty paragraphs
					$continue = false;
					break;
				default:
					$createParagraph = true;
					$continue = false;
					break;
			}
			
			$this->isStartOfLine = false;
			if ($createParagraph) {
				$this->createParagraph();
			}
			return $continue;
		}
		
		private function handleTableCell() {
			if ($this->peek() === '{') {
				$this->read();
				$rowType = 'tablerow';
			} else {
				$rowType = 'tablerowline';
			}
			
			if ($this->peek() === '!') {
				$this->read();
				$cellType = 'tableheader';
			} else {
				$cellType = 'tablecell';
			}
			
			if (!$this->isInScopeStack('table')) {
				//new table
				$this->openScope('table');
				$this->openScope($rowType);
			} else if ($this->isInScopeStack('tablecell', 'tableheader')) {
				if ($this->nextScope['type'] !== 'tablecell' && $this->nextScope['type'] !== 'tableheader') {
					$this->throwException(new Exception('Expected tablecell or tableheader scope, but got "' . $this->nextScope['type'] . '"'));
				}
				
				//close previous tablecell
				$this->closeScope($this->scopePop());
			} else if (!$this->isInScopeStack('tablerow')) {
				//new tablerow
				$this->openScope($rowType);
			}
			
			if ($this->peek() !== "\n" && $this->peek() !== null) {
				$this->openScope($cellType);
			}
		}
		
		private function handleImage() {
			$peek = $this->peek();
			$text = '';
			$data = array();
			while ($peek !== null) {
				if ($peek === ']') {
					//possible closure
					$this->read();
					if ($this->peek() === ']') {
						$text .= $this->read();
					} else {
						$data[] = $text;
						break;
					}
				} else if ($peek === '|') {
					$this->read();
					if ($this->peek() === '|') {
						//a literal vertical bar
						$text .= $this->read();
					} else {
						$data[] = $text;
						$text = '';
					}
				} else {
					$text .= $this->read();
				}
				
				$peek = $this->peek();
			}
			
			$img = '<img src="' . self::escape(array_shift($data)) . '" ';
			foreach ($data as $datum) {
				list($key, $value) = explode('=', $datum);
				$value = self::escape($value);
				switch ($key) {
					case 'alt':
						$img .= 'alt="' . $value . '" ';
						break;
					case 'width':
						$img .= 'width="' . intval($value) . 'px" ';
						break;
					case 'height':
						$img .= 'height="' . intval($value) . 'px" ';
						break;
				}
			}
			$img .= '/>';
			
			$this->out($img);
		}
		
		private function handleLinkOrImage() {
			$data = array();
			$text = '';
			$peek = $this->peek();
			while ($peek !== null && $peek !== '|' && $peek !== ']') {
				$text .= $this->read();
				if ($text === 'image:') {
					$this->handleImage();
					return;
				}
				
				$peek = $this->peek();
			}
			
			$closer = $this->read();
			
			if (strpos($text, '://') !== false) {
				$class = 'external';
				$href = $text;
			} else {
				$href = '/' . $text;
				$class = 'wiki';
			}
			
			$this->openScope('link', null, $class, $href);
		
			if ($closer === ']') {
				//the text is the same as the href, so close the scope
				$this->out(self::escape($text));
				$this->closeScopeUntil('link');
			}
			
			return $data;
		}
		
		private function createParagraph() {
			$unparagraphableScopes = self::$blockScopes;
			foreach (array('blockquote', 'tablerow')  as $type) {
				if (($key = array_search($type, $unparagraphableScopes)) !== false) {
					unset($unparagraphableScopes[$key]);
				}
			}
			
			if (!$this->isInScopeStack($unparagraphableScopes)) {
				$this->openScope('paragraph');
			}
		}
		
		private function openOrCloseUnnestableScope($type) {
			$nextScope = $this->scopePeek();
			if ($nextScope['type'] === $type) {
				$this->closeScope($this->scopePop());
			} else if ($this->isInScopeStack($type)) {
				$this->throwException(new Exception('Scope mismatch, must close ' . $this->nextScope['type'] . ' first'));
			} else {
				$this->openScope($type);
			}
		}
		
		private function closeScopeOfType($type) {
			$nextScope = $this->scopePeek();
			if ($nextScope['type'] !== $type) {
				$this->throwException(new Exception('Expected scope of type "' . $type . '", got "' . $nextScope['type'] . '"'));
			}
			
			$this->closeScope($this->scopePop());
		}
		
		private function handleNewLine() {
			$text = "\n";
			while ($this->peek() === "\n") {
				$text .= $this->read();
			}
			
			$this->closeScopes(strlen($text));
			if (strlen($text) > 1 && $this->nextScope['type'] !== 'preformatted') {
				$this->out("\n");
			}
			$this->isStartOfLine = true;
		}

		private function handleListItem($text) {
			$listType = (substr($text, -1) === '*') ? 'unorderedlist' : 'orderedlist';
			if (!$this->isInScopeStack('orderedlist', 'unorderedlist')) {
				//new list
				if (strlen($text) > 1) {
					$this->throwException(new Exception('New lists cannot be nested'));
				//@codeCoverageIgnoreStart
				}
				//@codeCoverageIgnoreEnd
				
				$this->openScope($listType);
			} else {
				//if a list is already started, and the nesting level is the same, then we need to close a listitem scope
				$nestedLists = 0;
				foreach ($this->scopeStack as $scope) {
					if ($scope['type'] === 'orderedlist' || $scope['type'] === 'unorderedlist') {
						$nestedLists++;
					}
				}
				
				$this->closeScopeUntil('listitem');
				$difference = strlen($text) - $nestedLists;
				if ($difference < 0) {
					//close previous list(s)
					for (; $difference; $difference++) {
						$this->closeScopeUntil('orderedlist', 'unorderedlist');
					}
				} else if ($difference === 1) {
					//start new list
					$this->openScope($listType);
				} else if ($difference > 1) {
					$this->throwException(new Exception('New lists cannot skip levels'));
				//@codeCoverageIgnoreStart
				}
				//@codeCoverageIgnoreEnd
			}
			
			//open list item
			$this->openScope('listitem');
		}
		
		private function openScope($type, $nestingLevel = null) {
			$this->verifyScope($type);
			
			$args = func_get_args();
			$type = array_shift($args);
			$this->scopePush($type, array_shift($args));
			
			$opener = self::$scopes[$type][0];
			if (count($args) > 0) {
				foreach ($args as $arg) {
					$opener = preg_replace('/\{.*?\}/', $arg, $opener, 1);
				}
			}
			
			$this->out($opener . (self::newlineOnOpen($type) ? "\n" : ''));
		}
		
		private function closeScopes($numNewLines) {
			if ($this->nextScope === null) {
				return;
			}
			
			if ($numNewLines > 1) {
				//close all scopes that are not manually closable
				while ($this->nextScope !== null && !self::isManuallyClosable($this->nextScope['type'])) {
					$this->closeScope($this->scopePop());
				}
				
				if ($this->nextScope !== null && $this->nextScope['type'] === 'preformatted') {
					$this->out(str_repeat("\n", $numNewLines));
				}
			} else if ($numNewLines === 1) {
				switch ($this->nextScope['type']) {
					case 'header':
						$scope = $this->scopePop();
						$this->closeScope($scope, $scope['nesting_level']);
						break;
					case 'defterm':
					case 'defdef':
					case 'preformattedline':
					case 'tablerowline':
						$this->closeScope($this->scopePop());
						break;
					case 'preformatted':
						$this->out("\n");
						break;
				}
			}
		}
		
		private function emptyScopeStack() {
			while ($this->nextScope !== null) {
				$scope = $this->scopePop();
				$this->closeScope($scope, $scope['nesting_level']);
			}
		}
		
		private function closeScope(array $scope) {
			$args = func_get_args();
			$scope = array_shift($args);
			$closer = preg_replace(array('/\{.*\}/'), $args, self::$scopes[$scope['type']][1]);
			$this->out($closer . (self::newlineOnClose($scope['type']) ? "\n" : ''));
		}
		
		private function closeScopeUntil($scopeType) {
			$terminatingScopes = func_get_args();
			$nextScope = $this->scopePeek();
			if ($nextScope !== null && !in_array($nextScope['type'], $terminatingScopes)) {
				$this->closeScope($this->scopePop(), false);
			}
			
			//the terminating scope
			if ($nextScope !== null) {
				$this->closeScope($this->scopePop(), false);
			}
		}

		private function peek($num = 1) {
			$index = $this->index;
			$text = '';
			while ($num) {
				if (!isset($this->wikitext[$index + 1])) {
					if (empty($text)) {
						$text = null;
					}
					
					break;
				}
				
				$text .= $this->wikitext[++$index];
				$num--;
			}
			
			return $text;
		}
		
		private function read($num = 1) {
			$text = '';
			while ($num) {
				if (!isset($this->wikitext[$this->index + 1])) {
					if (empty($text)) {
						$text = null;
					}
					
					break;
				}
				
				$text .= $this->wikitext[++$this->index];
				$num--;
			}
			
			return $text;
		}
		
		private function isInScopeStack($type) {
			$types = is_array($type) ? $type : func_get_args();
			foreach ($this->scopeStack as $scope) {
				if (in_array($scope['type'], $types)) {
					return true;
				}
			}
			
			return false;
		}
		
		private function verifyScope($newScopeType) {
			if (in_array($newScopeType, self::$blockScopes) && $this->isInScopeStack(self::$inlineScopes)) {
				$this->throwException(new Exception('Cannot nest a block level scope inside an inline level scope'));
			}
		}
		
		private static function newlineOnOpen($type) {
			return in_array($type, array(
				'orderedlist', 'unorderedlist', 'deflist', 'blockquote', 'table',
				'tablerowline', 'tablerow'
			));
		}
		
		private static function newlineOnClose($type) {
			return in_array(
				$type,
				array(
					'orderedlist', 'unorderedlist', 'deflist', 'blockquote',
					'paragraph', 'defterm', 'defdef', 'preformattedline', 
					'preformatted', 'listitem', 'header', 'table', 'tablerowline',
					'tablerow', 'tablecell', 'tableheader'
				)
			);
		}
		
		private static function isManuallyClosable($type) {
			return in_array($type, self::$manuallyClosableScopes);
		}
		
		private function throwException(Exception $e) {
			ob_end_clean();
			throw $e;
		}
		
	}

?>