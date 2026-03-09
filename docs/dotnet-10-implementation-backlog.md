# .NET 10 Implementation Backlog for TimesheetGPT

Derived from `docs/dotnet-10-opportunities.md` and aligned to the current solution shape.

## Prioritization principles
- Favor architectural payoff over cosmetic framework churn
- Prefer changes that make the API, DI boundaries, observability, and SaaS-readiness stronger
- Keep early phases safe, incremental, and reversible

---

## Phase 1 — Establish the real application boundary
**Goal:** stop the solution from remaining a UI-first prototype.

### Priority 1
- [x] Create typed endpoint groups in `TimesheetGPT.WebAPI`
- [x] Add concrete endpoints for:
  - [x] `POST /api/timesheets/generate`
  - [x] `POST /api/timesheets/generate-range`
  - [x] `POST /api/chat/messages`
  - [x] `GET /api/me`
  - [x] `GET /api/providers`
- [x] Return typed request/response contracts instead of stubbed endpoints
- [x] Add validation/problem-details style responses for invalid input

### Priority 2
- [ ] Move WebUI feature flows to consume the API boundary consistently
- [ ] Add endpoint filters for validation/auth/provider prerequisites
- [ ] Add integration tests around the new API contracts
- [ ] Add a consistent error contract for provider/AI failures

**Why first:** This unlocks testing, future frontend flexibility, and product boundaries without forcing persistence yet.

---

## Phase 2 — Improve orchestration and DI boundaries
**Goal:** move orchestration out of pages/components and into dedicated services.

### Priority 1
- [x] Introduce `ITimesheetGenerationService` in `TimesheetGPT.Core`
- [x] Move summary/range generation orchestration behind that interface
- [x] Keep `TimesheetService` as a thin facade while the UI transitions
- [x] Add a progress reporting hook for multi-day generation

### Priority 2
- [ ] Split chat orchestration into its own abstraction/service
- [ ] Introduce a normalized activity model that is provider-neutral
- [ ] Reduce direct UI awareness of Graph/Google-specific models
- [ ] Add structured logging/metrics around generation stages

**Why now:** This is the safest architectural improvement available in the current codebase.

---

## Phase 3 — Progress, streaming, and long-running workflows
**Goal:** improve responsiveness now, pave the way for background jobs next.

### Priority 1
- [x] Add progress hooks for date-range generation in Core
- [x] Surface progress states in the WebUI for range generation
- [ ] Add streaming chat responses from AI to UI/API
- [ ] Add server-sent events or SignalR for generation progress

### Priority 2
- [ ] Introduce job IDs for range generation
- [ ] Add `GET /api/timesheets/{id}` or `/runs/{id}` status endpoints
- [ ] Persist progress/history once a database exists

**Why here:** Perceived performance improves immediately, and the concepts translate cleanly into a background worker later.

---

## Phase 4 — Persistence and history
**Goal:** unlock reviewability, auditability, and SaaS workflows.

### Priority 1
- [ ] Add a persistence project/slice with EF Core
- [ ] Store users/workspaces/provider connection metadata
- [ ] Persist timesheet generation runs and entries
- [ ] Add health checks for database connectivity

### Priority 2
- [ ] Store prompt profiles/settings
- [ ] Capture evidence/source metadata per generated entry
- [ ] Add migrations and environment-safe connection handling

**Why after boundary work:** persistence pays off more once API/orchestration contracts are stable.

---

## Phase 5 — Background processing
**Goal:** remove long-running work from interactive UI execution.

### Priority 1
- [ ] Introduce a worker or queue-backed background processing path
- [ ] Convert range generation into an async job workflow
- [ ] Add retries and failure handling for Graph/Google/AI calls
- [ ] Report job progress/status to the UI

### Priority 2
- [ ] Add scheduled refresh/sync jobs
- [ ] Add cleanup/retention jobs
- [ ] Add cancellation support for user-triggered jobs

---

## Phase 6 — Observability, resilience, and SaaS hardening
**Goal:** make the system operable as it grows.

### Priority 1
- [ ] Add OpenTelemetry traces around generation/provider/AI steps
- [ ] Capture generation counts, durations, failures, and provider latency
- [ ] Add rate limiting for public endpoints
- [ ] Add downstream health checks for AI/provider dependencies

### Priority 2
- [ ] Add cost/token telemetry per generation
- [ ] Add abuse protection and tenant-aware quotas
- [ ] Add richer Aspire topology for backing services

---

## Recommended immediate next slice after this branch
1. Move the WebUI single-day and chat flows to call the API contracts directly
2. Add API integration tests for generation and chat endpoints
3. Introduce a provider-neutral activity schema in Core
4. Decide the first persistence mechanism and add a `TimesheetRun` record
5. Then convert range generation from in-process progress reporting to a background job model
