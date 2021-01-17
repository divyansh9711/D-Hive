using System;
using System.Collections.Generic;

namespace dhive.core.Syntax{
    public static class SyntaxFacts{
        public static int GetBinaryOperatorPrecednce(this SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PipePipeToken:
                    return 1;
                case SyntaxKind.AmpersandAmpersandToken:
                    return 2;
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;
                case SyntaxKind.EqualEqualToken:
                case SyntaxKind.ExclamationEqualToken:
                    return 4;
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;
                default:
                    return 0;
            }
        }
        public static int GetUnaryOperatorPrecednce(this SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.ExclamationToken:
                    return 6;
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
        public static IEnumerable<SyntaxKind> GetBinaryOperatorKinds(){
            var kinds = (SyntaxKind[]) Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds){
                if(GetUnaryOperatorPrecednce(kind) > 0){
                    yield return kind;
                }
            }
        }

          public static IEnumerable<SyntaxKind> GetUnaryOperatorKinds(){
            var kinds = (SyntaxKind[]) Enum.GetValues(typeof(SyntaxKind));
            foreach (var kind in kinds){
                if(GetBinaryOperatorPrecednce(kind) > 0){
                    yield return kind;
                }
            }
        }

        public static string GetText(SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PlusToken:return "+";
                case SyntaxKind.MinusToken: return "-";
                case SyntaxKind.StarToken: return "*";
                case SyntaxKind.SlashToken: return "/";
                case SyntaxKind.ExclamationToken: return "!";
                case SyntaxKind.EqualEqualToken: return "==";
                case SyntaxKind.ExclamationEqualToken: return "!=";
                case SyntaxKind.AmpersandAmpersandToken: return "&&";
                case SyntaxKind.PipePipeToken: return "||";
                case SyntaxKind.OpenParenthesisToken: return "(";
                case SyntaxKind.CloseParenthesisToken: return ")";
                case SyntaxKind.EqualToken: return "=";
                case SyntaxKind.FalseKeyword: return "false";
                case SyntaxKind.TrueKeyword: return "true";
                default: return "null";
            }
        }
    }
}
