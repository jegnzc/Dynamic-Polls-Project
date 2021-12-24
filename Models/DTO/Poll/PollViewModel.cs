using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevelTest.Models
{
    public class PollViewModel
    {
        public int Id { get; set; }
        [DisplayName("Título de la Encuesta"), Required]
        public string Name { get; set; }
        [DisplayName("Descripción"), Required]
        public string Description { get; set; }
        public string Url { get; set; }
        public PollFieldViewModel PollField { get; set; }
        public List<PollFieldViewModel> PollFields { get; set; }

        public PollViewModel()
        {
            if (PollFields == null)
            {
                PollFields = new List<PollFieldViewModel>();
            }
        }

    }
}