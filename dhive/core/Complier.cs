using dhive.core.Syntax;
using dhive.core.Binding;
using dhive.core;
using core;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace dhive.core{
    public sealed class Compiler{
        public Compiler(SyntaxTree syntax){
            Syntax = syntax;
        }

        public SyntaxTree Syntax { get; }

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables){
            var binder = new Binder(variables);
            var boundExpression = binder.BindExpression(Syntax.Root);
            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToImmutableArray();
            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);
            var evaluator = new Evaluator(boundExpression, variables);
            var value = evaluator.Evaluate();
            return new EvaluationResult(ImmutableArray<Diagnostics>.Empty, value);

        }
    }
}