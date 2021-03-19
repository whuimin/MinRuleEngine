using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

using Antlr4.Runtime.Misc;

namespace Min.RuleEngine
{
    public partial class RuleExpressionVisitor : RuleEngineBaseVisitor<Expression>
    {
        private readonly RuleObjectTypeMapping jRuleObjectTypeMapping;

        public ParameterExpression[] ParameterExpressions
        {
            get
            {
                return jRuleObjectTypeMapping.ParameterExpressions.ToArray();
            }
        }

        public RuleExpressionVisitor(IList<Type> argumentTypes)
        {
            jRuleObjectTypeMapping = new RuleObjectTypeMapping(argumentTypes);
        }

        #region Private method

        private (Expression, Expression) Convert(Expression leftExpression, Expression rightExpression)
        {
            var leftTypeCode = Type.GetTypeCode(leftExpression.Type);
            var rightTypeCode = Type.GetTypeCode(rightExpression.Type);

            if (leftTypeCode > rightTypeCode)
            {
                rightExpression = Expression.Convert(rightExpression, leftExpression.Type);
            }
            else if (leftTypeCode < rightTypeCode)
            {
                leftExpression = Expression.Convert(leftExpression, rightExpression.Type);
            }

            return (leftExpression, rightExpression);
        }

        #endregion

        public override Expression VisitJRule([NotNull] RuleEngineParser.JRuleContext context)
        {
            var declarationStatementListContext = context.declarationStatementList();
            var expressionStatementContext = context.expressionStatement();
            var ruleStatementContext = context.ruleStatement();
            if (declarationStatementListContext == null)
            {
                if (expressionStatementContext != null)
                {
                    return VisitExpressionStatement(expressionStatementContext);
                }

                return VisitRuleStatement(ruleStatementContext);
            }

            var declarationExpression = VisitDeclarationStatementList(declarationStatementListContext) as BlockExpression;

            if (expressionStatementContext != null)
            {
                var expressionStatementExpression = VisitExpressionStatement(expressionStatementContext);
                var blockExpression1 = declarationExpression.Expressions.Select(e => e).Append(expressionStatementExpression);
                return Expression.Block(jRuleObjectTypeMapping.LocalParameterExpressions, blockExpression1);
            }

            var ruleStatementExpression = VisitRuleStatement(ruleStatementContext);
            var blockExpression2 = declarationExpression.Expressions.Select(e => e).Append(ruleStatementExpression);
            return Expression.Block(jRuleObjectTypeMapping.LocalParameterExpressions, blockExpression2);
        }

        public override Expression VisitDeclarationStatementList([NotNull] RuleEngineParser.DeclarationStatementListContext context)
        {
            var declarationStatementContexts = context.declarationStatement();

            var expression = Expression.Block(declarationStatementContexts.Select(dsc => VisitDeclarationStatement(dsc)).Where(ds => ds != null));

            return expression;
        }

        public override Expression VisitDeclarationStatement([NotNull] RuleEngineParser.DeclarationStatementContext context)
        {
            var parameterDeclarationContext = context.parameterDeclaration();
            if (parameterDeclarationContext != null)
            {
                VisitParameterDeclaration(parameterDeclarationContext);
                return null;
            }

            return VisitVariableDeclaration(context.variableDeclaration());
        }

        public override Expression VisitParameterDeclaration([NotNull] RuleEngineParser.ParameterDeclarationContext context)
        {
            var typeExpression = VisitBaseType(context.baseType());
            VisitParameterDeclarator(context.parameterDeclarator());
            var variableName = jRuleObjectTypeMapping.PopName();

            var expression = Expression.Parameter(typeExpression.Type, variableName);
            jRuleObjectTypeMapping.AddParameterExpression(expression.Name, expression);
            return null;
        }

        public override Expression VisitVariableDeclaration([NotNull] RuleEngineParser.VariableDeclarationContext context)
        {
            return VisitVariableDeclarator(context.variableDeclarator());
        }

        public override Expression VisitBaseType([NotNull] RuleEngineParser.BaseTypeContext context)
        {
            var simpleTypeContext = context.simpleType();
            if (simpleTypeContext != null)
            {
                return VisitSimpleType(simpleTypeContext);
            }

            return VisitClassType(context.classType());
        }

