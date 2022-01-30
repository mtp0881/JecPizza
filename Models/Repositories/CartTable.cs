using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class CartTable
    {
        private string CSTR = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        public DataTable GetCartDataTable(Member member)
        {
            DataTable dt;

            using (SqlConnection connection = new SqlConnection(CSTR))
            {
                string sql = "Select G.GoodsId, GoodsName, Detail, Price, C.Num, GoodsImage from goods as G join Cart as C on G.GoodsId = C.GoodsId where C.MemberId = @memberId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@memberId", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@memberId"].Value = member.MemberId;

                DataSet ds = new DataSet();

                if (adapter.Fill(ds, "cart") == 0) return null;

                dt = ds.Tables["cart"];
            }

            return dt;
        }

        public Cart GetCart(Member member)
        {
            Cart cart = null;

            using (SqlConnection connection = new SqlConnection(CSTR))
            {
                string sql = "Select G.GoodsId, GoodsName, Detail, Price, C.Num, GoodsImage from goods as G join Cart as C on G.GoodsId = C.GoodsId where C.MemberId = @memberId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@memberId", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@memberId"].Value = member.MemberId;

                DataSet ds = new DataSet();

                if (adapter.Fill(ds, "cart") == 0) return null;

                DataTable dt = ds.Tables["cart"];

                cart = new Cart()
                {
                    GoodsId = dt.Rows[0]["GoodsId"].ToString(),
                    MemberId = dt.Rows[0]["MemberId"].ToString(),
                    CartId = dt.Rows[0]["CartId"].ToString(),
                    Num = int.Parse(dt.Rows[0]["Num"].ToString()),
                    ToppingCartId = dt.Rows[0]["TpCId"].ToString()
                };
            }

            return cart;
        }

        public Cart GetCart(Cart cart)
        {
            Cart c = null;

            using (SqlConnection conn = new SqlConnection(CSTR))
            {
                string sql = "Select * from cart where CartId = @cartId and goodsId = @gId";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@cartId", cart.CartId);
                adapter.SelectCommand.Parameters.AddWithValue("@gId", cart.GoodsId);

                DataSet ds = new DataSet();

                if (adapter.Fill(ds, "cart") == 0) return null;


                DataTable dt = ds.Tables["cart"];

                c = new Cart()
                {
                    GoodsId = dt.Rows[0]["GoodsId"].ToString(),
                    MemberId = dt.Rows[0]["MemberId"].ToString(),
                    CartId = dt.Rows[0]["CartId"].ToString(),
                    Num = int.Parse(dt.Rows[0]["Num"].ToString()),
                    ToppingCartId = dt.Rows[0]["TpCId"].ToString()
                };

            }


            return c;
        }


        public int Update(Cart cart)
        {
            int cnt = 0;
            using (SqlConnection connection = new SqlConnection(CSTR))
            {
                string sql = "Update Cart set Num = @num where GoodsId = @goodsId and memberId = @membId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.Add("@num", SqlDbType.Int);
                command.Parameters["@num"].Value = cart.Num;
                command.Parameters.AddWithValue("@goodsId", cart.GoodsId);
                command.Parameters.AddWithValue("@membId", cart.MemberId);


                connection.Open();
                cnt = command.ExecuteNonQuery();
                connection.Close();
            }

            return cnt;
        }

        public int Insert(Cart cart)
        {
            int cnt;

            using (SqlConnection conn = new SqlConnection(CSTR))
            {
                string sql = "Insert into cart values(@cId,@gId,@mId,@tId,@Num)";
                SqlCommand cmd = new SqlCommand(sql, conn);


                if (GetCart(cart) == null)
                {
                    cmd.Parameters.AddWithValue("@cId", cart.CartId);
                    cmd.Parameters.AddWithValue("@gId", cart.GoodsId);
                    cmd.Parameters.AddWithValue("@mId", cart.MemberId);
                    cmd.Parameters.AddWithValue("@tId", cart.ToppingCartId ?? "NULL");
                    cmd.Parameters.AddWithValue("@Num", cart.Num);

                    conn.Open();
                    cnt = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    sql = "Update Cart set Num = Num + @num Where MemberId = @membId and GoodsId = @goodsId";

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.Add("@num", SqlDbType.Int);
                    cmd.Parameters["@num"].Value = cart.Num;
                    cmd.Parameters.Add("@membId", SqlDbType.VarChar);
                    cmd.Parameters["@membId"].Value = cart.MemberId;
                    cmd.Parameters.Add("@goodsId", SqlDbType.Char);
                    cmd.Parameters["@goodsId"].Value = cart.GoodsId;
                    conn.Open();
                    cnt = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }




            return cnt;
        }

        public string GetCartId(Member member)
        {
            string id = null;

            using (SqlConnection conn = new SqlConnection(CSTR))
            {
                string sql = "Select CartId from Cart where MemberId = @membId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@membId", member.MemberId);

                DataSet ds = new DataSet();
                if (adapter.Fill(ds, "cart") == 0) return null;

                DataTable dt = ds.Tables["cart"];

                id = dt.Rows[0][0].ToString();
            }

            return id;
        }

        public int Delete(Cart cart)
        {
            int cnt = 0;
            using (SqlConnection connection = new SqlConnection(CSTR))
            {
                string sql = "delete from cart "
                             + "where memberId = @memberId "
                             + "and goodsId = @goodsId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@memberId", SqlDbType.VarChar);
                cmd.Parameters["@memberId"].Value = cart.MemberId;

                cmd.Parameters.Add("@goodsId", SqlDbType.Char);

                cmd.Parameters["@goodsId"].Value = cart.GoodsId;


                connection.Open();
                cnt = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return cnt;
        }

    }
}