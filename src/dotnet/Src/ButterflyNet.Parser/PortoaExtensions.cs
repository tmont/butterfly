using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ButterflyNet.Parser {
	/// <summary>
	/// Imported a few methods from Portoa so I don't need the full reference
	/// </summary>
	internal static class PortoaExtensions {
		public static string GetFriendlyName(this Type type, bool fullyQualified = true) {
			//the namespace is null if it's an anonymous type
			var name = (fullyQualified && type.Namespace != null) ? type.Namespace : string.Empty;

			if (type.IsGenericType) {
				//the substring crap gets rid of everything after the ` in Name, e.g. List`1 for List<T>
				name +=
					type.Name.Substring(0, type.Name.IndexOf("`"))
						+ "<"
							+ type
								.GetGenericArguments()
								.Select(genericType => genericType.GetFriendlyName(fullyQualified))
								.Aggregate((current, friendlyName) => current + ", " + friendlyName)
									+ ">";
			} else {
				if (type.Namespace == null) {
					name += "AnonymousType";
				} else {
					if (!string.IsNullOrEmpty(name)) {
						name += ".";
					}
					name += type.Name;
				}
			}

			return name;
		}

		public static IEnumerable<T> Walk<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var item in collection) {
				action(item);
			}

			return collection;
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return provider.GetAttributes<T>().Any();
		}

		public static T[] GetAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return (T[])provider.GetCustomAttributes(typeof(T), true);
		}
	}
}