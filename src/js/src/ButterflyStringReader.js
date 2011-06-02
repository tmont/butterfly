//stolen from sunlight
function ButterflyStringReader(text) {
	var index = 0,
		line = 1,
		column = 1,
		length,
		currentChar,
		nextReadBeginsLine;
	
	text = text.replace(/\r\n/g, "\n").replace(/\r/g, "\n"); //normalize line endings to unix
	
	length = text.length;
	currentChar = length > 0 ? text.charAt(0) : ButterflyStringReader.EOF;
	nextReadBeginsLine = currentChar === "\n";
	
	function reset() {
		index = 0;
		line = 1;
		column = 1;
		nextReadBeginsLine = false;
	}
	
	function getCharacters(count) {
		var value;
		if (count === 0) {
			return "";
		}

		count = count || 1;
		
		value = text.substring(index + 1, index + count + 1);
		return value === "" ? ButterflyStringReader.EOF : value;
	}
	
	this.peek = function(count) { return getCharacters(count);};
	this.substring = function() { return text.substring(index); };
	this.peekSubstring = function() { return text.substring(index + 1); };
	
	this.replace = function(start, end, value) {
		text = text.substring(0, start) + value + text.substring(end);
		length = text.length;
	};

	this.read = function (count) {
		var value = getCharacters(count),
			newlineCount,
			lastChar;
		
		if (value === "") {
			//this is a result of reading/peeking/doing nothing
			return value;
		}

		if (value !== ButterflyStringReader.EOF) {
			//advance index
			index += value.length;
			column += value.length;

			//update line count
			if (nextReadBeginsLine) {
				line++;
				column = 1;
				nextReadBeginsLine = false;
			}

			newlineCount = value.substring(0, value.length - 1).replace(/[^\n]/g, "").length;
			if (newlineCount > 0) {
				line += newlineCount;
				column = 1;
			}
			
			lastChar = last(value);
			if (lastChar === "\n") {
				nextReadBeginsLine = true;
			}

			currentChar = lastChar;
		} else {
			index = length;
			currentChar = ButterflyStringReader.EOF;
		}

		return value;
	};

	this.seekToNonWhitespace = function() {
		var match = /^(\s+)/.exec(this.peekSubstring());
		if (!match) {
			return;
		}
		
		this.read(match[1].length);
	};
	
	this.seek = function(to) {
		if (to < index - 1) {
			//go back to beginning and read until to
			reset();
			index = -1;
			if (to >= 0) {
				this.read(to + 1);
			}
		} else {
			//read from current index
			this.read(index - to + 1);
		}
	};
	
	this.value = function() { return text; };
	this.getLine = function() { return line; };
	this.getColumn = function() { return column; };
	this.isEof = function() { return index >= length; };
	this.isSol = function() { return column === 1; };
	this.isSolWs = function() {
		var temp = index,
			c;
		if (column === 1) {
			return true;
		}
		
		//look backward until we find a newline or a non-whitespace character
		while ((c = text.charAt(--temp)) !== "") {
			if (c === "\n") {
				return true;
			}
			if (!/\s/.test(c)) {
				return false;
			}
		}
		
		return true;
	};
	this.isEol = function() { return nextReadBeginsLine; };
	this.current = function() { return currentChar; }
	this.getIndex = function() { return index; };
};

ButterflyStringReader.EOF = -1;