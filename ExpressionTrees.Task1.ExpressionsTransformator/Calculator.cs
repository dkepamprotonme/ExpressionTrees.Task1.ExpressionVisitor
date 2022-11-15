using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class Calculator
    {
        public int Calculate(Expression<Func<int, int, int>> expression, Dictionary<string, int> dictionary)
        {
            var function = expression.Compile();
            var result = function(dictionary.First().Value, dictionary.Last().Value);
            return result;
        }
    }
}
