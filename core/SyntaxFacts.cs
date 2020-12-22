using System;
using System.Collections.Generic;

namespace core{
    internal static class SyntaxFacts{
        public static int GetBinaryOperatorPrecednce(this SyntaxKind kind){
            switch(kind){
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
