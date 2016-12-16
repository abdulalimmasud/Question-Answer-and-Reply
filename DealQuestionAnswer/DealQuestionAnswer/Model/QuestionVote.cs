using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class QuestionVote
    {
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public int IsUpVote { get; set; }
        public int IsDownVote { get; set; }

    }
}