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
                    using (SqlCommand cmd = new SqlCommand("pro_userlogin", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.Parameters.AddWithValue("@password", password);
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
        public Prod_data getFgDetails(int workordernumber)
        {
            Prod_data objDate = new Prod_data();
            try
            {
                using (SqlConnection pro_con = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand pro_cmd = new SqlCommand("pro_getfgdetails", pro_con))
                    {
                        pro_cmd.CommandType = CommandType.StoredProcedure;
                        pro_cmd.Parameters.AddWithValue("@workorderno",workordernumber);
                        dt = new DataTable();
                        da = new SqlDataAdapter(pro_cmd);
                        da.Fill(dt) ;
                        if (dt.Rows.Count > 0)
                        {
                            objDate.FG_Name = dt.Rows[0]["FgName"].ToString();
                            objDate.WO_Quantity= dt.Rows[0]["Quantity"].ToString();
                        }

                    }
                }

                return objDate;

            }
            catch(Exception ex)
            {
                return objDate;
            }
        }
    }
}
