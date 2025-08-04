using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILineService
    {
        IEnumerable<LineDto> GetAllLines(bool trackChanges);
        LineDto GetLineByLineCode(string lineCode);
        void SaveOrUpdateLine(LineDto lineDto);
        void DeleteOneLine(int id, bool trackChanges);
    }
}
