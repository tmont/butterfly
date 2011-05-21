﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class FormattingHelpGenerator {

		[Test]
		public void Generate_html_formatting_help_file() {
			const string helpMarkup = @"
! Butterfly Markup
(-''auto-generated on __[::timestamp]__''-)

__Butterfly__ is a semantic markup syntax created and developed by [http://tommymontgomery.com/|Tommy Montgomery]. 
It is similar to many wiki syntaxes (such as [http://www.mediawiki.org/wiki/MediaWiki|MediaWiki]). It's goal is to 
be familiar, easy to read, and simple to use.

This document serves as both a test of the markup itself and documentation on how to use it. This document was 
automatically generated by a unit test using __Butterfly__ itself to generate everything inside the ==<body>== tag.

!! Output Formats
__Butterfly__ uses an ''Analyzer'' to transform markup to an output format. The default is HTML, but you can implement 
your own analyzer by injecting a custom implementation of ==ButterflyNet.Parser.ButterflyAnalyzer== into a 
==ButterflyNet.Parser.ButterflyParser== instance.

!! Text Formatting
|! Style|! Markup|! Output|
| Bold| ==[!this text is __bold__]==| this text is __bold__|
| Emphasis| ==[!this text is ''emphasized'']==| this text is ''emphasized''|
| Teletype| ==[!this text is ==reminiscient of a typewriter==]==| this text is ==reminiscient of a typewriter==|
| Underline| ==[!this text is --important--]==| this text is --important--|
| Strike-through| ==[!this text is ---deleted---]==| this text is ---deleted---|
| Big| ==[!this text is (+big and (+huge+)+)]==| this text is (+big and (+huge+)+)|
| Small| ==[!this text is (-small and (-tiny-)-)]==| this text is (-small and (-tiny-)-)|

!!Links
Hyperlinks are surrounded by brackets. You can specify the text by separating it from the URL with a ==|==.

|! Markup|! Output|
| ==[![link]]]==| [link]|
| ==[![link|totally ''awesome'' link, brah!]]]==| [link|totally ''awesome'' link, brah!]|
| ==[![http://example.com/]]]==| [http://example.com/]|
| ==[![http://example.com/|external link]]]==| [http://example.com/|external link]|
| ==[![link.html|link text with [bracket]]]]]]]==| [link.html|link text with [bracket]]]|

You can change the base URL of local links by modifying the ==ButterflyParser.LocalLinkBaseUrl== property.

!!Headers

Headers start with a variable number of ==!== at the beginning of a line.

{{{{plaintext
!Header level 1
!!Header level 2
!!! !Header that starts with an exclamation point!
}}}}

!Header level 1
!!Header level 2
!!! !Header that starts with an exclamation point!

!! Lists
!!! Bullet Lists

Bulleted lists are opened by a ==*== (unordered) or a ==#== (ordered) at the start of a line. To increase the nesting 
level, add more asterisks or hashes. You cannot mix list types at the same depth.

The following markup:
{{{{plaintext
* Countries
*# United States
*#* West Coast
*#** Washington
*#** Oregon
*#** California
*#* East Coast
*#** Florida
*#** Maryland
*#** Georgia
*#** Massachusetts
*# Canada
*# Mexico
* See also
** Wikipedia: [http://en.wikipedia.org/wiki/List_of_sovereign_states]
}}}}

Gets transformed into:

* Countries
*# United States
*#* West Coast
*#** Washington
*#** Oregon
*#** California
*#* East Coast
*#** Florida
*#** Maryland
*#** Georgia
*#** Massachusetts
*# Canada
*# Mexico
* See also
** Wikipedia: [http://en.wikipedia.org/wiki/List_of_sovereign_states]

!!! Definition Lists
Definition terms are opened by a ==;== at the start of a line. Definitions of those terms are opened by a ==:== at 
the start of the subsequent line.

e.g.

{{{{plaintext
;Elephant
:Big grey animals that have superstitiously accurate memories
;Baby elephant
:Baby elephants are called ''calves'', apparently
}}}}

transforms into

;Elephant
:Big grey animals that have superstitiously accurate memories
;Baby elephant
:Baby elephants are called ''calves'', apparently

!! Preformatted Text
!!! Single-line

For single line snippets of preformatted text, start a line with a space.

{{{{plaintext
Not preformatted
 preformatted
}}}}

Not preformatted
 preformatted

!!! Multi-line
For multiline preformatted text, surround with =={{{== and ==}}}==. Note that markup is allowed 
inside these blocks.

{{{{plaintext
{{{
this is
preformatted
text and line breaks
and             whitespace
are preserved (and so is __markup__)
}}}
}}}}

{{{
this is
preformatted
text and line breaks
and             whitespace
are preserved (and so is __markup__)
}}}

!!! Preformatted Code
For chunks of code, surround with =={{{{== and ==}}}}==. All this does is disable formatting.

{{{plaintext
[!{{{{
this is
preformatted
text and line breaks
and             whitespace
are preserved (and so is __markup__)
}}}}]
}}}

{{{{
this is
preformatted
text and line breaks
and             whitespace
are preserved (and so is __markup__)
}}}}

!!!Specifying a programming language with syntax highlighting
You can specify a programming language immediately after the opening =={{{== or =={{{{==. By default, 
__Butterfly__ uses [http://sunlightjs.com/|Sunlight] for syntax highlighting, but it also supports 
[http://code.google.com/p/google-code-prettify/|Prettify], [http://softwaremaniacs.org/soft/highlight/en/|Highlight.js]
and [http://alexgorbatchev.com/SyntaxHighlighter/|Syntax Highlighter].

{{{{plaintext
{{{csharp
public class MyClass : ICloneable {
	public string GetSomething(int i) {
		if (i < 0) {
			return string.Empty;
		}

		return string.Format(""i: {0}"", i);
	}
}
}}}
}}}}

becomes

{{{csharp
public class MyClass : ICloneable {
	public string GetSomething(int i) {
		if (i < 0) {
			return string.Empty;
		}

		return string.Format(""i: {0}"", i);
	}
}
}}}

!! Blockquotes

Blockquotes are opened with ==<<== at the beginning of a line, and closed with ==>>==.

{{{{plaintext
<<this is a blockquote>>

<<
so is this
>>

<<
you can have

* markup
* as well as __textual styles__
** inside a blockquote
>>
}}}}

<<this is a blockquote>>

<<
so is this
>>

<<
you can have

* markup
* as well as __textual styles__
** inside a blockquote
>>

!! Horizontal Ruler
You can insert a horizontal ruler by creating a line consisting of four dashes ==[!----]==.

{{{{plaintext
this is some text
----
here is more text
}}}}

this is some text
----
here is more text

!! Modules
Modules are extensions to the parser that allow for injecting user-defined functionality in the form 
of function calls. Each module has a unique name, and can be passed arguments.

!!! Images
In __Butterfly__, images are implemented as modules.

|! Markup|! Output|
| ==[![:image|url=butterfly.png]]]==| [:image|url=butterfly.png]|
| ==[![:image|url=butterfly.png|title=A pretty butterfly|width=100|height=150]]]==| [:image|url=butterfly.png|title=A pretty butterfly|width=100|height=150]|
| ==[![:image|url=nonexistent.png|alt=[an image]]]]]]]==| [:image|url=nonexistent.png|alt=[an image]]]|

You can change the base URL of local images by modifying the ==ButterflyParser.LocalImageBaseUrl== property.

!!! HTML Entities
Since __Butterfly__ escapes all output (when it transforms to HTML), you can't render an HTML entity 
(the ==&== gets encoded to ==&amp;==). To get around this you can use the HTML entity module to render 
an HTML entity.

|! Markup|! Output|
| ==[![:entity|value=copy]]]==| [:entity|value=copy]|
| ==[![:entity|value=#169]]]==| [:entity|value=#169]|
| ==[![:entity|value=#xa9]]]==| [:entity|value=#xa9]|

!! Unicode

__Butterfly__ is unicode-safe, so you can just use straight-up Unicode and it will render as Unicode (unencoded). 
Make sure that you are rendering your page in UTF-8.

<<Один главный процесс и несколько рабочих, рабочие процессы работают под непривилегированным пользователем>>

!! Forced Line Breaks

To force a line break, use ==[!%%%]==.

{{{{plaintext
this text will
be on the same line

but this text %%% has a line break
}}}}

this text will 
be on the same line

but this text %%% has a line break

!! Tables
To render a table, start a line with ==|==. Each cell is separated by a ==|==. Use ==|!== 
to render a table header instead of a table cell.

{{{{plaintext
|! cell 1 |! cell 2 |
| row 2 cell 1 | row 2 cell 2|
| row 3 cell 1 | row 3 cell 2| row 3 cell 3 |
}}}}

becomes

|! cell 1 |! cell 2 |
| row 2 cell 1 | row 2 cell 2|
| row 3 cell 1 | row 3 cell 2| row 3 cell 3 |

Table rows are separated by linefeeds. If you need more complicated markup inside a table row, 
you can open a multiline table row with ==|{== and close it with ==}|==.

{{{{plaintext
|! header |! header2 |
|{ this is a multiline row 
You can have all kinds of markup in here.

* like a list
** and even a nested list!

| cells are still separated by a [!|] 

<<I'm sexy

[:entity|value=mdash] [http://tommymontgomery.com/|Tommy Montgomery]
>>
}|
}}}}

becomes

|! header |! header2 |
|{ this is a multiline row 
You can have all kinds of markup in here.

* like a list
** and even a nested list!

| cells are still separated by a [!|] 

<<I'm sexy

[:entity|value=mdash] [http://tommymontgomery.com/|Tommy Montgomery]
>>
}|

!! Unparsed Text
Surround text with ==[![!]== and ==]== to make the parser ignore it.

{{{{plaintext
[! this will __not__ be parsed
* not a list

| not a table |
]
}}}}

becomes

[! this will __not__ be parsed
* not a list

| not a table |
]

__Note__ that newlines are ignored as well, so a chunk of text inside ==[![!]]]== will 
always be one single paragraph.

!! Macros
Macros are another extensibility point. Macros are similar to modules, except that they are 
completely deterministic, whereas a module can render (or not render at all) in a variety of 
ways depending on context. A macro evaluates to a hard value.

Macros are surrounded by ==[![::]== and ==]==.

!!! Current Timestamp
The ==timestamp== macro prints the current timestamp in a variety of formats designated by the 
[http://msdn.microsoft.com/en-us/library/az4se3k1.aspx|.NET date formats]. If no format 
is given, it defaults to [http://msdn.microsoft.com/en-us/library/az4se3k1.aspx#UniversalSortable|the 
Universal Sortable format].

|! Markup|! Output|
| ==[![::timestamp]]]==| [::timestamp]|
| ==[![::timestamp|format=dddd, MMMM dd yyyy]]]==| [::timestamp|format=dddd, MMMM dd yyyy]|

";

			var parser = new ButterflyParser().LoadDefaultStrategies();
			parser.LocalImageBaseUrl = "";

			var stopWatch = Stopwatch.StartNew();
			var html = parser.ParseAndReturn(helpMarkup);
			stopWatch.Stop();
			Console.WriteLine("elapsed parsing time: {0}ms", stopWatch.ElapsedMilliseconds);


			var fileContext = string.Format(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">

<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""en-US"">
	<head>
		<title>Butterfly Formatting Help</title>
		<meta http-equiv=""content-type"" content=""text/html; charset=utf-8""/>
		<style type=""text/css"">
			body {{
				background-color: #FFFFFF;
				color: #000000;
				font-family: ""Droid Sans"", Verdana, sans-serif;
				font-size: 12pt;
				width: 80%;
				min-width: 800px;
				margin: auto;
			}}
			
			code, tt, pre {{
				font-family: Consolas, Inconsolata, ""Courier New"", monospace;
			}}
			
			dt {{
				background-color: #EEEEEE;
				font-weight: bold;
				margin-top: 10px;
			}}
			
			a {{
				color: #000099;
				text-decoration: underline;
			}}
			a:hover {{
				background-color: #EEEEEE;
			}}
			a.external {{
				padding-right: 14px;
				background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAwAAAAMCAYAAABWdVznAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAABl0RVh0U29mdHdhcmUAUGFpbnQuTkVUIHYzLjUuN6eEncwAAABASURBVChTY2AYOiBtZtp/GMbm6v9AQWQMVkNIA4pBIMUwTbhsgIvDFOMLQLBpWEyEi6NrxiVBXQ3oIQXjU5YWADmkKHLJiLiZAAAAAElFTkSuQmCC);
				background-position: right center;
				background-repeat: no-repeat;
			}}

			table {{
				border-collapse: collapse;
			}}
			table, td, th {{
				border: 1px solid #999999;
				padding: 5px;
			}}
			th {{
				background-color: #CBB1CA;
			}}
			blockquote {{
				border-left: 2px solid #666666;
				background-color: #EEEEEE;
				padding: 5px;
			}}
		</style>
		<link rel=""stylesheet"" type=""text/css"" href=""http://dl.sunlightjs.com/latest/themes/sunlight.default.css""/>
	</head>
	<body>
{0}

		<script type=""text/javascript"" src=""http://dl.sunlightjs.com/latest/sunlight-min.js""></script>
		<script type=""text/javascript"" src=""http://dl.sunlightjs.com/latest/lang/sunlight.csharp-min.js""></script>
		<script type=""text/javascript"">//<![CDATA[
			typeof(Sunlight) !== ""undefined"" && Sunlight.highlightAll();
		//]]></script>
	</body>
</html>", html);

			File.WriteAllText(@"..\..\formatting.html", fileContext, Encoding.UTF8);
		}

	}
}