using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Humanizer;

namespace Fuji_I.Models
{
    public class DataAccess
    {
        private readonly string _connectionstring;

        public DataAccess(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        DataTable dt;
        SqlDataAdapter da;
        public DataTable getLoginDetails(string userid, string password)
        {

            dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_getLoginDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@empid", userid);
                        cmd.Parameters.AddWithValue("@pwd", password);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            {
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
                using (SqlConnection conn = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_getLineDetails", conn))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@currendata", currentdate);
                        SqlDataAdapter sqlda=new SqlDataAdapter (cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0) {
                            foreach (DataRow row in dt.Rows) {
                                objdata=new Prod_data();
                                objdata.CurrentDate = row["CurrentDate"].ToString();
                                objdata.Hour = row["CurrentHour"].ToString();
                                objdata.FG_Name = row["FG_Name"].ToString();
                                objdata.Target = Convert.ToInt32(row["BoardTarget"].ToString());
                                objdata.Actual = row["BoardActual"].ToString();
                                lstdata.Add(objdata);
                            }
                        }

                    }
                }
                return lstdata;
            }
            catch (Exception ex)
            {
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
                using (SqlConnection conn = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_getWorkOrderDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@currendata", currentdate);
                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Work_order_data = new Work_order();
                                Work_order_data.LineDetaileID = Convert.ToInt32(row["LineDetaileID"].ToString());
                                Work_order_data.WorkOrderNumber = row["WorkOrderNumber"].ToString();
                                Work_order_data.FG_Number = row["FG_Number"].ToString();
                                Work_order_data.Qty = Convert.ToInt32(row["Qty"].ToString());
                                Work_order_data.Cycle_time = Convert.ToInt32(row["CycleTime"].ToString());
                                Work_order_data.uph =Convert.ToInt32(row["uph"].ToString());
                                Work_order_data.CurrentDate = row["CurrentDate"].ToString();

                                Work_order_list.Add(Work_order_data);
                            }
                        }

                    }
                }
                return Work_order_list;
            }
            catch (Exception ex)
            {
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
                using (SqlConnection conn = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_getDailyOutputDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@currendata", currentdate);
                        SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
                        sqlda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Daily_report_data = new Daily_report();
                                Daily_report_data.FG_Name = row["FG_Name"].ToString();
                                Daily_report_data.CurrentDate = Convert.ToDateTime( row["CurrentDate"]);
                                Daily_report_data.BoardActual = Convert.ToInt32(row["BoardActual"]);

                                Daily_report_list.Add(Daily_report_data);
                            }
                        }

                    }
                }
                return Daily_report_list;
            }
            catch (Exception ex)
            {
                return new List<Daily_report>();
            }

        }


        public List<string> getCustomerDetails()
        {
            List<string> result = new List<string>();
            try
            {

                dt = new DataTable();
                using (SqlConnection cus_conn = new SqlConnection(_connectionstring))
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
                return result;
            }

        }

        public List<int> getWorkOrdernumber(string customerno)
        {
            List<int> lstWorkOrderNumber = new List<int>();
            try
            {
                using (SqlConnection con_workorder = new SqlConnection(_connectionstring))
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
                return lstWorkOrderNumber;
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
