using System;
using core.Syntax;
using System.Collections.Generic;

namespace core.Binding{
    internal enum BoundNodeKind{
        LiteralExpression,
        UnaryExpression,
        BinaryExpression
    }
    internal enum BoundUnaryOperatorKind{
        Identity,
        Negation,
        
    }
    internal enum BoundBinaryOperatorKind{
        Addition,
        Substraction,
        Multiplication,
        Division
    }
    internal abstract class BoundNode{
        public abstract BoundNodeKind Kind {get;}
    }

    internal abstract class BoundExpression:BoundNode{
        public abstract Type Type {get;}
    }  

    internal sealed class BoundLiteralExpression: BoundExpression{
        public BoundLiteralExpression(Object value){
            Value = value;
        }
        public Object Value {get;}
        public override Type Type => Value.GetType();
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
    }

    internal sealed class BoundUnaryExpression:BoundExpression{
        public BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand){
            OperatorKind = operatorKind;
            Operand = operand;
        }

        public BoundUnaryOperatorKind OperatorKind {get;}
        public BoundExpression Operand {get;}
        public override Type Type => Operand.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    }  

    internal sealed class BoundBinaryExpression:BoundExpression{
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right){
            OperatorKind = operatorKind;
            Left = left;
            Right = right;
        }

        public BoundBinaryOperatorKind OperatorKind {get;}
        public BoundExpression Left {get;}

        public BoundExpression Right {get;}

        public override Type Type => Left.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
    }  

    internal sealed class Binder{
        private readonly List<String> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;

        public BoundExpression BindExpression(ExpressionSyntax syntax){
            switch (syntax.Kind){
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                default: throw new Exception($"Unexpected Syntax {syntax.Kind}");
            }
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax){
            var value = syntax.LietralToken.Value as int? ?? 0;
            return new BoundLiteralExpression(value);
        }
        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax){
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperatorKind = BindUnaryOperatorKind(syntax.OperatorToken.Kind, boundOperand.Type);
            if (boundOperatorKind == null){
                _diagnostics.Add($"TypeERR: Unary Operator '{syntax.OperatorToken.Text}' is not defined for type {boundOperand.Type}");
                return boundOperand;
            }
            return new BoundUnaryExpression(boundOperatorKind.Value ,boundOperand);
        }

        
        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax){
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperatorKind = BindLiteralOperatorKind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);
            if (boundOperatorKind == null){
                _diagnostics.Add($"TypeERR: Binary Operator '{syntax.OperatorToken.Text}' is not defined for types {boundLeft.Type} and {boundRight.Type}");
                return boundLeft;
            }
            return new BoundBinaryExpression(boundLeft, boundOperatorKind.Value, boundRight);
        }

        private BoundUnaryOperatorKind? BindUnaryOperatorKind(SyntaxKind kind, Type operandType){
            if (operandType != typeof(int))
                return null;
            switch(kind){
                case SyntaxKind.PlusToken:
                    return BoundUnaryOperatorKind.Identity;
                case SyntaxKind.MinusToken:
                    return BoundUnaryOperatorKind.Negation;
                default:
                    throw new Exception($"Unexpected unary operator {kind}");
            }
        }
        private BoundBinaryOperatorKind? BindLiteralOperatorKind(SyntaxKind kind, Type leftType, Type rightType){
             if (leftType != typeof(int) || rightType != typeof(int))
                return null;
            switch(kind){
                case SyntaxKind.PlusToken:
                    return BoundBinaryOperatorKind.Addition;
                case SyntaxKind.MinusToken:
                    return BoundBinaryOperatorKind.Substraction;
                case SyntaxKind.StarToken:
                    return BoundBinaryOperatorKind.Multiplication;
                case SyntaxKind.SlashToken:
                    return BoundBinaryOperatorKind.Division;
                default:
                    throw new Exception($"Unexpected binary operator {kind}");
            }
        }


    }
}