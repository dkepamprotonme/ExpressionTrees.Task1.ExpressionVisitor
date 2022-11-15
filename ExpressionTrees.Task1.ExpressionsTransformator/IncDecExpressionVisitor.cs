using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        // todo: feel free to add your code here
        public Expression Transform(Expression<Func<int,int>> expression)
        {
            return Visit(expression);
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add
                && node.Right.NodeType == ExpressionType.Constant
                && node.Right.Type == typeof(int)
                && Convert.ToInt32(((ConstantExpression)node.Right).Value) == 1)
            {
                return Expression.Increment(node.Left);
            }
            else if (node.NodeType == ExpressionType.Subtract
                && node.Right.NodeType == ExpressionType.Constant
                && node.Right.Type == typeof(int)
                && Convert.ToInt32(((ConstantExpression)node.Right).Value) == 1)
            {
                return Expression.Decrement(node.Left);
            }
            else
            {
                throw new NotSupportedException($"{node.NodeType} is not supported.");
            }
        }
    }
}
