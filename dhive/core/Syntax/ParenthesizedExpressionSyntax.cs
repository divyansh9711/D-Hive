using System.Collections.Generic;

namespace dhive.core.Syntax
{
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

}
