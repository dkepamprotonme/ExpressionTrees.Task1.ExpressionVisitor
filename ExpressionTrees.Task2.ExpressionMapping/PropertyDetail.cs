using System;
namespace ExpressionTrees.Task2.ExpressionMapping
{
    class PropertyDetail
    {
        public string Name { get; set; }
        public Type PropertyType { get; set; }
        public PropertyDetail(string name, Type propertyType)
        {
            Name = name;
            PropertyType = propertyType;
        }
    }
}
