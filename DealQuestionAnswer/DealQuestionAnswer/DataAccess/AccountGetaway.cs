using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using DealQuestionAnswer.Model;

namespace DealQuestionAnswer.DataAccess
{
    public class AccountGetaway
    {
        public static Users GetUserByLogin(Users user)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DealQuestionAnswerDBCS"].ConnectionString;
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "spLoginUser @mail, @pass";
                using(SqlCommand command = new SqlCommand(query,con))
                {
                    command.Parameters.AddWithValue("@mail", user.Email);
                    command.Parameters.AddWithValue("@pass", user.Password);

                }
            }
        }
    }
}