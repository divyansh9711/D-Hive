using System;
using System.Collections.Generic;

namespace core.Syntax{
    internal static class SyntaxFacts{
        public static int GetBinaryOperatorPrecednce(this SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PipeToken:
                    return 1;
                case SyntaxKind.AmpersandToken:
                    return 2;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 4;
                default:
                    return 0;
            }
        }
        public static int GetUnaryOperatorPrecednce(this SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.ExclamationToken:
                    return 5;
                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text){
             switch(text){
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IndentiferToken;
             }
        }
    }
}
