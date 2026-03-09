# AI Task Log — .NET 10 backlog + foundations

## Objective
Implement a practical first slice of the `.NET 10` opportunity work for TimesheetGPT, while documenting the work inside the repo.

Requested outcomes:
1. Create a prioritized implementation backlog document derived from `docs/dotnet-10-opportunities.md`
2. Implement the first worthwhile .NET 10-ready improvements in code
3. Restore/build/test as available
4. Summarize what was implemented, what remains, and which branch was used
5. Commit changes to a feature branch without touching `main`

Additional requirement from Brady:
- document everything in-repo under `ai-tasks/`

## Working branch
- `feature/dotnet10-backlog-and-foundations`

## Plan
1. Review current docs and solution structure
2. Create a concrete backlog document with phases and tasks
3. Make WebAPI a more real application boundary with typed endpoints
4. Improve Core DI boundaries by introducing a dedicated generation orchestration service
5. Add practical progress hooks for range generation
6. Validate with restore/build/test
7. Commit to the feature branch and record the final state here

## Actions taken
- Reviewed:
  - `README.md`
  - `docs/dotnet-10-opportunities.md`
  - `docs/architecture-proposal.md`
  - current `Gateway`, `WebAPI`, `WebUI`, and `Core` files relevant to generation and chat
- Created feature branch:
  - `feature/dotnet10-backlog-and-foundations`
- Added a dedicated generation orchestration service in `TimesheetGPT.Core`
  - `ITimesheetGenerationService`
  - `TimesheetGenerationService`
  - `GenerationProgressUpdate`
- Updated `Summary` to carry the generated date
- Kept `TimesheetService` as a thin facade over the new generation orchestration service plus chat/submission helpers
- Registered the new generation service through DI in `ConfigureServices`
- Replaced the stub-style API setup with typed endpoint groups for:
  - `POST /api/timesheets/generate`
  - `POST /api/timesheets/generate-range`
  - `POST /api/chat/messages`
  - `GET /api/me`
  - `GET /api/providers`
- Added API request/response contracts in `TimesheetGPT.WebAPI/Contracts`
- Added `ProblemDetails` support in API registration
- Updated the WebAPI project SDK configuration so minimal API features compile correctly while remaining library-hosted by `Gateway`
- Updated the date-range Blazor page to use the new range generation service with progress callbacks instead of mutating UI state from `Parallel.ForEachAsync`
- Added lightweight progress/status text to the shared results component
- Added a prioritized backlog doc in `docs/dotnet-10-implementation-backlog.md`
- Added this in-repo task log under `ai-tasks/`

## Files changed
### Documentation / task logging
- `ai-tasks/2026-03-09-dotnet10-foundations.md`
- `docs/dotnet-10-implementation-backlog.md`

### Core
- `src/TimesheetGPT.Core/ConfigureServices.cs`
- `src/TimesheetGPT.Core/Interfaces/ITimesheetGenerationService.cs`
- `src/TimesheetGPT.Core/Models/Generation/GenerationProgressUpdate.cs`
- `src/TimesheetGPT.Core/Models/Summary.cs`
- `src/TimesheetGPT.Core/Services/TimesheetGenerationService.cs`
- `src/TimesheetGPT.Core/Services/TimesheetService.cs`

### WebAPI
- `src/TimesheetGPT.WebAPI/Contracts/TimesheetContracts.cs`
- `src/TimesheetGPT.WebAPI/DependencyInjection.cs`
- `src/TimesheetGPT.WebAPI/Endpoints/ChatEndpoints.cs`
- `src/TimesheetGPT.WebAPI/Endpoints/EmailEndpoints.cs`
- `src/TimesheetGPT.WebAPI/Endpoints/MetaEndpoints.cs`
- `src/TimesheetGPT.WebAPI/Endpoints/TimesheetEndpoints.cs`
- `src/TimesheetGPT.WebAPI/TimesheetGPT.WebAPI.csproj`

### WebUI
- `src/TimesheetGPT.WebUI/Components/Results.razor`
- `src/TimesheetGPT.WebUI/Models/ResultsVm.cs`
- `src/TimesheetGPT.WebUI/Pages/DateRangeTimesheets.razor`

## Validation
### Restore
- `dotnet restore TimesheetGPT.sln`
- Result: succeeded

### Build
- `dotnet build TimesheetGPT.sln -c Debug`
- Result: succeeded

### Test
- `dotnet test TimesheetGPT.sln -c Debug --no-build`
- Result: no test projects currently present, so there were no repo tests to execute

## Issues encountered and resolved
1. **Minimal API types unavailable in WebAPI project**
   - Cause: `TimesheetGPT.WebAPI` used the base .NET SDK rather than the web SDK
   - Fix: switched to `Microsoft.NET.Sdk.Web` and kept `OutputType` as `Library` so the project still composes into `Gateway`
2. **Previous date-range page mutated component state from parallel work**
   - Fix: replaced the `Parallel.ForEachAsync` flow with orchestration in Core plus progress callbacks surfaced to the UI

## Remaining work
- Move the single-day and chat WebUI flows to consume the new API endpoints directly instead of relying on the server-side facade
- Add endpoint/integration tests for the new API contracts
- Introduce a provider-neutral activity model in Core
- Add persistence (`TimesheetRun`, history, prompt profiles, provider connection metadata)
- Convert range generation from in-process progress reporting to a background job + status endpoint model
- Add streaming chat/generation updates over SSE or SignalR

## Branch / commit info
- Branch: `feature/dotnet10-backlog-and-foundations`
- Commit: `97953ff` — `✨ Add .NET 10 API backlog and generation foundations`
- Remote push: pushed to `origin/feature/dotnet10-backlog-and-foundations`
