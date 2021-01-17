using System;
using System.Collections.Generic;
using dhive.core.Syntax;
using Xunit;

namespace dhive.test.core.Syntax
{
    public class ParserTest{
        [Theory]
        [MemberData(nameof(GetBinaryOperatorPairsData))]
        public void Parser_BinaryExpression_HonorsPrecedences(SyntaxKind op1, SyntaxKind op2){
            var op1Precedence = SyntaxFacts.GetBinaryOperatorPrecednce(op1);
            var op2Precedence = SyntaxFacts.GetBinaryOperatorPrecednce(op2);
            var op1Text = SyntaxFacts.GetText(op1);
            var op2Text = SyntaxFacts.GetText(op2);
            var text = $"a {op1Text} b {op2Text} c";
            var expression = SyntaxTree.Parse(text).Root;
            if(op1Precedence >= op2Precedence){
                using (var e = new AssertingEnumerator(expression)){
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken,"a");
                    e.AssertToken(op1, op1Text);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken,"b");
                    e.AssertToken(op2, op2Text);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken,"c");
                }
            }else{
                using (var e = new AssertingEnumerator(expression)){
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken, "a");
                    e.AssertToken(op1, op1Text);
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken,"b");
                    e.AssertToken(op2, op2Text);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken,"c");

                }
            }
        }
        public static IEnumerable<object[]> GetBinaryOperatorPairsData(){
            foreach(var op1 in SyntaxFacts.GetBinaryOperatorKinds()){
                foreach(var op2 in SyntaxFacts.GetBinaryOperatorKinds()){
                    yield return new object[]{op1,op2};
                }
            }
            
        } 
        [Theory]
        [MemberData(nameof(GetUnaryOperatorPairsData))]
        public void Parser_UnaryExpression_HonorsPrecedences(SyntaxKind unaryKind, SyntaxKind binaryKind){
            var unaryKindPrecedence = SyntaxFacts.GetUnaryOperatorPrecednce(unaryKind);
            var binaryKindPrecedence = SyntaxFacts.GetBinaryOperatorPrecednce(binaryKind);
            var unaryKindText = SyntaxFacts.GetText(unaryKind);
            var binaryKindText = SyntaxFacts.GetText(binaryKind);
            var text = $"{unaryKindText} a {binaryKindText} b";
            var expression = SyntaxTree.Parse(text).Root;
            if(unaryKindPrecedence >= binaryKindPrecedence){
                using (var e = new AssertingEnumerator(expression)){
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.UnaryExpression);
                    e.AssertToken(unaryKind, unaryKindText);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken, "a");
                    e.AssertToken(binaryKind,binaryKindText);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken, "b");
                }
            }else{
                using (var e = new AssertingEnumerator(expression)){
                    e.AssertNode(SyntaxKind.UnaryExpression);
                    e.AssertToken(unaryKind, unaryKindText);
                    e.AssertNode(SyntaxKind.BinaryExpression);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken, "a");
                    e.AssertToken(binaryKind,binaryKindText);
                    e.AssertNode(SyntaxKind.NameExpression);
                    e.AssertToken(SyntaxKind.IndentiferToken, "b");
                }
            }
        }
        public static IEnumerable<object[]> GetUnaryOperatorPairsData(){
            foreach(var unaryKind in SyntaxFacts.GetUnaryOperatorKinds()){
                foreach(var binaryKind in SyntaxFacts.GetBinaryOperatorKinds()){
                    yield return new object[]{unaryKind, binaryKind};
                }
            }
            
        } 
           
    }

        
}
