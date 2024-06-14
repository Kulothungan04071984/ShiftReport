using System.Xml;

namespace Fuji_I.Models
{
    public class services_details : Inter_details
    {

        public List<LineUtilization> GetLineUtilization()
        {
            try
            {
                xmldata();
                var lineUtilizations = new List<LineUtilization>();
                return lineUtilizations;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<ModuleUtilization> GetModuleUtilization()
        {
            try
            {
                xmldata();
                var moduleUtilizations = new List<ModuleUtilization>();
                return moduleUtilizations;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void xmldata()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\kulo\\kulo\\Test.xml");

            // Select all book nodes
            XmlNodeList bookNodes = doc.SelectNodes("/catalog/book");

            // Iterate through each book node and extract data
            foreach (XmlNode bookNode in bookNodes)
            {
                // Get the value of the 'id' attribute
                string id = bookNode.Attributes["id"].Value;

                // Get the value of each child node
                string author = bookNode["author"].InnerText;
                string title = bookNode["title"].InnerText;
                string genre = bookNode["genre"].InnerText;
                string price = bookNode["price"].InnerText;
                string publishDate = bookNode["publish_date"].InnerText;
                string description = bookNode["description"].InnerText;

                // Print the data
                //Console.WriteLine($"ID: {id}");
                //Console.WriteLine($"Author: {author}");
                //Console.WriteLine($"Title: {title}");
                //Console.WriteLine($"Genre: {genre}");
                //Console.WriteLine($"Price: {price}");
                //Console.WriteLine($"Publish Date: {publishDate}");
                //Console.WriteLine($"Description: {description}");
                //Console.WriteLine();
            }
        }

    }
}
