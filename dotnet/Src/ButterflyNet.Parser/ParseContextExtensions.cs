namespace ButterflyNet.Parser {
	public static class ParseContextExtensions {
		public static void AdvanceInput(this ParseContext context, int amount = 1) {
			context.Input.Read(amount);
			context.UpdateCurrentChar();
		}

		public static void UpdateCurrentChar(this ParseContext context) {
			context.CurrentChar = context.Input.Current;
		}
	}
}