using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class Questions
    {
        public int Id { get; set; }
        public string Question { get; set; }
        
        public int UserId { get; set; }
        public int DealId { get; set; }
        public string DateTime { get; set; }
        public int IsActive { get; set; }
    }
}