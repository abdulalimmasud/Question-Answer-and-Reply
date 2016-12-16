using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using DealQuestionAnswer.Model;
using System.Data;

namespace DealQuestionAnswer.DataAccess
{    
    public class QuestionGetaway
    {
        //update Questions set Question='' where Id=1
        public static int UpdateQuestion(Questions question)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Questions set Question=@que where Id=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@que", question.Question);
                cmd.Parameters.AddWithValue("@id", question.Id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static int DeleteQuestion(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Questions set IsActive=@isAct where Id=@id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@isAct", 0);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static int SaveQuestion(Questions question)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert Questions(Question,UserId,DealId) values(@que,@user,@deal)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@que", question.Question);
                cmd.Parameters.AddWithValue("@user", question.UserId);
                cmd.Parameters.AddWithValue("@deal", question.DealId);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static DataTable GetQuestions(int id)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                using (SqlCommand command = new SqlCommand("spGetQuestions @id", con))
                {
                    command.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    sda.Fill(dt);
                }
                return dt;                
            }
        }
        public static int InsertQueUpVote(QuestionVote upVote)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert QuestionVotes (UpVote, QuestionId, UserId, IsUpVote) values(@upVote,@queId,@userId,@isUpVote)";
                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@upVote", upVote.UpVote);
                    cmd.Parameters.AddWithValue("@queId", upVote.QuestionId);
                    cmd.Parameters.AddWithValue("@userId", upVote.UserId);
                    cmd.Parameters.AddWithValue("@isUpVote", upVote.IsUpVote);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static int InsertQueDownVote(QuestionVote downVote)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert QuestionVotes (DownVote, QuestionId, UserId, IsDownVote) values(@downVote,@queId,@userId,@isDownVote)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@downVote", downVote.DownVote);
                    cmd.Parameters.AddWithValue("@queId", downVote.QuestionId);
                    cmd.Parameters.AddWithValue("@userId", downVote.UserId);
                    cmd.Parameters.AddWithValue("@isDownVote", downVote.IsDownVote);
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}