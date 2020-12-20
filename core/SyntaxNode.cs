using System.Collections;
using System;
using System.Collections.Generic;

namespace core{
    abstract class SyntaxNode{
        public abstract SyntaxKind Kind {get;}
        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
    abstract class ExpressionSyntax : SyntaxNode{}

    sealed class NumberExpressionSyntax: ExpressionSyntax{
        public NumberExpressionSyntax(SyntaxToken numberToken){
            NumberToken = numberToken;
        }
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return NumberToken;
        }
        public SyntaxToken NumberToken {get;}    
    }

    sealed class BinaryExpressionSyntax: ExpressionSyntax{
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
        private ExpressionSyntax Left {get; }
        private ExpressionSyntax Right {get; }
        private SyntaxToken OperatorToken {get ;}
    }
    
}
