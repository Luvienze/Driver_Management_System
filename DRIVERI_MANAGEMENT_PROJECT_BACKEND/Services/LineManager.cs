using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class LineManager : ILineService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        
        public LineManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }
        public void DeleteOneLine(int id, bool trackChanges)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Line ID must be greater than zero.", nameof(id));
            }
            var entity = _manager.Line.GetLineById(id, trackChanges);
            if (entity is null)
            {
                string message = $"Line with id {id} not found.";
                _logger.LogInfo(message);
                throw new LineNotFoundException(id);
            }
            _manager.Line.DeleteLine(entity);
            _manager.Save();

        }

        public IEnumerable<LineDto> GetAllLines(bool trackChanges)
        {
            var lines = _manager.Line.GetAllLines(trackChanges);
            if (lines is null || !lines.Any())
            {
                _logger.LogInfo("No lines found.");
                return Enumerable.Empty<LineDto>();
            }
            return lines.Select(line => _mapper.Map<LineDto>(line)).ToList();
        }

        public LineDto GetLineByLineCode(string lineCode)
        {
            if(lineCode is null)
            {
                throw new ArgumentNullException(nameof(lineCode), "Line code cannot be null.");
            }
            var lines = _manager.Line.GetLineByLineCode(lineCode, false);
            if (lines is null)
            {
                string message = $"No lines found with line code {lineCode}.";
                _logger.LogInfo(message);
                throw new LineNotFoundException(lines.Id);
            }
            return _mapper.Map<LineDto>(lines);

        }

        public void SaveOrUpdateLine(LineDto lineDto)
        {
            int id = lineDto.Id;
            if (lineDto is null) throw new LineNotFoundException(id);

            if (id <= 0)
            {
                var line = _mapper.Map<Line>(lineDto);
                _manager.Line.SaveOrUpdateLine(line);
                _manager.Save();

            }
            else if (id > 0)
            {
                var existingLine = _manager.Line.GetLineByLineCode(lineDto.LineCode, false);
                if (existingLine is null) throw new LineNotFoundException(id);
                existingLine = _mapper.Map<Line>(lineDto);
                _manager.Line.SaveOrUpdateLine(existingLine);
                _manager.Save();
            }
        }
    }
}
