using System.Collections.Generic;

public interface ITimeEntryRepository
{
    
    TimeEntry Create(TimeEntry timeEntry);

    TimeEntry Update(long id, TimeEntry timeEntry);

    void Delete(long id);

    TimeEntry Find(long id);

    bool Contains(long id);

    IEnumerable<TimeEntry> List();
}