        public override Expression VisitSimpleType([NotNull] RuleEngineParser.SimpleTypeContext context)
        {
            var numericTypeContext = context.numericType();
            if (numericTypeContext != null)
            {
                return VisitNumericType(numericTypeContext);
            }

            return VisitBoolType(context.boolType());
        }

        public override Expression VisitNumericType([NotNull] RuleEngineParser.NumericTypeContext context)
        {
            var integralTypeContext = context.integralType();
            if (integralTypeContext != null)
            {
                return VisitIntegralType(integralTypeContext);
            }

            return VisitFloatingPointType(context.floatingPointType());
        }

        public override Expression VisitBoolType([NotNull] RuleEngineParser.BoolTypeContext context)
        {
            var typeName = context.GetText();
            var type = jRuleObjectTypeMapping.GetType(typeName);
            if (type == null)
            {
                throw new NotSupportedException($"Not support type '{typeName}'.");
            }

            return Expression.Variable(type);
        }

        public override Expression VisitIntegralType([NotNull] RuleEngineParser.IntegralTypeContext context)
        {
            var typeName = context.GetText();
            var type = jRuleObjectTypeMapping.GetType(typeName);
            if (type == null)
            {
                throw new NotSupportedException($"Not support type '{typeName}'.");
            }

            return Expression.Variable(type);
        }

        public override Expression VisitFloatingPointType([NotNull] RuleEngineParser.FloatingPointTypeContext context)
        {
            var typeName = context.GetText();
            var type = jRuleObjectTypeMapping.GetType(typeName);
            if (type == null)
            {
                throw new NotSupportedException($"Not support type '{typeName}'.");
            }

            return Expression.Variable(type);
        }

        public override Expression VisitClassType([NotNull] RuleEngineParser.ClassTypeContext context)
        {
            var namespaceOrTypeNameContext = context.namespaceOrTypeName();
            if (namespaceOrTypeNameContext != null)
            {
                return VisitNamespaceOrTypeName(namespaceOrTypeNameContext);
            }

            return VisitStringType(context.stringType());
        }

        public override Expression VisitNamespaceOrTypeName([NotNull] RuleEngineParser.NamespaceOrTypeNameContext context)
        {
            var typeName = context.GetText();
            if (typeName == "var")
            {
                return null;
            }

            var type = jRuleObjectTypeMapping.GetType(typeName);
            if (type == null)
            {
                throw new NotSupportedException($"Not support type '{typeName}'.");
            }
            return Expression.Variable(type);
        }

        public override Expression VisitStringType([NotNull] RuleEngineParser.StringTypeContext context)
        {
            var typeName = context.GetText();
            var type = jRuleObjectTypeMapping.GetType(typeName);
            return Expression.Variable(type);
        }

        public override Expression VisitParameterDeclarator([NotNull] RuleEngineParser.ParameterDeclaratorContext context)
        {
            var identifierContext = context.identifier();
            var variableName = context.GetText();
            jRuleObjectTypeMapping.PushName(variableName);
            return null;
        }

        public override Expression VisitVariableDeclarator([NotNull] RuleEngineParser.VariableDeclaratorContext context)
        {
            var identifierContext = context.identifier();
            var variableName = identifierContext.GetText();

            var rightExpression = VisitVariableInitializer(context.variableInitializer());
            var leftExpression = Expression.Variable(rightExpression.Type, variableName);
            jRuleObjectTypeMapping.AddLocalParameterExpression(variableName, leftExpression);

            return Expression.Assign(leftExpression, rightExpression);
        }

        public override Expression VisitVariableInitializer([NotNull] RuleEngineParser.VariableInitializerContext context)
        {
            var expressionContext = context.expression();
            if (expressionContext != null)
            {
                return VisitExpression(expressionContext);
            }

            return VisitArrayInitializer(context.arrayInitializer());
        }

