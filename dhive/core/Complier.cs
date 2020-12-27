using dhive.core.Syntax;
using dhive.core.Binding;
using dhive.core;
using core;
using System.Linq;
using System;

namespace dhive.core{
    public sealed class Compiler{
        public Compiler(SyntaxTree syntax){
            Syntax = syntax;
        }

        public SyntaxTree Syntax { get; }

        public EvaluationResult Evaluate(){
            var binder = new Binder();
            var boundExpression = binder.BindExpression(Syntax.Root);
            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();
            if (diagnostics.Any())
                return new EvaluationResult(diagnostics,null);
            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<String>(),value);

        }
    }
}