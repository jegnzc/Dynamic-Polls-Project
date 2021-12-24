using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollAnswerViewModel
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public int PollFieldId { get; set; }
        public PollFieldViewModel PollField { get; set; }
    }
}