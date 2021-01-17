using System;
using System.Collections.Generic;
namespace dhive.core.Syntax{
    internal sealed class Lexer{
        private readonly string _text;
        private readonly DiagnosticsBag _diagnostics = new DiagnosticsBag();

        private int _position;
        private int _start;
        private SyntaxKind _kind;
        private object _value;
        public DiagnosticsBag Diagnostics => _diagnostics;
        public Lexer(string text){
            _text = text;
        }
        private char Current => Peek(0);
        private char LookAhead => Peek(1);
        private char Peek(int offset){
            var index = _position + offset;
            if (index >= _text.Length)
                return '\0';
            return _text[index];
        }
        
        public SyntaxToken Lex(){
            _start = _position;
            _kind = SyntaxKind.NoneToken;
            _value = null;
            switch(Current){
                case '\0': 
                    _kind = SyntaxKind.EndOfFileToken;
                    break;
                case '+': 
                    _kind = SyntaxKind.PlusToken;
                    _position++;
                    break;
                case '-': 
                    _kind = SyntaxKind.MinusToken;
                    _position++;
                    break;
                case '/': 
                    _kind = SyntaxKind.SlashToken;
                    _position++;
                    break;
                case '*': 
                    _kind = SyntaxKind.StarToken;
                    _position++;
                    break;
                case '(': 
                    _kind = SyntaxKind.OpenParenthesisToken;
                    _position++;
                    break;
                case ')': 
                    _kind = SyntaxKind.CloseParenthesisToken;
                    _position++;
                    break;
                case '&':
                    if(LookAhead == '&'){
                        _position += 2;
                        _kind = SyntaxKind.AmpersandAmpersandToken;
                        break;
                    } 
                    break;
                case '|':
                    if(LookAhead == '|') {
                        _position += 2;
                        _kind = SyntaxKind.PipePipeToken;
                        break;
                    }
                    break;
                case '=':
                    _position++;
                    if (Current != '=')
                        _kind = SyntaxKind.EqualToken;
                    else
                    {
                        _position++;
                        _kind = SyntaxKind.EqualEqualToken;
                    }
                    break;
                case '!':
                    _position++;
                    if (Current != '=')
                    {
                        _kind = SyntaxKind.ExclamationToken;
                    }
                    else
                    {
                        _position++;
                        _kind = SyntaxKind.ExclamationEqualToken;
                    }
                    break;
                case '0': case '1': case '2': case '3': case '4':
                case '5': case '6': case '7': case '8': case '9':
                    ReadNumberToken();
                    break;
                case ' ': case '\t': case '\n': case '\r':
                    ReadWhiteSpaceToken();
                    break;
                default: 
                    if (char.IsLetter(Current)){
                        ReadIdentiferOrKeyToken();
                    }else if (char.IsWhiteSpace(Current)){
                        ReadWhiteSpaceToken();
                    }else{
                        _diagnostics.ReportUnrecognisedCharacter(_position, Current);
                        _position++;
                    }
                    break; 
            }
        
            var length = _position - _start;
            var text = SyntaxFacts.GetText(_kind);
            if(text == null){
                text = _text.Substring(_start, length);
            }
            return new SyntaxToken(_kind, _start, text, _value);
        }

        private void ReadIdentiferOrKeyToken()
        {
            while (char.IsLetter(Current))
                _position++;
            var length = _position - _start;
            var text = _text.Substring(_start, length);
            _kind = SyntaxFacts.GetKeywordKind(text);
        }

        private void ReadWhiteSpaceToken()
        {
            while (char.IsWhiteSpace(Current))
                _position++;
            _kind = SyntaxKind.WhitespaceToken;
        }

        private void ReadNumberToken()
        {
            while (char.IsDigit(Current))
                _position++;

            var length = _position - _start;
            var text = _text.Substring(_start, length);
            if (!int.TryParse(text, out var value))
                _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), _text, typeof(int));
            _kind = SyntaxKind.NumberToken;
            _value = value;
        }
    }
}
