using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;

namespace Min.RuleEngine
{
    internal class ParserErrorListener : BaseErrorListener
    {
        public List<string> Errors { get; } = new List<string>();

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Errors.Add($"Error symbol: '{offendingSymbol.Text}', line {line}:{charPositionInLine}, {msg}.");
        }
    }
}
