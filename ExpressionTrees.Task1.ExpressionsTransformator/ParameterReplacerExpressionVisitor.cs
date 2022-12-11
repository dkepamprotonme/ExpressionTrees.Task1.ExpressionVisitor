using System.Linq.Expressions;
namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class ParameterReplacerExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;
        public ParameterReplacerExpressionVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }
        public override Expression Visit(Expression node)
        {
            if (node == _from)
            {
                return _to;
            }
            return base.Visit(node);
        }
    }
}
