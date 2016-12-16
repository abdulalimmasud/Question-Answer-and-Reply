using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class Replys
    {
        public int Id { get; set; }
        public String Reply { get; set; }
        public int AnswerID { get; set; }
        public int UserId { get; set; }
        public string DateTime { get; set; }
        public int IsActive { get; set; }
    }
}