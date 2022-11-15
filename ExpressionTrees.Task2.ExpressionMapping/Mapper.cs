using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ExpressionTrees.Task2.ExpressionMapping
{
    public class Mapper<TSource, TDestination>
    {
        private readonly Func<TSource, TDestination> _mapFunction;
        private readonly List<MapperRule<TSource>> _ruleList;
        internal Mapper(Func<TSource, TDestination> func)
        {
            _mapFunction = func;
            _ruleList = new List<MapperRule<TSource>>();
        }
        public void AddRule(Expression<Func<TSource, object>> source, Expression<Func<TDestination, object>> destination, Func<TSource, object> convertFunction = null)
        {
            var sourceMemberExpression = source.Body as MemberExpression;
            if (sourceMemberExpression == null)
            {
                var sourceUnaryExpression = source.Body as UnaryExpression;
                sourceMemberExpression = sourceUnaryExpression.Operand as MemberExpression;
            }
            var sourcePropertyDetail = new PropertyDetail(sourceMemberExpression.Member.Name, sourceMemberExpression.Type);
            var destinationMemberExpression = destination.Body as MemberExpression;
            if (destinationMemberExpression == null)
            {
                var destinationUnaryExpression = destination.Body as UnaryExpression;
                destinationMemberExpression = destinationUnaryExpression.Operand as MemberExpression;
            }
            var destinationPropertyDetail = new PropertyDetail(destinationMemberExpression.Member.Name, destinationMemberExpression.Type);
            var mapperRule = new MapperRule<TSource>(sourcePropertyDetail, destinationPropertyDetail, convertFunction);
            _ruleList.Add(mapperRule);
        }
        public TDestination Map(TSource source)
        {
            var destination = _mapFunction(source);
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = destinationProperties
                    .FirstOrDefault(d => d.Name == sourceProperty.Name && d.PropertyType == sourceProperty.PropertyType);
                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
            if (_ruleList.Any())
            {
                foreach (var rule in _ruleList)
                {
                    var sourceProperty = sourceProperties
                       .FirstOrDefault(d => d.Name == rule.SourcePropertyDetail.Name && d.PropertyType == rule.SourcePropertyDetail.PropertyType);
                    var destinationProperty = destinationProperties
                        .FirstOrDefault(d => d.Name == rule.DestinationPropertyDetail.Name && d.PropertyType == rule.DestinationPropertyDetail.PropertyType);
                    object sourcePropertyValue = sourceProperty.GetValue(source);
                    if (rule.ConvertFunction != null)
                    {
                        sourcePropertyValue = rule.ConvertFunction.DynamicInvoke(source);
                    }
                    destinationProperty.SetValue(destination, sourcePropertyValue);
                }
            }
            return destination;
        }
    }
}
