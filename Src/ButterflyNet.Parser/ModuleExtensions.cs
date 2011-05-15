using System;
using System.Collections.Generic;
using System.Reflection;
using ButterflyNet.Parser.Macros;
using ButterflyNet.Parser.Modules;

namespace ButterflyNet.Parser {
	public static class ModuleExtensions {
		public static void Load(this object obj, IDictionary<string, string> dictionary) {
			var type = obj.GetType();

			foreach (var key in dictionary.Keys) {
				var property = type.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
				if (property != null) {
					property.SetValue(obj, Convert.ChangeType(dictionary[key], property.PropertyType), null);
				}
			}
		}

		public static NamedTypeRegistry<IButterflyModule> LoadDefaults(this NamedTypeRegistry<IButterflyModule> registry) {
			return registry
				.RegisterType<ImageModule>("image")
				.RegisterType<CategoryModule>("category");
		}

		public static NamedTypeRegistry<IButterflyMacro> LoadDefaults(this NamedTypeRegistry<IButterflyMacro> registry) {
			return registry.RegisterType<TimestampMacro>("timestamp");
		}
	}
}