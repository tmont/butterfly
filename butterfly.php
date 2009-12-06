<?php

	class Butterfly {
		
		private $wikitext;
		private $index;
		private $scopeStack;
		
		private static $scopes = array(
			//name                    opener             closer               containers
			//block level
			'paragraph'      => array('<p>',             '</p>',              array('listitem', 'tablecell', 'tableheader', 'blockquote')),
			'unorderedlist'  => array('<ul>',            '</ul>',             array('paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem')),
			'orderedlist'    => array('<ol>',            '</ol>',             array('paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem')),
			'listitem'       => array('<li>',            '</li>',             array('unorderedlist', 'orderedlist')),
			'deflist'        => array('<dl>',            '</dl>',             array('paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem')),
			'defterm'        => array('<dt>',            '</dt>',             array('paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem')),
			'defdef'         => array('<dd>',            '</dd>',             array('paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem')),
			'header'         => array('<h{level}>',      '</h{level}>',       array()),
			'table'          => array('<table>',         '</table>',          array('paragraph', 'blockquote', 'listitem')),
			'tablecell'      => array('<td>',            '</td>',             array('tablerow')),
			'tablerow'       => array('<tr>',            '</tr>',             array('table')),
			'tableheader'    => array('<th>',            '</th>',             array('tablerow')),
			'preformatted'   => array('<pre>',           '</pre>',            array('paragraph', 'blockquote', 'listitem', 'tablecell', 'tableheader')),
			'blockquote'     => array('<blockquote><div>', '</div></blockquote>', array('blockquote', 'paragraph', 'listitem', 'tablecell', 'tableheader')),
			
			//inline
			'strong'         => array('<strong>',        '</strong>',         array('link', 'paragraph', 'code', 'emphasis', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'teletype')),
			'emphasis'       => array('<em>',            '</em>',             array('link', 'paragraph', 'strong', 'code', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'teletype')),
			'strikethrough'  => array('<del>',           '</del>',            array('link', 'paragraph', 'strong', 'emphasis', 'preformatted', 'code', 'underline', 'small', 'big', 'teletype')),
			'underline'      => array('<ins>',           '</ins>',            array('link', 'paragraph', 'strong', 'emphasis', 'preformatted', 'strikethrough', 'code', 'small', 'big', 'teletype')),
			'small'          => array('<small>',         '</small>',          array('link', 'paragraph', 'strong', 'emphasis', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'teletype', 'code')),
			'big'            => array('<big>',           '</big>',            array('link', 'paragraph', 'strong', 'emphasis', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'teletype', 'code')),
			'teletype'       => array('<tt>',            '</tt>',             array('link', 'paragraph', 'strong', 'emphasis', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'code')),
			'code'           => array('<code>',          '</code>',           array('link', 'paragraph', 'blockquote', 'tablecell', 'tableheader', 'listitem', 'link', 'strong', 'emphasis', 'preformatted', 'strikethrough', 'underline', 'small', 'big', 'teletype')),
			'link'           => array('<a class="{class}" href="{href}">', '</a>', array('paragraph', 'listitem', 'header', 'tablecell', 'tableheader', 'preformatted', 'blockquote', 'strong', 'emphasis', 'strikethrough', 'underline', 'small', 'big', 'teletype', 'code')),
			'image'          => array('<img alt="{alt}" src="{src}"/>', null, array('link', 'paragraph', 'listitem', 'header', 'tablecell', 'tableheader', 'preformatted', 'blockquote', 'strong', 'emphasis', 'strikethrough', 'underline', 'small', 'big', 'teletype', 'code')),
			
			'module'         => array(null, null, array())
		);
		
		private static $blockScopes = array(
			'paragraph', 'unorderedlist', 'orderedlist', 'listitem', 
			'header', 'table', 'tablecell', 'tablerow', 'tableheader', 
			'preformatted', 'blockquote', 'deflist', 'defterm', 'defdef'
		);
		
		private static $inlineScopes = array(
			'strong', 'emphasis', 'strikethrough', 'unerline',
			'small', 'big', 'teletype', 'code', 'link', 'image'
		);
		
		private $isStartOfLine;
		
		public function __construct() {
			$this->init();
		}
		
		private function init($wikitext = '') {
			$this->scopeStack = array();
			$this->wikitext = $wikitext;
			$this->index = -1;
			$this->isStartOfLine = true;
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
			
			$text = $this->read();
			while ($text !== null) {
				if ($this->isStartOfLine) {
					$this->isStartOfLine = false;
					switch ($text) {
						case '!':
							while ($this->peek() === '!') {
								$text .= $this->read();
							}
							
							$this->openScope('header', min(6, strlen($text)));
							break;
						case "\n":
							$this->handleNewLine();
							break;
						case '*':
						case '#':
							$peek = $this->peek();
							while ($peek === '*' || $peek === '#') {
								$text .= $this->read();
								$peek = $this->peek();
							}
							
							$this->handleListItem($text);
							break;
						case ';':
							if (!$this->isInScopeStack('deflist')) {
								$this->openScope('deflist');
							}
							
							$this->openScope('defterm');
							break;
						case ':':
							if (!$this->isInScopeStack('deflist')) {
								throw new Exception('Cannot have a definition without a definition term');
							}
							
							$this->openScope('defdef');
							break;
						case '<':
							if ($this->peek() === '<') {
								$this->read();
								$this->openScope('blockquote');
							}
							break;
						default:
							//if scope stack has no block elements, then start a new paragraph
							if (!$this->isInScopeStack(self::$blockScopes)) {
								$this->openScope('paragraph');
							}
							
							$this->out($text);
							break;
					}
				} else {
					switch ($text) {
						case "\n":
							$this->handleNewLine();
							break;
						default:
							$this->out($text);
							break;
					}
				}
				
				$text = $this->read();
			}
			
			//close orphaned scopes
			$this->closeScopes(2);
			
			if ($return) {
				return ob_get_clean();
			}
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
				}
				
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
				}
			}
			
			//open list item
			$this->openScope('listitem');
		}
		
		private function openScope($type, $nestingLevel = null) {
			$args = func_get_args();
			$type = array_shift($args);
			$this->scopePush($type, $nestingLevel);
			$opener = preg_replace(array('/\{.*\}/'), $args, self::$scopes[$type][0]);
			$this->out($opener . (self::isIndentedType($type) ? "\n" : ''), true);
		}
		
		private function closeScopes($numNewLines) {
			$nextScope = $this->scopePeek();
			if ($nextScope === null) {
				return;
			}
			
			if ($numNewLines > 1) {
				$this->emptyScopeStack();
			} else if ($numNewLines === 1) {
				switch ($nextScope['type']) {
					case 'header':
						$this->closeScope($this->scopePop(), $nextScope['nesting_level']);
						break;
					case 'defterm':
					case 'defdef':
						$this->closeScope($this->scopePop());
						break;
				}
			}
		}
		
		private function emptyScopeStack() {
			while (count($this->scopeStack)) {
				$scope = $this->scopePop();
				$this->closeScope($scope, $scope['nesting_level']);
			}
		}
		
		private function closeScope(array $scope) {
			$args = func_get_args();
			$scope = array_shift($args);
			$closer = preg_replace(array('/\{.*\}/'), $args, self::$scopes[$scope['type']][1]);
			$this->out($closer . "\n", self::isIndentedType($scope['type']), false);
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

		private function peek() {
			return isset($this->wikitext[$this->index + 1]) ? $this->wikitext[$this->index + 1] : null;
		}
		
		private function read() {
			if (isset($this->wikitext[$this->index + 1])) {
				return $this->wikitext[++$this->index];
			}
			
			return null;
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
		
		private static function isIndentedType($type) {
			return in_array($type, array('orderedlist', 'unorderedlist', 'deflist', 'blockquote'));
		}
		
		private function throwException(Exception $e) {
			ob_end_clean();
			throw $e;
		}
		
	}

?>