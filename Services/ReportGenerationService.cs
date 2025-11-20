namespace TimeStoneReport.Services;

using System.Text;
using TimeStoneReport.Models;
using Microsoft.JSInterop;

public class ReportGenerationService
{
    private readonly IJSRuntime _jsRuntime;

    public ReportGenerationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public string GenerateMarkdownReport(
        IEnumerable<TimeStoneTask> tasks,
        IEnumerable<Project> allProjects,
        DateTime startDate,
        DateTime endDate,
        IEnumerable<int> selectedProjectIds)
    {
        var sb = new StringBuilder();

        // Report Header
        sb.AppendLine("# TimeStone - Time Report");
        sb.AppendLine();
        sb.AppendLine($"**Report Generated:** {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine();
        sb.AppendLine($"**Date Range:** {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
        sb.AppendLine();

        // Selected Projects Filter
        if (selectedProjectIds.Any())
        {
            sb.AppendLine("**Filtered Projects:**");
            foreach (var projectId in selectedProjectIds)
            {
                var project = allProjects.FirstOrDefault(p => p.Id == projectId);
                if (project != null)
                {
                    sb.AppendLine($"- {project.Code}: {project.Name}");
                }
            }
            sb.AppendLine();
        }
        else
        {
            sb.AppendLine("**Projects:** All Projects");
            sb.AppendLine();
        }

        // Summary Section
        sb.AppendLine("## Summary");
        sb.AppendLine();

        var totalTasks = tasks.Count();
        var completedTasks = tasks.Count(t => t.IsCompleted);
        var pendingTasks = totalTasks - completedTasks;

        var totalHours = tasks.Sum(t => t.GetTimeSpentInRange(startDate, endDate).TotalHours);

        sb.AppendLine($"- **Total Tasks:** {totalTasks}");
        sb.AppendLine($"- **Completed Tasks:** {completedTasks}");
        sb.AppendLine($"- **Pending Tasks:** {pendingTasks}");
        sb.AppendLine($"- **Total Time Spent:** {totalHours:F2} hours");
        sb.AppendLine();

        // Time by Project
        sb.AppendLine("## Time by Project");
        sb.AppendLine();

        var projectGroups = tasks
            .Where(t => t.ProjectId.HasValue)
            .GroupBy(t => t.ProjectId!.Value)
            .OrderByDescending(g => g.Sum(t => t.GetTimeSpentInRange(startDate, endDate).TotalHours));

        if (projectGroups.Any())
        {
            sb.AppendLine("| Project Code | Project Name | Tasks | Hours |");
            sb.AppendLine("|--------------|--------------|-------|-------|");

            foreach (var group in projectGroups)
            {
                var project = allProjects.FirstOrDefault(p => p.Id == group.Key);
                if (project != null)
                {
                    var hours = group.Sum(t => t.GetTimeSpentInRange(startDate, endDate).TotalHours);
                    var taskCount = group.Count();
                    sb.AppendLine($"| {project.Code} | {project.Name} | {taskCount} | {hours:F2} |");
                }
            }
            sb.AppendLine();
        }

        // Unassigned tasks
        var unassignedTasks = tasks.Where(t => !t.ProjectId.HasValue).ToList();
        if (unassignedTasks.Any())
        {
            var unassignedHours = unassignedTasks.Sum(t => t.GetTimeSpentInRange(startDate, endDate).TotalHours);
            sb.AppendLine($"**Unassigned Tasks:** {unassignedTasks.Count} tasks, {unassignedHours:F2} hours");
            sb.AppendLine();
        }

        // Detailed Task Breakdown
        sb.AppendLine("## Detailed Task Breakdown");
        sb.AppendLine();

        // Group by project
        var tasksByProject = tasks
            .Where(t => t.ProjectId.HasValue)
            .GroupBy(t => t.ProjectId!.Value)
            .OrderBy(g => {
                var proj = allProjects.FirstOrDefault(p => p.Id == g.Key);
                return proj?.Code ?? "";
            });

        foreach (var group in tasksByProject)
        {
            var project = allProjects.FirstOrDefault(p => p.Id == group.Key);
            if (project != null)
            {
                sb.AppendLine($"### {project.Code}: {project.Name}");
                sb.AppendLine();

                foreach (var task in group.OrderByDescending(t => t.GetTimeSpentInRange(startDate, endDate)))
                {
                    AppendTaskDetails(sb, task, startDate, endDate);
                }
            }
        }

        // Unassigned tasks section
        if (unassignedTasks.Any())
        {
            sb.AppendLine("### Unassigned Tasks");
            sb.AppendLine();

            foreach (var task in unassignedTasks.OrderByDescending(t => t.GetTimeSpentInRange(startDate, endDate)))
            {
                AppendTaskDetails(sb, task, startDate, endDate);
            }
        }

        // Footer
        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine("*Generated by TimeStone Time Tracking System*");

        return sb.ToString();
    }

    private void AppendTaskDetails(StringBuilder sb, TimeStoneTask task, DateTime startDate, DateTime endDate)
    {
        var timeInRange = task.GetTimeSpentInRange(startDate, endDate);
        
        sb.AppendLine($"#### {task.Title}");
        sb.AppendLine();
        sb.AppendLine($"- **Description:** {task.Description}");
        sb.AppendLine($"- **Priority:** {task.Priority}");
        sb.AppendLine($"- **Status:** {(task.IsCompleted ? "Completed" : "Pending")}");
        sb.AppendLine($"- **Due Date:** {task.DueDate:yyyy-MM-dd}");
        sb.AppendLine($"- **Time Spent (in range):** {timeInRange.TotalHours:F2} hours");
        sb.AppendLine();

        // Time Records in range
        var recordsInRange = task.TimeRecords
            .Where(r => r.StartTime >= startDate && r.StartTime <= endDate)
            .OrderBy(r => r.StartTime)
            .ToList();

        if (recordsInRange.Any())
        {
            sb.AppendLine("**Time Records:**");
            sb.AppendLine();
            sb.AppendLine("| Date | Start Time | End Time | Duration | Notes |");
            sb.AppendLine("|------|------------|----------|----------|-------|");

            foreach (var record in recordsInRange)
            {
                var duration = record.EndTime.HasValue
                    ? (record.EndTime.Value - record.StartTime).TotalHours
                    : (DateTime.Now - record.StartTime).TotalHours;

                var endTimeStr = record.EndTime.HasValue
                    ? record.EndTime.Value.ToString("HH:mm")
                    : "Ongoing";

                var notes = string.IsNullOrWhiteSpace(record.Notes) ? "-" : record.Notes;

                sb.AppendLine($"| {record.StartTime:yyyy-MM-dd} | {record.StartTime:HH:mm} | {endTimeStr} | {duration:F2}h | {notes} |");
            }
            sb.AppendLine();
        }

        sb.AppendLine();
    }

    public async Task<bool> SaveReportWithPickerAsync(string content, string fileName)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        var base64 = Convert.ToBase64String(bytes);

        return await _jsRuntime.InvokeAsync<bool>("saveFileAs", fileName, base64);
    }
}
