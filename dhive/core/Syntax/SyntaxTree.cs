using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace dhive.core.Syntax{
    public sealed class SyntaxTree{
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, IEnumerable<string> diagnostics){
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        public static SyntaxTree Parse(string text){
            var parser = new Parser(text);
            return parser.Parse();
        }
        public IEnumerable<string> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOfFileToken {get;}
    }   
}
