using System;
using System.Collections;
using System.Collections.Generic;

namespace core.Syntax{
    
    sealed class SyntaxTree{
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, List<string> diagnostics){
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        public static SyntaxTree Parse(string text){
            var parser = new Parser(text);
            return parser.Parse();
        }
        public IReadOnlyList<string> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOfFileToken {get;}
    }   
}
