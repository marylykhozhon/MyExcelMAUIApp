using Xunit;
using MyExcelMAUIApp;

namespace MyExcelMAUIApp.Tests
{
    public class GridTests
    {
        [Fact]
        public void Test_GetColumnName()
        {
            int columnIndex = 1;

            string result = MainPage.GetColumnName(columnIndex);

            Assert.Equal("A", result);
        }
    }
}