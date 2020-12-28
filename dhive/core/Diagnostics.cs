using System;

namespace dhive.core
{
    public sealed class Diagnostics{
        public Diagnostics(TextSpan span, String message){
            Span = span;
            Message = message;
        }

        public TextSpan Span { get; }
        public string Message { get; }
        public override string ToString() => Message;
        
    }
}