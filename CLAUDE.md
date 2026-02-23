# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Sentaur Survivors Leaderboard — a .NET 9.0 demo application (Sentry integration showcase) with a game leaderboard, JWT auth, and lottery system. Built for conference demos (GDC 2025).

## Architecture

Four projects in `Sentaur.Leaderboard.sln`:

- **Sentaur.Leaderboard** — Shared data models library (`ScoreEntry` record)
- **Sentaur.Leaderboard.Api** — ASP.NET Core minimal API backend (PostgreSQL + EF Core, JWT auth)
- **Sentaur.Leaderboard.Web** — Blazor WebAssembly frontend (static SPA)
- **Sentaur.Leaderboard.AntiCheat** — Google Cloud Function (serverless, processes Sentry event stream)

The API uses minimal APIs (all endpoints in `Program.cs`, no controllers). The Web app fetches scores from the deployed API at `https://sentaur-leaderboard-f7z2cjcdzq-uc.a.run.app/score`.

## Build & Run Commands

```bash
# Build entire solution
dotnet build -c Release

# Run API locally (http://localhost:5203)
dotnet run --project Sentaur.Leaderboard.Api

# Run Web frontend locally (http://localhost:5064)
dotnet run --project Sentaur.Leaderboard.Web

# Publish Web app for static hosting
dotnet publish Sentaur.Leaderboard.Web -c Release -o webapp

# Restore dependencies
dotnet restore

# EF Core database migrations (from Api project directory)
dotnet ef database update --project Sentaur.Leaderboard.Api
```

## Local Development Setup

Requires .NET 9.0 SDK and the WASM workload (`dotnet workload install wasm-tools`). The API needs a local PostgreSQL instance — connection details are in `Sentaur.Leaderboard.Api/appsettings.Development.json`.

## Key Files

- `Sentaur.Leaderboard.Api/Program.cs` — All API endpoints and middleware configuration
- `Sentaur.Leaderboard.Api/LeaderboardContext.cs` — EF Core DbContext
- `Sentaur.Leaderboard/ScoreEntry.cs` — Shared score data model
- `Sentaur.Leaderboard.Web/Pages/Score.razor` — Main leaderboard UI page
- `Sentaur.Leaderboard.Web/wwwroot/index.html` — HTML shell with Sentry JS SDK init

## API Endpoints

| Method | Path | Auth | Purpose |
|--------|------|------|---------|
| GET | `/score` | No | Fetch leaderboard scores |
| POST | `/score` | Yes | Submit a score |
| DELETE | `/score?name&score` | Yes | Remove a score |
| POST | `/token` | No | Get JWT token (basic auth) |
| GET | `/lottery` | Yes | Draw random lottery winner |
| GET | `/lottery/entries` | Yes | Get lottery entries |

## Deployment

- **API**: Docker → Google Cloud Run (via `cloudbuild.yaml`)
- **Web**: GitHub Actions → GitHub Pages (via `.github/workflows/dotnet-build.yml`)
- CI builds run on ubuntu-22.04, windows-latest, and macos-latest

## Sentry Integration

Sentry is deeply integrated across all projects for error tracking, performance profiling, and session replay. The DSN and release version are configured in `appsettings.json` files and `wwwroot/index.html`.
