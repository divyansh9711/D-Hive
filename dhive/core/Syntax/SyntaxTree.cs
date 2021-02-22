using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using dhive.core.Text;

namespace dhive.core.Syntax{
    public sealed class SyntaxTree{
        public SyntaxTree(ExpressionSyntax root, SyntaxToken endOfFileToken, ImmutableArray<Diagnostics> diagnostics){
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        public static SyntaxTree Parse(string text){
            var sourceText = SourceText.From(text);
            return Parse(sourceText);
        }

        public static SyntaxTree Parse(SourceText text){
            var parser = new Parser(text);
            return parser.Parse();
        }
        public ImmutableArray<Diagnostics> Diagnostics {get;}
        public ExpressionSyntax Root {get;}
        public SyntaxToken EndOfFileToken {get;}

        public static IEnumerable<SyntaxToken> ParseToken(SourceText text){
            var lexer = new Lexer(text);
            while(true){
                var token = lexer.Lex();
                if(token.Kind == SyntaxKind.EndOfFileToken){
                    break;
                }
                yield return token;
            }
        }

        public static IEnumerable<SyntaxToken> ParseToken(string text){
            var sourceText = SourceText.From(text);
            return ParseToken(sourceText);
        }
    }   
}
