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
            while (true){
                Console.Write(">");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    return;
                if (line == "$showtree") {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse tree" : "Not Showing ParseTree");
                    continue;
                }
                else if (line == "$cls"){
                    Console.Clear();
                    continue;
                }
                var syntaxTree = SyntaxTree.Parse(line);
                var compiler = new Compiler(syntaxTree);
                var result = compiler.Evaluate(variables);
                var diagnostics = result.Diagnostics;
                if (showTree) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    syntaxTree.Root.WriteTo(Console.Out);
                    Console.ResetColor();
                }
                if (!result.Diagnostics.Any()){
                    Console.WriteLine(result.Value);
                }
                else {
                    var text = syntaxTree.Text;
                    foreach (var diagnostic in diagnostics)
                    {
                        var lineIndex = text.GetLineIndex(diagnostic.Span.Start);
                        var lineNumber =  lineIndex + 1;
                        var character = diagnostic.Span.Start - text.Lines[lineIndex].Start + 1;
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"({lineNumber}, {character}): ");
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
                }
            }
        }
    }
}
