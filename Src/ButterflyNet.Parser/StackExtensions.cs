using System;
using System.Collections.Generic;
using System.Linq;

namespace ButterflyNet.Parser {
	public static class StackExtensions {
		public static bool IsEmpty<T>(this Stack<T> stack) {
			return stack.Count == 0;
		}

		public static T PeekOrDefault<T>(this Stack<T> stack) {
			return stack.IsEmpty() ? default(T) : stack.Peek();
		}

		public static bool ContainsType(this Stack<IScope> stack, params Type[] types) {
			return stack.Any(scope => types.Contains(scope.GetType()));
		}
	}
}