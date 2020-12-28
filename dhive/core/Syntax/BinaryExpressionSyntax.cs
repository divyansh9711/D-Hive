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
        public override IEnumerable<SyntaxNode> GetChildren(){
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
        public ExpressionSyntax Left {get; }
        public ExpressionSyntax Right {get; }
        public SyntaxToken OperatorToken {get ;}
    }
}
