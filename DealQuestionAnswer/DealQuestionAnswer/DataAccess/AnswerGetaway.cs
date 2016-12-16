using DealQuestionAnswer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DealQuestionAnswer.DataAccess
{
    public class AnswerGetaway
    {
        public static int SaveAnswer(Answers answer)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert Answers(Answer, QuestionId, UserId) values(@ans,@qId,@uId)";
                using (SqlCommand cmd = new SqlCommand(query, con)) {
                    cmd.Parameters.AddWithValue("@ans", answer.Answer);
                    cmd.Parameters.AddWithValue("@qId", answer.QuestionId);
                    cmd.Parameters.AddWithValue("@uId", answer.UserId);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }                
            }
        }
        public static DataTable GetAnswerByQueId(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                string query = "spGetAnswers @id";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
                return dt;
            }
        }
        public static int InsertAnsUpVote(AnswerVote upVote)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert AnswerVotes (UpVote, AnswerId, UserId, IsUpVote) values(@upVote,@ansId,@userId,@isUpVote)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@upVote", upVote.UpVote);
                    cmd.Parameters.AddWithValue("@ansId", upVote.AnswerId);
                    cmd.Parameters.AddWithValue("@userId", upVote.UserId);
                    cmd.Parameters.AddWithValue("@isUpVote", upVote.IsUpVote);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static int InsertAnsDownVote(AnswerVote downVote)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert AnswerVotes (DownVote, AnswerId, UserId, IsDownVote) values(@downVote,@ansId,@userId,@isDownVote)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@downVote", downVote.DownVote);
                    cmd.Parameters.AddWithValue("@ansId", downVote.AnswerId);
                    cmd.Parameters.AddWithValue("@userId", downVote.UserId);
                    cmd.Parameters.AddWithValue("@isDownVote", downVote.IsDownVote);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static int UpdateAnswer(Answers answer)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Answers set Answer=@ans where Id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ans", answer.Answer);
                cmd.Parameters.AddWithValue("@id", answer.Id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static int DeleteAnswer(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Answers set IsActive=@isAct where Id=@id";
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