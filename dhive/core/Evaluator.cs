using System;
using System.Collections.Generic;
using dhive.core.Binding;
using dhive.core.Syntax;
using dhive.core;
namespace core{
    internal sealed  class Evaluator{
        private readonly BoundExpression _root;
        private readonly Dictionary<VariableSymbol, object> _variables;

        public Evaluator (BoundExpression root, Dictionary<VariableSymbol, object> variables)
        {
            _root = root;
            _variables = variables;
        }
        public object Evaluate(){
            return EvaluateExpression(_root);
        }
        private object EvaluateExpression(BoundExpression root){
            if(root is BoundLiteralExpression n)
                return n.Value;
            if(root is BoundVariableExpression v)
                return _variables[v.Variable];
            if(root is BoundAssignmentExpression a){
                var value = EvaluateExpression(a.Expression);
                _variables[a.Variable] = value;
                return value;
            }
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