using System;
using System.Collections.Generic;

namespace dhive.core.Syntax{
    internal sealed class Parser{
        private int _position;
        private DiagnosticsBag _diagnostics = new DiagnosticsBag(); 
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

        public IEnumerable<Diagnostics> Diagnostics => _diagnostics;
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
            _diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, Current.Position, null, null);
        } 

        public SyntaxTree Parse(){
            var expression = ParseExpression();
            var eofToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(expression, eofToken, _diagnostics);
        }

        private ExpressionSyntax ParseBinaryExpression(int parentPrecedence = 0){
            ExpressionSyntax left;
            var unaryExpressionPrecednce = Current.Kind.GetUnaryOperatorPrecednce();
            if(unaryExpressionPrecednce != 0 && unaryExpressionPrecednce >= parentPrecedence){
                var operatorToken = NextToken();
                var operand = ParseBinaryExpression(unaryExpressionPrecednce);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            }else{
                left = ParsePrimaryExpression();
            }
            while(true){
                var precedence = Current.Kind.GetBinaryOperatorPrecednce();
                if(precedence == 0 || precedence <= parentPrecedence)
                    break;
                var operatorToken =  NextToken();
                var right = ParseBinaryExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }
            return left;
        }

        private ExpressionSyntax ParseExpression() => ParseAssignmentExpression();

        private ExpressionSyntax ParseAssignmentExpression()
        {
            if(Peek(0).Kind == SyntaxKind.IndentiferToken && Peek(1).Kind == SyntaxKind.EqualToken){
                var left = NextToken();
                var operatorToken = NextToken();
                var right = ParseAssignmentExpression();
                return new AssignmentExpressionSyntax(left, operatorToken, right);
            }
            return ParseBinaryExpression();
        }

        private ExpressionSyntax ParsePrimaryExpression(){
            switch(Current.Kind){
                case SyntaxKind.OpenParenthesisToken:{
                       var left = NextToken();
                    var expression = ParseBinaryExpression();
                    var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                    return new ParenthesizedExpressionSyntax(left,expression,right);
                }
                case SyntaxKind.TrueKeyword:
                case SyntaxKind.FalseKeyword:{
                    var keywordToken = NextToken();
                    var value = keywordToken.Kind == SyntaxKind.TrueKeyword;
                    return new LiteralExpressionSyntax(keywordToken, value);
                }
                case SyntaxKind.IndentiferToken:{
                    var identifierToken = NextToken();
                    return new NameExpressionSyntax(identifierToken);
                }
                default:{
                    var numberToken = MatchToken(SyntaxKind.NumberToken);
                    return new LiteralExpressionSyntax(numberToken);
                }
            }
        }
    }
    
}
