using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private int id;
        private string firstName;
        private string lastName;
        private List<int> coveredExamsCollection;
        private IUniversity university;



        public Student(int studentId, string firstName, string lastName)
        {
            this.Id = studentId;
            this.FirstName = firstName;
            this.LastName = lastName;
            coveredExamsCollection = new List<int>();
        }

        public int Id
        {
            get { return id; }
            private set
            {
                id = value;
            }
        }

        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }

                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }

                lastName = value;
            }
        }

        public IReadOnlyCollection<int> CoveredExams => coveredExamsCollection;

        public IUniversity University
        {
            get { return university; }
            private set
            {
                university = value;
            }
        }

        public void CoverExam(ISubject subject)
        {
            int subjectID = subject.Id;
            coveredExamsCollection.Add(subjectID);
        }

        public void JoinUniversity(IUniversity university)
        {
            this.university = university;
        }
    }
}
