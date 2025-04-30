using ClosedXML.Excel;
using CsvHelper;
using FakeDataGenerator.Core.Models;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FakeDataGenerator.Core.Services;

public class Exporter
{
    public string ToCsv(IEnumerable<Person> data)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(data);
        return writer.ToString();
    }

    public string ToJson(IEnumerable<Person> data)
    {
        return JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    public void ToExcel(IEnumerable<Person> data, string filePath)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Persons");


        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 3).Value = "Email";
        worksheet.Cell(1, 4).Value = "Birth Date";


        int row = 2;
        foreach (var person in data)
        {
            worksheet.Cell(row, 1).Value = person.Id;
            worksheet.Cell(row, 2).Value = person.Name;
            worksheet.Cell(row, 3).Value = person.Email;
            worksheet.Cell(row, 4).Value = person.BirthDate;
            row++;
        }

        worksheet.Columns().AdjustToContents();
        worksheet.Row(1).Style.Font.Bold = true;

        workbook.SaveAs(filePath);
    }
}