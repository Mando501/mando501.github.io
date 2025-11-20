# TimeStone Report Generator - UR-024 Prototype

## 🚀 Live Demo

**This application is deployed on GitHub Pages!**

Visit: `https://[YOUR-USERNAME].github.io/TimeStoneReport/`

_(After deployment, update this URL with your GitHub username)_

## Overview
This is a throwaway prototype implementing **UR-024: Faculty Generates Time Reports** using .NET 9 Blazor WebAssembly and MudBlazor UI components.

## Requirements Implementation

### UR-024 Functional Requirement
**"The user shall be able to generate reports detailing the time spent on tasks and projects."**

This prototype implements all flows defined in UR-024:

### Main Flow ✅
1. **Precondition**: User starts on the Tasks view (`/tasks`)
2. User clicks "Generate Report" button
3. System displays Report Generation view (`/generate-report`)
4. System defaults date range to first and last day of current month
5. User can select start date
6. User can select end date
7. User can select zero or more projects (multi-select dropdown)
8. User specifies file name for the report
9. User clicks "Generate Report"
   - System validates file location is provided
   - System filters tasks by date range and selected projects
   - System generates comprehensive Markdown report with:
     - Summary statistics
     - Time breakdown by project
     - Detailed task information
     - Individual time records
   - System downloads the report file
10. System returns to Tasks view

### Alternative Flow: Cancel ✅
- User can click "Cancel" button at any time
- System returns to Tasks view without generating report

### Exception Flows ✅
1. **File Location Required**: 
   - If no filename provided, system displays error message
   - User remains on Generate Report view to correct

2. **I/O Error**: 
   - If file download fails, system notifies user
   - System returns to Tasks view

### Extension Point: File Location ✅
- User enters filename in text field
- Note: Blazor WASM downloads to browser's default download folder
- Path specification is handled by the browser

## Technology Stack

- **.NET 9** - Latest .NET framework
- **Blazor WebAssembly** - Client-side web application
- **MudBlazor 8.14.0** - Material Design component library
- **C# 12** - Programming language

## Project Structure

```
TimeStoneReport/
├── Models/
│   ├── TimeStoneTask.cs      # Task entity with time tracking
│   ├── Project.cs             # Project entity
│   └── TimeRecord.cs          # Individual time record
├── Services/
│   ├── MockDataService.cs     # Mock data provider
│   └── ReportGenerationService.cs  # Report generation logic
├── Pages/
│   ├── Home.razor             # Home/dashboard page
│   ├── Tasks.razor            # Task list view (precondition)
│   ├── Projects.razor         # Project list view
│   └── GenerateReport.razor   # Report generation UI (main feature)
├── Layout/
│   ├── MainLayout.razor       # Application layout
│   └── NavMenu.razor          # Navigation menu
└── wwwroot/
    └── index.html             # Entry point with download JS
```

## Features Implemented

### 1. Tasks View (Precondition)
- Displays all tasks with time records
- Shows task statistics (completed, pending, total hours)
- Table view with project associations
- Direct link to Generate Report

### 2. Generate Report Page (Main Feature)
- **File Location Input**: Text field for report filename
- **Date Range Selection**: 
  - Defaults to current month (first to last day)
  - DatePicker controls with validation
- **Project Filter**:
  - Multi-select dropdown
  - Option to select zero or more projects
  - Visual chips showing selected projects
- **Preview Section**: 
  - Real-time statistics based on selections
  - Shows task count, project count, hours, days
- **Validation**:
  - File location required check
  - Date range validation
- **Actions**:
  - Generate Report button (with loading state)
  - Cancel button

### 3. Report Generation
The generated Markdown report includes:

```markdown
# TimeStone - Time Report

**Report Generated:** [timestamp]
**Date Range:** [start] to [end]
**Filtered Projects:** [selected projects or "All Projects"]

## Summary
- Total Tasks
- Completed Tasks
- Pending Tasks
- Total Time Spent

## Time by Project
[Table showing time breakdown by project]

## Detailed Task Breakdown
### [Project Name]
#### [Task Title]
- Description
- Priority
- Status
- Due Date
- Time Spent

**Time Records:**
[Table with date, start time, end time, duration, notes]
```

### 4. Mock Data
The prototype includes realistic mock data:
- 3 Projects (TimeStone App, Client Portal, Internal Tools)
- 7 Tasks with various statuses and priorities
- 14 Time Records spanning multiple days
- Time records in different date ranges for testing filters

## Running the Prototype

### Run Locally

#### Prerequisites
- .NET 9 SDK installed

#### Steps
1. Navigate to project directory:
   ```powershell
   cd c:\Users\dwork\Source\CSC5200\TimeStoneReport
   ```

2. Run the application:
   ```powershell
   dotnet run
   ```

3. Open browser to: `http://localhost:5112`

### Deploy to GitHub Pages

This project is configured for automatic deployment to GitHub Pages:

1. **Push to GitHub**:
   ```bash
   git init
   git add .
   git commit -m "Initial commit"
   git branch -M main
   git remote add origin https://github.com/[YOUR-USERNAME]/TimeStoneReport.git
   git push -u origin main
   ```

