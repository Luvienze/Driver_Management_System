package com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.implementation;

import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.repository.LineRepository;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.dto.LineDTO;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.entity.Line;
import com.eren.DRIVER_MANAGEMENT_PROJECT_BACKEND.service.LineService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class LineServiceImpl implements LineService {

    @Autowired
    private LineRepository lineRepository;

    /**
     * Retrieves a {@link LineDTO} by the given line code.
     *
     * @param lineCode the code of the line to find.
     * @return the {@link LineDTO} if found; {@code null} if the line code is null, empty, or no matching line found.
     */
    @Override
    public LineDTO getLineByLineCode(String lineCode) {
        if(lineCode == null || lineCode.isEmpty()) return null;
        Line line = lineRepository.findLineByLineCode(lineCode);
        if(line == null) return null;
        return convertToDto(line);
    }

    /**
     * Retrieves all lines as a list of {@link LineDTO}.
     *
     * @return a list containing all lines.
     */
    @Override
    public List<LineDTO> getAllLines() {
        return lineRepository.findAll().stream()
                .map(this::convertToDto)
                .collect(Collectors.toList());
    }

    /**
     * Converts a {@link Line} entity to its corresponding {@link LineDTO}.
     *
     * @param line the {@link Line} entity to convert.
     * @return the converted {@link LineDTO}.
     */
    private LineDTO convertToDto (Line line) {
        LineDTO lineDTO = new LineDTO();
        lineDTO.setId(line.getId());
        lineDTO.setLineCode(line.getLineCode());
        lineDTO.setLineName(line.getLineName());
        lineDTO.setIsActive(line.getIsActive());
        return lineDTO;
    }
}