        public override Expression VisitArrayInitializer([NotNull] RuleEngineParser.ArrayInitializerContext context)
        {
            var variableInitializerContexts = context.variableInitializer();
            var expressions = variableInitializerContexts.Select(vic => VisitVariableInitializer(vic));
            var expression = expressions.First();

            var typeName = $"HashSet<{expression.Type.Name}>";
            var type = jRuleObjectTypeMapping.GetType(typeName);
            if (type == null)
            {
                throw new NotSupportedException($"Not support type '{typeName}'.");
            }
            var newExpression = Expression.New(type);
            return Expression.ListInit(newExpression, expressions);
        }

        public override Expression VisitExpressionStatement([NotNull] RuleEngineParser.ExpressionStatementContext context)
        {
            var expression = VisitExpression(context.expression());

            var returnType = jRuleObjectTypeMapping.ReturnType;
            if (expression.Type != returnType)
            {
                expression = Expression.Convert(expression, returnType);
            }
            return expression;
        }

        public override Expression VisitRuleStatement([NotNull] RuleEngineParser.RuleStatementContext context)
        {
            var whenThenStatementContexts = context.whenThenStatement();

            var elseStatementContext = context.elseStatement();
            var expression = VisitElseStatement(elseStatementContext);
            for (int i = whenThenStatementContexts.Length - 1; i >= 0; i--)
            {
                var whenThenExpression = VisitWhenThenStatement(whenThenStatementContexts[i]) as ConditionalExpression;
                var trueExpression = whenThenExpression.IfTrue;
                (trueExpression, expression) = Convert(trueExpression, expression);
                expression = Expression.Condition(whenThenExpression.Test, trueExpression, expression);
            }

            var returnType = jRuleObjectTypeMapping.ReturnType;
            if (expression.Type != returnType)
            {
                expression = Expression.Convert(expression, returnType);
            }
            return expression;
        }

        public override Expression VisitWhenThenStatement([NotNull] RuleEngineParser.WhenThenStatementContext context)
        {
            var whenExpression = VisitWhenStatement(context.whenStatement());
            var thenExpression = VisitThenStatement(context.thenStatement());

            return Expression.IfThen(whenExpression, thenExpression);
        }

