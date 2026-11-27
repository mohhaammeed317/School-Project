using schoolproject.data.entites;
using schoolproject.infrastructure.infrabases;

namespace schoolproject.infrastructure.abstracts
{
    public interface isubjectrepository : igenericrepo<Subjects>
    {
        Task<List<Subjects>> GetSubjectsListAsync();
        Task<Subjects> GetSubjectbyidtAsync(int id);
    }
}
