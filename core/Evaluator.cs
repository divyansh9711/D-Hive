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
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                switch(b.OperatorKind){
                    case BoundBinaryOperatorKind.Addition:
                        return (int) left + (int) right;
                    case BoundBinaryOperatorKind.Substraction:
                        return (int) left - (int) right;
                    case BoundBinaryOperatorKind.Multiplication:
                        return (int) left * (int) right;
                    case BoundBinaryOperatorKind.Division:
                        return (int) left/(int) right;
                    case BoundBinaryOperatorKind.LogicalAnd:
                        return (bool) left && (bool) right;
                    case BoundBinaryOperatorKind.LogicalOr:
                        return (bool) left || (bool) right;
                    default:
                        throw new Exception($"EXC: Invalid operator {b.OperatorKind}");
                }
            }
            if(root is BoundUnaryExpression u){
                var operand = EvaluateExpression(u.Operand);
                switch(u.OperatorKind){
                    case BoundUnaryOperatorKind.Identity:
                        return (int) operand;
                    case BoundUnaryOperatorKind.Negation:
                        return - (int) operand;
                    case BoundUnaryOperatorKind.LogicalNegation:
                        return ! (bool) operand;
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