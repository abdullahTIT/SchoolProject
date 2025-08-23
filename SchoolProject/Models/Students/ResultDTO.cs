using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class ResultDTO
    {
        public int ResultID { get; set; }
        public int StudentID { get; set; }
        public int ExamID { get; set; }
        public decimal? Score { get; set; }
        public bool IsAbsent { get; set; }
        public string Notes { get; set; }

        public ResultDTO() { }

        public ResultDTO(int resultID, int studentID, int examID, decimal? score, bool isAbsent, string notes)
        {
            ResultID = resultID;
            StudentID = studentID;
            ExamID = examID;
            Score = score;
            IsAbsent = isAbsent;
            Notes = notes;
        }
    }

}
