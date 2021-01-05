using System;
using System.Collections.Generic;
using System.Linq;
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

        [Theory]
        [MemberData(nameof(GetTokensPairData))]
        public void Lexer_Lex_TokenPairTest(SyntaxKind t1Kind, string t1Text, SyntaxKind t2Kind, string t2Text){
            var text = t1Text + t2Text;
            var tokens = SyntaxTree.ParseToken(text).ToArray();
            Assert.Equal(2, tokens.Length);
            Assert.Equal(tokens[0].Kind, t1Kind);
            Assert.Equal(tokens[0].Text, t1Text);
            Assert.Equal(tokens[1].Kind, t2Kind);
            Assert.Equal(tokens[1].Text, t2Text);
        }

        public static IEnumerable<Object[]> GetTokensData(){
            foreach(var t in GetTokens().Concat(GetSeparators())){
                yield return new object[] {t.kind, t.text};
            }
        }

         public static IEnumerable<Object[]> GetTokensPairData(){
            foreach(var t in GetTokenPairs()){
                yield return new object[] {t.t1Kind, t.t1Text, t.t2Kind, t.t2Text};
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
                (SyntaxKind.IndentiferToken, "abc"),
                (SyntaxKind.IndentiferToken, "a"),
                (SyntaxKind.NumberToken, "1"),
                (SyntaxKind.NumberToken, "23"),
                
            };

        }

        private static IEnumerable<(SyntaxKind kind,string text)> GetSeparators(){
            return new[]{
                (SyntaxKind.WhitespaceToken, " "),
                (SyntaxKind.WhitespaceToken, "  "),
                (SyntaxKind.WhitespaceToken, "\r"),
                (SyntaxKind.WhitespaceToken, "\n"),
                (SyntaxKind.WhitespaceToken, "\r\n"),   
            };

        }

        private static bool RequiresSeprator(SyntaxKind t1Kind, SyntaxKind t2Kind){
            var t1isKeyword = t1Kind.ToString().EndsWith("Keyword");
            var t2isKeyword = t2Kind.ToString().EndsWith("Keyword");

            if(t1Kind == SyntaxKind.IndentiferToken && t2Kind == SyntaxKind.IndentiferToken){
                return true;
            }
            if(t1isKeyword && t2isKeyword){
                return true;
            }
            if((t1isKeyword && t2Kind == SyntaxKind.IndentiferToken) || (t2isKeyword && t1Kind == SyntaxKind.IndentiferToken)){
                return true;
            }
            if(t1Kind == SyntaxKind.NumberToken && t2Kind == SyntaxKind.NumberToken){
                return true;
            }
            if(t1Kind == SyntaxKind.ExclamationToken && t2Kind == SyntaxKind.EqualEqualToken){
                return true;
            }
            if(t1Kind == SyntaxKind.ExclamationToken && t2Kind == SyntaxKind.EqualToken){
                return true;
            }
            if(t1Kind == SyntaxKind.EqualToken && t2Kind == SyntaxKind.EqualEqualToken){
                return true;
            }
            if(t1Kind == SyntaxKind.EqualToken && t2Kind == SyntaxKind.EqualToken){
                return true;
            }
            return false;
        }

         private static IEnumerable<(SyntaxKind t1Kind, string t1Text, SyntaxKind t2Kind, string t2Text)> GetTokenPairs(){
            foreach(var t1 in GetTokens()){
                foreach(var t2 in GetTokens()){
                    if(RequiresSeprator(t1.kind, t2.kind)) continue;
                    yield return (t1.kind, t1.text, t2.kind, t2.text);
                }
            }    
        }
    }  
        
}
