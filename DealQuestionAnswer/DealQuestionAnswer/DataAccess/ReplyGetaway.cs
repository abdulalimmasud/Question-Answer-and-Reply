using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using DealQuestionAnswer.Model;

namespace DealQuestionAnswer.DataAccess
{
    public class ReplyGetaway
    {
        public static int SaveReply(Replys reply)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert Replys(Reply,AnswerId,UserId) values(@rply,@aId,@uId)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@rply", reply.Reply);
                    cmd.Parameters.AddWithValue("@aId", reply.AnswerID);
                    cmd.Parameters.AddWithValue("@uId", reply.UserId);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataTable GetReplyByAnsId(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                string query = "spGetReplys @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
                return dt;
            }
        }
        public static int UpdateReply(Replys reply)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Replys set Reply=@rply where Id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@rply", reply.Reply);
                cmd.Parameters.AddWithValue("@id", reply.Id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static int DeleteReply(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Replys set IsActive=@isAct where Id=@id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@isAct", 0);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}