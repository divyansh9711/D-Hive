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
                switch(b.Op.Kind){
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
                    case BoundBinaryOperatorKind.Equals:
                        return Equals(left, right);
                    case BoundBinaryOperatorKind.NotEquals:
                        return !Equals(left, right);
                    default:
                        throw new Exception($"EXC: Invalid operator {b.Op.Kind}");
                }
            }
            if(root is BoundUnaryExpression u){
                var operand = EvaluateExpression(u.Operand);
                switch(u.Op.Kind){
                    case BoundUnaryOperatorKind.Identity:
                        return (int) operand;
                    case BoundUnaryOperatorKind.Negation:
                        return - (int) operand;
                    case BoundUnaryOperatorKind.LogicalNegation:
                        return ! (bool) operand;
                    default:
                        throw new Exception($"EXC: Invalid operator {u.Op.Kind}");

                }
            }
            // if (root is ParenthesizedExpressionSyntax p){
            //     return EvaluateExpression(p.Expression);
            // }
            throw new Exception($"EXC: Invalid operator {root.Kind}");
        }
    }
    
}