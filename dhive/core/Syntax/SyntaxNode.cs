using System.Collections;
using System;
using System.Collections.Generic;

namespace dhive.core.Syntax{
    public abstract class SyntaxNode{
        public abstract SyntaxKind Kind {get;}
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
    public abstract class ExpressionSyntax : SyntaxNode{}

    public sealed class LiteralExpressionSyntax: ExpressionSyntax{
        public LiteralExpressionSyntax(SyntaxToken literalToken)
            :this(literalToken, literalToken.Value)
        {

        }
        public LiteralExpressionSyntax(SyntaxToken literalToken, object value){
            LietralToken = literalToken;
            Value = value;
        }
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public object Value { get; }
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return LietralToken;
        }
        public SyntaxToken LietralToken {get;}    
    }

    public sealed class BinaryExpressionSyntax: ExpressionSyntax{
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right){
            Left = left;    
            Right = right;
            OperatorToken = operatorToken;
        }
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
        public ExpressionSyntax Left {get; }
        public ExpressionSyntax Right {get; }
        public SyntaxToken OperatorToken {get ;}
    }

    public sealed class ParenthesizedExpressionSyntax: ExpressionSyntax{
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesis, ExpressionSyntax expression, SyntaxToken closeParenthesis){
            OpenParenthesis = openParenthesis;
            Expression = expression;
            CloseParenthesis = closeParenthesis;
        }

        public SyntaxToken OpenParenthesis {get;}
        public SyntaxToken CloseParenthesis {get;}
        public ExpressionSyntax Expression {get;}
        public override SyntaxKind Kind => SyntaxKind.ParenthesizeExpression;
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return OpenParenthesis;
            yield return Expression;
            yield return CloseParenthesis;
        }


    }
    sealed class UnaryExpressionSyntax: ExpressionSyntax{
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand){
            Operand = operand;
            OperatorToken = operatorToken;
        }
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return OperatorToken;
            yield return Operand;
        }
        public SyntaxToken OperatorToken {get ;}
        public ExpressionSyntax Operand {get; }
        
    }

}