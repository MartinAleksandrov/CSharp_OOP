using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> subjects;
        private IRepository<IStudent> students;
        private IRepository<IUniversity> universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }


        public string AddStudent(string firstName, string lastName)
        {
            string studentFullName = $"'{firstName} {lastName}";

            IStudent student = students.FindByName(studentFullName);

            if (student != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            int studentIDs = students.Models.Count() + 1;
            student = new Student(studentIDs, firstName, lastName);

            students.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != "HumanitySubject" && subjectType != "EconomicalSubject" && subjectType != "TechnicalSubject")
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            else if (subjects.Models.Any(s => s.Name == subjectName))
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            else
            {
                ISubject subject;

                int subjectId = subjects.Models.Count + 1;

                if (subjectType == "HumanitySubject")
                {
                    subject = new HumanitySubject(subjectId, subjectName);
                }
                else if (subjectType == "EconomicalSubject")
                {
                    subject = new EconomicalSubject(subjectId, subjectName);
                }
                else
                {
                    subject = new TechnicalSubject(subjectId, subjectName);
                }

                subjects.AddModel(subject);
                return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, subjects.GetType().Name);
            }
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName)!=null )
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            else
            {

                IUniversity university;

                List<int> convertedSubjects = new List<int>();

                foreach (var sub in subjects.Models)
                {
                    foreach (var reqSub in requiredSubjects)
                    {
                        if (sub.Name == reqSub)
                        {
                            convertedSubjects.Add(sub.Id);
                        }
                    }
                }

                int universityIDs = universities.Models.Count + 1;

                university = new University(universityIDs, universityName, category, capacity, convertedSubjects);
                universities.AddModel(university);

                return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
            }
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent student = students.FindByName(studentName);

            string[] splitedName = studentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = splitedName[0];
            string lastName = splitedName[1];

            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName, lastName);
            }
            else if (!universities.Models.Any(u => u.Name == universityName))
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            IUniversity university = universities.FindByName(universityName);


            int[] examsToCover = university.RequiredSubjects.ToArray();

            int coveredExamsCount = 0;

            foreach (var exam in examsToCover)
            {
                foreach (var coveredExam in student.CoveredExams)
                {
                    if (exam==coveredExam)
                    {
                        coveredExamsCount++;
                    }
                }
            }

            if (examsToCover.Count() > coveredExamsCount)
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }


            if (student.University == universities.FindByName(universityName))
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            }
            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);

        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = students.FindById(studentId);
            ISubject subject = subjects.FindById(subjectId);

            if (student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }
            else if (subject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }
            else if (student.CoveredExams.Contains(subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }
            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);

            int studentsCount = 0;

            foreach (var uni in students.Models)
            {
                if (uni.University == university) 
                {
                    studentsCount++;
                }
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {university.Capacity-studentsCount}");

            return sb.ToString().TrimEnd();
        }
    }
}
