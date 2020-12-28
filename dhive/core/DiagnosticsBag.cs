using System.Collections.Generic;
using System.Collections;
using dhive.core;
using System;
using dhive.core.Syntax;

namespace dhive.core
{
    public sealed class DiagnosticsBag: IEnumerable<Diagnostics>{

        public IEnumerator<Diagnostics> GetEnumerator() => _diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()  => GetEnumerator();
        public readonly List<Diagnostics> _diagnostics = new List<Diagnostics>();

        private void Report(TextSpan span , string message){
            _diagnostics.Add(new Diagnostics(span, message));
        }
        public void ReportInvalidNumber(TextSpan span, string text, Type type){
            var message = $"ERR: The number {text}, is not Valid {type}";
            Report(span, message);
        }

        internal void AddRange(DiagnosticsBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        public void ReportUnrecognisedCharacter(int position, char character){
            var message = $"ERR: Unrecognised Character, {character}";
            Report(new TextSpan(position, 1), message);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind found, SyntaxKind expected){
            var message = $"ERR: Unexpected Token: '{found}',expected '{expected}'";
            Report(span, message);
        }

        public void ReportUnaryTypeError(TextSpan span, string text, Type type)
        {
            var message =   $"TypeERR: Unary Operator '{text}' is not defined for type {type}";
            Report(span, message);
        }

        public void ReportBinaryTypeError(TextSpan span, string text, Type leftType, Type rightType){
            var message =   $"TypeERR: Binary Operator '{text}' is not defined for type {leftType} and {rightType}";
            Report(span, message);        
        }

        public void ReportUndefinedName(TextSpan span, string text)
        {
            var message = $"NameERR: Variable '{text}' dose not exist";
            Report(span, message);        
        }
    }
}