using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace dhive.core.Syntax{
    public sealed class SyntaxTree{
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, ImmutableArray<Diagnostics> diagnostics){
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        public static SyntaxTree Parse(string text){
            var parser = new Parser(text);
            return parser.Parse();
        }
        public ImmutableArray<Diagnostics> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOfFileToken {get;}

        public static IEnumerable<SyntaxToken> ParseToken(string text){
            var lexer = new Lexer(text);
            while(true){
                var token = lexer.Lex();
                if(token.Kind == SyntaxKind.EndOfFileToken){
                    break;
                }
                yield return token;
            }
        }
    }   
}
