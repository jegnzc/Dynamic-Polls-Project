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
        public string Name { get; set; } // Nombre del Campo
        public string DisplayName { get; set; } // Título del Campo (Se mostrará en la pantalla)
        public bool Required { get; set; }
        [ForeignKey("Poll")]
        public int PollId { get; set; }
        public virtual Poll Poll { get; set; }
        [ForeignKey("PollFieldType")]
        public int PollFieldTypeId { get; set; }
        public virtual PollFieldType PollFieldType { get; set; }
        [JsonIgnore]
        public virtual ICollection<PollAnswer> PollAnswers { get; set; }
    }
}