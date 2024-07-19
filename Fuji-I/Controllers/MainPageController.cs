using Fuji_I.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

public class MainPageController : Controller
{
    private readonly Inter_details _inter_Details;
    private readonly IConfiguration _configuration;

    public MainPageController(Inter_details inter_Details, IConfiguration configuration)
    {
        _inter_Details = inter_Details;
        _configuration = configuration;
    }

    public IActionResult ShiftReport()
    {
        string filePath1 = _configuration["AppSettings:FilePath1"];
        string filePath2 = _configuration["AppSettings:FilePath2"];
        string filePath3 = _configuration["AppSettings:FilePath3"];

        const int StartRowLine = 8;
        const int EndRowLine = 18;
        const int StartRowCycle = 1922;
        const int EndRowCycle = 1934;

        List<Line1Utilization> Line1PieData = new List<Line1Utilization>();
        List<Line2Utilization> Line2PieData = new List<Line2Utilization>();
        List<Line3Utilization> Line3PieData = new List<Line3Utilization>();
        List<Module1Utilization> Line1CycleData = new List<Module1Utilization>();
        List<Module2Utilization> Line2CycleData = new List<Module2Utilization>();
        List<Module3Utilization> Line3CycleData = new List<Module3Utilization>();

        try
        {
            ProcessCsvFile<Line1Utilization, Module1Utilization>(filePath1, StartRowLine, EndRowLine, StartRowCycle, EndRowCycle, Line1PieData, (label, value) => new Line1Utilization { Label = label, Value = value }, Line1CycleData, (label, value) => new Module1Utilization { Label = label, Value = value });
        }
        catch (FileNotFoundException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Filenotfounderror");
            //Log.Error(ex, $"File not found for filePath1: {ex.Message}");
            Line1PieData.Add(new Line1Utilization { Label = "Null", Value = 0 });
        }
        catch (UnauthorizedAccessException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "UnauthorizedAccessException");
            //Log.Error(ex, $"Unauthorized access for filePath1: {ex.Message}");
            Line1PieData.Add(new Line1Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex) when (ex.Message.Contains("being used by another process"))
        {
            writeErrorMessage(ex.Message.ToString(), "Filereaderror");
            //Log.Error(ex, $"File is in use for filePath1: {ex.Message}");
            Line1PieData.Add(new Line1Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "IOException");
            //Log.Error(ex, $"IO Exception for filePath1: {ex.Message}");
            Line1PieData.Add(new Line1Utilization { Label = "Null", Value = 0 });
        }
        catch (Exception ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Exception");
            //Log.Error(ex, $"Error processing filePath1: {ex.Message}");
            Line1PieData.Add(new Line1Utilization { Label = "Null", Value = 0 });
        }

        try
        {
            ProcessCsvFile<Line2Utilization, Module2Utilization>(filePath2, StartRowLine, EndRowLine, StartRowCycle, EndRowCycle, Line2PieData, (label, value) => new Line2Utilization { Label = label, Value = value }, Line2CycleData, (label, value) => new Module2Utilization { Label = label, Value = value });
        }
        catch (FileNotFoundException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Filenotfounderror");
            //Log.Error(ex, $"File not found for filePath2: {ex.Message}");
            Line2PieData.Add(new Line2Utilization { Label = "Null", Value = 0 });
        }
        catch (UnauthorizedAccessException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "UnauthorizedAccessException");
            //Log.Error(ex, $"Unauthorized access for filePath2: {ex.Message}");
            Line2PieData.Add(new Line2Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex) when (ex.Message.Contains("being used by another process"))
        {
            writeErrorMessage(ex.Message.ToString(), "Filereaderror");
            //Log.Error(ex, $"File is in use for filePath2: {ex.Message}");
            Line2PieData.Add(new Line2Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "IOException");
            //Log.Error(ex, $"IO Exception for filePath2: {ex.Message}");
            Line2PieData.Add(new Line2Utilization { Label = "Null", Value = 0 });
        }
        catch (Exception ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Exception");
            //Log.Error(ex, $"Error processing filePath2: {ex.Message}");
            Line2PieData.Add(new Line2Utilization { Label = "Null", Value = 0 });
        }

        try
        {
            ProcessCsvFile<Line3Utilization, Module3Utilization>(filePath3, StartRowLine, EndRowLine, StartRowCycle, EndRowCycle, Line3PieData, (label, value) => new Line3Utilization { Label = label, Value = value }, Line3CycleData, (label, value) => new Module3Utilization { Label = label, Value = value });
        }
        catch (FileNotFoundException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Filenotfounderror");
            //Log.Error(ex, $"File not found for filePath3: {ex.Message}");
            Line3PieData.Add(new Line3Utilization { Label = "Null", Value = 0 });
        }
        catch (UnauthorizedAccessException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "UnauthorizedAccessException");
            //Log.Error(ex, $"Unauthorized access for filePath3: {ex.Message}");
            Line3PieData.Add(new Line3Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex) when (ex.Message.Contains("being used by another process"))
        {
            writeErrorMessage(ex.Message.ToString(), "Filereaderror");
            //Log.Error(ex, $"File is in use for filePath3: {ex.Message}");
            Line3PieData.Add(new Line3Utilization { Label = "Null", Value = 0 });
        }
        catch (IOException ex)
        {
            writeErrorMessage(ex.Message.ToString(), "IOException");
            //Log.Error(ex, $"IO Exception for filePath3: {ex.Message}");
            Line3PieData.Add(new Line3Utilization { Label = "Null", Value = 0 });
        }
        catch (Exception ex)
        {
            writeErrorMessage(ex.Message.ToString(), "Exception");
            //Log.Error(ex, $"Error processing filePath3: {ex.Message}");
            Line3PieData.Add(new Line3Utilization { Label = "Null", Value = 0 });
        }

        var objUtilization = new Utilization
        {
            lstLine1Utilization = Line1PieData,
            lstLine2Utilization = Line2PieData,
            lstLine3Utilization = Line3PieData,
            lstModule1Utilization = Line1CycleData,
            lstModule2Utilization = Line2CycleData,
            lstModule3Utilization = Line3CycleData,
        };

        return View(objUtilization);
    }

