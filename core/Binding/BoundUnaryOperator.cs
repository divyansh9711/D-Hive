using System;
using core.Syntax;

namespace core.Binding{
    internal sealed class BoundUnaryOperator{
        private BoundUnaryOperator(SyntaxKind syntax, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
        {
            Syntax = syntax;
            Kind = kind;
            OperandType = operandType;
            ResultType = resultType;
        }
        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
        }
        public SyntaxKind SyntaxKind { get; }
        public SyntaxKind Syntax { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OperandType { get; }
        public Type ResultType { get; }

        private static BoundUnaryOperator[] _opertors = {
            new BoundUnaryOperator(SyntaxKind.ExclamationToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),
            new BoundUnaryOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
            new BoundUnaryOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
        };

        public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, Type operandType){
            foreach(var op in _opertors){
                if(op.SyntaxKind == syntaxKind && op.OperandType == operandType)
                    return op;
            }
            return null;
        }
    }
    
}