2. **Enable GitHub Pages**:
   - Go to your repository on GitHub
   - Click **Settings** → **Pages**
   - Under "Source", select **Deploy from a branch**
   - Select branch: **gh-pages** and folder: **/ (root)**
   - Click **Save**

3. **Automatic Deployment**:
   - GitHub Actions workflow will automatically build and deploy
   - Check **Actions** tab to see deployment progress
   - Your app will be live at: `https://[YOUR-USERNAME].github.io/TimeStoneReport/`

4. **Update README**:
   - After deployment, update the Live Demo URL in this README with your actual GitHub username

## Testing the UR-024 Flow

### Test Scenario 1: Main Flow
1. Open application at `http://localhost:5112`
2. Click "Tasks" in navigation menu
3. Click "Generate Report" button
4. Observe:
   - Date range defaults to current month ✅
   - File name is pre-filled with `TimeReport_[current-month].md` ✅
5. Select start date: `2025-11-01`
6. Select end date: `2025-11-30`
7. Select projects: "TS-APP" and "CP-2024"
8. Enter filename: `MyTimeReport.md`
9. Click "Generate Report"
10. Observe:
   - Report file downloads ✅
   - Success notification appears ✅
   - Returns to Tasks view ✅

### Test Scenario 2: Cancel Flow
1. Navigate to Generate Report page
2. Make some selections
3. Click "Cancel"
4. Verify: Returns to Tasks view without generating report ✅

### Test Scenario 3: Validation (File Location Required)
1. Navigate to Generate Report page
2. Clear the filename field
3. Click "Generate Report"
4. Observe:
   - Error message displays ✅
   - Red error text under field ✅
   - Snackbar notification ✅
   - Remains on Generate Report view ✅

### Test Scenario 4: Date Range Filtering
1. Navigate to Generate Report page
2. Set date range to last month
3. Generate report
4. Open downloaded file
5. Verify: Only tasks with time records in that range are included ✅

### Test Scenario 5: Project Filtering
1. Navigate to Generate Report page
2. Select only one project (e.g., "TS-APP")
3. Generate report
4. Open downloaded file
5. Verify: Only tasks from selected project are included ✅

## Key Implementation Details

### Date Range Default Logic
```csharp
protected override void OnInitialized()
{
    var now = DateTime.Now;
    _startDate = new DateTime(now.Year, now.Month, 1);
    _endDate = _startDate.Value.AddMonths(1).AddDays(-1);
}
```

### File Location Validation
```csharp
if (string.IsNullOrWhiteSpace(_filePath))
{
    _filePathError = true;
    _filePathErrorText = "File location is required...";
    Snackbar.Add("File location is required!", Severity.Error);
    return;
}
```

### Report Download (Blazor WASM)
```javascript
window.downloadFile = function (fileName, contentType, base64Content) {
    const linkSource = `data:${contentType};base64,${base64Content}`;
    const downloadLink = document.createElement("a");
    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
};
```

## Limitations (As a Throwaway Prototype)

1. **No Backend**: All data is in-memory mock data
2. **No Database**: Data resets on page refresh
3. **Browser Download Only**: Files download to browser's default location
4. **No Authentication**: No user login/security
5. **No Edit/Delete**: Tasks and projects are read-only
6. **No Time Record Creation**: Mock data only
7. **No Report History**: Each generation is independent

## Future Enhancements (Not in Scope)

- Save report generation history
- Email report functionality
- PDF/Excel export options
- Report templates
- Scheduled report generation
- Custom report fields
- Time record filtering in report

## Compliance with UR-024

✅ **Functional Requirement**: Fully implemented
✅ **User Story**: Addresses faculty need to generate billing reports
✅ **Main Flow**: All 9 steps implemented
✅ **Alternative Flow (Cancel)**: Implemented
✅ **Exception Flow (File Location Required)**: Implemented
✅ **Exception Flow (I/O Error)**: Error handling included
✅ **Extension Point (Browse for File)**: Adapted for web (text input)
✅ **Preconditions**: Tasks view implemented
✅ **Postconditions**: Returns to Tasks view, report file created

## Notes

- This is a **throwaway prototype** for demonstration purposes
- Created as a standalone solution (not integrated with existing ProjectMocks)
- Focus on implementing UR-024 requirements, not production quality
- Mock data provides realistic scenarios for testing
- UI follows Material Design principles via MudBlazor
- All flows from UR-024 specification are implemented and testable

## Files Generated

When you generate a report, the downloaded Markdown file will contain:
- Report metadata (date range, generation time)
- Summary statistics
- Time breakdown by project (table format)
- Detailed task information for each project
- Individual time records with dates and durations

Example filename: `TimeReport_2025-11.md`

## Access Points

- **Home**: `http://localhost:5112/`
- **Tasks**: `http://localhost:5112/tasks` (precondition)
- **Projects**: `http://localhost:5112/projects`
- **Generate Report**: `http://localhost:5112/generate-report` (main feature)

---

**Generated**: November 19, 2025
**Requirement**: UR-024-FACULTY-GENERATES-TIME-REPORTS
**Technology**: .NET 9 Blazor WebAssembly + MudBlazor
