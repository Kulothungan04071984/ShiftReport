using Fuji_I.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Engineering;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Emit;

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
        // Get yesterday's date in "yyyyMMdd" format
        var today = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)).ToString("yyyyMMdd");
        var filepath = "D:\\shiftreport\\" + today;

        // Ensure the directory exists
        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        // Define file paths for Line1, Line2, and Line3 CSV files
        string filePath1 = filepath + "\\" + today + "_Line1.csv";
        string filePath2 = filepath + "\\" + today + "_Line2.csv";
        string filePath3 = filepath + "\\" + today + "_Line3.csv";

        // Define initial start and end rows (you can adjust these based on your specific requirement)
        //const int StartRowLine = 8;
        //const int EndRowLine = 20;
        //const int StartRowbcount = 604;
        //const int EndRowbcount = 606;
        //const int StartRowCycle = 1922;
        //const int EndRowCycle = 1933;

        // Initialize lists to store utilization data for each line
        List<Line1Utilization> Line1PieData = new List<Line1Utilization>();
        List<Line2Utilization> Line2PieData = new List<Line2Utilization>();
        List<Line3Utilization> Line3PieData = new List<Line3Utilization>();
        List<Module1Utilization> Line1CycleData = new List<Module1Utilization>();
        List<Module2Utilization> Line2CycleData = new List<Module2Utilization>();
        List<Module3Utilization> Line3CycleData = new List<Module3Utilization>();
        List<OEE1Utilization> Line1OEEData = new List<OEE1Utilization>();
        List<OEE2Utilization> Line2OEEData = new List<OEE2Utilization>();
        List<OEE3Utilization> Line3OEEData = new List<OEE3Utilization>();

        // Variables to store the start row for each word found in the CSV files
        int StartRowLine1 = -1, StartRowbcount1 = -1, StartRowCycle1 = -1;
        int StartRowLine2 = -1, StartRowbcount2 = -1, StartRowCycle2 = -1;
        int StartRowLine3 = -1, StartRowbcount3 = -1, StartRowCycle3 = -1;

        try
        {
            ProcessCsvFileWithRowSearch(filePath1, ref StartRowLine1, ref StartRowbcount1, ref StartRowCycle1);
            int StartRowLine1_1 = StartRowLine1 + 2;
            int EndRowLine1 = StartRowLine1+12;
            int StartRowbcount1_1 = StartRowbcount1 - 1;
            int EndRowbcount1 = StartRowbcount1 + 2;
            int StartRowCycle1_1 = StartRowCycle1 + 5;
            int EndRowCycle1 = StartRowCycle1 + 11;

            ProcessCsvFile<Line1Utilization, Module1Utilization, OEE1Utilization>(filePath1, StartRowLine1_1, EndRowLine1, StartRowbcount1_1, EndRowbcount1, StartRowCycle1_1, EndRowCycle1, Line1PieData, (label, value) => new Line1Utilization { Label = label, Value = value }, Line1CycleData, (label, value) => new Module1Utilization { Label = label, Value = value }, Line1OEEData, (label, value1) => new OEE1Utilization { Label = label, Value = value1 });
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
            ProcessCsvFileWithRowSearch(filePath2, ref StartRowLine2, ref StartRowbcount2, ref StartRowCycle2);
            int StartRowLine1_2 = StartRowLine2 + 2;
            int EndRowLine2 = StartRowLine2 + 12;
            int StartRowbcount1_2 = StartRowbcount2 - 1;
            int EndRowbcount2 = StartRowbcount2 + 2;
            int StartRowCycle1_2 = StartRowCycle2 + 5;
            int EndRowCycle2 = StartRowCycle2 + 11;

            ProcessCsvFile<Line2Utilization, Module2Utilization, OEE2Utilization>(filePath2, StartRowLine1_2, EndRowLine2, StartRowbcount1_2, EndRowbcount2, StartRowCycle1_2, EndRowCycle2, Line2PieData, (label, value) => new Line2Utilization { Label = label, Value = value }, Line2CycleData, (label, value) => new Module2Utilization { Label = label, Value = value }, Line2OEEData, (label, value1) => new OEE2Utilization { Label = label, Value = value1 });
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
            ProcessCsvFileWithRowSearch(filePath3, ref StartRowLine3, ref StartRowbcount3, ref StartRowCycle3);
            int StartRowLine1_3 = StartRowLine3 + 2;
            int EndRowLine3 = StartRowLine3 + 12;
            int StartRowbcount1_3 = StartRowbcount3 - 1;
            int EndRowbcount3 = StartRowbcount3 + 2;
            int StartRowCycle1_3 = StartRowCycle3 + 5;
            int EndRowCycle3 = StartRowCycle3 + 11;
            ProcessCsvFile<Line3Utilization, Module3Utilization, OEE3Utilization>(filePath3, StartRowLine1_3, EndRowLine3, StartRowbcount1_3, EndRowbcount3, StartRowCycle1_3, EndRowCycle3, Line3PieData, (label, value) => new Line3Utilization { Label = label, Value = value }, Line3CycleData, (label, value) => new Module3Utilization { Label = label, Value = value }, Line3OEEData, (label, value1) => new OEE3Utilization { Label = label, Value = value1 });
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

        // Construct the result object to pass to the view
        var objUtilization = new Utilization
        {
            lstOEE1Utilization = Line1OEEData,
            lstOEE2Utilization = Line2OEEData,
            lstOEE3Utilization = Line3OEEData,
            lstLine1Utilization = Line1PieData,
            lstLine2Utilization = Line2PieData,
            lstLine3Utilization = Line3PieData,
            lstModule1Utilization = Line1CycleData,
            lstModule2Utilization = Line2CycleData,
            lstModule3Utilization = Line3CycleData,
        };

        return View(objUtilization);
    }

 

    // Method to process the CSV files and search for specific rows
    private void ProcessCsvFileWithRowSearch(string filePath, ref int StartRowLine, ref int StartRowbcount, ref int StartRowCycle)
    {
        if (System.IO.File.Exists(filePath))
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            string[] searchWords = {"Actual Panel Output", "Line Running Time Rate",  "<Cycle Time Report>" };

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(searchWords[0]) && StartRowbcount == -1) // Actual Panel Count
                {
                    StartRowbcount = i + 1;
                }
                if (lines[i].Contains(searchWords[1]) && StartRowLine == -1) // Line Running Time Rate
                {
                    StartRowLine = i + 1;
                }
                if (lines[i].Contains(searchWords[2]) && StartRowCycle == -1) // <Cycle Time Report>
                {
                    StartRowCycle = i + 1;
                }
                if (StartRowbcount != -1 && StartRowLine != -1 && StartRowCycle != -1)
                    break; // Exit the loop if all words are found
            }
        }
    }

    // Method to handle errors in file processing
    private void HandleFileProcessingError(Exception ex, List<dynamic> pieData)
    {
        writeErrorMessage(ex.Message.ToString(), "FileProcessingError");
        pieData.Add(new Line1Utilization { Label = "Null", Value = 0 }); // Example; adjust based on the data type
    }

    #region ProcessFiles 

    // Processing Line 1, 2, 3 CSV files
    private void ProcessCsvFile<T1, T2, T3>(string filePath, int startRowLine, int endRowLine, int startRowcountb, int endRowcountb, int startRowCycle, int endRowCycle, List<T1> dataList1, Func<string, decimal, T1> createObject1, List<T2> dataList2, Func<string, decimal, T2> createObject2, List<T3> dataList3, Func<string,decimal, T3> createObject3)
    {
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                int Runtimevalue = 0;
                int pptimesum = 0;
                int downtimesum = 0;
                int Panelcountsum = 0;
                int perf1 = 0;
                int perf2 = 0;
                int currentRowLine = 0;
                int currentRowcountb = 0;
                int currentRowCycle = 0;
                int maxColumnValue = int.MinValue;
                bool isReadingLine = true;

                // Initializing variables to store the max value to calculate the cycle time
                decimal maxValueCondition1 = decimal.MinValue;
                string maxLabelCondition1 = null;
                decimal maxValueCondition2 = decimal.MinValue;
                string maxLabelCondition2 = null;

                while (!reader.EndOfStream && (currentRowLine <= endRowLine-1 || currentRowcountb <= endRowcountb || currentRowCycle <= endRowCycle))
                {
                    var line = reader.ReadLine();
                    //Debug.WriteLine($"Reading line {currentRowLine + currentRowCycle}: {line}");

                    var values = line.Split(',');

                    if (currentRowLine >= startRowLine && currentRowLine <= endRowLine-1 && isReadingLine && values.Length >= 2)
                    {
                        string label = values[0].Trim();
                        string valueStr = values[1].Trim();
                        decimal value = ConvertToDecimal(valueStr);

                        string valueStr1 = values[2].Trim();

                        int value1 = ConvertToSeconds(valueStr1);

                        // Calculate downtimesum for rows 1, 5, and 10
                        if (currentRowLine == startRowLine+5 || currentRowLine == startRowLine + 6 || currentRowLine == startRowLine + 7 || currentRowLine == startRowLine + 8 ||
                            currentRowLine == startRowLine + 9)
                        {
                            downtimesum += value1;

                            Debug.WriteLine($"Reading line {currentRowLine}: {value1}");
                        }

                        // Calculate runtimesum for rows 1 to 10
                        if (currentRowLine >= startRowLine && currentRowLine <= startRowLine+10)
                        {
                            pptimesum += value1;
                            Debug.WriteLine($"Reading line {currentRowLine}: {value1}");
                        }


                        dataList1.Add(createObject1(label, value));

                        Debug.WriteLine($"Added to chartData: Label={label}, Value={value}");
                    }
                    else if (currentRowcountb >= startRowcountb && currentRowcountb <= endRowcountb && !isReadingLine && values.Length >= 22)

                    {

                        if (currentRowLine == startRowcountb)
                        {

                            for (int i = 1; i <= 24; i++)
                            {

                                // Parse each value to integer and add to sum
                                if (i < values.Length)
                                {
                                    Debug.WriteLine($"within length if Value={values[i]}");

                                    if (int.TryParse(values[i], out int value))
                                    {
                                        Panelcountsum += value;
                                        //max value
                                        if (value > maxColumnValue)
                                        {
                                            maxColumnValue = value;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to parse value at column {i} in row {605}");
                                    }
                                }
                            }
                            Debug.WriteLine($"Panel Count sum: Value={Panelcountsum},MaxColumnValue={maxColumnValue}");

                        }

                    }

                    else if (currentRowCycle >= startRowCycle && currentRowCycle <= endRowCycle && !isReadingLine && values.Length >= 7)
                    {
                        // Condition 1: Rows 1925, 1926, 1927, 1928, 1931, 1934 for Line 1
                        if (currentRowCycle == startRowCycle+2 || currentRowCycle == startRowCycle+3 || currentRowCycle == startRowCycle+4 ||
                            currentRowCycle == startRowCycle+5 || currentRowCycle == startRowCycle+8 || currentRowCycle == startRowCycle+11)
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

                            Debug.WriteLine($"Condition 1 - Read Label={label}, Value={value}");
                        }

                        // Condition 2: Rows 1923, 1924, 1929, 1930, 1932, 1933 for Line 2
                        else if (currentRowCycle == startRowCycle || 
                                currentRowCycle == startRowCycle+1 || currentRowCycle == startRowCycle+6 || currentRowCycle == startRowCycle+7 || currentRowCycle == startRowCycle+9 
                             || currentRowCycle == startRowCycle+10)
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

                            Debug.WriteLine($"Condition 2 -Read line={currentRowCycle}, Read Label={label}, Value={value}");
                        }
                    }

                    if (currentRowLine > endRowLine)
                    {
                        isReadingLine = false;
                    }

                    currentRowLine++;
                    currentRowcountb++;
                    currentRowCycle++;
                }
                //Availability calculation
                decimal Availability = ConvertToAvailability(pptimesum, downtimesum);
                Debug.WriteLine("Availability" + Availability);// value to pass 

                //Performace calculation
                decimal Performance = ConvertToPerformance(Panelcountsum, maxColumnValue);
                Debug.WriteLine("Performace" + Performance);

                //Quality Calculation
                decimal Quality = 99;
                Debug.WriteLine("Quality" + Quality);

                dataList3.Add(createObject3("Availability",Availability));
                dataList3.Add(createObject3("Performace",Performance));
                dataList3.Add(createObject3("Quality",Quality));

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
    private int ConvertToSeconds(string valueStr1)
    {
        try
        {
            Debug.WriteLine("value before split", valueStr1);
            var timec = valueStr1.Split(':');
            Debug.WriteLine("timec" + string.Join(",", timec));
            if (timec.Length != 3)
            {
                throw new FormatException("Invalid time format");
            }

            var h = int.Parse(timec[0]);
            var m = int.Parse(timec[1]);
            var s = int.Parse(timec[2]);
            Debug.WriteLine($"Hours:{h}, M:{m}, S:{s}");
            var totaldt = ((h * 3600) + (m * 60) + (s));
            Debug.WriteLine("totaldt:" + totaldt);
            return totaldt;

        }
        catch (FormatException ex)
        {
            throw new Exception(ex.Message);

        }


    }

    private decimal ConvertToAvailability(int Avail1, int Avail2)
    {

        int Runtimevalue = Avail1 - Avail2;
        decimal Availabilitytime = ((decimal)Runtimevalue / Avail1) * 100;
        string Availabilitystr = Availabilitytime.ToString("0.00"); // Format to 2 decimal place
        decimal Availability = decimal.Parse(Availabilitystr);//value to pass
        return Availability;
    }

    private decimal ConvertToPerformance(int perf1, int perf2)
    {
        int Panelcountsum = perf1;
        int countvalue = (perf2 * 24);
        decimal Perf3 = ((decimal)Panelcountsum / countvalue) * 100;
        string Performancestr = Perf3.ToString("0.00"); // Format to 2 decimal place
        decimal Performance = decimal.Parse(Performancestr);//value to pass
        return Performance;
    }




    public void writeErrorMessage(string errorMessage, string functionName)
    {
        var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LineReport" + "\\" + DateTime.Now.ToString("dd-MM-yyyy");
        //string systemPath = _configuration["AppSettings:FilePath4"]; 
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

#endregion ProcessFiles 
