using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollField
    {
        public int Id { get; set; }
        [ForeignKey("Poll")]
        public int PollId { get; set; }
        public virtual Poll Poll { get; set; }
        [ForeignKey("PollFieldType")]
        public int PollFieldTypeId { get; set; }
        public virtual PollFieldType PollFieldType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
    }
}