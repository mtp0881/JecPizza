using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JecPizza.Models.Repositories
{
    public class CardTable
    {
        private string CSTR = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;

        public DataTable GetCardDataTable(Member member)
        {
            DataTable dt;

            using (SqlConnection connection = new SqlConnection(CSTR))
            {
                string sql = "SELECT * FROM card inner join member ON card.MemberId = Member.MemberId WHERE Member.memberid = @memberId";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                adapter.SelectCommand.Parameters.Add("@memberId", SqlDbType.VarChar);
                adapter.SelectCommand.Parameters["@memberId"].Value = member.MemberId;

                DataSet ds = new DataSet();

                if (adapter.Fill(ds, "card") == 0) return null;

                dt = ds.Tables["card"];
            }

            return dt;
        }
        

        //public DataTable GetCard (Member member)
        //{
        //    DataTable dt = null;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        string sql = "SELECT card.CardNum,card.Code,card.Name,card.Period,card.MemberId " + 
        //                     "FROM card inner join member ON card.MemberId = Member.MemberId " +
        //                     "WHERE memberid = @memberid";

        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
        //        adapter.SelectCommand.Parameters.Add("@memberid", SqlDbType.VarChar);
        //        adapter.SelectCommand.Parameters["@memberid"].Value = member.MemberId;
        //        DataSet ds = new DataSet();
        //        int cnt = adapter.Fill(ds, "card");
        //        dt = ds.Tables["card"];
        //    }
        //    return dt;
        //}

        //public Card GetCard (Card card)
        //{
        //    Card retCard = null;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        string sql = "SELECT * FROM card WHERE memberid = @memberid";
        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
        //        adapter.SelectCommand.Parameters.Add("@memberid", SqlDbType.VarChar);
        //        adapter.SelectCommand.Parameters["@memberid"].Value = card.MemberId;
        //        DataSet ds = new DataSet();
        //        int cnt = adapter.Fill(ds, "card");
        //        if (cnt != 0)
        //        {
        //            DataTable dt = ds.Tables["card"];
        //            retCard = new Card();
        //            retCard.CardNum = dt.Rows[0]["CardNum"].ToString();
        //            retCard.Name = dt.Rows[0]["Name"].ToString();
        //            retCard.Period = (DateTime)dt.Rows[0]["Period"];
        //            retCard.Code = dt.Rows[0]["Code"].ToString();
        //            retCard.MemberId = dt.Rows[0]["MemberId"].ToString();
        //        }
        //    }
        //    return retCard;
        //}

        //public int Update(Card card)
        //{
        //    int cnt = 0;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        string sql = "UPDATE cart " +
        //                     "SET cardNum=@cardNum,Name=@Name,Period=@Period,Code=@Code,MemberId=@MemberId " +
        //                     "WHERE memberid=@memberid and cardNum=@cardNum";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.Parameters.Add("@cardNum", SqlDbType.Char);
        //        command.Parameters["@cardNum"].Value = card.CardNum;
        //        command.Parameters.Add("@Name", SqlDbType.VarChar);
        //        command.Parameters["@Name"].Value = card.Name;
        //        command.Parameters.Add("@Period", SqlDbType.DateTime);
        //        command.Parameters["@Period"].Value = card.Period;
        //        command.Parameters.Add("@Code", SqlDbType.Char);
        //        command.Parameters["@Code"].Value = card.Code;
        //        command.Parameters.Add("@MemberId", SqlDbType.VarChar);
        //        command.Parameters["@MemberId"].Value = card.MemberId;
        //    }
        //    return cnt;
        //}

        //public int Insert(Card card)
        //{
        //    int cnt = 0;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        SqlCommand command;
        //        if (GetCard(card) == null)
        //        {
        //            string sql = "INSERT INTO card VALUES(@cardNum,@Name,@Period,@Code,@MemberId)";
        //            command = new SqlCommand(sql, connection);
        //            command.Parameters.Add("@cardNum", SqlDbType.Char);
        //            command.Parameters["@cardNum"].Value = card.CardNum;
        //            command.Parameters.Add("@Name", SqlDbType.VarChar);
        //            command.Parameters["@Name"].Value = card.Name;
        //            command.Parameters.Add("@Period", SqlDbType.DateTime);
        //            command.Parameters["@Period"].Value = card.Period;
        //            command.Parameters.Add("@Code", SqlDbType.Char);
        //            command.Parameters["@Code"].Value = card.Code;
        //            command.Parameters.Add("@MemberId", SqlDbType.VarChar);
        //            command.Parameters["@MemberId"].Value = card.MemberId;
        //        }
        //        else
        //        {
        //            string sql = "UPDATE cart SET @Code=@Code where memberid=@memberid and cardNum=@cardNum";
        //            command = new SqlCommand(sql, connection);
        //            command.Parameters.Add("@Code", SqlDbType.Char);
        //            command.Parameters["@Code"].Value = card.Code;
        //            command.Parameters.Add("@memberid", SqlDbType.VarChar);
        //            command.Parameters["@memberid"].Value = card.MemberId;
        //            command.Parameters.Add("@cardNum", SqlDbType.Char);
        //            command.Parameters["@cardNum"].Value = card.CardNum;
        //        }
        //        connection.Open();
        //        cnt = command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    return cnt;
        //}

        //public int Delete(Card card)
        //{s
        //    int cnt = 0;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        string sql = "DELETE FROM card WHERE memberid=@memberid AND cardNum=@cardNum";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.Parameters.Add("@memberid", SqlDbType.VarChar);
        //        command.Parameters["@memberid"].Value = card.MemberId;
        //        command.Parameters.Add("@cardNum", SqlDbType.Char);
        //        command.Parameters["@cardNum"].Value = card.CardNum;
        //        connection.Open();
        //        cnt = command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    return cnt;
        //}

        //public int Delete(Member member)
        //{
        //    int cnt = 0;
        //    string cstr = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(cstr))
        //    {
        //        string sql = "DELETE FROM card WHERE memberid=@memberid";
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.Parameters.Add("@memberid", SqlDbType.VarChar);
        //        command.Parameters["@memberid"].Value = member.MemberId;
        //        connection.Open();
        //        cnt = command.ExecuteNonQuery();
        //        connection.Close();
        //    }
        //    return cnt;
        //}
    }
}