        public override Expression VisitWhenStatement([NotNull] RuleEngineParser.WhenStatementContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitThenStatement([NotNull] RuleEngineParser.ThenStatementContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitElseStatement([NotNull] RuleEngineParser.ElseStatementContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitExpression([NotNull] RuleEngineParser.ExpressionContext context)
        {
            var assignmentContext = context.assignment();
            if (assignmentContext != null)
            {
                VisitAssignment(assignmentContext);
            }

            return VisitNonAssignmentExpression(context.nonAssignmentExpression());
        }

        public override Expression VisitAssignment([NotNull] RuleEngineParser.AssignmentContext context)
        {
            var leftExpression = VisitUnaryExpression(context.unaryExpression());
            var rightExpression = VisitExpression(context.expression());

            var operatorSign = context.assignmentOperator().GetText();
            switch (operatorSign)
            {
                case "=":
                    return Expression.Assign(leftExpression, rightExpression);
                default:
                    throw new NotSupportedException($"Not support operator '{operatorSign}'.");
            }
        }

        public override Expression VisitNonAssignmentExpression([NotNull] RuleEngineParser.NonAssignmentExpressionContext context)
        {
            return VisitConditionalExpression(context.conditionalExpression());
        }

        public override Expression VisitConditionalOrExpression([NotNull] RuleEngineParser.ConditionalOrExpressionContext context)
        {
            var conditionalAndExpressionContexts = context.conditionalAndExpression();

            var leftExpression = VisitConditionalAndExpression(conditionalAndExpressionContexts[0]);
            for (int i = 1; i < conditionalAndExpressionContexts.Length; i++)
            {
                var rightExpression = VisitConditionalAndExpression(conditionalAndExpressionContexts[i]);
                leftExpression = Expression.MakeBinary(ExpressionType.OrElse, leftExpression, rightExpression);
            }

            return leftExpression;
        }

        public override Expression VisitConditionalAndExpression([NotNull] RuleEngineParser.ConditionalAndExpressionContext context)
        {
            var equalityExpressionContexts = context.equalityExpression();

            var leftExpression = VisitEqualityExpression(equalityExpressionContexts[0]);
            for (int i = 1; i < equalityExpressionContexts.Length; i++)
            {
                var rightExpression = VisitEqualityExpression(equalityExpressionContexts[i]);
                leftExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftExpression, rightExpression);
            }

            return leftExpression;
        }

        public override Expression VisitEqualityExpression([NotNull] RuleEngineParser.EqualityExpressionContext context)
        {
            var leftExpression = VisitRelationalExpression(context.relationalExpression(0));
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                var operatorSign = context.GetChild(i).GetText();
                var rightExpression = VisitRelationalExpression((RuleEngineParser.RelationalExpressionContext)context.GetChild(i + 1));

                ExpressionType expressionType;
                switch (operatorSign)
                {
                    case "==":
                        expressionType = ExpressionType.Equal;
                        break;
                    case "!=":
                        expressionType = ExpressionType.NotEqual;
                        break;
                    default:
                        throw new NotSupportedException($"Not support operator '{operatorSign}'.");
                }

                (leftExpression, rightExpression) = Convert(leftExpression, rightExpression);
                leftExpression = Expression.MakeBinary(expressionType, leftExpression, rightExpression);
            }
            return leftExpression;
        }

        public override Expression VisitRelationalExpression([NotNull] RuleEngineParser.RelationalExpressionContext context)
        {
            var leftExpression = VisitAdditiveExpression(context.additiveExpression(0));
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                var operatorSign = context.GetChild(i).GetText();
                var rightExpression = VisitAdditiveExpression((RuleEngineParser.AdditiveExpressionContext)context.GetChild(i + 1));

                ExpressionType expressionType;
                switch (operatorSign)
                {
                    case ">":
                        expressionType = ExpressionType.GreaterThan;
                        break;
                    case "<":
                        expressionType = ExpressionType.LessThan;
                        break;
                    case ">=":
                        expressionType = ExpressionType.GreaterThanOrEqual;
                        break;
                    case "<=":
                        expressionType = ExpressionType.LessThanOrEqual;
                        break;
                    default:
                        throw new NotSupportedException($"Not support operator '{operatorSign}'.");
                }

                (leftExpression, rightExpression) = Convert(leftExpression, rightExpression);
                leftExpression = Expression.MakeBinary(expressionType, leftExpression, rightExpression);
            }
            return leftExpression;
        }

        public override Expression VisitAdditiveExpression([NotNull] RuleEngineParser.AdditiveExpressionContext context)
        {
            var leftExpression = VisitMultiplicativeExpression(context.multiplicativeExpression(0));
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                var operatorSign = context.GetChild(i).GetText();
                var rightExpression = VisitMultiplicativeExpression((RuleEngineParser.MultiplicativeExpressionContext)context.GetChild(i + 1));

                ExpressionType expressionType;
                switch (operatorSign)
                {
                    case "+":
                        expressionType = ExpressionType.Add;
                        break;
                    case "-":
                        expressionType = ExpressionType.Subtract;
                        break;
                    default:
                        throw new NotSupportedException($"Not support operator '{operatorSign}'.");
                }

                (leftExpression, rightExpression) = Convert(leftExpression, rightExpression);
                leftExpression = Expression.MakeBinary(expressionType, leftExpression, rightExpression);
            }
            return leftExpression;
        }

        public override Expression VisitMultiplicativeExpression([NotNull] RuleEngineParser.MultiplicativeExpressionContext context)
        {
            var leftExpression = VisitUnaryExpression(context.unaryExpression(0));
            for (int i = 1; i < context.ChildCount; i += 2)
            {
                var operatorSign = context.GetChild(i).GetText();
                var rightExpression = VisitUnaryExpression((RuleEngineParser.UnaryExpressionContext)context.GetChild(i + 1));

                ExpressionType expressionType;
                switch (operatorSign)
                {
                    case "*":
                        expressionType = ExpressionType.Multiply;
                        break;
                    case "/":
                        expressionType = ExpressionType.Divide;
                        break;
                    case "%":
                        expressionType = ExpressionType.Modulo;
                        break;
                    default:
                        throw new NotSupportedException($"Not support operator '{operatorSign}'.");
                }

                (leftExpression, rightExpression) = Convert(leftExpression, rightExpression);
                leftExpression = Expression.MakeBinary(expressionType, leftExpression, rightExpression);
            }
            return leftExpression;
        }

