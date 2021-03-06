//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ../../Min.RuleEngine\RuleEngine.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Min.RuleEngine {
#pragma warning disable 3021
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="RuleEngineParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IRuleEngineVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.jRule"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitJRule([NotNull] RuleEngineParser.JRuleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.declarationStatementList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationStatementList([NotNull] RuleEngineParser.DeclarationStatementListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.declarationStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationStatement([NotNull] RuleEngineParser.DeclarationStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.parameterDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameterDeclaration([NotNull] RuleEngineParser.ParameterDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] RuleEngineParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.parameterDeclarator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameterDeclarator([NotNull] RuleEngineParser.ParameterDeclaratorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.variableDeclarator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclarator([NotNull] RuleEngineParser.VariableDeclaratorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.variableInitializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableInitializer([NotNull] RuleEngineParser.VariableInitializerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.arrayInitializer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayInitializer([NotNull] RuleEngineParser.ArrayInitializerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.expressionStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionStatement([NotNull] RuleEngineParser.ExpressionStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.ruleStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRuleStatement([NotNull] RuleEngineParser.RuleStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.whenThenStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhenThenStatement([NotNull] RuleEngineParser.WhenThenStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.whenStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhenStatement([NotNull] RuleEngineParser.WhenStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.thenStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitThenStatement([NotNull] RuleEngineParser.ThenStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStatement([NotNull] RuleEngineParser.ElseStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] RuleEngineParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] RuleEngineParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.assignmentOperator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignmentOperator([NotNull] RuleEngineParser.AssignmentOperatorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.nonAssignmentExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNonAssignmentExpression([NotNull] RuleEngineParser.NonAssignmentExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.conditionalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionalExpression([NotNull] RuleEngineParser.ConditionalExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.conditionalOrExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionalOrExpression([NotNull] RuleEngineParser.ConditionalOrExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.conditionalAndExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConditionalAndExpression([NotNull] RuleEngineParser.ConditionalAndExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.equalityExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqualityExpression([NotNull] RuleEngineParser.EqualityExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.relationalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRelationalExpression([NotNull] RuleEngineParser.RelationalExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.additiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdditiveExpression([NotNull] RuleEngineParser.AdditiveExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.multiplicativeExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicativeExpression([NotNull] RuleEngineParser.MultiplicativeExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.unaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryExpression([NotNull] RuleEngineParser.UnaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.primaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimaryExpression([NotNull] RuleEngineParser.PrimaryExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.primaryExpressionStart"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimaryExpressionStart([NotNull] RuleEngineParser.PrimaryExpressionStartContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.literalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralExpression([NotNull] RuleEngineParser.LiteralExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.simpleNameExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleNameExpression([NotNull] RuleEngineParser.SimpleNameExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.parenthesisExpressions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisExpressions([NotNull] RuleEngineParser.ParenthesisExpressionsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.memberAccessExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberAccessExpression([NotNull] RuleEngineParser.MemberAccessExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.literalAccessExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralAccessExpression([NotNull] RuleEngineParser.LiteralAccessExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] RuleEngineParser.LiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.booleanLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanLiteral([NotNull] RuleEngineParser.BooleanLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.stringLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringLiteral([NotNull] RuleEngineParser.StringLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.memberAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberAccess([NotNull] RuleEngineParser.MemberAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.bracketExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBracketExpression([NotNull] RuleEngineParser.BracketExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.indexerArgument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIndexerArgument([NotNull] RuleEngineParser.IndexerArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.methodInvocation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodInvocation([NotNull] RuleEngineParser.MethodInvocationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.predefinedType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredefinedType([NotNull] RuleEngineParser.PredefinedTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.baseType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBaseType([NotNull] RuleEngineParser.BaseTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.simpleType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleType([NotNull] RuleEngineParser.SimpleTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.numericType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericType([NotNull] RuleEngineParser.NumericTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.integralType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntegralType([NotNull] RuleEngineParser.IntegralTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.floatingPointType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFloatingPointType([NotNull] RuleEngineParser.FloatingPointTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.classType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassType([NotNull] RuleEngineParser.ClassTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.boolType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolType([NotNull] RuleEngineParser.BoolTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.stringType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringType([NotNull] RuleEngineParser.StringTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.namespaceOrTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamespaceOrTypeName([NotNull] RuleEngineParser.NamespaceOrTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] RuleEngineParser.IdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.typeArgumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeArgumentList([NotNull] RuleEngineParser.TypeArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.argumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentList([NotNull] RuleEngineParser.ArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgument([NotNull] RuleEngineParser.ArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleEngineParser.qualifiedAliasMember"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitQualifiedAliasMember([NotNull] RuleEngineParser.QualifiedAliasMemberContext context);
}
} // namespace Min.RuleEngine
