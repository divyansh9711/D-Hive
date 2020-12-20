using System;
namespace core{
    enum SyntaxKind{
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        EqualToken,
        NoneToken,
        EndOfFileToken
    }
    class SyntaxToken{
        public SyntaxToken(SyntaxKind kind, int position, string text, Object value){
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
        public SyntaxKind Kind {get;}
        public int Position {get;}
        public string Text {get;}
        public Object Value {get;}
    }
}
