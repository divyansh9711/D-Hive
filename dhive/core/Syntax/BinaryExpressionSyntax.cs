using System.Collections.Generic;

namespace dhive.core.Syntax
{
    public sealed class BinaryExpressionSyntax: ExpressionSyntax{
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right){
            Left = left;    
            Right = right;
            OperatorToken = operatorToken;
        }
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
       
        public ExpressionSyntax Left {get; }
        public ExpressionSyntax Right {get; }
        public SyntaxToken OperatorToken {get ;}
    }
}