        public override Expression VisitUnaryExpression([NotNull] RuleEngineParser.UnaryExpressionContext context)
        {
            var primaryExpressionContext = context.primaryExpression();
            if (primaryExpressionContext != null)
            {
                return VisitPrimaryExpression(primaryExpressionContext);
            }

            var expression = VisitUnaryExpression(context.unaryExpression());

            var operatorSign = context.GetChild(0).GetText();
            switch (operatorSign)
            {
                case "+":
                    return expression;
                case "-":
                    var leftExpression = Expression.Convert(Expression.Constant(0), expression.Type);
                    return Expression.Subtract(leftExpression, expression);
                case "!":
                    return Expression.Not(expression);
                default:
                    throw new NotSupportedException($"Not support operator '{operatorSign}'.");
            }
        }

        public override Expression VisitPrimaryExpression([NotNull] RuleEngineParser.PrimaryExpressionContext context)
        {
            var primaryExpressionStartContext = context.primaryExpressionStart();
            var expression = VisitPrimaryExpressionStart(primaryExpressionStartContext);

            var bracketExpressionContexts = context.bracketExpression();//Not Implemented

            var memberAccessContexts = context.memberAccess();
            var methodInvocationContexts = context.methodInvocation();
            if (memberAccessContexts.Length > 0 || memberAccessContexts.Length > 0)
            {
                for (int i = 1; i < context.ChildCount; i++)
                {
                    var parseTree = context.GetChild(i);
                    if (parseTree is RuleEngineParser.MemberAccessContext)
                    {
                        var constantExpression = VisitMemberAccess(parseTree as RuleEngineParser.MemberAccessContext) as ConstantExpression;

                        var parseTree1 = context.GetChild(i + 1);
                        if (parseTree1 is RuleEngineParser.MethodInvocationContext)
                        {
                            var blockExpression = VisitMethodInvocation(parseTree1 as RuleEngineParser.MethodInvocationContext) as BlockExpression;
                            expression = Expression.Call(expression, expression.Type.GetMethod(constantExpression.Value.ToString(), blockExpression.Expressions.Select(v => v.Type).ToArray()), blockExpression.Expressions);
                            i++;
                        }
                        else
                        {
                            expression = Expression.Property(expression, constantExpression.Value.ToString());
                        }
                    }
                }
            }

            return expression;
        }

        public override Expression VisitPrimaryExpressionStart([NotNull] RuleEngineParser.PrimaryExpressionStartContext context)
        {
            var literalExpressionContext = context.literalExpression();
            if (literalExpressionContext != null)
            {
                return VisitLiteralExpression(literalExpressionContext);
            }

            var simpleNameExpressionContext = context.simpleNameExpression();
            if (simpleNameExpressionContext != null)
            {
                return VisitSimpleNameExpression(simpleNameExpressionContext);
            }

            var parenthesisExpressionsContext = context.parenthesisExpressions();
            if (parenthesisExpressionsContext != null)
            {
                return VisitParenthesisExpressions(parenthesisExpressionsContext);
            }

            var memberAccessExpressionContext = context.memberAccessExpression();
            if (memberAccessExpressionContext != null)
            {
                return VisitMemberAccessExpression(memberAccessExpressionContext);
            }

            var literalAccessExpressionContext = context.literalAccessExpression();
            return VisitLiteralAccessExpression(literalAccessExpressionContext);
        }

        public override Expression VisitBracketExpression([NotNull] RuleEngineParser.BracketExpressionContext context)
        {
            throw new NotSupportedException($"Not support expression \"{context.GetText()}\".");
        }

        public override Expression VisitMemberAccess([NotNull] RuleEngineParser.MemberAccessContext context)
        {
            var identifierContext = context.identifier();
            var typeArgumentListContext = context.typeArgumentList();//Not Implemented

            return Expression.Constant(identifierContext.GetText());
        }

