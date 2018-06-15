using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[Route("/time-entries")]
public class TimeEntryController : ControllerBase
{
    private readonly ITimeEntryRepository _timeEntryRepository;

    public TimeEntryController(ITimeEntryRepository timeEntryRepository)
    {
        this._timeEntryRepository = timeEntryRepository;
    }

    [HttpGet("{id}", Name = "GetTimeEntry")]
    public IActionResult  Read(long id)
    {
        return _timeEntryRepository.Contains(id)? (IActionResult) Ok(_timeEntryRepository.Find(id)) : NotFound();
    }

    [HttpPost]
    public IActionResult  Create([FromBody] TimeEntry timeEntry)
    {
        var createdTimeEntry = _timeEntryRepository.Create(timeEntry);

        return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
    }

    [HttpGet]
    public IActionResult  List()
    {
        return Ok(_timeEntryRepository.List());
    }

    [HttpPut("{id}")]
    public IActionResult  Update(long id, [FromBody] TimeEntry timeEntry)
    {
        return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Update(id, timeEntry)) : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult  Delete(long id)
    {
        if (!_timeEntryRepository.Contains(id))
        {
            return NotFound();
        }
        _timeEntryRepository.Delete(id);
        return NoContent();
    }
}