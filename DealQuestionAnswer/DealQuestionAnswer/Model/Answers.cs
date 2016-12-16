using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class Answers
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string DateTime { get; set; }
        public int IsActive { get; set; }
    }
}