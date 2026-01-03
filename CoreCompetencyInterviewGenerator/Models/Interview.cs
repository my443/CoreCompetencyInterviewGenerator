using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreCompetencyInterviewGenerator.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public string InterviewName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
