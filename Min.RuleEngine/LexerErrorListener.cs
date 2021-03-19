using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

using Antlr4.Runtime;

namespace Min.RuleEngine
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        public List<string> Errors { get; } = new List<string>();

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Errors.Add($"Error symbol: '{offendingSymbol}', line {line}:{charPositionInLine}, {msg}.");
        }
    }
}
