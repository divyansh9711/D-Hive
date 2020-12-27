using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace dhive.core.Syntax{
    public enum SyntaxKind{
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        ExclamationToken,
        EqualEqualToken,
        ExclamationEqualToken,
        AmpersandToken,
        PipeToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        EqualToken,
        NoneToken,
        EndOfFileToken,
        NumberExpression,
        BinaryExpression,
        LiteralExpression,
        UnaryExpression,
        FalseKeyword,
        TrueKeyword,
        IndentiferToken,
        ParenthesizeExpression
    }
    public class SyntaxToken : SyntaxNode{
        public SyntaxToken(SyntaxKind kind, int position, string text, Object value){
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
        public override SyntaxKind Kind {get;}
        public int Position {get;}
        public string Text {get;}
        public Object Value {get;}

        public override IEnumerable<SyntaxNode> GetChildren(){
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
