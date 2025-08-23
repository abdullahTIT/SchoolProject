using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students.Exams
{
    public class ExamTypeDTO
    {
        public int ExamTypeID { get; set; }
        public string ExamTypeName { get; set; }
        public string Description { get; set; }

        public ExamTypeDTO() { }

        public ExamTypeDTO(int examTypeID, string examTypeName, string description)
        {
            ExamTypeID = examTypeID;
            ExamTypeName = examTypeName;
            Description = description;
        }
    }

}
