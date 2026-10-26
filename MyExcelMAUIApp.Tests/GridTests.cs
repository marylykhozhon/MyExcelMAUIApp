using Xunit;
using MyExcelMAUIApp;

namespace MyExcelMAUIApp.Tests
{
    public class GridTests
    {
        [Fact]
        public void Test_GetColumnName()
        {
            // Arrange (Підготовка даних)
            int columnIndex = 1;

            // Act (Виконання методу)
            string result = MainPage.GetColumnName(columnIndex);

            // Assert (Перевірка результату)
            Assert.Equal("A", result);
        }
    }
}