using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class ParameterReplacer
    {
        public LambdaExpression Replace(LambdaExpression expression, Dictionary<string, int> dictionary)
        {
            var parameters = expression.Parameters.ToList();
            var parameter = parameters.FirstOrDefault();
            if (parameter != null)
            {
                int nodeValue;
                if (dictionary.TryGetValue(parameter.Name, out nodeValue))
                {
                    var replacer = new ParameterReplacerExpressionVisitor(parameter, Expression.Constant(nodeValue, typeof(int)));
                    var body = replacer.Visit(expression.Body);
                    parameters.Remove(parameter);
                    expression = Expression.Lambda(body, parameters);
                    return Replace(expression, dictionary);
                }
            }
            return expression;
        }
    }
}
