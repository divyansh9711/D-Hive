using System.Collections.Generic;
namespace dhive.core.Syntax{
    internal sealed class Lexer{
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        public Lexer(string text){
            _text = text;
        }
        private char Current => Peek(0);
        private char LookAhead => Peek(1);
        private char Peek(int offset){
            var index = _position + offset;
            if (_position >= _text.Length)
                return '\0';
            return _text[index];
        }
        
        private void Next(){
            _position++;
        }
        public SyntaxToken Lex(){

            if (_position >= _text.Length){
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);

            }

            if(char.IsDigit(Current)){
                var start = _position;
                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start,length);
                if(!int.TryParse(text,out var value))
                    _diagnostics.Add($"ERR: invalid assignment '{_text}' not valid Int32.");
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }
            if (char.IsWhiteSpace(Current)){
                var start = _position;
                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start,length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }
            if (char.IsLetter(Current)){
                var start = _position;
                while (char.IsLetter(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start,length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }
            switch(Current){
                case '+': return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                case '-': return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                case '/': return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                case '*': return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                case '(': return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
                case ')': return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
                case '&':{
                    if(LookAhead == '&') 
                        return new SyntaxToken(SyntaxKind.AmpersandToken, _position += 2, "&&", null);
                    break;
                }
                case '|':{
                    if(LookAhead == '|') 
                        return new SyntaxToken(SyntaxKind.PipeToken, _position += 2, "||", null);
                    break;
                }
                case '=':{
                    if(LookAhead == '=') 
                        return new SyntaxToken(SyntaxKind.EqualEqualToken, _position += 2, "==", null);
                    break;
                }
                case '!':{
                    if(LookAhead == '=') 
                        return new SyntaxToken(SyntaxKind.ExclamationEqualToken, _position += 2, "!=", null);
                    return new SyntaxToken(SyntaxKind.ExclamationToken, _position++, "!", null);
                }
            }
            _diagnostics.Add($"ERR: unrecognised character input: '{Current}");
            return new SyntaxToken(SyntaxKind.NoneToken, _position++, _text.Substring(_position -1, 1), null);
            
        }
    }
}