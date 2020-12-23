using System;
namespace core.Binding{
    internal sealed class BoundBinaryExpression:BoundExpression{
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right){
            Op = op;
            Left = left;
            Right = right;
        }

        public BoundBinaryOperator Op {get;}
        public BoundExpression Left {get;}

        public BoundExpression Right {get;}

        public override Type Type => Left.Type;
        public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;
    } 
}