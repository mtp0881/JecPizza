using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class ToppingCartTable
    {
        public IList<Topping> GetAllToppingCart(ToppingCart toppingCart)
        {
            IList<Topping> retToppingCart = null;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from ToppingCart where TpCId = @tpcid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@tpcid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@tpcid"].Value = toppingCart.TpCId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "toppingcart");

                if(cnt != 0)
                {
                    DataTable dt = ds.Tables["toppingcart"];

                    retToppingCart = new List<Topping>();

                    foreach (DataRow tpId in dt.Rows)
                    {
                        sql = "Select * from Topping where ToppingId = @tpId";

                        adapter = new SqlDataAdapter(sql, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@tpId", tpId["ToppingId"].ToString());
                        adapter.Fill(ds, "topping");

                        DataTable topTable = ds.Tables["topping"];

                        foreach (DataRow topping in topTable.Rows)
                        {
                            Topping top = new Topping()
                            {
                                ToppingId = topping["ToppingId"].ToString(),
                                ToppingName = topping["ToppingName"].ToString(),
                                Price = int.Parse(topping["Price"].ToString()),
                                Image = topping["Image"].ToString()
                            };

                            retToppingCart.Add(top);
                        }
                        
                    }


                }
            }

            return retToppingCart;
        }

        public ToppingCart GetToppingCart(ToppingCart toppingCart)
        {
            ToppingCart retToppingCart = null;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from toppingcart where tpcid = @tpcid and toppingid = @toppingid";
 
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@tpcid", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@tpcid"].Value = toppingCart.TpCId;

                adapter.SelectCommand.Parameters.Add("@toppingid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@toppingid"].Value = toppingCart.ToppingId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "cart");
                if (cnt != 0)
                {
                    DataTable dt = ds.Tables["cart"];
                    retToppingCart = new ToppingCart();

                    retToppingCart.TpCId = dt.Rows[0]["tpcid"].ToString();
                    retToppingCart.ToppingId = (Topping)dt.Rows[0]["toppingid"];
                    retToppingCart.Num = (int)dt.Rows[0]["num"];
                }
            }

            return retToppingCart;
        }

        public int Update(ToppingCart toppingCart)
        {
            int cnt = 0;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "update toppingcart set num = @num where toppingid = @toppingid";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@num", SqlDbType.Int);
                command.Parameters["@num"].Value = toppingCart.Num;

                command.Parameters.Add("@toppingid", SqlDbType.Char);
                command.Parameters["@toppingid"].Value = toppingCart.ToppingId;

                connection.Open();
                cnt = command.ExecuteNonQuery();
                connection.Close();
            }

            return cnt;
        }

        public int Insert(ToppingCart toppingCart)
        {
            int cnt = 0;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                SqlCommand command;
                if (GetToppingCart(toppingCart) == null)
                {
                    string sql = "insert into toppingcart values(@tpcid,@toppingid,@num)";

                    command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@tpcid", SqlDbType.VarChar);
                    command.Parameters["@tpcid"].Value = toppingCart.TpCId;

                    command.Parameters.Add("@toppingid", SqlDbType.Char);
                    command.Parameters["@toppingid"].Value = toppingCart.ToppingId;

                    command.Parameters.Add("@num", SqlDbType.Int);
                    command.Parameters["@num"].Value = toppingCart.Num;
                }
                else
                {
                    string sql = "update toppingcart set num = num + @num where tpcid = @tpcid and toppingid = @toppingid";
 
                    command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@num", SqlDbType.Int);
                    command.Parameters["@num"].Value = toppingCart.Num;

                    command.Parameters.Add("@tpcid", SqlDbType.VarChar);
                    command.Parameters["@tpcid"].Value = toppingCart.TpCId;

                    command.Parameters.Add("@toppingid", SqlDbType.Char);
                    command.Parameters["@toppingid"].Value = toppingCart.ToppingId;
                }

                connection.Open();
                cnt = command.ExecuteNonQuery();
                connection.Close();
            }

            return cnt;
        }

        public int Delete(ToppingCart toppingCart)
        {
            int cnt = 0;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "delete from toppingcart where tcpid = @tpcid and toppingid = @toppingid";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@tpcid", SqlDbType.VarChar);
                command.Parameters["@tpcid"].Value = toppingCart.TpCId;

                command.Parameters.Add("@toppingid", SqlDbType.Char);
                command.Parameters["@toppingid"].Value = toppingCart.ToppingId;

                connection.Open();
                cnt = command.ExecuteNonQuery();
                connection.Close();
            }

            return cnt;
        }
    }
}