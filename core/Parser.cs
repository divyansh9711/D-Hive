using System;
using System.Collections.Generic;

namespace core{
    class Parser{
        private int _position;
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
        }
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
            return new SyntaxToken(kind, Current.Position, null, null);
        } 

        public ExpressionSyntax Parse(){
            var left = ParsePrimaryExpression();
            while(Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken){
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);        
            }
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression(){
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
    
}
