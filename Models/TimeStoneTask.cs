namespace TimeStoneReport.Models;

public class TimeStoneTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int? ProjectId { get; set; }
    public ICollection<TimeRecord> TimeRecords { get; set; } = new List<TimeRecord>();
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;

    public TimeSpan TimeSpent
    {
        get
        {
            TimeSpan total = TimeSpan.Zero;
            foreach (var record in TimeRecords)
            {
                if (record.EndTime.HasValue)
                {
                    total += record.EndTime.Value - record.StartTime;
                }
                else
                {
                    total += DateTime.Now - record.StartTime;
                }
            }
            return total;
        }
    }

    public TimeSpan GetTimeSpentInRange(DateTime startDate, DateTime endDate)
    {
        TimeSpan total = TimeSpan.Zero;
        foreach (var record in TimeRecords)
        {
            if (record.StartTime >= startDate && record.StartTime <= endDate)
            {
                if (record.EndTime.HasValue)
                {
                    total += record.EndTime.Value - record.StartTime;
                }
                else if (DateTime.Now <= endDate)
                {
                    total += DateTime.Now - record.StartTime;
                }
            }
        }
        return total;
    }
}

public enum PriorityLevel
{
    Low,
    Medium,
    High,
    Critical
}
