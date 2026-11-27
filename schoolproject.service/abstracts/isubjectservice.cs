using schoolproject.data.entites;
using schoolproject.data.helpers;

namespace schoolproject.service.abstracts
{
    public interface isubjectservice
    {
        Task<List<Subjects>> GetSubjectsListAsync();
        Task<Subjects> GetSubjectbyidAsync(int id);
        Task<Subjects> GetSubjectbyidwithincludetAsync(int id);
        Task<string> addsync(Subjects subject);
        Task<string> updatesubject(Subjects subject);
        Task<string> deletesubject(Subjects subject);
        Task<bool> nameexist(string name);
        Task<bool> isnameisexistexcludeself(string name, int id);
        Task<bool> candelete(int id);
        IQueryable<Subjects> getsubjectquerable();
        IQueryable<Subjects> filtersubjectpaginatedquerable(subjectorderingenum order, string search);
    }
}
