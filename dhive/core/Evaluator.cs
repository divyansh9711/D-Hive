using System;
using System.Collections.Generic;
using dhive.core.Binding;
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
        private object EvaluateExpression(BoundExpression root)
        {
            switch (root.Kind)
            {
                case BoundNodeKind.LiteralExpression:
                    return EvaluateLiteral((BoundLiteralExpression)root);
                case BoundNodeKind.VariableExpression:
                    return EvaluateVariableExpression((BoundVariableExpression)root);
                case BoundNodeKind.AssignmentExpression:
                    return EvaluateAssignementExpression((BoundAssignmentExpression)root);
                case BoundNodeKind.BinaryExpression:
                    return EvaluateBinaryExpression((BoundBinaryExpression)root);
                case BoundNodeKind.UnaryExpression:
                    return EvaulateUnaryExpression((BoundUnaryExpression)root);
                default:
                    throw new Exception($"EXC: Unexpected operator {root.Kind}");
            }
        }
        private object EvaluateBinaryExpression(BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);
            switch (b.Op.Kind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return (int)left + (int)right;
                case BoundBinaryOperatorKind.Substraction:
                    return (int)left - (int)right;
                case BoundBinaryOperatorKind.Multiplication:
                    return (int)left * (int)right;
                case BoundBinaryOperatorKind.Division:
                    return (int)left / (int)right;
                case BoundBinaryOperatorKind.LogicalAnd:
                    return (bool)left && (bool)right;
                case BoundBinaryOperatorKind.LogicalOr:
                    return (bool)left || (bool)right;
                case BoundBinaryOperatorKind.Equals:
                    return Equals(left, right);
                case BoundBinaryOperatorKind.NotEquals:
                    return !Equals(left, right);
                default:
                    throw new Exception($"EXC: Invalid operator {b.Op.Kind}");
            }
        }
        private object EvaulateUnaryExpression(BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Operand);
            switch (u.Op.Kind)
            {
                case BoundUnaryOperatorKind.Identity:
                    return (int)operand;
                case BoundUnaryOperatorKind.Negation:
                    return -(int)operand;
                case BoundUnaryOperatorKind.LogicalNegation:
                    return !(bool)operand;
                default:
                    throw new Exception($"EXC: Invalid operator {u.Op.Kind}");

            }
        }
        private object EvaluateAssignementExpression(BoundAssignmentExpression a)
        {
            var value = EvaluateExpression(a.Expression);
            _variables[a.Variable] = value;
            return value;
        }
        private object EvaluateVariableExpression(BoundVariableExpression v)
        {
            return _variables[v.Variable];
        }
        private static object EvaluateLiteral(BoundLiteralExpression n)
        {
            return n.Value;
        }
    }
    
}