using System.Linq;
using System;
using core.Syntax;
namespace core
{
    internal static class Program
    {
        static void Main(){
            var showTree = false;
            while (true)
            {
                Console.Write(">");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;
                if(line == "/showtree"){
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse tree": "Not Showing ParseTree");
                    continue;
                }else if(line == "/cls"){
                    Console.Clear();
                    continue;
                } 
                var syntaxTree = SyntaxTree.Parse(line);
                if(showTree){
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }
                if (syntaxTree.Diagnostics.Any()){
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var error in syntaxTree.Diagnostics){
                        Console.WriteLine(error);
                    }
                    Console.ResetColor();
                }else{
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
            }
        }
        static void PrettyPrint(SyntaxNode node, String indent = "", bool isLast = true){
            var marker = isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);
            if (node is SyntaxToken t && t.Value != null){
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();
            indent += isLast ? "   " : "│   ";
            var lastChild = node.GetChildren().LastOrDefault();
            indent += "    ";
            foreach (var child in node.GetChildren()){
                PrettyPrint(child, indent, child == lastChild);
            }
        }
    }
}
