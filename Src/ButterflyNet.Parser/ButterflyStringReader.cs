using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ButterflyNet.Parser {

	[DebuggerDisplay("Length = {Length}, Index = {Index} (line: {Line}, col: {Column}, Current: {Current}, Next = {Peek(1)})")]
	public sealed class ButterflyStringReader : TextReader {
		public const int NoValue = -1;
		private string input;
		private int index;
		public int Line { get; private set; }
		public int Column { get; private set; }
		private bool nextReadIncreasesLineCount;

		public ButterflyStringReader(string input) {
			this.input = input.NormalizeEol();
			Reset();
		}

		private void Reset() {
			index = -1;
			Line = 1;
			Column = 0;
			nextReadIncreasesLineCount = false;
		}

		private int ReadFromInput() {
			return ++index < input.Length ? input[index] : NoValue;
		}

		public int Length { get { return input.Length; } }

		public bool IsEof { get { return index >= Length; } }
		public bool IsStartOfLine { get { return Column == 1; } }
		public bool IsStartOfFile { get { return IsStartOfLine && Line == 1; } }
		public int Current { get { return index < Length ? input[index] : NoValue; } }
		public string Value { get { return input; } }
		public int Index { get { return index; } }

		public string Peek(int charsToRead, bool throwOnEof = false) {
			var count = 0;
			var chars = new StringBuilder();
			while (count < charsToRead) {
				if (index + count + 1 >= input.Length) {
					break;
				}

				chars.Append(input[index + 1 + count++]);
			}

			if (throwOnEof && charsToRead != count) {
				throw new Exception("Unexpected EOF");
			}

			return chars.ToString();
		}

		public override int Peek() {
			var peek = Peek(1);
			return string.IsNullOrEmpty(peek) ? NoValue : peek[0];
		}

		public override int Read() {
			var value = ReadFromInput();
			if (value == -1) {
				return NoValue;
			}

			Column++;

			if (nextReadIncreasesLineCount) {
				Line++;
				Column = 1;
				nextReadIncreasesLineCount = false;
			}

			if (value == '\n') {
				nextReadIncreasesLineCount = true;
			}

			return value.IsValidWikiChar() ? value : Read();
		}

		public string Read(int count, bool throwOnEof = false) {
			if (count <= 0) {
				throw new ArgumentOutOfRangeException("count", "count must be greater than zero");
			}

			var buffer = new char[count];
			var charsRead = Read(buffer, 0, count);

			if (throwOnEof && charsRead != count) {
				throw new Exception("Unexpected EOF");
			}

			return buffer.ToString();
		}

		public void SeekToNonWhitespace() {
			while (Peek().IsWhitespace()) {
				Read();
			}
		}

		public void Seek(int to) {
			if (to < 0 || to > Length) {
				throw new ArgumentOutOfRangeException("to", to, string.Format("to must be greater than or equal to zero and less than Length ({0})", Length));
			}

			if (to == index) {
				return;
			}

			var count = Length - to;
			if (to < index) {
				Reset();
				count = to;
			}

			if (count > 0) {
				Read(count);
			}
		}

		public void Replace(int start, int end, string value) {
			input = input.Substring(0, start) + value + input.Substring(end);
		}

		public override string ToString() {
			return string.Format("Length = {0}, Index = {1} (line: {2}, col: {3}, Current: {4}, Next = {5})", Length, Index, Line, Column, Current, Peek(1));
		}

	}

	public static class TextReaderExtensions {
		public static bool IsValidWikiChar(this int i) {
			return (i >= 0x20 && i < 0x7F) || i == '\t' || i == '\n';
		}

		public static bool IsWhitespace(this int i) {
			return i == '\t' || i == ' ';
		}
	}
}