using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Students
{
    public class StudentDisciplineDTO
    {
        public int DisciplineID { get; set; }
        public int StudentID { get; set; }
        public DateTime DisciplineDate { get; set; }
        public int Points { get; set; }
        public string Notes { get; set; }

        public StudentDisciplineDTO() { }

        public StudentDisciplineDTO(int disciplineID, int studentID, DateTime disciplineDate, int points, string notes)
        {
            DisciplineID = disciplineID;
            StudentID = studentID;
            DisciplineDate = disciplineDate;
            Points = points;
            Notes = notes;
        }
    }

}
