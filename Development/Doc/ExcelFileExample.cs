using System;

namespace RR.Framework.Interop.Excel
{
    /// <summary>
    /// How to use an excel file object.
    /// </summary>
    public static class ExcelFileExample
    {
        public static void Sample()
        {
            // First, initialize excel:
            ExcelFile.Initialize(ExcelFile.AdobePDFPrinterName);

            // Create a new excel file object to open, read or modify any excel file:
            var excelFile = new ExcelFile();

            // Set a file path:
            excelFile.Path = @"C:\Postdoc Research\INRA\SiriusQuality1.5\sema_sirius2009\Data\INRA-BBSRC 16cv project\ExcelFileExample.cs";

            // Open the file !
            excelFile.Open();

            // Get all sheets and then do something with them:
            var sheets = excelFile.Sheets;
            foreach (var sheet in sheets)
            {
                // Get the first cell value:
                var firstCellValue = sheet.GetRange(1, 1).GetStringValue();

                // Get the range used in this sheet:
                var usedRange = sheet.UsedRange;

                var numberOfRow = usedRange.Rows.Count;
                var numberOfColumns = usedRange.Columns.Count;

                // iterate throught all cell in the given range:
                // Excel use indexes starting at 1.
                for (var i = 1; i <= numberOfRow; ++i)
                {
                    for (var j = 1; j <= numberOfColumns; ++j)
                    {
                        var cell = sheet.GetRange(i, j);
                        // Let's print the cell value:
                        Console.WriteLine(cell.GetStringValue());
                    }
                }
            }

            // Access a worksheet by its name:
            var mySheetNamedLikeThat = excelFile.GetSheet("NamedLikeThat");

            // Clear the file if you want:
            excelFile.Clear();

            // Or close it:
            excelFile.Close();

            // After the file is close, we can change the path and open a new one.
        }
    }
}