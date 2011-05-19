using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterflyNet.Parser {
	public static class StackExtensions {
		public static bool IsEmpty<T>(this Stack<T> stack) {
			return stack.Count == 0;
		}

		public static T PeekOrDefault<T>(this Stack<T> stack, int index = 0) {
			if (stack.IsEmpty() || index >= stack.Count) {
				return default(T);
			}

			return index <= 0 ? stack.Peek() : stack.Skip(stack.Count - index - 1).First();
		}

		public static bool ContainsType(this Stack<IScope> stack, params Type[] types) {
			return stack.Any(scope => types.Contains(scope.GetType()));
		}
	}
}