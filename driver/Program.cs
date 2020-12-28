using System.Linq;
using System;
using dhive.core.Syntax;
using dhive.core;
using System.Collections.Generic;

namespace core
{
    internal static class Program
    {
        static void Main(){
            var showTree = false;
            var variables = new Dictionary<VariableSymbol, object>();
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
                var result = compiler.Evaluate(variables);
                var diagnostics = result.Diagnostics;
                if(showTree){
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }
                if (diagnostics.Any()){
                    foreach (var diagnostic in diagnostics){
                        //TODO: Handle this case "1 +" 
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(diagnostic);
                        Console.ResetColor();
                        var prefix = line.Substring(0, diagnostic.Span.Start);
                        var error = line.Substring(diagnostic.Span.Start, diagnostic.Span.Length);
                        var suffix = line.Substring(diagnostic.Span.End);
                        Console.Write("    ");
                        Console.Write(prefix);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(error);
                        Console.ResetColor();
                        Console.Write(suffix);
                        Console.WriteLine();
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
