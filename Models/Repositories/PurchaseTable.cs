using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class PurchaseTable
    {
        private string CSTR = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        public DataTable GetPurchaseHistory(Member membId)
        {
            DataTable dt = null;

            using (SqlConnection conn = new SqlConnection(CSTR))
            {
                string sql = "Select * from Purchase where memberId = @membId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                adapter.SelectCommand.Parameters.AddWithValue("@membId", membId.MemberId);

                DataSet ds = new DataSet();

                if (adapter.Fill(ds, "pur") == 0) return null;
                
                dt = ds.Tables["pur"];
            }

            return dt;
        }

        public bool Insert(Purchase purchase)
        {
            bool res = false;

            using (SqlConnection conn = new SqlConnection(CSTR))
            {
                string sql = "Insert into Purchase Values(@purId,@MembId,@CartId,@CardNum,@date,GetDate(),@address,@total";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@purId", purchase.PurId);
                cmd.Parameters.AddWithValue("@MembId", purchase.MemId);
                cmd.Parameters.AddWithValue("@CartId", purchase.CartId);
                cmd.Parameters.AddWithValue("@CardNum", purchase.CardNum);
                cmd.Parameters.AddWithValue("@date", purchase.DeliveryDate);
                cmd.Parameters.AddWithValue("@address", purchase.PurchaseDate);
                cmd.Parameters.AddWithValue("@total", purchase.Total);

                conn.Open();
                res = cmd.ExecuteNonQuery() > 0;
                conn.Close();
                
            }

            return res;
        }
    }
}