using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollFieldViewModel
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public PollViewModel Poll { get; set; }
        public int PollFieldTypeId { get; set; }
        public PollFieldTypeViewModel PollFieldType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
    }
}