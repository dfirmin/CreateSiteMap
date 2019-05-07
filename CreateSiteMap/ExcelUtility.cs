using System;
using System.Collections.Generic;
using System.Text;

namespace CreateSiteMap
{
    public class ExcelUtility
    {
        public static string GetIntQ(OfficeOpenXml.ExcelWorksheet sheet, int row, int col)
        {
            var str = sheet.Cells[row, col].Value?.ToString();
            return String.IsNullOrWhiteSpace(str) ? null : str.Trim();
            // return str.Trim();
        }


        public static int LookFor(OfficeOpenXml.ExcelWorksheet sheet, string searchFor)
        {
            searchFor = searchFor.ToLower();
            for (var i = 1; i <= (sheet.Dimension?.Columns ?? 0); i++)
            {
                var cell = sheet.Cells[1, i];
                if (cell != null && cell.Text.ToLower() == searchFor)
                {
                    return i;
                }
            }
            throw new Exception("Cannot find column heading: '" + searchFor + "'");
        }
    }
}
