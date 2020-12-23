using System;
namespace core.Binding{
   
    internal abstract class BoundExpression:BoundNode{
        public abstract Type Type {get;}
    }  
}