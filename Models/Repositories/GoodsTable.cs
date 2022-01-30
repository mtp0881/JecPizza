using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class GoodsTable
    {
        private string DbCon = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        public DataTable GetGoodsDataTable()
        {
            DataTable dt = null;

            using (SqlConnection connection = new SqlConnection(DbCon))
            {
                string sql = "Select * from Goods";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);


                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "goods");
                dt = ds.Tables["goods"];
            }

            return dt;
        }

        public int InsertNewGoods(Goods goods)
        {
            int cnt = 0;

            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Insert into Goods Values(@goodsId,@goodsName,@price,@Detail,@reccomend,@newGoods,@hasTopping,@goodsImage,@goodsGroup)";

                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.AddWithValue("@goodsId", goods.GoodsId);
                sqlCommand.Parameters.AddWithValue("@goodsName", goods.GoodsName);
                sqlCommand.Parameters.AddWithValue("@price", goods.Price);
                sqlCommand.Parameters.AddWithValue("@Detail", goods.Detail);
                sqlCommand.Parameters.AddWithValue("@reccomend", goods.Recommend);
                sqlCommand.Parameters.AddWithValue("@newGoods", goods.NewGoods);
                sqlCommand.Parameters.AddWithValue("@hasTopping", goods.HasTopping);
                sqlCommand.Parameters.AddWithValue("@goodsImage", goods.GoodsImage);
                sqlCommand.Parameters.AddWithValue("@goodsGroup", goods.GoodsGroupId.GroupId);


                conn.Open();
                cnt = sqlCommand.ExecuteNonQuery();
                conn.Close();

            }

            return cnt;
        }

        public Goods GetGoods(string goodsId)
        {
            Goods goods = null;

            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Select * from Goods where GoodsId = @gId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@gId", goodsId);
                DataSet ds = new DataSet();

                if (adapter.Fill(ds,"goods") <= 0) return null;

                DataTable dt = ds.Tables["goods"];

                string id = dt.Rows[0][0].ToString();
                string name = dt.Rows[0][1].ToString();
                int p = int.Parse(dt.Rows[0][2].ToString());
                goods = new Goods()
                {
                    GoodsId = dt.Rows[0][0].ToString(),
                    GoodsName = dt.Rows[0][1].ToString(),
                    Price = int.Parse(dt.Rows[0][2].ToString()),
                    Detail = dt.Rows[0][3].ToString(),
                    Recommend = bool.Parse(dt.Rows[0][4].ToString()),
                    NewGoods = bool.Parse(dt.Rows[0][5].ToString()),
                    HasTopping = bool.Parse(dt.Rows[0][6].ToString()),
                    GoodsImage = dt.Rows[0][7].ToString(),
                    GoodsGroupId = new GoodsGroup() { GroupId = char.Parse(dt.Rows[0][8].ToString()) }

                };

            }

            return goods;
        }
        public int InsertRangeGoods(IList<Goods> goodsList)
        {
            int cnt = 0;


            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                foreach (Goods goods in goodsList)
                {
                    string sql = "Insert into Goods Values(@goodsId,@goodsName,@price,@Detail,@recommend,@newGoods,@hasTopping,@goodsImage,@goodsGroup)";

                    SqlCommand sqlCommand = new SqlCommand(sql, conn);

                    sqlCommand.Parameters.AddWithValue("@goodsId", goods.GoodsId);
                    sqlCommand.Parameters.AddWithValue("@goodsName", goods.GoodsName);
                    sqlCommand.Parameters.AddWithValue("@price", goods.Price);
                    sqlCommand.Parameters.AddWithValue("@Detail", goods.Detail);
                    sqlCommand.Parameters.AddWithValue("@recommend", goods.Recommend);
                    sqlCommand.Parameters.AddWithValue("@newGoods", goods.NewGoods);
                    sqlCommand.Parameters.AddWithValue("@hasTopping", goods.HasTopping);
                    sqlCommand.Parameters.AddWithValue("@goodsImage", goods.GoodsImage);
                    sqlCommand.Parameters.AddWithValue("@goodsGroup", goods.GoodsGroupId.GroupId);


                    conn.Open();
                    cnt = sqlCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }

            return cnt;
        }

        public DataTable GetNewGoods()
        {
            DataTable dt = null;


            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Select * from Goods where NewGoods = 1";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();

                adapter.Fill(ds, "newGoods");
                dt = ds.Tables["newGoods"];
            }

            return dt;
        }

        public DataTable GetRecGoods()
        {
            DataTable dt = null;


            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Select * from Goods where Recommend = 1";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();

                int cnt = adapter.Fill(ds, "recGoods");
                dt = ds.Tables["recGoods"];
            }

            return dt;
        }


        public bool DeleteGoods(Goods goods)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Delete from Goods Where GoodsId = @goodsId";

                SqlCommand comm = new SqlCommand(sql, conn);

                comm.Parameters.AddWithValue("@goodsId", goods.GoodsId);

                conn.Open();
                success = comm.ExecuteNonQuery() > 0;
                conn.Open();
            }

            return success;
        }

        public bool UpdateGoods(Goods goods)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(DbCon))
            {
                string sql = "Update Goods set GoodsName = @goodsName, Price = @price,Detail = @Detail,Recommend = @recommend, NewGoods = @newGoods,HasTopping = @hasTopping," +
                             "GoodsImage = @goodsImage,GoodsGroup = @goodsGroup Where GoodsId = @goodsId";

                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.AddWithValue("@goodsId", goods.GoodsId);
                sqlCommand.Parameters.AddWithValue("@goodsName", goods.GoodsName);
                sqlCommand.Parameters.AddWithValue("@price", goods.Price);
                sqlCommand.Parameters.AddWithValue("@Detail", goods.Detail);
                sqlCommand.Parameters.AddWithValue("@recommend", goods.Recommend);
                sqlCommand.Parameters.AddWithValue("@newGoods", goods.NewGoods);
                sqlCommand.Parameters.AddWithValue("@hasTopping", goods.HasTopping);
                sqlCommand.Parameters.AddWithValue("@goodsImage", goods.GoodsImage);
                sqlCommand.Parameters.AddWithValue("@goodsGroup", goods.GoodsGroupId.GroupId);

                conn.Open();
                success = sqlCommand.ExecuteNonQuery() > 0;
                conn.Close();
            }

            return success;
        }

        public DataTable SearchGoods(string str)
        {
            DataTable dt = new DataTable();

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "SELECT * FROM Goods WHERE GoodsName LIKE @str";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@str", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@str"].Value = "%" + str + "%";

                DataSet ds = new DataSet();
                adapter.Fill(ds, "sgoods");
                dt = ds.Tables["sgoods"];
            }

            return dt;
        }

    }
}