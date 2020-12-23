using System;
namespace core.Binding{
    internal sealed class BoundLiteralExpression: BoundExpression{
        public BoundLiteralExpression(Object value){
            Value = value;
        }
        public Object Value {get;}
        public override Type Type => Value.GetType();
        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
    }
}