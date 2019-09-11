using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
{
    private readonly ITimeEntryRepository _timeEntryRepository;
    private readonly IOperationCounter<TimeEntry> _operationCounter;

    public TimeEntryController(ITimeEntryRepository timeEntryRepository, IOperationCounter<TimeEntry> operationCounter)
    {
        this._timeEntryRepository = timeEntryRepository;
        _operationCounter = operationCounter;
    }

    [HttpGet("{id}", Name = "GetTimeEntry")]
    public IActionResult  Read(long id)
    {
        _operationCounter.Increment(TrackedOperation.Read);

        return _timeEntryRepository.Contains(id)? (IActionResult) Ok(_timeEntryRepository.Find(id)) : NotFound();
    }

    [HttpPost]
    public IActionResult  Create([FromBody] TimeEntry timeEntry)
    {
        _operationCounter.Increment(TrackedOperation.Create);

        var createdTimeEntry = _timeEntryRepository.Create(timeEntry);

        return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
    }

    [HttpGet]
    public IActionResult  List()
    {
        _operationCounter.Increment(TrackedOperation.List);
        return Ok(_timeEntryRepository.List());
    }

    [HttpPut("{id}")]
    public IActionResult  Update(long id, [FromBody] TimeEntry timeEntry)
    {
        _operationCounter.Increment(TrackedOperation.Update);
        return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Update(id, timeEntry)) : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult  Delete(long id)
    {
        _operationCounter.Increment(TrackedOperation.Delete);
        if (!_timeEntryRepository.Contains(id))
        {
            return NotFound();
        }
        _timeEntryRepository.Delete(id);
        return NoContent();
    }
}
}