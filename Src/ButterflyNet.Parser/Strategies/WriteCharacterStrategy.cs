namespace ButterflyNet.Parser.Strategies {
	public class WriteCharacterStrategy : ParseStrategyBase {
		public override int Priority { get { return int.MaxValue; } }
		protected override bool Scopable {
			get {
				return false;
			}
		}


		protected override sealed void Execute(ParseContext context) {
			context.Buffer.Append(GetChar(context));
		}

		/// <summary>
		/// Derived classes can override this method to handle any escaping
		/// </summary>
		protected virtual char GetChar(ParseContext context) {
			return (char)context.CurrentChar;
		}
	}
}