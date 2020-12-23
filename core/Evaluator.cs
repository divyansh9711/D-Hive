using System;
using System.Collections.Generic;
using core.Syntax;
using core.Binding;
namespace core{
    internal sealed  class Evaluator{
        private readonly BoundExpression _root;
        public Evaluator (BoundExpression root){
            _root = root;
        }
        public object Evaluate(){
            return EvaluateExpression(_root);
        }
        private object EvaluateExpression(BoundExpression root){
            if(root is BoundLiteralExpression n)
                return n.Value;
            if(root is BoundBinaryExpression b){
                var left = (int) EvaluateExpression(b.Left);
                var right = (int) EvaluateExpression(b.Right);
                switch(b.OperatorKind){
                    case BoundBinaryOperatorKind.Addition:
                        return left + right;
                    case BoundBinaryOperatorKind.Substraction:
                        return left - right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return left * right;
                    case BoundBinaryOperatorKind.Division:
                        return left/right;
                    default:
                        throw new Exception($"EXC: Invalid operator {b.OperatorKind}");
                }
            }
            if(root is BoundUnaryExpression u){
                var operand = (int) EvaluateExpression(u.Operand);
                switch(u.OperatorKind){
                    case BoundUnaryOperatorKind.Identity:
                        return operand;
                    case BoundUnaryOperatorKind.Negation:
                        return -operand;
                    default:
                        throw new Exception($"EXC: Invalid operator {u.OperatorKind}");

                }
            }
            // if (root is ParenthesizedExpressionSyntax p){
            //     return EvaluateExpression(p.Expression);
            // }
            throw new Exception($"EXC: Invalid operator {root.Kind}");
        }
    }
    
}