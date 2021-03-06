using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using dhive.core.Text;

namespace dhive.core.Syntax
{
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
        public override TextSpan Span => new TextSpan(Position, Text?.Length ?? 0);

        
    }
}
