using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealQuestionAnswer.DataAccess;
using DealQuestionAnswer.Model;
using System.Data;

namespace DealQuestionAnswer.BusinessLogic
{
    public class ReplyManager
    {
        public static string IsReplyInserted(Replys reply)
        {
            int rowAffected = ReplyGetaway.SaveReply(reply);
            return rowAffected > 0 ? "true" : "false";
        }
        public static DataTable RetriveReplys(int id)
        {
            return ReplyGetaway.GetReplyByAnsId(id);
        }
        public static string IsReplyUpdated(Replys reply)
        {
            int rowAffected = ReplyGetaway.UpdateReply(reply);
            return rowAffected > 0 ? "true" : "false";
        }
        public static string IsReplyDeleted(int id)
        {
            int rowAffected = ReplyGetaway.DeleteReply(id);
            return rowAffected > 0 ? "true" : "false";
        }
    }
}