using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCompetencyInterviewGenerator.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }  // Cannot be null - Every question must belong to a category
        public List<Interview> Interviews { get; set; } = new List<Interview>();
    }
}
