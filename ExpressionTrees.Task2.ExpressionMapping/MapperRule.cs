using System;
using System.Reflection;
namespace ExpressionTrees.Task2.ExpressionMapping
{
    class MapperRule<TSource>
    {
        public PropertyDetail SourcePropertyDetail { get; set; }
        public PropertyDetail DestinationPropertyDetail { get; set; }
        public Func<TSource, object> ConvertFunction { get; set; }
        public MapperRule(PropertyDetail sourcePropertyDetail, PropertyDetail destinationPropertyDetail, Func<TSource, object> convertFunction)
        {
            SourcePropertyDetail = sourcePropertyDetail;
            DestinationPropertyDetail = destinationPropertyDetail;
            ConvertFunction = convertFunction;
        }
    }
}
