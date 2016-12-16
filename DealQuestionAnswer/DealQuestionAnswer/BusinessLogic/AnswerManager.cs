using DealQuestionAnswer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealQuestionAnswer.DataAccess;
using System.Data;

namespace DealQuestionAnswer.BusinessLogic
{
    public class AnswerManager
    {
        public static string IsAnswerInserted(Answers answer)
        {
            int rowAffected = AnswerGetaway.SaveAnswer(answer);
            return rowAffected > 0 ? "true" : "false";
        }
        public static DataTable RetriveAnswer(int id)
        {
            return AnswerGetaway.GetAnswerByQueId(id);
        }
        public static string IsAnsUpVoteInsert(AnswerVote upVote)
        {
            int rowAffected = AnswerGetaway.InsertAnsUpVote(upVote);
            return (rowAffected > 0) ? "true" : "false";
        }
        public static string IsAnsDownVoteInsert(AnswerVote downVote)
        {
            int rowAffected = AnswerGetaway.InsertAnsDownVote(downVote);
            return (rowAffected > 0) ? "true" : "false";
        }
        public static string IsAnswerUpdated(Answers answer)
        {
            int rowAffected = AnswerGetaway.UpdateAnswer(answer);
            return rowAffected > 0 ? "true" : "false";
        }
        public static string IsQuestionDeleted(int id)
        {
            int rowAffectd = AnswerGetaway.DeleteAnswer(id);
            return rowAffectd > 0 ? "true" : "false";
        }
    }
}