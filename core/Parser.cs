using System;
using System.Collections.Generic;

namespace core{
    class Parser{
        private int _position;
        private List<string> _diagnostics = new List<string>(); 
        private readonly SyntaxToken[] _tokens;
        public Parser(String text){
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(text);
            SyntaxToken token;
            do{
                token = lexer.NextToken();
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

        private SyntaxToken Match(SyntaxKind kind){
            if (Current.Kind == kind){
                return NextToken(); 
            }
            _diagnostics.Add($"ERR: Unexpected Token: '{Current.Kind}',expected '{kind}'");
            return new SyntaxToken(kind, Current.Position, null, null);
        } 

        public SyntaxTree Parse(){
            var expression = parseTerm();
            var eofToken = Match(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(expression, eofToken, _diagnostics);
        }

        private ExpressionSyntax ParseExpression(){
            return parseTerm();
        }
        private ExpressionSyntax parseTerm(){
            var left = parseFactor();
            while(Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken){
                var operatorToken = NextToken();
                var right = parseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);        
            }
            return left;
        }

        private ExpressionSyntax parseFactor(){
            var left = ParsePrimaryExpression();
            while(Current.Kind == SyntaxKind.StarToken || Current.Kind == SyntaxKind.SlashToken){
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);        
            }
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression(){
            if (Current.Kind == SyntaxKind.OpenParenthesisToken){
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left,expression,right);
            }
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
    
}
