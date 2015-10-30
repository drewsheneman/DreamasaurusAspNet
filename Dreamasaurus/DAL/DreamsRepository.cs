using Dreamasaurus.Models;

namespace Dreamasaurus.DAL
{
    public interface IDreamsRepository
    {
    }

    public class DreamsRepository : GenericRepository<Dream>, IDreamsRepository
    {
        public DreamsRepository(DreamsDbContext context) : base(context)
        {
        }
    }

}