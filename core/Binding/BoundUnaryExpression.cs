using System;    
namespace core.Binding{
    internal sealed class BoundUnaryExpression:BoundExpression{
        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand){
            Op = op;
            Operand = operand;
        }

        public BoundUnaryOperator Op { get; }
        public BoundExpression Operand {get;}
        public override Type Type => Operand.Type;
        public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    }  

}