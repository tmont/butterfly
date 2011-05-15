using System;

namespace ButterflyNet.Parser.Macros {
	public class TimestampMacro : IButterflyMacro {
		private const string DefaultFormat = "yyyy-MM-dd HH:mm:ss";

		public string Format { get; set; }

		public string GetValue() {
			return DateTime.UtcNow.ToString(Format ?? DefaultFormat);
		}
	}
}