        public override Expression VisitMethodInvocation([NotNull] RuleEngineParser.MethodInvocationContext context)
        {
            return VisitArgumentList(context.argumentList());
        }

        public override Expression VisitArgumentList([NotNull] RuleEngineParser.ArgumentListContext context)
        {
            var argumentContexts = context.argument();

            return Expression.Block(argumentContexts.Select(ac => VisitArgument(ac)));
        }

        public override Expression VisitArgument([NotNull] RuleEngineParser.ArgumentContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitLiteralExpression([NotNull] RuleEngineParser.LiteralExpressionContext context)
        {
            return VisitLiteral(context.literal());
        }

        public override Expression VisitSimpleNameExpression([NotNull] RuleEngineParser.SimpleNameExpressionContext context)
        {
            var identifierContext = context.identifier();
            var typeArgumentListContext = context.typeArgumentList();//Not Implemented

            var name = identifierContext.GetText();
            var expression = jRuleObjectTypeMapping.GetParameterExpression(name);
            if (expression == null)
            {
                throw new NullReferenceException($"Variable '{name}' is not defined.");
            }

            return expression;
        }

        public override Expression VisitParenthesisExpressions([NotNull] RuleEngineParser.ParenthesisExpressionsContext context)
        {
            return VisitExpression(context.expression());
        }

        public override Expression VisitMemberAccessExpression([NotNull] RuleEngineParser.MemberAccessExpressionContext context)
        {
            var predefinedTypeContext = context.predefinedType();
            if (predefinedTypeContext != null)
            {
                return VisitPredefinedType(predefinedTypeContext);
            }

            return VisitQualifiedAliasMember(context.qualifiedAliasMember());
        }

        public override Expression VisitLiteralAccessExpression([NotNull] RuleEngineParser.LiteralAccessExpressionContext context)
        {
            throw new NotSupportedException($"Not support expression \"{context.GetText()}\".");
        }

        public override Expression VisitPredefinedType([NotNull] RuleEngineParser.PredefinedTypeContext context)
        {
            throw new NotSupportedException($"Not support expression \"{context.GetText()}\".");
        }

        public override Expression VisitQualifiedAliasMember([NotNull] RuleEngineParser.QualifiedAliasMemberContext context)
        {
            throw new NotSupportedException($"Not support expression \"{context.GetText()}\".");
        }

        public override Expression VisitLiteral([NotNull] RuleEngineParser.LiteralContext context)
        {
            var booleanLiteralContext = context.booleanLiteral();
            if (booleanLiteralContext != null)
            {
                return VisitBooleanLiteral(booleanLiteralContext);
            }

            var stringLiteralContext = context.stringLiteral();
            if (stringLiteralContext != null)
            {
                return VisitStringLiteral(stringLiteralContext);
            }

            var terminalNode = context.INTEGER_LITERAL();
            if (terminalNode != null)
            {
                var value = ulong.Parse(terminalNode.GetText());
                if (value <= int.MaxValue)
                {
                    return Expression.Constant((int)value);
                }
                else
                {
                    return Expression.Constant(value);
                }
            }

            terminalNode = context.REAL_LITERAL();
            if (terminalNode != null)
            {
                var value = decimal.Parse(terminalNode.GetText());
                return Expression.Constant(value);
            }

            terminalNode = context.CHARACTER_LITERAL();
            if (terminalNode != null)
            {
                var value = terminalNode.GetText();
                if (value.Length > 1)
                {
                    return Expression.Constant(value);
                }
                else if (value.Length == 1)
                {
                    return Expression.Constant(value[0]);
                }
                else
                {
                    throw new NotSupportedException("Not support '' value.");
                }
            }

            return Expression.Constant(null);
        }

        public override Expression VisitBooleanLiteral([NotNull] RuleEngineParser.BooleanLiteralContext context)
        {
            return Expression.Constant(bool.Parse(context.GetText()));
        }

        public override Expression VisitStringLiteral([NotNull] RuleEngineParser.StringLiteralContext context)
        {
            return Expression.Constant(context.GetText().Trim('"'));
        }
    }
}
