using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class AnswerVoteRetrive:Answers
    {
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public string Name { get; set; }
    }
}