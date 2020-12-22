using System;
using System.Collections.Generic;

namespace core{
    internal sealed class Parser{
        private int _position;
        private List<string> _diagnostics = new List<string>(); 
        private readonly SyntaxToken[] _tokens;
        public Parser(String text){
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do{
                token = lexer.Lex();
                if(token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.NoneToken){
                    tokens.Add(token);
                }
            } while(token.Kind != SyntaxKind.EndOfFileToken);
            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;
        private SyntaxToken Peek(int offset){
            var index = _position + offset;
            if (index >= _tokens.Length){
                return _tokens[_tokens.Length - 1];
            }
            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken(){
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind){
            if (Current.Kind == kind){
                return NextToken(); 
            }
            _diagnostics.Add($"ERR: Unexpected Token: '{Current.Kind}',expected '{kind}'");
            return new SyntaxToken(kind, Current.Position, null, null);
        } 

        public SyntaxTree Parse(){
            var expression = ParseExpression();
            var eofToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(expression, eofToken, _diagnostics);
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0){
            var left = ParsePrimaryExpression();
            while(true){
                var precedence = GetBinaryOperatorPrecednce(Current.Kind);
                if(precedence == 0 || precedence <= parentPrecedence)
                    break;
                var operatorToken =  NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        private static int GetBinaryOperatorPrecednce(SyntaxKind kind){
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

        private ExpressionSyntax ParsePrimaryExpression(){
            if (Current.Kind == SyntaxKind.OpenParenthesisToken){
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left,expression,right);
            }
            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
    
}
