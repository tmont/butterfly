using System;

namespace ButterflyNet.Parser {
	/// <summary>
	/// Indicates that an <see cref="IParseStrategy"/> implementation should not
	/// be loaded by default.
	/// </summary>
	/// <see cref="ParserExtensions.LoadDefaultStrategies"/>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	internal sealed class NonDefaultAttribute : Attribute { }

	/// <summary>
	/// Indicates that an <see cref="IParseStrategy"/> implementation should be excluded
	/// when loading strategies from a namespace or assembly.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ExcludeAttribute : Attribute { }
}