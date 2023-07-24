using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> subjects;

        public SubjectRepository()
        {
            subjects = new List<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models => subjects;

        public void AddModel(ISubject model)
        {
            subjects.Add(model);
        }

        public ISubject FindById(int id) => subjects.FirstOrDefault(i => i.Id == id);

        public ISubject FindByName(string name) => subjects.FirstOrDefault(i => i.Name == name);
    }
}
