using System;
using dhive.core.Syntax;
using dhive.core;
using System.Collections.Generic;
namespace dhive.core.Binding{
    internal sealed class Binder{
        private readonly DiagnosticsBag _diagnostics = new DiagnosticsBag();
        public DiagnosticsBag Diagnostics => _diagnostics;

        public BoundExpression BindExpression(ExpressionSyntax syntax){
            switch (syntax.Kind){
                case SyntaxKind.LiteralExpression:
                    return BindLiteralExpression((LiteralExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                case SyntaxKind.ParenthesizeExpression:
                    return BindExpression(((ParenthesizedExpressionSyntax)syntax).Expression);
                default: throw new Exception($"Unexpected Syntax {syntax.Kind}");
            }
        }

        private BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax){
            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }
        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax){
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperator = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);
            if (boundOperator == null){
                _diagnostics.ReportUnaryTypeError(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundOperand.Type);
                return boundOperand;
            } 
            return new BoundUnaryExpression(boundOperator, boundOperand);
        }

        
        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax){
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);
            if (boundOperator == null){
                _diagnostics.ReportBinaryTypeError(syntax.OperatorToken.Span, syntax.OperatorToken.Text, boundLeft.Type, boundRight.Type);
                return boundLeft;
            }
            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }

    }
}