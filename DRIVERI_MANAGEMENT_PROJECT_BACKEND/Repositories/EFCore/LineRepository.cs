using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class LineRepository : RepositoryBase<Line>, ILineRepository
    {
        public LineRepository(RepositoryContext context) : base(context)
        {
        }
        public void SaveOrUpdateLine(Line line) => Create(line);

        public void DeleteLine(Line line) => Delete(line);

        public IQueryable<Line> GetAllLines(bool trackChanges) =>
            FindAll(trackChanges);

        public Line GetLineById(int lineId, bool trackChanges) =>
            FindByCondition(l => l.Id.Equals(lineId), trackChanges)
            .SingleOrDefault();

        public Line GetLineByLineCode(string lineCode, bool trackChanges) =>
            FindByCondition(l => l.LineCode.Equals(lineCode), trackChanges)
            .SingleOrDefault();

    }
}
