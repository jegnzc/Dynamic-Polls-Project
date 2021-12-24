using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollFieldViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nombre del Campo")]
        public string Name { get; set; }
        [DisplayName("Título del Campo")]
        public string DisplayName { get; set; }
        [DisplayName("Respuesta")]
        public string Text { get; set; }
        public int? Numeric { get; set; }
        [UIHint("Date")]
        public DateTime? Date { get; set; }
        [DisplayName("Requerido")]
        public bool Required { get; set; }
        public int PollId { get; set; }
        public PollViewModel Poll { get; set; }
        public int PollFieldTypeId { get; set; }
        public string PollFieldTypeDesc { get; set; }
        public PollFieldTypeViewModel PollFieldType { get; set; }
        public List<PollAnswerViewModel> PollAnswers { get; set; }
    }
}