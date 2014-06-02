using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class AppendPrependTests
    {
        [Test]
        public void Append_Adds_Item_At_End_Of_Sequence()
        {
            var input = new[] { 4, 8, 15, 16, 23 };
            var item = 42;
            var expected = new[] { 4, 8, 15, 16, 23, 42 };
            var actual = input.Append(item);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Prepend_Insert_Item_At_Beginning_Of_Sequence()
        {
            var input = new[] { 8, 15, 16, 23, 42 };
            var item = 4;
            var expected = new[] { 4, 8, 15, 16, 23, 42 };
            var actual = input.Prepend(item);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
