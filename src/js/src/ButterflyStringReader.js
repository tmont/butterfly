function last(thing) {
	return thing.charAt ? thing.charAt(thing.length - 1) : thing[thing.length - 1];
}

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

	function getCharacters(count) {
		var value;
		if (count === 0) {
			return "";
		}

		count = count || 1;
		
		value = text.substring(index + 1, index + count + 1);
		return value === "" ? ButterflyStringReader.EOF : value;
	}
	
	this.toString = function() {
		return "length = " + length + "; index = " + index + "; line = " + line + "; column = " + column + "; current = [" + currentChar + "]";
	};
	this.peek = function(count) { return getCharacters(count);};
	this.substring = function() { return text.substring(index); };
	this.peekSubstring = function() { return text.substring(index + 1); };

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
	this.value = text;
};

ButterflyStringReader.EOF = -1;