using System.Collections.Generic;
using System.Linq;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        void Add(Course course);
        void Delete(int id);
    }

    
}