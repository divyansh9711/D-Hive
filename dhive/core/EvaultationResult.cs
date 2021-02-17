using dhive.core.Syntax;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace dhive.core{
   
     public sealed class EvaluationResult{
        public EvaluationResult(ImmutableArray<Diagnostics> diagnostics, object value){
            Diagnostics = diagnostics;
            Value = value;
        }

        public ImmutableArray<Diagnostics> Diagnostics { get; }
        public object Value { get; }
    }
}