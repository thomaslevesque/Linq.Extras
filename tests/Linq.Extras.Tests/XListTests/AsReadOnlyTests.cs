using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XListTests
{
    public class AsReadOnlyTests
    {
        [Fact]
        public void AsReadOnly_Throws_If_List_IsNull()
        {
            IList<int> list = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => list.AsReadOnly());
            ex.ParamName.Should().Be("list");
        }

        [Fact]
        public void AsReadOnly_Returns_List_With_Same_Items()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };

            // Use as a static method, because full .NET already has a AsReadOnly instance method
            var result = XList.AsReadOnly(items);

            result.Should().Equal(items);
        }

        [Fact]
        public void AsReadOnly_Returns_A_List_That_Throws_On_Attempt_To_Modify()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };

            // Call as a static method, because full .NET already has a AsReadOnly instance method which would be used instead
            IList<int> result = XList.AsReadOnly(items);
            
            Assert.Throws<NotSupportedException>(() => result.Clear());
            Assert.Throws<NotSupportedException>(() => result.Add(99));
            Assert.Throws<NotSupportedException>(() => result.Insert(1, 99));
            Assert.Throws<NotSupportedException>(() => result.RemoveAt(0));
            Assert.Throws<NotSupportedException>(() => result.Remove(42));
            Assert.Throws<NotSupportedException>(() => result[0] = 99);
        }

        [Fact]
        public void AsReadOnly_Returns_A_List_That_Reflects_Changes_In_The_Original_List()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };

            // Use as a static method, because full .NET already has a AsReadOnly instance method
            var result = XList.AsReadOnly(items);

            // Make some random changes in the original list
            items.Remove(16);
            items.RemoveAt(1);
            items.Add(99);
            items.Swap(2, 3);

            // The read-only collection is only a view of the original, it should reflect the changes
            result.Should().Equal(items);
        }


    }
}
