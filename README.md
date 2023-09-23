# TimesheetGPT 🕒

Goal is to make timesheets easier (especially for non-devs) by 
- Getting data from various sources (currently Microsoft Graph, soon GitHub, Trello, DevOps maybe)
- Use ChatGPT API to summarize all the data
- Use all the summarys to make a Timesheet

## Technical stuff
Right now API isn't used at all, the Blazor Server app handles auth then talks to Application layer directly.
This is ok right now but I planned to use the API to do all the work, then we can integrate this into timesheeting software.
