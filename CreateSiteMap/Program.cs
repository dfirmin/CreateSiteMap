using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using OfficeOpenXml;

namespace CreateSiteMap
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string site = @"";
            string filePath = @"";
            var file = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                ColumnNumbers columnNumbers = new ColumnNumbers();
                columnNumbers.Location = ExcelUtility.LookFor(worksheet, "Location");
                columnNumbers.Frequency = ExcelUtility.LookFor(worksheet, "Frequency");
                columnNumbers.LastModified = ExcelUtility.LookFor(worksheet, "LastModified");
                columnNumbers.Priority = ExcelUtility.LookFor(worksheet, "Priority");


                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                SiteMapModel maps = new SiteMapModel();
                List<SiteMap> siteMaps = new List<SiteMap>();

                for (int row = 2; row <= rowCount; row++)
                {

                    var location = ExcelUtility.GetIntQ(worksheet, row, columnNumbers.Location);
                    var frequency = ExcelUtility.GetIntQ(worksheet, row, columnNumbers.Frequency);
                    var lastModified = ExcelUtility.GetIntQ(worksheet, row, columnNumbers.LastModified);
                    var priority = ExcelUtility.GetIntQ(worksheet, row, columnNumbers.Priority);

                    SiteMap map = new SiteMap();
                    map.ChangeFrequency = (Frequency)Enum.Parse(typeof(Frequency), frequency);
                    map.Location = site + location;
                    map.Priority = Convert.ToDouble(priority);
                    map.LastModified = Convert.ToDateTime(lastModified);
                    siteMaps.Add(map);

                }

                maps.SiteMaps = siteMaps;
                XmlSerializer xml = new XmlSerializer(maps.GetType());
                var xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");

                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);

                xml.Serialize(writer, maps);
                string xmlString = stringWriter.ToString();
                Console.WriteLine(xmlString);

            }
        }

        private class ColumnNumbers
        {
            public int Location;
            public int Priority;
            public int LastModified;
            public int Frequency;

        }
    }


}
