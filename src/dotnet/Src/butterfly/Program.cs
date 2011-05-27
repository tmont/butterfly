using System;
using System.IO;
using Argopt;
using ButterflyNet.Parser;

namespace butterfly {
	class Program {

		public class ButterflyOptions {
			[Flag, Alias("h", "?")]
			[Description("Shows this help message")]
			public bool Help { get; set; }

			[ValueProperty]
			[Description("File with markup to parse; if not given reads from stdin", ValueName = "file")]
			public string File { get; set; }
		}

		static void Main(string[] args) {
			var result = OptionParser.Parse<ButterflyOptions>(args);

			var contract = result.Contract;

			if (contract.Help) {
				Console.WriteLine(OptionParser.GetDescription<ButterflyOptions>());
				return;
			}

			string text;

			if (string.IsNullOrEmpty(contract.File)) {
				//read from stdin
				text = Console.In.ReadToEnd();
			} else {
				try {
					text = File.ReadAllText(contract.File);
				} catch (Exception e) {
					Console.WriteLine(string.Format("An error occurred trying to read from {0}", contract.File));
					Console.WriteLine();
					Console.WriteLine(e);
					return;
				}
			}

			var parser = new ButterflyParser().LoadDefaultStrategies();
			try {
				Console.WriteLine(parser.ParseAndReturn(text));
			} catch (ParseException e) {
				Console.WriteLine("An error occurred during parsing");
				Console.WriteLine();
				Console.WriteLine(e);
			}
		}
	}
}
