using FakeDataGenerator.Core.Models;
using FakeDataGenerator.Core.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;


namespace FakeDataGenerator.UI;

public partial class MainWindow : Window
{
    private readonly DataGenerator _dataGenerator = new();
    private readonly Exporter _exporter = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(CountTextBox.Text, out int count) || count < 1)
        {
            MessageBox.Show("Please enter a valid number greater than 0");
            return;
        }

        var format = (FormatComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
        var data = _dataGenerator.GeneratePersons(count);

        var saveFileDialog = new SaveFileDialog();

        switch (format)
        {
            case "Excel":
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        _exporter.ToExcel(data, saveFileDialog.FileName);
                        MessageBox.Show("Excel file saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving Excel file: {ex.Message}");
                    }
                }
                break;

            case "CSV":
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, _exporter.ToCsv(data));
                    MessageBox.Show("CSV file saved successfully!");
                }
                break;

            case "JSON":
                saveFileDialog.Filter = "JSON Files (*.json)|*.json";
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, _exporter.ToJson(data));
                    MessageBox.Show("JSON file saved successfully!");
                }
                break;
        }
    }
}