using dhive.core.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace dhive.core{
   
     public sealed class EvaluationResult{
        public EvaluationResult(IEnumerable<Diagnostics> diagnostics, object value){
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }

        public IReadOnlyList<Diagnostics> Diagnostics { get; }
        public object Value { get; }
    }
}