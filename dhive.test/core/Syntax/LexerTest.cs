using System;
using System.Collections.Generic;
using dhive.core.Syntax;
using Xunit;

namespace dhive.test.core.Syntax
{
    public class LexerTest
    {
        [Theory]
        [MemberData(nameof(GetTokensData))]
        public void Lexer_Lex_Test(SyntaxKind kind, string text){
            var tokens = SyntaxTree.Parse(text);
            
        }

        public static IEnumerable<Object[]> GetTokensData(){
            foreach(var t in GetTokens()){
                yield return new object[] {t.kind, t.text};
            }
        }

        private static IEnumerable<(SyntaxKind kind,string text)> GetTokens(){
            return new[]{
                (SyntaxKind.IndentiferToken, "abc"),
                (SyntaxKind.IndentiferToken, "a"),
                (SyntaxKind.IndentiferToken, "asdf"),
                (SyntaxKind.IndentiferToken, "adfcv")
            };
        }
    }
}
