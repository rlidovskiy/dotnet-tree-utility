using System;
using System.IO;
using Xunit;

namespace TreeUtility.Tests
{
    public class TreeBuilderTests
    {
        private const string FullResult = @"├───project
│	├───file.txt (19b)
│	└───gopher.png (70372b)
├───static
│	├───a_lorem
│	│	├───dolor.txt (empty)
│	│	├───gopher.png (70372b)
│	│	└───ipsum
│	│		└───gopher.png (70372b)
│	├───css
│	│	└───body.css (28b)
│	├───empty.txt (empty)
│	├───html
│	│	└───index.html (57b)
│	├───js
│	│	└───site.js (10b)
│	└───z_lorem
│		├───dolor.txt (empty)
│		├───gopher.png (70372b)
│		└───ipsum
│			└───gopher.png (70372b)
├───zline
│	├───empty.txt (empty)
│	└───lorem
│		├───dolor.txt (empty)
│		├───gopher.png (70372b)
│		└───ipsum
│			└───gopher.png (70372b)
└───zzfile.txt (empty)
";

        private const string DirsOnlyResult = @"├───project
├───static
│	├───a_lorem
│	│	└───ipsum
│	├───css
│	├───html
│	├───js
│	└───z_lorem
│		└───ipsum
└───zline
	└───lorem
		└───ipsum
";

        private const string TestDataPath = @"../../../../testdata";
        
        [Fact]
        public void FullTreeTest()
        {
            var output = new StringWriter();
            var builder = new TreeBuilder(output);
            builder.Build(TestDataPath, true);

            var result = output.ToString();
            
            Assert.Equal(FullResult, result);
        }
        
        [Fact]
        public void DirOnlyTreeTest()
        {
            var output = new StringWriter();
            var builder = new TreeBuilder(output);
            builder.Build(TestDataPath, false);

            var result = output.ToString();
            
            Assert.Equal(DirsOnlyResult, result);
        }
    }
}