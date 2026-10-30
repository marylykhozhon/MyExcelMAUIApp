using Xunit;
using MyExcelMAUIApp;

namespace MyExcelMAUIApp.Tests
{
    public class GridTests
    {
        [Fact]
        public void Test_GetColumnName_FirstColumn_ReturnsA()
        {
            int columnIndex = 1;
            string expected = "A";

            string actual = MainPage.GetColumnName(columnIndex);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetColumnName_26thColumn_ReturnsZ()
        {
            int columnIndex = 26;
            string expected = "Z";

            string actual = MainPage.GetColumnName(columnIndex);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_GetColumnName_27thColumn_ReturnsAA()
        {
            int columnIndex = 27;
            string expected = "AA";

            string actual = MainPage.GetColumnName(columnIndex);

            Assert.Equal(expected, actual);
        }
    }
}