using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ILineRepository : IRepositoryBase<Line>
    {
        IQueryable<Line> GetAllLines(bool trackChanges);
        Line GetLineByLineCode(string lineCode, bool trackChanges);
        Line GetLineById(int lineId, bool trackChanges);
        void SaveOrUpdateLine(Line line);
        void DeleteLine(Line line);
    }
}
