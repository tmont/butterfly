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
			'link'             => array('<tt>',              '</tt>'),
		);
		
		private static $blockScopes = array(
			'paragraph', 'unorderedlist', 'orderedlist', 'listitem', 
			'header', 'table', 'tablecell', 'tablerow', 'tableheader', 
			'preformatted', 'blockquote', 'deflist', 'defterm', 'defdef',
			'preformattedline'
		);
		
		private static $inlineScopes = array(
			'strong', 'emphasis', 'strikethrough', 'underline',
			'small', 'big', 'teletype', 'link'
		);
		
		private static $manuallyClosableScopes = array(
			'blockquote', 'preformatted', 'code'
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
		
		private function out($string, $indent = false, $opening = true) {
			echo ($indent ? str_repeat('  ', max(0, count($this->scopeStack) - (int)$opening)) : '') . $string;
		}
		
		public function toHtml($wikitext, $return = false) {
			$this->init($wikitext);
			
			if ($return) {
				ob_start();
			}
			
			while (($text = $this->read()) !== null) {
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
				case '_': //bold
					if ($this->peek() === '_') {
						$this->read();
						$this->openOrCloseUnnestableScope('strong');
					} else {
						$this->printPlainText($text);
					}
					break;
				case '\'': //emphasis
					if ($this->peek() === '\'') {
						$this->read();
						$this->openOrCloseUnnestableScope('emphasis');
					} else {
						$this->printPlainText($text);
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
						$this->printPlainText($text);
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
						$this->printPlainText($text);
					}
					break;
				case '+': //big closer
					if ($this->peek() === ')' && $this->nextScope['type'] === 'big') {
						$this->read();
						$this->closeScopeOfType('big');
					} else {
						$this->printPlainText($text);
					}
					break;
				case '}': //preformatted block closer
					if ($this->peek(2) === '}}' && $this->isInScopeStack('preformatted')) {
						$this->read(2);
						$this->closeScopeUntil('preformatted');
					} else {
						$this->printPlainText($text);
					}
					break;
				case '>': //blockquote closer
					if ($this->peek() === '>' && $this->isInScopeStack('blockquote')) {
						$this->read();
						$this->closeScopeUntil('blockquote');
					} else {
						$this->printPlainText($text);
					}
					break;
				default:
					$this->printPlainText($text);
					break;
			}
		}
		
		private function handleStartOfLine($text) {
			$continue = true;
			switch ($text) {
				case '!': //header
					while ($this->peek() === '!') {
						$text .= $this->read();
					}
					
					$this->openScope('header', min(6, strlen($text)));
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
						throw new Exception('Cannot have a definition without a definition term');
					}
					
					$this->openScope('defdef');
					break;
				case '<': //blockquote opener
					if ($this->peek() === '<') {
						$this->read();
						$this->openScope('blockquote');
					} else {
						$this->printPlainText($text);
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
					}
					break;
				case '-': //horizontal ruler
					if ($this->peek(4) === "---" || $this->peek(4) === "---\n") {
						$this->read(3);
						$this->out("<hr />\n");
					} else {
						$continue = false;
					}
					break;
				default:
					$continue = false;
					break;
			}
			
			$this->isStartOfLine = false;
			if (!$continue) {
				$this->createParagraph();
			}
			return $continue;
		}
		
		private function createParagraph() {
			if (!$this->isInScopeStack(self::$blockScopes)) {
				$this->openScope('paragraph');
			}
		}
		
		private function printPlainText($text) {
			$this->out($text);
		}
		
		private function openOrCloseUnnestableScope($type) {
			$nextScope = $this->scopePeek();
			if ($nextScope['type'] === $type) {
				$this->closeScope($this->scopePop());
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
			$args = func_get_args();
			$type = array_shift($args);
			$this->scopePush($type, $nestingLevel);
			$opener = preg_replace(array('/\{.*\}/'), $args, self::$scopes[$type][0]);
			$this->out($opener . (self::newlineOnOpen($type) ? "\n" : ''), self::indentOnOpen($type));
		}
		
		private function closeScopes($numNewLines) {
			$nextScope = $this->scopePeek();
			if ($nextScope === null) {
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
				switch ($nextScope['type']) {
					case 'header':
						$this->closeScope($this->scopePop(), $nextScope['nesting_level']);
						break;
					case 'defterm':
					case 'defdef':
					case 'preformattedline':
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
			$this->out($closer . (self::newlineOnClose($scope['type']) ? "\n" : ''), self::indentOnClose($scope['type']), false);
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
		
		private static function indentOnOpen($type) {
			return in_array($type, array('orderedlist', 'unorderedlist', 'listitem', 'deflist', 'blockquote', 'defterm', 'defdef'));
		}
		
		private static function indentOnClose($type) {
			return self::newlineOnOpen($type);
		}
		
		private static function newlineOnOpen($type) {
			return in_array($type, array('orderedlist', 'unorderedlist', 'deflist', 'blockquote'));
		}
		
		private static function newlineOnClose($type) {
			return in_array(
				$type,
				array(
					'orderedlist', 'unorderedlist', 'deflist', 'blockquote',
					'paragraph', 'defterm', 'defdef', 'preformattedline', 
					'preformatted', 'listitem', 'header'
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