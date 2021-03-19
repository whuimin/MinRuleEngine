using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq.Expressions;

using Antlr4.Runtime;

namespace Min.RuleEngine
{
    public partial class RuleExpression<TDelegate>
    {
        private static readonly ConcurrentDictionary<string, Lazy<TDelegate>> functions = new ConcurrentDictionary<string, Lazy<TDelegate>>();

        private RuleEngineParser.JRuleContext BuildContext(string expressionText)
        {
            var lexerErrorListener = new LexerErrorListener();
            var antlrInputStream = new AntlrInputStream(expressionText);
            var lexer = new RuleEngineLexer(antlrInputStream);
            lexer.AddErrorListener(lexerErrorListener);

            var parserErrorListener = new ParserErrorListener();
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new RuleEngineParser(commonTokenStream);
            parser.AddErrorListener(parserErrorListener);

            var context = parser.jRule();
            if (context.exception != null)
            {
                throw new Exception(string.Join(Environment.NewLine, lexerErrorListener.Errors.Union(parserErrorListener.Errors)), context.exception);
            }

            return context;
        }

        private Expression<TDelegate> Lambda(string expressionText)
        {
            var functionType = typeof(TDelegate);
            var parameterTypes = new List<Type>();
            foreach (var argumentType in functionType.GenericTypeArguments)
            {
                parameterTypes.Add(argumentType);
            }

            var visitor = new RuleExpressionVisitor(parameterTypes);
            var context = BuildContext(expressionText);
            var expression = visitor.Visit(context);

            var parameterExpressions = visitor.ParameterExpressions;
            if (parameterExpressions.Length == 0)
            {
                return Expression.Lambda<TDelegate>(expression);
            }
            else
            {
                return Expression.Lambda<TDelegate>(expression, parameterExpressions);
            }
        }

        public TDelegate Function(string expressionText)
        {
            var lazyFunction = functions.GetOrAdd(expressionText, new Lazy<TDelegate>(() =>
            {
                var lambda = Lambda(expressionText);

                var function = lambda.Compile();

                return function;
            }));

            return lazyFunction.Value;
        }
    }
}
