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
            var tokens = SyntaxTree.ParseToken(text);
            var token = Assert.Single(tokens);
            Assert.Equal(kind, token.Kind);
            Assert.Equal(text, token.Text);
        }

        public static IEnumerable<Object[]> GetTokensData(){
            foreach(var t in GetTokens()){
                yield return new object[] {t.kind, t.text};
            }
        }

        private static IEnumerable<(SyntaxKind kind,string text)> GetTokens(){
            return new[]{

                
                (SyntaxKind.PlusToken, "+"),
                (SyntaxKind.MinusToken, "-"),
                (SyntaxKind.StarToken, "*"),
                (SyntaxKind.SlashToken, "/"),
                (SyntaxKind.ExclamationToken, "!"),
                (SyntaxKind.EqualEqualToken, "=="),
                (SyntaxKind.ExclamationEqualToken, "!="),
                (SyntaxKind.AmpersandAmpersandToken, "&&"),
                (SyntaxKind.PipePipeToken, "||"),
                (SyntaxKind.OpenParenthesisToken, "("),
                (SyntaxKind.CloseParenthesisToken, ")"),
                (SyntaxKind.EqualToken, "="),
                (SyntaxKind.FalseKeyword, "false"),
                (SyntaxKind.TrueKeyword, "true"),

                (SyntaxKind.WhitespaceToken, " "),
                (SyntaxKind.WhitespaceToken, "  "),
                (SyntaxKind.WhitespaceToken, "\r"),
                (SyntaxKind.WhitespaceToken, "\n"),
                (SyntaxKind.WhitespaceToken, "\r\n"),
                (SyntaxKind.IndentiferToken, "abc"),
                (SyntaxKind.IndentiferToken, "a"),
                (SyntaxKind.NumberToken, "1"),
                (SyntaxKind.NumberToken, "23"),
                
            };

        }
    }
}
