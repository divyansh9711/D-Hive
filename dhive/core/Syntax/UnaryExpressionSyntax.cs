using System.Collections.Generic;

namespace dhive.core.Syntax
{
    sealed class UnaryExpressionSyntax: ExpressionSyntax{
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand){
            Operand = operand;
            OperatorToken = operatorToken;
        }
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        
        public SyntaxToken OperatorToken {get ;}
        public ExpressionSyntax Operand {get; }
        
    }

}
