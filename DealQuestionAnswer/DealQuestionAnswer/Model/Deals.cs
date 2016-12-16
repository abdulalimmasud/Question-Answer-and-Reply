using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealQuestionAnswer.Model
{
    public class Deals
    {
        public int Id { get; set; }
        public string DealHeadLine { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public int MerchantId { get; set; }
    }
}