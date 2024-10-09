using HashDictionary.Impl;
namespace HashDictionary.Tests
{
    public class HashDicttionaryTests
    {
        [Fact]
        public void AddAndIndexerGetterAreConsistent()
        {
            IDictionary<int, int> dict = new HashDictionary<int, int>();
            dict.Add(1, 10);
            Equal(10, dict[1]);

            dict.Add(2, 20);
            Equal(10, dict[1]);
            Equal(20, dict[2]);
        }

        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new[] { 10 }, 1)]
        [InlineData(new[] { 10, 20 }, 2)]
        public void AddAndCountAreConsistent(int[] elements, int expectedCount)
        {
            IDictionary<int, int> dict = new HashDictionary<int, int>();
            for (int i = 0; i < elements.Length; i++)
            {
                dict.Add(i, elements[i]);
            }
            Equal(expectedCount, dict.Count);
        }
    }
}