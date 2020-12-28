using System.Collections.Generic;

namespace dhive.core.Syntax
{
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
