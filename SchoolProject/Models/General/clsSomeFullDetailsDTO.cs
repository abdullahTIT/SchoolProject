using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.General
{
    public class clsSomeFullDetailsDTO
    {
        public class vw_EmployeeFullDetailsDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string IdentifierValue { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string JobTitleName { get; set; }
            public string JobDescription { get; set; }
            public string IsTeaching { get; set; }          // "نعم" أو "لا"
            public string IsAdministrative { get; set; }    // "نعم" أو "لا"
            public string CanTeach { get; set; }             // "نعم" أو "لا"
            public string RequiresCertification { get; set; } // "نعم" أو "لا"
            public DateTime? HireDate { get; set; }
            public DateTime? TerminationDate { get; set; }
            public string EmployeeIsActive { get; set; }    // "نعم" أو "لا"
            public decimal Salary { get; set; }
            public string EmployeeNotes { get; set; }
            public string Gender { get; set; }               // "ذكر" أو "أنثى"
            public DateTime? BirthDate { get; set; }
            public vw_EmployeeFullDetailsDTO() { }

            public vw_EmployeeFullDetailsDTO(int employeeID, string fullName, string identifierValue, string phone, string email, string address,
                                          string jobTitleName, string jobDescription, string isTeaching, string isAdministrative,
                                          string canTeach, string requiresCertification, DateTime? hireDate, DateTime? terminationDate,
                                          string employeeIsActive, decimal salary, string employeeNotes, string gender,
                                          DateTime? birthDate)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                IdentifierValue = identifierValue;
                Phone = phone;
                Email = email;
                Address = address;
                JobTitleName = jobTitleName;
                JobDescription = jobDescription;
                IsTeaching = isTeaching;
                IsAdministrative = isAdministrative;
                CanTeach = canTeach;
                RequiresCertification = requiresCertification;
                HireDate = hireDate;
                TerminationDate = terminationDate;
                EmployeeIsActive = employeeIsActive;
                Salary = salary;
                EmployeeNotes = employeeNotes;
                Gender = gender;
                BirthDate = birthDate;
            }
        }
        public class vw_StudentFullInfoDTO
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string Gender { get; set; }              // "ذكر" أو "أنثى"
            public DateTime? BirthDate { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string PersonIsActive { get; set; }      // "نعم" أو "لا"
            public string IdentifierValue { get; set; }
            public string IdentifierTypeName { get; set; }
            public DateTime? EnrollmentDate { get; set; }
            public string StageName { get; set; }
            public string DocumentPath { get; set; }
            public string StudentNotes { get; set; }
            public string StudentIsActive { get; set; }     // "نعم" أو "لا"
            public string GuardianFullName { get; set; }
            public string GuardianJob { get; set; }
            public string GuardianNotes { get; set; }
            public string FeeTypeName { get; set; }
            public DateTime? DueDate { get; set; }
            public string FeeStatus { get; set; }
            public string FeeNotes { get; set; }
            public decimal TotalFeePaid { get; set; }
            public decimal TotalAmountPaid { get; set; }

            public vw_StudentFullInfoDTO() { }

            public vw_StudentFullInfoDTO(int studentID, string fullName, string gender, DateTime? birthDate, string phone, string email,
                                      string address, string personIsActive, string identifierValue, string identifierTypeName,
                                      DateTime? enrollmentDate, string stageName, string documentPath, string studentNotes, string studentIsActive,
                                      string guardianFullName, string guardianJob, string guardianNotes, string feeTypeName, DateTime? dueDate,
                                      string feeStatus, string feeNotes, decimal totalFeePaid, decimal totalAmountPaid)
            {
                StudentID = studentID;
                FullName = fullName;
                Gender = gender;
                BirthDate = birthDate;
                Phone = phone;
                Email = email;
                Address = address;
                PersonIsActive = personIsActive;
                IdentifierValue = identifierValue;
                IdentifierTypeName = identifierTypeName;
                EnrollmentDate = enrollmentDate;
                StageName = stageName;
                DocumentPath = documentPath;
                StudentNotes = studentNotes;
                StudentIsActive = studentIsActive;
                GuardianFullName = guardianFullName;
                GuardianJob = guardianJob;
                GuardianNotes = guardianNotes;
                FeeTypeName = feeTypeName;
                DueDate = dueDate;
                FeeStatus = feeStatus;
                FeeNotes = feeNotes;
                TotalFeePaid = totalFeePaid;
                TotalAmountPaid = totalAmountPaid;
            }
        }
        public class vw_EmployeeMonthlySalaryDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public DateTime SalaryMonth { get; set; }
            public decimal BaseSalary { get; set; }
            public int AbsenceDays { get; set; }
            public decimal DeductionPerAbsent { get; set; }
            public decimal NetSalary { get; set; }
            public bool Paid { get; set; }
            public DateTime? PaymentDate { get; set; }

            public vw_EmployeeMonthlySalaryDTO() { }

            public vw_EmployeeMonthlySalaryDTO(int employeeID, string fullName, DateTime salaryMonth, decimal baseSalary, int absenceDays,
                decimal deductionPerAbsent, decimal netSalary, bool paid, DateTime? paymentDate)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryMonth = salaryMonth;
                BaseSalary = baseSalary;
                AbsenceDays = absenceDays;
                DeductionPerAbsent = deductionPerAbsent;
                NetSalary = netSalary;
                Paid = paid;
                PaymentDate = paymentDate;
            }
        }
        public class vw_EmployeeMonthlyAttendanceDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string SalaryMonth { get; set; }
            public int PresentDays { get; set; }

            public vw_EmployeeMonthlyAttendanceDTO() { }

            public vw_EmployeeMonthlyAttendanceDTO(int employeeID, string fullName, string salaryMonth, int presentDays)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryMonth = salaryMonth;
                PresentDays = presentDays;
            }
        }
        public class vw_FullEmployeeMonthlySalaryDetailsDTO
        {



            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public DateTime SalaryMonth { get; set; }
            public decimal BaseSalary { get; set; }
            public int AbsenceDays { get; set; }
            public decimal DeductionPerAbsent { get; set; }
            public decimal TotalDeduction { get; set; }
            public decimal NetSalary { get; set; }
            public bool Paid { get; set; }
            public DateTime? PaymentDate { get; set; }

            public vw_FullEmployeeMonthlySalaryDetailsDTO() { }

            public vw_FullEmployeeMonthlySalaryDetailsDTO(int employeeID, string fullName, DateTime salaryMonth, decimal baseSalary,
                int absenceDays, decimal deductionPerAbsent, decimal totalDeduction, decimal netSalary, bool paid, DateTime? paymentDate)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryMonth = salaryMonth;
                BaseSalary = baseSalary;
                AbsenceDays = absenceDays;
                DeductionPerAbsent = deductionPerAbsent;
                TotalDeduction = totalDeduction;
                NetSalary = netSalary;
                Paid = paid;
                PaymentDate = paymentDate;
            }
        }
        public class vw_UnpaidEmployeeDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public DateTime SalaryMonth { get; set; }
            public decimal BaseSalary { get; set; }
            public decimal NetSalary { get; set; }
            public bool Paid { get; set; }

            public vw_UnpaidEmployeeDTO() { }

            public vw_UnpaidEmployeeDTO(int employeeID, string fullName, DateTime salaryMonth, decimal baseSalary, decimal netSalary, bool paid)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryMonth = salaryMonth;
                BaseSalary = baseSalary;
                NetSalary = netSalary;
                Paid = paid;
            }
        }
        public class vw_EmployeeMonthlyAbsenceDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string SalaryMonth { get; set; }
            public int AbsenceDays { get; set; }

            public vw_EmployeeMonthlyAbsenceDTO() { }

            public vw_EmployeeMonthlyAbsenceDTO(int employeeID, string fullName, string salaryMonth, int absenceDays)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryMonth = salaryMonth;
                AbsenceDays = absenceDays;
            }
        }
        public class vw_TerminatedEmployeeDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public int JobTitleID { get; set; }
            public DateTime HireDate { get; set; }
            public DateTime? TerminationDate { get; set; }
            public string Notes { get; set; }

            public vw_TerminatedEmployeeDTO() { }

            public vw_TerminatedEmployeeDTO(int employeeID, string fullName, int jobTitleID, DateTime hireDate, DateTime? terminationDate, string notes)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                JobTitleID = jobTitleID;
                HireDate = hireDate;
                TerminationDate = terminationDate;
                Notes = notes;
            }
        }
        public class vw_EmployeeAnnualSalarySummaryDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public int SalaryYear { get; set; }
            public decimal TotalBaseSalary { get; set; }
            public decimal TotalDeductions { get; set; }
            public decimal TotalNetSalary { get; set; }

            public vw_EmployeeAnnualSalarySummaryDTO() { }

            public vw_EmployeeAnnualSalarySummaryDTO(int employeeID, string fullName, int salaryYear,
                decimal totalBaseSalary, decimal totalDeductions, decimal totalNetSalary)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                SalaryYear = salaryYear;
                TotalBaseSalary = totalBaseSalary;
                TotalDeductions = totalDeductions;
                TotalNetSalary = totalNetSalary;
            }
        }
        public class vw_CurrentEmployeeSalaryDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public decimal BaseSalary { get; set; }
            public decimal Allowances { get; set; }
            public decimal Deductions { get; set; }
            public DateTime EffectiveDate { get; set; }

            public vw_CurrentEmployeeSalaryDTO() { }

            public vw_CurrentEmployeeSalaryDTO(int employeeID, string fullName, decimal baseSalary, decimal allowances, decimal deductions, DateTime effectiveDate)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                BaseSalary = baseSalary;
                Allowances = allowances;
                Deductions = deductions;
                EffectiveDate = effectiveDate;
            }
        }
        public class vw_StudentFeeStatusDTO
        {
            public int StudentFeeID { get; set; }
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string FeeName { get; set; }
            public decimal TotalRequired { get; set; }
            public decimal TotalPaid { get; set; }
            public decimal Remaining { get; set; }
            public string PaymentStatus { get; set; }
            public DateTime? DueDate { get; set; }
            public string Status { get; set; }
            public string Notes { get; set; }

            public vw_StudentFeeStatusDTO() { }

            public vw_StudentFeeStatusDTO(int studentFeeID, int studentID, string fullName, string feeName,
                                         decimal totalRequired, decimal totalPaid, decimal remaining,
                                         string paymentStatus, DateTime? dueDate, string status, string notes)
            {
                StudentFeeID = studentFeeID;
                StudentID = studentID;
                FullName = fullName;
                FeeName = feeName;
                TotalRequired = totalRequired;
                TotalPaid = totalPaid;
                Remaining = remaining;
                PaymentStatus = paymentStatus;
                DueDate = dueDate;
                Status = status;
                Notes = notes;
            }
        }
        public class vw_StudentFinancialStatusDTO
        {
            public int StudentID { get; set; }
            public int StudentFeeID { get; set; }
            public string FeeName { get; set; }
            public decimal TotalFee { get; set; }
            public decimal TotalPaid { get; set; }
            public decimal Remaining { get; set; }
            public int TotalInstallments { get; set; }
            public int PaidInstallments { get; set; }
            public int UnpaidInstallments { get; set; }
            public DateTime? LastDueDate { get; set; }
            public string PaymentStatus { get; set; }

            public vw_StudentFinancialStatusDTO() { }

            public vw_StudentFinancialStatusDTO(int studentID, int studentFeeID, string feeName, decimal totalFee,
                                                decimal totalPaid, decimal remaining, int totalInstallments,
                                                int paidInstallments, int unpaidInstallments, DateTime? lastDueDate,
                                                string paymentStatus)
            {
                StudentID = studentID;
                StudentFeeID = studentFeeID;
                FeeName = feeName;
                TotalFee = totalFee;
                TotalPaid = totalPaid;
                Remaining = remaining;
                TotalInstallments = totalInstallments;
                PaidInstallments = paidInstallments;
                UnpaidInstallments = unpaidInstallments;
                LastDueDate = lastDueDate;
                PaymentStatus = paymentStatus;
            }
        }
        public class vw_UpcomingInstallmentsDTO
        {
            public int InstallmentID { get; set; }
            public int StudentFeeID { get; set; }
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public DateTime DueDate { get; set; }
            public decimal Amount { get; set; }
            public bool IsPaid { get; set; }

            public vw_UpcomingInstallmentsDTO() { }

            public vw_UpcomingInstallmentsDTO(int installmentID, int studentFeeID, int studentID, string fullName, DateTime dueDate, decimal amount, bool isPaid)
            {
                InstallmentID = installmentID;
                StudentFeeID = studentFeeID;
                StudentID = studentID;
                FullName = fullName;
                DueDate = dueDate;
                Amount = amount;
                IsPaid = isPaid;
            }
        }
        public class vw_WeeklyRoomScheduleDTO
        {
            public string RoomName { get; set; }
            public string ClassName { get; set; }
            public string SubjectName { get; set; }
            public int EmployeeID { get; set; }
            public string DayOfWeek { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public int AcademicYearID { get; set; }

            public vw_WeeklyRoomScheduleDTO() { }

            public vw_WeeklyRoomScheduleDTO(string roomName, string className, string subjectName, int employeeID, string dayOfWeek,
                                           TimeSpan startTime, TimeSpan endTime, int academicYearID)
            {
                RoomName = roomName;
                ClassName = className;
                SubjectName = subjectName;
                EmployeeID = employeeID;
                DayOfWeek = dayOfWeek;
                StartTime = startTime;
                EndTime = endTime;
                AcademicYearID = academicYearID;
            }
        }
        public class vw_DailyRoomScheduleDTO
        {
            public string RoomName { get; set; }
            public string ClassName { get; set; }
            public string SubjectName { get; set; }
            public string DayOfWeek { get; set; }
            public string StartTime { get; set; }  // formatted hh:mm
            public string EndTime { get; set; }    // formatted hh:mm
            public int AcademicYearID { get; set; }

            public vw_DailyRoomScheduleDTO() { }

            public vw_DailyRoomScheduleDTO(string roomName, string className, string subjectName,
                                           string dayOfWeek, string startTime, string endTime, int academicYearID)
            {
                RoomName = roomName;
                ClassName = className;
                SubjectName = subjectName;
                DayOfWeek = dayOfWeek;
                StartTime = startTime;
                EndTime = endTime;
                AcademicYearID = academicYearID;
            }
        }
        public class vw_WeeklyClassScheduleDTO
        {
            public string ClassName { get; set; }
            public string SubjectName { get; set; }
            public int TeacherID { get; set; }
            public string RoomName { get; set; }
            public string DayOfWeek { get; set; }
            public string StartTime { get; set; }  // formatted hh:mm
            public string EndTime { get; set; }    // formatted hh:mm
            public int AcademicYearID { get; set; }

            public vw_WeeklyClassScheduleDTO() { }

            public vw_WeeklyClassScheduleDTO(string className, string subjectName, int teacherID, string roomName,
                                            string dayOfWeek, string startTime, string endTime, int academicYearID)
            {
                ClassName = className;
                SubjectName = subjectName;
                TeacherID = teacherID;
                RoomName = roomName;
                DayOfWeek = dayOfWeek;
                StartTime = startTime;
                EndTime = endTime;
                AcademicYearID = academicYearID;
            }
        }
        public class vw_TeacherSchedulesDTO
        {
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string ClassName { get; set; }
            public string SubjectName { get; set; }
            public string RoomName { get; set; }
            public string DayOfWeek { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public int AcademicYearID { get; set; }

            public vw_TeacherSchedulesDTO() { }

            public vw_TeacherSchedulesDTO(int employeeID, string fullName, string className, string subjectName,
                                         string roomName, string dayOfWeek, TimeSpan startTime, TimeSpan endTime,
                                         int academicYearID)
            {
                EmployeeID = employeeID;
                FullName = fullName;
                ClassName = className;
                SubjectName = subjectName;
                RoomName = roomName;
                DayOfWeek = dayOfWeek;
                StartTime = startTime;
                EndTime = endTime;
                AcademicYearID = academicYearID;
            }
        }
        public class vw_AvailableRoomsNowDTO
        {
            public int RoomID { get; set; }
            public string RoomName { get; set; }
            public int RoomTypeID { get; set; }
            public int Capacity { get; set; }
            public string Location { get; set; }
            public bool IsAvailable { get; set; }
            public bool IsReservable { get; set; }
            public string Notes { get; set; }

            public vw_AvailableRoomsNowDTO() { }

            public vw_AvailableRoomsNowDTO(int roomID, string roomName, int roomTypeID, int capacity, string location,
                                          bool isAvailable, bool isReservable, string notes)
            {
                RoomID = roomID;
                RoomName = roomName;
                RoomTypeID = roomTypeID;
                Capacity = capacity;
                Location = location;
                IsAvailable = isAvailable;
                IsReservable = isReservable;
                Notes = notes;
            }
        }
        public class vw_StudentFinalGradesDTO
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string StageName { get; set; }
            public string SubjectName { get; set; }
            public string ExamTypeName { get; set; }
            public decimal Grade { get; set; }

            public vw_StudentFinalGradesDTO() { }

            public vw_StudentFinalGradesDTO(int studentID, string fullName, string stageName,
                                            string subjectName, string examTypeName, decimal grade)
            {
                StudentID = studentID;
                FullName = fullName;
                StageName = stageName;
                SubjectName = subjectName;
                ExamTypeName = examTypeName;
                Grade = grade;
            }
        }
        public class vw_StudentAverageGradesDTO
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string StageName { get; set; }
            public string AcademicYearName { get; set; }
            public string ExamTypeName { get; set; }
            public decimal AverageGrade { get; set; }
            public int SubjectsCount { get; set; }

            public vw_StudentAverageGradesDTO() { }

            public vw_StudentAverageGradesDTO(int studentID, string fullName, string stageName, string academicYearName,
                                              string examTypeName, decimal averageGrade, int subjectsCount)
            {
                StudentID = studentID;
                FullName = fullName;
                StageName = stageName;
                AcademicYearName = academicYearName;
                ExamTypeName = examTypeName;
                AverageGrade = averageGrade;
                SubjectsCount = subjectsCount;
            }
        }
        public class vw_TopStudentPerStageYearDTO
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string StageName { get; set; }
            public string AcademicYearName { get; set; }
            public decimal AverageGrade { get; set; }
            public int RankInStage { get; set; }

            public vw_TopStudentPerStageYearDTO() { }

            public vw_TopStudentPerStageYearDTO(int studentID, string fullName, string stageName,
                                                string academicYearName, decimal averageGrade, int rankInStage)
            {
                StudentID = studentID;
                FullName = fullName;
                StageName = stageName;
                AcademicYearName = academicYearName;
                AverageGrade = averageGrade;
                RankInStage = rankInStage;
            }
        }
        public class vw_StudentPassFailStatusDTO
        {
            public int StudentID { get; set; }
            public string FullName { get; set; }
            public string StageName { get; set; }
            public string AcademicYearName { get; set; }
            public decimal AverageGrade { get; set; }
            public string FinalStatus { get; set; } // "pass" or "fail"

            public vw_StudentPassFailStatusDTO() { }

            public vw_StudentPassFailStatusDTO(int studentID, string fullName, string stageName,
                                               string academicYearName, decimal averageGrade, string finalStatus)
            {
                StudentID = studentID;
                FullName = fullName;
                StageName = stageName;
                AcademicYearName = academicYearName;
                AverageGrade = averageGrade;
                FinalStatus = finalStatus;
            }
        }

    }
}
