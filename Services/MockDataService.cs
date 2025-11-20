namespace TimeStoneReport.Services;

using TimeStoneReport.Models;

public class MockDataService
{
    public ICollection<Project> Projects { get; private set; }
    public ICollection<TimeStoneTask> Tasks { get; private set; }

    public MockDataService()
    {
        // Initialize Projects
        Projects = new List<Project>
        {
            new() {
                Id = 1,
                Name = "TimeStone Application",
                Code = "TS-APP",
                Description = "A comprehensive time tracking and task management application built with Blazor and MudBlazor.",
                CreatedDate = DateTime.Now.AddMonths(-3),
                Notes = "Main project for time tracking system."
            },
            new() {
                Id = 2,
                Name = "Client Portal",
                Code = "CP-2024",
                Description = "Customer-facing portal for viewing project status and time reports.",
                CreatedDate = DateTime.Now.AddMonths(-2),
                Notes = "Focus on mobile responsiveness."
            },
            new() {
                Id = 3,
                Name = "Internal Tools",
                Code = "INT-TOOLS",
                Description = "Collection of internal utilities and automation scripts.",
                CreatedDate = DateTime.Now.AddMonths(-1),
                Notes = "Low priority - work during downtime."
            }
        };

        // Initialize Tasks with TimeRecords
        Tasks = new List<TimeStoneTask>
        {
            new() {
                Id = 1,
                Title = "Design Database Schema",
                Description = "Create an efficient database schema for the new project.",
                DueDate = DateTime.Now.AddDays(7),
                IsCompleted = false,
                Notes = "Consider using NoSQL for flexibility.",
                Priority = PriorityLevel.High,
                ProjectId = 1,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 1,
                        TaskId = 1,
                        StartTime = DateTime.Now.AddDays(-5).AddHours(-4),
                        EndTime = DateTime.Now.AddDays(-5),
                        Notes = "Initial schema design"
                    },
                    new() {
                        Id = 2,
                        TaskId = 1,
                        StartTime = DateTime.Now.AddDays(-2).AddHours(-3),
                        EndTime = DateTime.Now.AddDays(-2).AddHours(-1).AddMinutes(-25),
                        Notes = "Schema refinement"
                    }
                }
            },
            new() {
                Id = 2,
                Title = "Implement Authentication",
                Description = "Set up user authentication and authorization using OAuth2 and JWT.",
                DueDate = DateTime.Now.AddDays(14),
                IsCompleted = false,
                Notes = "Look into OAuth2 and JWT best practices.",
                Priority = PriorityLevel.Critical,
                ProjectId = 1,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 3,
                        TaskId = 2,
                        StartTime = DateTime.Now.AddDays(-8).AddHours(-5),
                        EndTime = DateTime.Now.AddDays(-8).AddHours(-2),
                        Notes = "Research authentication methods"
                    },
                    new() {
                        Id = 4,
                        TaskId = 2,
                        StartTime = DateTime.Now.AddDays(-3).AddHours(-6),
                        EndTime = DateTime.Now.AddDays(-3).AddHours(-2),
                        Notes = "Implementation start"
                    }
                }
            },
            new() {
                Id = 3,
                Title = "Frontend UI Design",
                Description = "Design the user interface for the client portal with focus on UX.",
                DueDate = DateTime.Now.AddDays(21),
                IsCompleted = false,
                Notes = "Focus on user experience and accessibility.",
                Priority = PriorityLevel.Medium,
                ProjectId = 2,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 5,
                        TaskId = 3,
                        StartTime = DateTime.Now.AddDays(-10).AddHours(-4),
                        EndTime = DateTime.Now.AddDays(-10).AddHours(-1),
                        Notes = "Created wireframes"
                    },
                    new() {
                        Id = 6,
                        TaskId = 3,
                        StartTime = DateTime.Now.AddDays(-6).AddHours(-5),
                        EndTime = DateTime.Now.AddDays(-6).AddHours(-2).AddMinutes(-30),
                        Notes = "Design mockups"
                    }
                }
            },
            new() {
                Id = 4,
                Title = "Write Unit Tests",
                Description = "Create comprehensive unit tests for the existing codebase.",
                DueDate = DateTime.Now.AddDays(-5),
                IsCompleted = true,
                Notes = "Aim for at least 80% code coverage.",
                Priority = PriorityLevel.Medium,
                ProjectId = 1,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 7,
                        TaskId = 4,
                        StartTime = DateTime.Now.AddDays(-15).AddHours(-3),
                        EndTime = DateTime.Now.AddDays(-15).AddHours(-1),
                        Notes = "Setup test framework"
                    },
                    new() {
                        Id = 8,
                        TaskId = 4,
                        StartTime = DateTime.Now.AddDays(-12).AddHours(-4),
                        EndTime = DateTime.Now.AddDays(-12).AddHours(-1).AddMinutes(-30),
                        Notes = "Wrote unit tests"
                    },
                    new() {
                        Id = 9,
                        TaskId = 4,
                        StartTime = DateTime.Now.AddDays(-8).AddHours(-2),
                        EndTime = DateTime.Now.AddDays(-8).AddHours(-1),
                        Notes = "Code review fixes"
                    }
                }
            },
            new() {
                Id = 5,
                Title = "Build Automation Scripts",
                Description = "Create scripts to automate deployment and repetitive tasks.",
                DueDate = DateTime.Now.AddDays(20),
                IsCompleted = false,
                Notes = "Start with deployment automation.",
                Priority = PriorityLevel.Low,
                ProjectId = 3,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 10,
                        TaskId = 5,
                        StartTime = DateTime.Now.AddDays(-4).AddHours(-2),
                        EndTime = DateTime.Now.AddDays(-4).AddHours(-1),
                        Notes = "Initial scripting"
                    }
                }
            },
            new() {
                Id = 6,
                Title = "Personal Research",
                Description = "Research new technologies and best practices for professional development.",
                DueDate = DateTime.Now.AddDays(30),
                IsCompleted = false,
                Notes = "No specific project - general learning.",
                Priority = PriorityLevel.Low,
                ProjectId = null,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 11,
                        TaskId = 6,
                        StartTime = DateTime.Now.AddDays(-7).AddHours(-2),
                        EndTime = DateTime.Now.AddDays(-7).AddHours(-0).AddMinutes(-30),
                        Notes = "Read articles on modern architecture"
                    },
                    new() {
                        Id = 12,
                        TaskId = 6,
                        StartTime = DateTime.Now.AddDays(-1).AddHours(-1),
                        EndTime = DateTime.Now.AddDays(-1).AddHours(-0).AddMinutes(-15),
                        Notes = "Watched tutorial videos"
                    }
                }
            },
            new() {
                Id = 7,
                Title = "API Integration",
                Description = "Integrate third-party APIs for enhanced functionality.",
                DueDate = DateTime.Now.AddDays(10),
                IsCompleted = false,
                Notes = "Priority integration: payment gateway.",
                Priority = PriorityLevel.High,
                ProjectId = 2,
                TimeRecords = new List<TimeRecord>
                {
                    new() {
                        Id = 13,
                        TaskId = 7,
                        StartTime = DateTime.Now.AddDays(-20).AddHours(-5),
                        EndTime = DateTime.Now.AddDays(-20).AddHours(-2),
                        Notes = "API research and planning"
                    },
                    new() {
                        Id = 14,
                        TaskId = 7,
                        StartTime = DateTime.Now.AddDays(-18).AddHours(-4),
                        EndTime = DateTime.Now.AddDays(-18).AddHours(-1),
                        Notes = "Started integration"
                    }
                }
            }
        };
    }

    public Project? GetProjectById(int id)
    {
        return Projects.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<TimeStoneTask> GetTasksByProject(int projectId)
    {
        return Tasks.Where(t => t.ProjectId == projectId);
    }

    public IEnumerable<TimeStoneTask> GetTasksInDateRange(DateTime startDate, DateTime endDate)
    {
        return Tasks.Where(t => t.TimeRecords.Any(r => 
            r.StartTime >= startDate && r.StartTime <= endDate));
    }

    public IEnumerable<TimeStoneTask> GetTasksByProjectsInDateRange(
        IEnumerable<int> projectIds, DateTime startDate, DateTime endDate)
    {
        if (!projectIds.Any())
        {
            return GetTasksInDateRange(startDate, endDate);
        }

        return Tasks.Where(t => 
            (!t.ProjectId.HasValue || projectIds.Contains(t.ProjectId.Value)) &&
            t.TimeRecords.Any(r => r.StartTime >= startDate && r.StartTime <= endDate));
    }
}
