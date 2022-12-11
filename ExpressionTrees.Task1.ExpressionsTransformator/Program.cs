/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();
            // todo: feel free to add your code here
            var visitor = new IncDecExpressionVisitor();
            Expression<Func<int, int>> addExpression = (int x) => x + 1;
            var incrementExpression = visitor.Transform(addExpression);
            Console.WriteLine($"Expression: {addExpression} Replaced: {incrementExpression}");
            Console.WriteLine();
            Expression<Func<int, int>> subtractExpression = (int x) => x - 1;
            var decrementExpression = visitor.Transform(subtractExpression);
            Console.WriteLine($"Expression: {subtractExpression} Replaced: {decrementExpression}");
            Console.WriteLine();
            var parameterReplacer = new ParameterReplacer();
            Expression<Func<int, int, int>> expression = (int x, int y) => x + 1 + y - 1;
            var dictionary = new Dictionary<string, int>() { { "x", 10 }, { "y", 20 } };
            var replaced = parameterReplacer.Replace(expression, dictionary);
            var compiled = replaced.Compile();
            var result = compiled.DynamicInvoke();
            Console.WriteLine($"Expression: {expression} Dictionary: {string.Join(",", dictionary)} Replaced: {replaced} Result: {result}");
            Console.ReadLine();
        }
    }
}
