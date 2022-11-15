using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        // todo: add as many test methods as you wish, but they should be enough to cover basic scenarios of the mapping generator
        [TestMethod]
        public void TestSameNameAndSameDataType()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var res = mapper.Map(new Foo() { Id = 10 });
            Assert.IsTrue(res.Id == 10);
        }
        [TestMethod]
        public void TestDifferentNameAndSameDataType()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            mapper.AddRule(s => s.Name, d => d.DifferentName);
            var res = mapper.Map(new Foo() { Name = "John" });
            Assert.IsTrue(res.DifferentName == "John");
        }
        [TestMethod]
        public void TestDifferentNameAndDifferentDataType()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            mapper.AddRule(s => s.StringInt32Value, d => d.Int32Value, s => int.Parse(s.StringInt32Value));
            var res = mapper.Map(new Foo() { StringInt32Value = "10" });
            Assert.IsTrue(res.Int32Value == 10);
        }
    }
}
