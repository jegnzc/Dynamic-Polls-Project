using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PollField> PollFields { get; set; }
    }
}