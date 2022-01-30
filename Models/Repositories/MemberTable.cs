using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class MemberTable
    {
        public bool LoginMember(Member member)
        {
            bool ret = false;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from member where email = @email and password = @password";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@email", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@email"].Value = member.MemberId;

                adapter.SelectCommand.Parameters.Add("@password", SqlDbType.NVarChar);
                adapter.SelectCommand.Parameters["@password"].Value = member.Password;

                DataSet ds = new DataSet();
                ret = adapter.Fill(ds) > 0;
            }

            return ret;
        }

        public Member GetMember(Member member)
        {
            Member retMember = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "select * from member where Email = @MemberId and password = @pass";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@MemberId", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@MemberId"].Value = member.MemberId;
                adapter.SelectCommand.Parameters.Add("@pass", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@pass"].Value = member.Password;

                DataSet ds = new DataSet();
                int cnt = adapter.Fill(ds, "member");

                if (cnt != 0)
                {
                    retMember = new Member();

                    DataTable dt = ds.Tables["member"];
                    retMember.MemberId = dt.Rows[0]["memberId"].ToString();
                    retMember.MemberName = dt.Rows[0]["membername"].ToString();
                    retMember.UserName = dt.Rows[0]["username"].ToString();
                    retMember.MemberName = dt.Rows[0]["membername"].ToString();
                    retMember.Email = dt.Rows[0]["email"].ToString();
                    retMember.Tel = dt.Rows[0]["tel"].ToString();
                    retMember.PostCode = dt.Rows[0]["postcode"].ToString();
                    retMember.Address = dt.Rows[0]["address"].ToString();
                    retMember.Dob = DateTime.Parse(dt.Rows[0]["dob"].ToString());
                    retMember.Sex = (bool)dt.Rows[0]["sex"];
                    retMember.Password = dt.Rows[0]["Password"].ToString();
                    retMember.Img = dt.Rows[0]["img"].ToString();
                }
            }

            return retMember;
        }

    

        public DataTable AccountPurchaseData(Member member)
        {
            DataTable ret = null;

            string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cstr))
            {
                string sql = "SELECT *"+
                        " FROM(((Member LEFT OUTER JOIN Purchase ON Member.MemberId = Purchase.MemberId)"+
                        " LEFT OUTER JOIN Cart ON Purchase.CartId = Cart.CartId)"+
                        " LEFT OUTER JOIN Goods ON Cart.GoodsId = Goods.GoodsId)"+
                        " WHERE Member.memberid = @memberid";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@MemberId", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@MemberId"].Value = member.MemberId;

                DataSet ds = new DataSet();

                adapter.Fill(ds, "purchseData");

                ret = ds.Tables["purchseData"];
            }

            return ret;
        }

    public int Insert(Member member)
        {
            int cnt = 0;

            if (GetMember(member) == null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "insert into member values (@memberid,@username,@membername,@email,@tel,@postcode,@address,@dob,@sex,null,@password)";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@memberid", SqlDbType.VarChar);
                    command.Parameters["@memberid"].Value = member.MemberId;

                    command.Parameters.Add("@username", SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = member.UserName;

                    command.Parameters.Add("@membername", SqlDbType.NVarChar);
                    command.Parameters["@membername"].Value = member.MemberName;

                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = member.Email;

                    command.Parameters.Add("@tel", SqlDbType.VarChar);
                    command.Parameters["@tel"].Value = member.Tel;

                    command.Parameters.Add("@postcode", SqlDbType.VarChar);
                    command.Parameters["@postcode"].Value = member.PostCode.Contains("-") ? member.PostCode.Replace("-", "") : member.PostCode;

                    command.Parameters.Add("@address", SqlDbType.NVarChar);
                    command.Parameters["@address"].Value = member.Address;

                    command.Parameters.Add("@dob", SqlDbType.DateTime);
                    command.Parameters["@dob"].Value = member.Dob;

                    command.Parameters.Add("@sex", SqlDbType.Bit);
                    command.Parameters["@sex"].Value = member.Sex;

                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = member.Password;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }

            return cnt;
        }

        public int Update(Member member)
        {
            int cnt = 0;

            if (GetMember(member) != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "update member set username = @username,membername = @membername,email = @email,tel = @tel,postcode = @postcode,address = @address,img = @img,password = @password where memberid = @memberid";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@memberid", SqlDbType.VarChar);
                    command.Parameters["@memberid"].Value = member.MemberId;

                    command.Parameters.Add("@username", SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = member.UserName;

                    command.Parameters.Add("@membername", SqlDbType.NVarChar);
                    command.Parameters["@membername"].Value = member.MemberName;

                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = member.Email;

                    command.Parameters.Add("@tel", SqlDbType.VarChar);
                    command.Parameters["@tel"].Value = member.Tel;

                    command.Parameters.Add("@postcode", SqlDbType.VarChar);
                    command.Parameters["@postcode"].Value = member.PostCode;

                    command.Parameters.Add("@address", SqlDbType.NVarChar);
                    command.Parameters["@address"].Value = member.Address;

                    command.Parameters.Add("@img", SqlDbType.VarChar);
                    command.Parameters["@img"].Value = member.Img;

                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = member.Password;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            return cnt;
        }

        public int Delete(Member member)
        {
            int cnt = 0;

            if (GetMember(member) != null)
            {
                string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(cstr))
                {
                    string sql = "delete from member where memberid = @memberid and password = @password";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.Add("@memberid", SqlDbType.VarChar);
                    command.Parameters["@memberid"].Value = member.MemberId;

                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = member.Password;

                    connection.Open();
                    cnt = command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            return cnt;
        }
    }
}