    // Processing Line 1, 2, 3 CSV files
    private void ProcessCsvFile<T1, T2>(string filePath, int startRowLine, int endRowLine, int startRowCycle, int endRowCycle, List<T1> dataList1, Func<string, decimal, T1> createObject1, List<T2> dataList2, Func<string, decimal, T2> createObject2)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                int currentRowLine = 0;
                int currentRowCycle = 0;
                bool isReadingLine = true;

                // Initializing variables to store the max value to calculate the cycle time
                decimal maxValueCondition1 = decimal.MinValue;
                string maxLabelCondition1 = null;
                decimal maxValueCondition2 = decimal.MinValue;
                string maxLabelCondition2 = null;

                while (!reader.EndOfStream && (currentRowLine <= endRowLine || currentRowCycle <= endRowCycle))
                {
                    var line = reader.ReadLine();
                    //Debug.WriteLine($"Reading line {currentRowLine + currentRowCycle}: {line}");

                    var values = line.Split(',');

                    if (currentRowLine >= startRowLine && currentRowLine <= endRowLine && isReadingLine && values.Length >= 2)
                    {
                        string label = values[0].Trim();
                        string valueStr = values[1].Trim();
                        decimal value = ConvertToDecimal(valueStr);

                        dataList1.Add(createObject1(label, value));
                        //Debug.WriteLine($"Added to chartData: Label={label}, Value={value}");
                    }
                    else if (currentRowCycle >= startRowCycle && currentRowCycle <= endRowCycle && !isReadingLine && values.Length >= 7)
                    {
                        // Condition 1: Rows 1925, 1926, 1927, 1928, 1931, 1934 for Line 1
                        if (currentRowCycle == 1925 || currentRowCycle == 1926 || currentRowCycle == 1927 || currentRowCycle == 1928 || currentRowCycle == 1931 || currentRowCycle == 1934)
                        {
                            string label = values[2].Trim();
                            string valueStr = values[6].Trim();
                            decimal value = ConvertToDecimal(valueStr);

                            // Check if the current value is greater than the previously recorded maximum value for Condition 1
                            if (value > maxValueCondition1)
                            {
                                maxValueCondition1 = value;
                                maxLabelCondition1 = label;
                            }

                            //Debug.WriteLine($"Condition 1 - Read Label={label}, Value={value}");
                        }
                        // Condition 2: Rows 1923, 1924, 1929, 1930, 1932, 1933 for Line 2
                        else if (currentRowCycle == 1923 || currentRowCycle == 1924 || currentRowCycle == 1929 || currentRowCycle == 1930 || currentRowCycle == 1932 || currentRowCycle == 1933)
                        {
                            string label = values[2].Trim();
                            string valueStr = values[6].Trim();
                            decimal value = ConvertToDecimal(valueStr);

                            // Check if the current value is greater than the previously recorded maximum value for Condition 2
                            if (value > maxValueCondition2)
                            {
                                maxValueCondition2 = value;
                                maxLabelCondition2 = label;
                            }

                            //Debug.WriteLine($"Condition 2 - Read Label={label}, Value={value}");
                        }
                    }

                    if (currentRowLine > endRowLine)
                    {
                        isReadingLine = false;
                    }

                    currentRowLine++;
                    currentRowCycle++;
                }

