namespace TimeStoneReport.Models;

public class TimeRecord
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Notes { get; set; } = string.Empty;

    public TimeSpan Duration
    {
        get
        {
            if (EndTime.HasValue)
            {
                return EndTime.Value - StartTime;
            }
            return DateTime.Now - StartTime;
        }
    }
}
