using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DealQuestionAnswer.Model;
using DealQuestionAnswer.BusinessLogic;
using System.Data;

namespace DealQuestionAnswer.UI
{
    public partial class DealInteface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string InsertQuestion(string question)
        {
            Questions que = new Questions();
            que.Question = question;
            que.UserId = 1;
            que.DealId = 1;
            return QuestionManager.IsQuestionSaved(que);
        }
        [WebMethod]
        public static QuestionVotesRetrive[]  GetQuestions(int id)
        {
            List<QuestionVotesRetrive> questionList = new List<QuestionVotesRetrive>();
            foreach (DataRow dtr in QuestionManager.RetriveQuestions(id).Rows)
            {
                QuestionVotesRetrive qv = new QuestionVotesRetrive();
                qv.Id = int.Parse(dtr["Id"].ToString());
                qv.Name = dtr["Name"].ToString();
                qv.Question = dtr["Question"].ToString();
                qv.UserId = int.Parse(dtr["UserId"].ToString());
                qv.DateTime = dtr["DateTime"].ToString();
                qv.UpVoteCount = int.Parse(dtr["PositiveCount"].ToString());
                qv.DownVoteCount = int.Parse(dtr["NegativeCount"].ToString());
                questionList.Add(qv);
            }
            return questionList.ToArray();
        }
        [WebMethod]
        public static string InsertQueUpVote(int queId, int userId)
        {
            QuestionVote questionVote = new QuestionVote();
            questionVote.UpVote = 1;
            questionVote.QuestionId = queId;
            questionVote.UserId = userId;
            questionVote.IsUpVote = 1;

            return QuestionManager.IsQueUpVoteInsert(questionVote);
        }
        [WebMethod]
        public static string InsertQueDownVote(int queId, int userId)
        {
            QuestionVote questionVote = new QuestionVote();
            questionVote.DownVote = 1;
            questionVote.QuestionId = queId;
            questionVote.UserId = userId;
            questionVote.IsDownVote = 1;

            return QuestionManager.IsQueDownVoteInsert(questionVote);
        }
        [WebMethod]
        public static string DeleteQuestion(int id)
        {
            return QuestionManager.IsQuestionDelete(id);
        }
        [WebMethod]
        public static string UpdateQuestion(int queId, string question)
        {
            Questions que = new Questions();
            que.Id = queId;
            que.Question = question;
            return QuestionManager.IsQuestionUpdate(que);
        }
        [WebMethod]
        public static string InsertAnswer(string ans, int qId, int uId)
        {
            Answers answer = new Answers();
            answer.Answer = ans;
            answer.QuestionId = qId;
            answer.UserId = uId;

            return AnswerManager.IsAnswerInserted(answer);
        }
        [WebMethod]
        public static AnswerVoteRetrive[] GetAnswers(int id)
        {
            List<AnswerVoteRetrive> answerList = new List<AnswerVoteRetrive>();
            foreach (DataRow dtr in AnswerManager.RetriveAnswer(id).Rows)
            {
                AnswerVoteRetrive answer = new AnswerVoteRetrive();
                answer.Id = int.Parse(dtr["Id"].ToString());
                answer.Answer = dtr["Answer"].ToString();
                answer.DateTime = dtr["DateTime"].ToString();
                answer.Name = dtr["Name"].ToString();
                answer.UpVoteCount = int.Parse(dtr["PostiveCount"].ToString());
                answer.DownVoteCount = int.Parse(dtr["NegativeCount"].ToString());
                answerList.Add(answer);
            }
            return answerList.ToArray();
        }
        [WebMethod]
        public static string InsertAnsUpVote(int ansId, int userId)
        {
            AnswerVote answerVote = new AnswerVote();
            answerVote.UpVote = 1;
            answerVote.AnswerId = ansId;
            answerVote.UserId = userId;
            answerVote.IsUpVote = 1;

            return AnswerManager.IsAnsUpVoteInsert(answerVote);
        }
        [WebMethod]
        public static string InsertAnsDownVote(int ansId, int userId)
        {
            AnswerVote answerVote = new AnswerVote();
            answerVote.DownVote = 1;
            answerVote.AnswerId = ansId;
            answerVote.UserId = userId;
            answerVote.IsDownVote = 1;

            return AnswerManager.IsAnsDownVoteInsert(answerVote);
        }
        [WebMethod]
        public static string UpdateAnswer(int ansId, string answer)
        {
            Answers ans = new Answers();
            ans.Id = ansId;
            ans.Answer = answer;
            return AnswerManager.IsAnswerUpdated(ans);
        }
        [WebMethod]
        public static string DeleteAnswer(int id)
        {
            return AnswerManager.IsQuestionDeleted(id);
        }
        [WebMethod]
        public static string InsertReply(string rply, int aId, int uId)
        {
            Replys reply = new Replys();
            reply.Reply = rply;
            reply.AnswerID = aId;
            reply.UserId = uId;
            return ReplyManager.IsReplyInserted(reply);
        }
        [WebMethod]
        public static ReplyRetrive[] GetReplys(int id)
        {
            List<ReplyRetrive> replys = new List<ReplyRetrive>();
            foreach (DataRow dtr in ReplyManager.RetriveReplys(id).Rows)
            {
                ReplyRetrive reply = new ReplyRetrive();
                reply.Id = int.Parse(dtr["Id"].ToString());
                reply.Reply = dtr["Reply"].ToString();
                reply.DateTime = dtr["DateTime"].ToString();
                reply.UserName = dtr["Name"].ToString();
                replys.Add(reply);
            }
            return replys.ToArray();
        }
        [WebMethod]
        public static string UpdateReply(int repId, string reply)
        {
            Replys rply = new Replys();
            rply.Id = repId;
            rply.Reply = reply;
            return ReplyManager.IsReplyUpdated(rply);
        }
        [WebMethod]
        public static string DeleteReply(int repId)
        {
            return ReplyManager.IsReplyDeleted(repId);
        }
    }
}