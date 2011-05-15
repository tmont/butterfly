using System;
using System.Collections;
using System.Collections.Generic;

namespace ButterflyNet.Parser {
	public class NamedTypeRegistry<T> : IEnumerable<KeyValuePair<string, Type>> {

		private readonly IDictionary<string, Type> registry = new Dictionary<string, Type>();

		public NamedTypeRegistry<T> RegisterType<TType>(string identifier) where TType : T {
			registry[identifier] = typeof(TType);
			return this;
		}

		public NamedTypeRegistry<T> RegisterType(string identifier, Type type) {
			if (!typeof(T).IsAssignableFrom(type)) {
				throw new ArgumentException(string.Format("type must implement {0}", typeof(T).Name));
			}

			registry[identifier] = type;
			return this;
		}

		public Type this[string id] { get { return registry.ContainsKey(id) ? registry[id] : null; } }

		public IEnumerator<KeyValuePair<string, Type>> GetEnumerator() {
			return registry.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

}