                // After processing all rows, add the maximum values and labels to dataList2 for both conditions
                if (maxLabelCondition1 != null)
                {
                    dataList2.Add(createObject2(maxLabelCondition1, maxValueCondition1));
                    //Debug.WriteLine($"Condition 1 - Added to chartData: Label={maxLabelCondition1}, Value={maxValueCondition1}");
                }

                if (maxLabelCondition2 != null)
                {
                    dataList2.Add(createObject2(maxLabelCondition2, maxValueCondition2));
                    //Debug.WriteLine($"Condition 2 - Added to chartData: Label={maxLabelCondition2}, Value={maxValueCondition2}");
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Log.Error(ex, $"File not found: {ex.Message}");
            throw;
        }
        catch (UnauthorizedAccessException ex)
        {
            Log.Error(ex, $"Unauthorized access: {ex.Message}");
            throw;
        }
        catch (IOException ex) when (ex.Message.Contains("being used by another process"))
        {
            Log.Error(ex, $"File is in use: {ex.Message}");
            throw;
        }
        catch (IOException ex)
        {
            Log.Error(ex, $"IO Exception: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error reading file: {ex.Message}");
            throw;
        }
    }

    // Helper method to convert CSV cell value to decimal
    private decimal ConvertToDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;

        if (decimal.TryParse(value, out decimal decimalResult))
        {
            return decimalResult;
        }

        value = value.TrimEnd('%');

        if (decimal.TryParse(value, out decimal result))
        {
            if (value.EndsWith("%"))
            {
                result /= 100;
            }

            return result;
        }
        else
        {
            //Debug.WriteLine($"Error converting '{value}' to decimal.");
            return 0;
        }
    }

    

    public void writeErrorMessage(string errorMessage, string functionName)
    {
        //var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\iTime" + "\\" + DateTime.Now.ToString("dd-MM-yyyy");
        string systemPath = _configuration["AppSettings:FilePath4"]; 
        if (!Directory.Exists(systemPath))
        {
            Directory.CreateDirectory(systemPath);
        }

        string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
        string errorLogFileName = $"ErrorLog_{currentDate}.txt";
        string errorLogPath = Path.Combine(systemPath, errorLogFileName);

        //string WrErrorLog = String.Format(@"{0}\{1}.txt", systemPath, "ErrorLogInRFIDTag");
        using (StreamWriter errLogs = new StreamWriter(errorLogPath, true))
        {
            errLogs.WriteLine("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
            errLogs.WriteLine("---------------------------------------------------" + DateTime.Now + "----------------------------------------------" + Environment.NewLine);
            errLogs.WriteLine(errorMessage + Environment.NewLine + "-----" + functionName);
            errLogs.Close();
        }

    }
}


