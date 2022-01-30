using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class ToppingTable
    {
        public DataTable GetAllTopping()
        {
            DataTable dt = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from topping";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "topping");
                dt = ds.Tables["topping"];
            }

            return dt;
        }

        public Topping GetTopping(Topping topping)
        {
            Topping retTopping = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from topping where toppingid = @toppingid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@toppingid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@toppingid"].Value = topping.ToppingId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "topping");

                if (cnt != 0)
                {
                    retTopping = new Topping();

                    DataTable dt = ds.Tables["reserve"];

                    retTopping.ToppingName = dt.Rows[0]["toppingname"].ToString();
                    retTopping.Price = (int)dt.Rows[0]["price"];
                    retTopping.Image = dt.Rows[0]["image"].ToString();
                }
            }

            return retTopping;
        }
        public int Insert(Topping topping)
        {
            int cnt = 0;

            if (GetTopping(topping) == null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "insert into topping values (@toppingid,@toppingname,@price,@image)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@toppingid", SqlDbType.Char);
                    command.Parameters["@toppingid"].Value = topping.ToppingId;

                    command.Parameters.Add("@toppingname", SqlDbType.NVarChar);
                    command.Parameters["@toppingname"].Value = topping.ToppingName;

                    command.Parameters.Add("@price", SqlDbType.Int);
                    command.Parameters["@price"].Value = topping.Price;

                    command.Parameters.Add("@image", SqlDbType.NVarChar);
                    command.Parameters["@image"].Value = topping.Image;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }
        public int Update(Topping topping)
        {
            int cnt = 0;

            if (GetTopping(topping) != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "update reserve set toppingname = @toppingname,price = @price,image = @image where toppingid = @toppingid";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@toppingid", SqlDbType.Char);
                    command.Parameters["@toppingid"].Value = topping.ToppingId;

                    command.Parameters.Add("@toppingname", SqlDbType.NVarChar);
                    command.Parameters["@toppingname"].Value = topping.ToppingName;

                    command.Parameters.Add("@price", SqlDbType.Int);
                    command.Parameters["@price"].Value = topping.Price;

                    command.Parameters.Add("@image", SqlDbType.NVarChar);
                    command.Parameters["@image"].Value = topping.Image;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            return cnt;
        }
        public int Delete(Topping topping)
        {
            int cnt = 0;

            if (GetTopping(topping) != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "delete from topping where toppingid = @toppingid";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@toppingid", SqlDbType.Char);
                    command.Parameters["@toppingid"].Value = topping.ToppingId;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }
    }
}