using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealQuestionAnswer.Model;
using DealQuestionAnswer.DataAccess;
using System.Data;

namespace DealQuestionAnswer.BusinessLogic
{
    public class QuestionManager
    { 
        public static string IsQuestionSaved(Questions question)
        {
            int rowAffected = QuestionGetaway.SaveQuestion(question);
            if (rowAffected > 0)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        public static string IsQuestionDelete(int id)
        {
            int rowAffected = QuestionGetaway.DeleteQuestion(id);
            return rowAffected > 0 ? "true" : "false";
        }
        public static DataTable RetriveQuestions(int id)
        {
            return QuestionGetaway.GetQuestions(id);
        }
        public static string IsQueUpVoteInsert(QuestionVote upVote)
        {
            int rowAffected = QuestionGetaway.InsertQueUpVote(upVote);
            return (rowAffected > 0) ? "true" : "false";
        }
        public static string IsQueDownVoteInsert(QuestionVote downVote)
        {
            int rowAffected = QuestionGetaway.InsertQueDownVote(downVote);
            return (rowAffected > 0) ? "true" : "false";
        }
        public static string IsQuestionUpdate(Questions question)
        {
            int rowAffected = QuestionGetaway.UpdateQuestion(question);
            return rowAffected > 0 ? "true" : "false";
        }
    }
}