using System.Collections.Generic;

namespace dhive.core.Syntax
{
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
        
        public SyntaxToken LietralToken {get;}    
    }

}
