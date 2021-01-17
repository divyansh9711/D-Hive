using System;
using System.Collections.Generic;
using dhive.core;
using dhive.core.Syntax;
using Xunit;

namespace dhive.test.core
{
    public class EvaluationTests{
        [Theory]
        [InlineData("1", 1)]
        [InlineData("+1", 1)]
        [InlineData("-1", -1)]
        [InlineData("1 + 2", 3)]
        [InlineData("1 * 2", 2)]
        [InlineData("9 / 3", 3)]
        [InlineData("(1 + 2) * 10", 30)]
        [InlineData("12 == 12", true)]
        [InlineData("12 == 123", false)]
        [InlineData("12 != 123", true)]
        [InlineData("12 != 12", false)]
        [InlineData("true == true", true)]
        [InlineData("false == false", true)]
        [InlineData("true != false", true)]
        [InlineData("true != true", false)]


        [InlineData("true || false", true)]
        [InlineData("true && false", false)]
        [InlineData("true && true", true)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("!false", true)]
        [InlineData("!true", false)]
        [InlineData("(a=10) * a", 100)]

        public void SyntaxFact_GetText_RoundTrips(string text, object expectedResult){
            var syntaxTree = SyntaxTree.Parse(text);
            var compilation = new Compiler(syntaxTree);
            var variables = new Dictionary<VariableSymbol, object>();
            var result = compilation.Evaluate(variables);
            Assert.Empty(result.Diagnostics);
            Assert.Equal(expectedResult, result.Value);
        }
        
    }
        
}
