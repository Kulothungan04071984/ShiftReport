using CsvHelper;
using System.Globalization;
using System.IO;
using System.Collections.Generic;


namespace Fuji_I.Models
{
    public class services_details : Inter_details
    {

        public List<Line1Utilization> GetLine1Utilization()
        {
            try
            {
                //xmldata();
                var line1Utilizations = new List<Line1Utilization>();
                return line1Utilizations;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        public List<Line2Utilization> GetLine2Utilization()
        {
            try
            {
               // xmldata();
                var Line2Utilizations = new List<Line2Utilization>();
                return Line2Utilizations;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Line3Utilization> GetLine3Utilization()
        {
            try
            {
                // xmldata();
                var Line3Utilizations = new List<Line3Utilization>();
                return Line3Utilizations;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Line4Utilization> GetLine4Utilization()
        {
            try
            {
                // xmldata();
                var Line4Utilizations = new List<Line4Utilization>();
                return Line4Utilizations;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Line5Utilization> GetLine5Utilization()
        {
            try
            {
                // xmldata();
                var Line5Utilizations = new List<Line5Utilization>();
                return Line5Utilizations;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string DataTableToHtml(DataTable dt)
        //{
        //    StringWriter sw = new StringWriter();
        //    sw.WriteLine("<table border='1'>");

        //    // Write the header
        //    sw.WriteLine("<tr>");
        //    foreach (DataColumn column in dt.Columns)
        //    {
        //        sw.WriteLine($"<th>{column.ColumnName}</th>");
        //    }
        //    sw.WriteLine("</tr>");

        //    // Write the data
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        sw.WriteLine("<tr>");
        //        foreach (var item in row.ItemArray)
        //        {
        //            sw.WriteLine($"<td>{item}</td>");
        //        }
        //        sw.WriteLine("</tr>");
        //    }
        //    sw.WriteLine("</table>");

        //    return sw.ToString();
        //}
        public List<List<string>> ReadCsv(string filePath)
        {
            var csvData = new List<List<string>>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    csvData.Add(new List<string>(values));
                }
            }

            return csvData;
        }

        public string ConvertCsvToHtmlTable(string csvFilePath)
        {
            try
            {
                var html = new System.Text.StringBuilder();
                html.Append("<table border='1'>");

                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>();

                    // Add header row
                    var header = ((IDictionary<string, object>)records.First()).Keys;
                    html.Append("<tr>");
                    foreach (var column in header)
                    {
                        html.Append($"<th>{column}</th>");
                    }
                    html.Append("</tr>");

                    // Add data rows
                    foreach (var record in records)
                    {
                        html.Append("<tr>");
                        foreach (var value in (IDictionary<string, object>)record)
                        {
                            html.Append($"<td>{value.Value}</td>");
                        }
                        html.Append("</tr>");
                    }
                }

                html.Append("</table>");
                return html.ToString();
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public List<List<string>> ReadCsv1(string filePath1)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> ReadCsv2(string filePath2)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> ReadCsv3(string filePath3)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> ReadCsv4(string filePath4)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> ReadCsv5(string filePath5)
        {
            throw new NotImplementedException();
        }




        //public void xmldata()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("C:\\kulo\\kulo\\Test.xml");

        //    // Select all book nodes
        //    XmlNodeList bookNodes = doc.SelectNodes("/catalog/book");

        //    // Iterate through each book node and extract data
        //    foreach (XmlNode bookNode in bookNodes)
        //    {
        //        // Get the value of the 'id' attribute
        //        string id = bookNode.Attributes["id"].Value;

        //        // Get the value of each child node
        //        string author = bookNode["author"].InnerText;
        //        string title = bookNode["title"].InnerText;
        //        string genre = bookNode["genre"].InnerText;
        //        string price = bookNode["price"].InnerText;
        //        string publishDate = bookNode["publish_date"].InnerText;
        //        string description = bookNode["description"].InnerText;

        //        // Print the data
        //        //Console.WriteLine($"ID: {id}");
        //        //Console.WriteLine($"Author: {author}");
        //        //Console.WriteLine($"Title: {title}");
        //        //Console.WriteLine($"Genre: {genre}");
        //        //Console.WriteLine($"Price: {price}");
        //        //Console.WriteLine($"Publish Date: {publishDate}");
        //        //Console.WriteLine($"Description: {description}");
        //        //Console.WriteLine();
        //    }
        //}

    }
}
