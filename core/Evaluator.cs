using System;
using System.Collections.Generic;

namespace core{
    class Evaluator{
        private readonly ExpressionSyntax _root;
        public Evaluator (ExpressionSyntax root){
            _root = root;
        }
        public int Evaluate(){
            return EvaluateExpression(_root);
        }
        private int EvaluateExpression(ExpressionSyntax root){
            if(root is LiteralExpressionSyntax n)
                return (int) n.LietralToken.Value;
            if(root is BinaryExpressionSyntax b){
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);
                switch(b.OperatorToken.Kind){
                    case SyntaxKind.PlusToken:
                        return left + right;
                    case SyntaxKind.MinusToken:
                        return left - right;
                    case SyntaxKind.StarToken:
                        return left * right;
                    case SyntaxKind.SlashToken:
                        return left/right;
                    default:
                        throw new Exception($"EXC: Invalid operator {b.OperatorToken.Kind}");
                }
            }
            if (root is ParenthesizedExpressionSyntax p){
                return EvaluateExpression(p.Expression);
            }
            throw new Exception($"EXC: Invalid operator {root.Kind}");
        }
    }
    
}