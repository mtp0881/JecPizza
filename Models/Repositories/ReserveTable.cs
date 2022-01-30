using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class ReserveTable
    {
        public Reserve GetReserve(Reserve reserve)
        {
            Reserve retReserve = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from reserve where reserveid = @reserveid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@reserveid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@reserveid"].Value = reserve.ReserveId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "reserve");

                if (cnt != 0)
                {
                    retReserve = new Reserve();

                    DataTable dt = ds.Tables["reserve"];

                    retReserve.MemberId = dt.Rows[0]["memberid"].ToString();
                    retReserve.ReserveDateTime = DateTime.Parse(dt.Rows[0]["reservedatetime"].ToString());
                    retReserve.Num = (int)dt.Rows[0]["num"];
                    retReserve.TableNo = (int)dt.Rows[0]["tableno"];
                }
            }

            return retReserve;
        }


        public DataTable GetAllReserve()
        {
            DataTable dt = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from reserve";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "reserve");
                dt = ds.Tables["reserve"];
            }

            return dt;
        }

        public int Insert(Reserve reserve)
        {
            int cnt = 0;

            if (GetReserve(reserve) == null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "insert into reserve values (@reserveid,@memberid,@num,@tableno,@time)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@reserveid", SqlDbType.Char);
                    command.Parameters["@reserveid"].Value = reserve.ReserveId;

                    command.Parameters.Add("@memberid", SqlDbType.VarChar);
                    command.Parameters["@memberid"].Value = reserve.MemberId;

                    command.Parameters.Add("@time", SqlDbType.DateTime);
                    command.Parameters["@time"].Value = reserve.ReserveDateTime;

                    command.Parameters.Add("@num", SqlDbType.Int);
                    command.Parameters["@num"].Value = reserve.Num;

                    command.Parameters.Add("@tableno", SqlDbType.Int);
                    command.Parameters["@tableno"].Value = reserve.TableNo;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }

        public int Update(int num, string reid)
        {
            int cnt = 0;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "update reserve set num = @num where reserveid = @reid";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@num", SqlDbType.Int);
                command.Parameters["@num"].Value = num;

                command.Parameters.Add("@reid", SqlDbType.VarChar);
                command.Parameters["@reid"].Value = reid;

                connection.Open();
                cnt = command.ExecuteNonQuery();

                connection.Close();
            }
            return cnt;
        }

        public int Delete(string reid)
        {
            int cnt = 0;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "delete from reserve where reserveid = @reserveid";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@reserveid", SqlDbType.Char);
                command.Parameters["@reserveid"].Value = reid;

                connection.Open();
                cnt = command.ExecuteNonQuery();

                connection.Close();
            }

            return cnt;
        }

        //public List<int> GettableId(DateTime datetime)
        //{
        //    List<int> ret_list = null;
        //    DataTable dt = null;

        //    if (datetime != null)
        //    {
        //        string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //        using (SqlConnection connection = new SqlConnection(cstr))
        //        {

        //            string sql = "select TableNo from Reserve where ReserveDateTime = @time";

        //            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

        //            adapter.SelectCommand.Parameters.Add("@time", SqlDbType.DateTime);
        //            adapter.SelectCommand.Parameters["@time"].Value = datetime;

        //            DataSet ds = new DataSet();
        //            adapter.Fill(ds, "reserve");
        //            dt = ds.Tables["reserve"];
        //            ret_list = new List<int>();

        //            foreach (DataRow item in dt.Rows)
        //            {
        //                ret_list.Add(int.Parse(item[0].ToString()));
        //            }
        //        }
        //    }

        //    return ret_list;
        //}

        public DataTable GettableId(DateTime datetime)
        {
            DataTable dt = null;

            if (datetime != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {

                    string sql = "select * from Reserve where ReserveDateTime = @time";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                    adapter.SelectCommand.Parameters.Add("@time", SqlDbType.DateTime);
                    adapter.SelectCommand.Parameters["@time"].Value = datetime;

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "reserve");
                    dt = ds.Tables["reserve"];
                }
            }

            return dt;
        }

        public DataTable GetReserve(Member member)
        {
            DataTable dt = new DataTable();

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from reserve where MemberId = @mid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@mid", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@mid"].Value = member.MemberId;

                DataSet ds = new DataSet();
                adapter.Fill(ds, "reserve");
                dt = ds.Tables["reserve"];
            }

            return dt;
        }

        public DataTable GethourReserve(DateTime dateTime)
        {
            DataTable dt = new DataTable();
            TimeSpan ts = new TimeSpan(0, 2, 0, 0);

            DateTime aftertime = dateTime - ts;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "SELECT TableNo FROM Reserve WHERE ReserveDateTime BETWEEN @aftertime AND @dateTime";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@aftertime", SqlDbType.DateTime);
                adapter.SelectCommand.Parameters["@aftertime"].Value = aftertime;
                adapter.SelectCommand.Parameters.Add("@dateTime", SqlDbType.DateTime);
                adapter.SelectCommand.Parameters["@dateTime"].Value = dateTime;

                DataSet ds = new DataSet();
                adapter.Fill(ds, "reserve");
                dt = ds.Tables["reserve"];
            }

            return dt;
        }
    }
}