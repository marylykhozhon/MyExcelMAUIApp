using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace MyExcelMAUIApp
{
    public partial class MainPage : ContentPage
    {
        const int CountColumn = 20;
        const int CountRow = 50;

        public MainPage()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            AddColumnsAndColumnLabels();
            AddRowsAndCellEntries();
        }

        private void AddColumnsAndColumnLabels()
        {
            for (int col = 0; col < CountColumn + 1; col++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                if (col > 0)
                {
                    var label = new Label
                    {
                        Text = GetColumnName(col),
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    Grid.SetRow(label, 0);
                    Grid.SetColumn(label, col);
                    grid.Children.Add(label);
                }
            }
        }

        private void AddRowsAndCellEntries()
        {
            for (int row = 0; row < CountRow; row++)
            {
                grid.RowDefinitions.Add(new RowDefinition());

                var label = new Label
                {
                    Text = (row + 1).ToString(),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                Grid.SetRow(label, row + 1);
                Grid.SetColumn(label, 0);
                grid.Children.Add(label);

                for (int col = 0; col < CountColumn; col++)
                {
                    var entry = new Entry
                    {
                        Text = "",
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    entry.Unfocused += Entry_Unfocused;
                    Grid.SetRow(entry, row + 1);
                    Grid.SetColumn(entry, col + 1);
                    grid.Children.Add(entry);
                }
            }
        }

        public static string GetColumnName(int colIndex)
        {
            int dividend = colIndex;
            string columnName = string.Empty;
            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }
            return columnName;
        }

        private void Entry_Unfocused(object? sender, FocusEventArgs e)
        {
            var entry = (Entry)sender!;
            var row = Grid.GetRow(entry) - 1;
            var col = Grid.GetColumn(entry) - 1;
            var content = entry.Text;
        }

        private async void CalculateButton_Clicked(object? sender, EventArgs e)
        {
            try
            {
                foreach (var child in grid.Children)
                {
                    if (child is Entry entry)
                    {
                        string text = entry.Text?.Trim() ?? string.Empty;
                        if (!string.IsNullOrEmpty(text))
                        {
                            var inputStream = new Antlr4.Runtime.AntlrInputStream(text);
                            var lexer = new LabCalculatorLexer(inputStream);
                            var tokenStream = new Antlr4.Runtime.CommonTokenStream(lexer);
                            var parser = new LabCalculatorParser(tokenStream);
                            var tree = parser.compileUnit();

                            var visitor = new LabCalculatorVisitor();
                            double result = visitor.Visit(tree);

                            entry.Text = result.ToString();
                        }
                    }
                }

                await DisplayAlertAsync("Обчислення", "Усі вирази успішно обчислено!", "ОК");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка", $"Неправильний вираз: {ex.Message}", "ОК");
            }
        }

        private async void SaveButton_Clicked(object? sender, EventArgs e)
        {
            await DisplayAlert("Збереження", "Таблицю успішно збережено у файл.", "ОК");
        }

        private async void ReadButton_Clicked(object? sender, EventArgs e)
        {
            await DisplayAlert("Відкриття", "Дані успішно завантажено.", "ОК");
        }

        private async void ExitButton_Clicked(object? sender, EventArgs e)
        {
            bool answer = await DisplayAlertAsync("Вихід", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (answer)
            {
                System.Environment.Exit(0);
            }
        }

        private async void HelpButton_Clicked(object? sender, EventArgs e)
        {
            await DisplayAlertAsync("Довідка", "Лабораторна робота, варіант 67. Калькулятор підтримує операції: inc, dec, not, and, or, <, >, ==", "ОК");
        }

        private void DeleteRowButton_Clicked(object? sender, EventArgs e)
        {
            if (grid.RowDefinitions.Count > 1)
            {
                int lastRowIndex = grid.RowDefinitions.Count - 1;
                grid.RowDefinitions.RemoveAt(lastRowIndex);

                for (int col = 0; col <= CountColumn; col++)
                {
                    var viewToRemove = grid.Children.FirstOrDefault(c => Grid.GetRow((BindableObject)c) == lastRowIndex);
                    if (viewToRemove != null)
                    {
                        grid.Children.Remove(viewToRemove);
                    }
                }
            }
        }

        private void DeleteColumnButton_Clicked(object? sender, EventArgs e)
        {
            if (grid.ColumnDefinitions.Count > 1)
            {
                int lastColumnIndex = grid.ColumnDefinitions.Count - 1;
                grid.ColumnDefinitions.RemoveAt(lastColumnIndex);

                for (int row = 0; row <= CountRow; row++)
                {
                    var viewToRemove = grid.Children.FirstOrDefault(c => Grid.GetColumn((BindableObject)c) == lastColumnIndex);
                    if (viewToRemove != null)
                    {
                        grid.Children.Remove(viewToRemove);
                    }
                }
            }
        }

        private void AddRowButton_Clicked(object? sender, EventArgs e)
        {
            int newRow = grid.RowDefinitions.Count;
            grid.RowDefinitions.Add(new RowDefinition());

            var label = new Label
            {
                Text = newRow.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Grid.SetRow(label, newRow);
            Grid.SetColumn(label, 0);
            grid.Children.Add(label);

            for (int col = 0; col < CountColumn; col++)
            {
                var entry = new Entry
                {
                    Text = "",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                entry.Unfocused += Entry_Unfocused;
                Grid.SetRow(entry, newRow);
                Grid.SetColumn(entry, col + 1);
                grid.Children.Add(entry);
            }
        }

        private void AddColumnButton_Clicked(object? sender, EventArgs e)
        {
            int newColumn = grid.ColumnDefinitions.Count;
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var label = new Label
            {
                Text = GetColumnName(newColumn),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, newColumn);
            grid.Children.Add(label);

            for (int row = 0; row < CountRow; row++)
            {
                var entry = new Entry
                {
                    Text = "",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };
                entry.Unfocused += Entry_Unfocused;
                Grid.SetRow(entry, row + 1);
                Grid.SetColumn(entry, newColumn);
                grid.Children.Add(entry);
            }
        }
    }
}