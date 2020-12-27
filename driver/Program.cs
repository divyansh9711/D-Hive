using System.Linq;
using System;
using dhive.core.Syntax;
using dhive.core;
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
                var compiler = new Compiler(syntaxTree);
                var result = compiler.Evaluate();
                var diagnostics = result.Diagnostics;

                if(showTree){
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }
                if (diagnostics.Any()){
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach (var error in diagnostics){
                        Console.WriteLine(error);
                    }
                    Console.ResetColor();
                }else{
                    Console.WriteLine(result.Value);
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
