using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Humanizer;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Math;

namespace Fuji_I.Models
{
    public class DataAccess
    {
        private readonly string _connectionstring1;
        private readonly string _connectionstring2;

        public DataAccess(IConfiguration configuration)
        {
             _connectionstring1 = configuration.GetConnectionString("DefaultConnection");
            //_connectionstring1 = configuration.GetConnectionString("TestConnection");
            _connectionstring2 = configuration.GetConnectionString("ProdDataConnection");
        }
        DataTable dt;
        SqlDataAdapter da;
        public DataTable getLoginDetails(string userid, string password)
        {
            writeErrorMessage(_connectionstring1.ToString(), "getLoginDetails - Connection Details");
            dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionstring1))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_getLoginDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@empid", userid);
                        cmd.Parameters.AddWithValue("@pwd", password);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        writeErrorMessage(dt.Rows.Count.ToString(), "getLoginDetails - Data Table Rows Count");
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getLoginDetails - Error");
                return dt;
            }
        }

        public List<Prod_data> getLineDetails(string currentdate)
        {
            dt=new DataTable();
            Prod_data objdata;
            List<Prod_data> lstdata=new List<Prod_data>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionstring2))
                {
                    using (SqlCommand cmd = new SqlCommand("DIGI_SMTDASHBOARD_AOI", conn))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datarep", "Hourly_report");
                        cmd.Parameters.AddWithValue("@from_date", currentdate);
                        SqlDataAdapter sqlda=new SqlDataAdapter (cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0) {
                            foreach (DataRow row in dt.Rows) {
                                objdata=new Prod_data();
                                objdata.CurrentDate = row["CurrentDate"].ToString();
                                objdata.Hour = row["Hour"].ToString();
                                objdata.WorkOrder = row["WorkOrder"].ToString();
                                objdata.FG_Name = (row["FG_Name"].ToString());
                                objdata.PCB_COUNT = Convert.ToInt32(row["PCB_COUNT"].ToString());
                                lstdata.Add(objdata);
                            }
                        }

                    }
                }
                return lstdata;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getLineDetails");
                return new List<Prod_data>();
            }
           
        }


        public List<Work_order> getWorkOrderDetails(string currentdate)
        {
            dt = new DataTable();
            Work_order Work_order_data;
            List<Work_order> Work_order_list = new List<Work_order>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionstring2))
                {
                    using (SqlCommand cmd = new SqlCommand("DIGI_SMTDASHBOARD_AOI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datarep", "Work_order");
                        cmd.Parameters.AddWithValue("@from_date", currentdate);
                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Work_order_data = new Work_order();
                                //Work_order_data.LineDetaileID = Convert.ToInt32(row["LineDetaileID"].ToString());
                                Work_order_data.CurrentDate = row["CurrentDate"].ToString();
                                Work_order_data.WorkOrderNumber = row["WorkOrderNumber"].ToString();
                                Work_order_data.FG_Name = row["FG_Name"].ToString();
                                Work_order_data.Buildtype = row["Buildtype"].ToString();
                                Work_order_data.Qty = Convert.ToInt32(row["Qty"].ToString());
                                
                                //Work_order_data.Cycle_time = Convert.ToInt32(row["CycleTime"].ToString());
                                //Work_order_data.uph =Convert.ToInt32(row["uph"].ToString());
                                

                                Work_order_list.Add(Work_order_data);
                            }
                        }

                    }
                }
                return Work_order_list;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getWorkOrderDetails");
                return new List<Work_order>();
            }

        }


        public List<Daily_report> getDailyOutputDetails(string currentdate)
        {
            dt = new DataTable();
            Daily_report Daily_report_data;
            List<Daily_report> Daily_report_list = new List<Daily_report>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionstring2))
                {
                    using (SqlCommand cmd = new SqlCommand("DIGI_SMTDASHBOARD_AOI", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datarep", "Day_report");
                        cmd.Parameters.AddWithValue("@from_date", currentdate);
                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Daily_report_data = new Daily_report();
                                Daily_report_data.CurrentDate = row["CurrentDate"].ToString();
                                Daily_report_data.WorkOrder = row["WorkOrder"].ToString();
                                Daily_report_data.FG_Name = row["FG_Name"].ToString();
                                Daily_report_data.PCB_COUNT = Convert.ToInt32(row["PCB_COUNT"]);
                                Daily_report_list.Add(Daily_report_data);
                            }
                        }

                    }
                }
                return Daily_report_list;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getDailyOutputDetails");
                return new List<Daily_report>();
            }

        }


        public List<string> getCustomerDetails()
        {
            List<string> result = new List<string>();
            try
            {

                dt = new DataTable();
                using (SqlConnection cus_conn = new SqlConnection(_connectionstring2))
                {
                    using (SqlCommand cus_cmd = new SqlCommand("pro_getcustomerlist", cus_conn))
                    {
                        cus_cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cus_cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(dr["customername"].ToString());
                            }
                        }
                    }
                    return result;
                }
            }

            catch(Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getCustomerDetails");
                return result;
            }

        }

        public List<int> getWorkOrdernumber(string customerno)
        {
            List<int> lstWorkOrderNumber = new List<int>();
            try
            {
                using (SqlConnection con_workorder = new SqlConnection(_connectionstring2))
                {
                    using (SqlCommand com_workOrder = new SqlCommand("pro_getworkorderno", con_workorder))
                    {
                        com_workOrder.CommandType = CommandType.StoredProcedure;
                        com_workOrder.Parameters.AddWithValue("@customername", customerno);
                        dt = new DataTable();
                        da = new SqlDataAdapter(com_workOrder);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                lstWorkOrderNumber.Add(Convert.ToInt32(dr["Workorder_no"]));
                            }
                        }
                    }
                }
                return lstWorkOrderNumber;
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "getWorkOrdernumber");
                return lstWorkOrderNumber;
            }
        }

        public int UpdatePassword(string empid, string password)
        {
            int updateResult = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionstring1))
                {
                    using (SqlCommand com_password = new SqlCommand("pro_UpdatePassword", sqlConnection))
                    {
                        com_password.CommandType = CommandType.StoredProcedure;
                        com_password.Parameters.AddWithValue("@empid", empid);
                        com_password.Parameters.AddWithValue("@Password", password);
                        sqlConnection.Open();
                        updateResult = com_password.ExecuteNonQuery();
                        sqlConnection.Close();
                        return updateResult;
                    }
                }
            }
            catch (Exception ex)
            {
                writeErrorMessage(ex.Message.ToString(), "UpdatePassword");
                return updateResult;
            }
        }

        public void writeErrorMessage(string errorMessage, string functionName)
        {
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Messages  -" + "\\" + DateTime.Now.ToString("dd-MM-yyyy");

            if (!Directory.Exists(systemPath))
            {
                Directory.CreateDirectory(systemPath);
            }

            string WrErrorLog = String.Format(@"{0}\{1}.txt", systemPath, "LineReportDetails");
            using (StreamWriter errLogs = new StreamWriter(WrErrorLog, true))
            {
                errLogs.WriteLine("--------------------------------------------------------------------------------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine("---------------------------------------------------" + DateTime.Now + "----------------------------------------------" + Environment.NewLine);
                errLogs.WriteLine(errorMessage + Environment.NewLine + "-----" + functionName);
                errLogs.Close();
            }
        }
        //public Prod_data getFgDetails(int workordernumber)
        //{
        //    Prod_data objDate = new Prod_data();
        //    try
        //    {
        //        using (SqlConnection pro_con = new SqlConnection(_connectionstring))
        //        {
        //            using (SqlCommand pro_cmd = new SqlCommand("pro_getfgdetails", pro_con))
        //            {
        //                pro_cmd.CommandType = CommandType.StoredProcedure;
        //                pro_cmd.Parameters.AddWithValue("@workorderno",workordernumber);
        //                dt = new DataTable();
        //                da = new SqlDataAdapter(pro_cmd);
        //                da.Fill(dt) ;
        //                if (dt.Rows.Count > 0)
        //                {
        //                    objDate.FG_Name = dt.Rows[0]["FgName"].ToString();
        //                    objDate.WO_Quantity= dt.Rows[0]["Quantity"].ToString();
        //                }

        //            }
        //        }

        //        return objDate;

        //    }
        //    catch(Exception ex)
        //    {
        //        return objDate;
        //    }
        //}
    }
}
