using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class StuffTable
    {
        public Stuff GetStuff(Stuff stuff)
        {
            Stuff retStuff = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from stuff where goodsid = @goodsid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@goodsid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@goodsid"].Value = stuff.GoodsId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "stuff");

                if (cnt != 0)
                {
                    retStuff = new Stuff();

                    DataTable dt = ds.Tables["stuff"];

                    retStuff.StuffId = dt.Rows[0]["stuffid"].ToString();
                    retStuff.GoodsId = dt.Rows[0]["goodsid"].ToString();
                    retStuff.MaterialId = (int)dt.Rows[0]["materialid"];
                }
            }

            return retStuff;
        }

        public int Insert(Stuff stuff)
        {
            int cnt = 0;

            if (GetStuff(stuff) == null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "insert into stuff values (@stuffid,@goodsid,@materialid)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@reserveid", SqlDbType.Char);
                    command.Parameters["@reserveid"].Value = stuff.StuffId;

                    command.Parameters.Add("@goodsid", SqlDbType.Char);
                    command.Parameters["@goodsid"].Value = stuff.GoodsId;

                    command.Parameters.Add("@materialid", SqlDbType.Int);
                    command.Parameters["@materialid"].Value = stuff.MaterialId;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }

        public int Delete(Stuff stuff)
        {
            int cnt = 0;

            if (GetStuff(stuff) != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "delete from stuff where stuffid = @stuffid";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@stuffid", SqlDbType.Char);
                    command.Parameters["@stuffid"].Value = stuff.StuffId;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }

        public List<Material> GetMaterial(string goodsId)
        {
            List<Material> materials = null;
            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select materialname,allergy from material where materialid in (select materialid from stuff where goodsid = @goodsid)";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@goodsid", SqlDbType.Char);
                adapter.SelectCommand.Parameters["@goodsid"].Value = goodsId;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "material");

                if(cnt != 0)
                {
                    materials = new List<Material>();
                    DataTable dt = ds.Tables["materials"];

                    foreach (DataRow item in dt.Rows)
                    {
                        Material material = new Material()
                        {
                            Allergy = bool.Parse(item["Allergy"].ToString()),
                            MaterialName = item["MaterialName"].ToString()
                        };

                        materials.Add(material);
                    }
                }

                return materials;
            }
        }
    }
}