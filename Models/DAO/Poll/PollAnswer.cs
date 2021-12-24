using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        [ForeignKey("PollField")]
        public int PollFieldId { get; set; }
        public virtual PollField PollField { get; set; }

    }
}