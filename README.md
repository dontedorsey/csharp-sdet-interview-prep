# C# SDET Interview Prep — Framework Skeleton

Companion repo for the **C# SDET Interview Prep Kit** (available on Gumroad).

This skeleton demonstrates the patterns, project structure, and CI pipeline configuration covered in the guide. Adapt it to your target application — every `data-testid` selector, endpoint URL, and model class is a placeholder.

---

## What the Code Looks Like

```csharp
// Fluent test data builder — no magic strings, no telescoping constructors
var user = new UserBuilder()
    .WithRole(UserRole.Admin)
    .WithEmail("qa@example.com")
    .Build();

// Page Object Model with Playwright for .NET
var dashboard = await loginPage.LoginAsync("qa@example.com", "ValidPass1!");
Assert.That(await dashboard.GetWelcomeTextAsync(), Does.Contain("Welcome"));

// RestSharp integration test
var response = await _client.ExecutePostAsync<OrderResponse>(request);
Assert.That((int)response.StatusCode, Is.EqualTo(201));
```

---

## Structure

```
csharp-sdet-interview-prep/
├── src/
│   └── TestFramework/          # Shared builders, config, helpers (no test runner dependency)
│       ├── Builders/           # UserBuilder, OrderBuilder — Builder pattern (Section 02)
│       ├── Configuration/      # TestConfig — reads from env vars or testsettings.json
│       ├── Helpers/            # WaitHelper, AssertHelper
│       └── Models/             # User, Order, enums
├── tests/
│   ├── UnitTests/              # C# fundamentals examples (Section 01)
│   ├── IntegrationTests/       # RestSharp API tests (Section 05)
│   └── E2ETests/               # Playwright POM (Section 02)
│       ├── Base/               # PlaywrightTestBase — tracing, headless config
│       ├── Pages/              # BasePage, LoginPage, DashboardPage
│       └── Tests/              # LoginTests
├── .github/workflows/
│   ├── ci.yml                  # Unit + Integration on every PR
│   └── e2e.yml                 # E2E on main push + nightly
└── azure-pipelines.yml         # Azure DevOps equivalent
```

---

## Quick Start

### 1. Clone and configure

```bash
git clone https://github.com/dontedorsey/csharp-sdet-interview-prep.git
cd csharp-sdet-interview-prep

# Copy example config and fill in your values
cp testsettings.example.json testsettings.local.json
# Edit testsettings.local.json — never commit this file
```

### 2. Create the solution

```bash
dotnet new sln -n CSharpSdetInterviewPrep
dotnet sln add src/TestFramework/TestFramework.csproj
dotnet sln add tests/UnitTests/UnitTests.csproj
dotnet sln add tests/IntegrationTests/IntegrationTests.csproj
dotnet sln add tests/E2ETests/E2ETests.csproj
```

### 3. Restore and build

```bash
dotnet restore
dotnet build --configuration Release
```

### 4. Install Playwright browsers (E2E only)

```bash
pwsh tests/E2ETests/bin/Release/net8.0/playwright.ps1 install chromium
```

### 5. Run tests by category

```bash
# Unit tests only
dotnet test --filter "Category=Unit"

# Integration tests (requires running API)
dotnet test --filter "Category=Integration"

# E2E tests (requires running app + BASE_URL set)
dotnet test --filter "Category=E2E"

# All except E2E (good for local dev loop)
dotnet test --filter "Category!=E2E"
```

---

## Configuration

Tests read config from (in priority order):

1. Environment variables prefixed with `TEST_` — used in CI
2. `testsettings.local.json` — local dev, gitignored
3. `testsettings.json` — committed defaults (non-sensitive only)

| Variable | Purpose | Default |
|---|---|---|
| `TEST_BASE_URL` | Browser test base URL | `http://localhost:3000` |
| `TEST_API_BASE_URL` | API test base URL | `http://localhost:5000` |
| `TEST_EMAIL` | Test user email | `test@example.com` |
| `TEST_PASSWORD` | Test user password | `TestPass1!` |
| `TEST_AUTH_MODE` | `password` or `token` | `password` |
| `TEST_TOKEN` | Bearer token (if auth mode = token) | _(empty)_ |
| `CI` | Set to `true` to force headless | _(unset = headed)_ |

---

## CI Pipeline Setup

### GitHub Actions

Secrets required (Settings → Secrets and variables → Actions):

- `TEST_API_BASE_URL`
- `TEST_EMAIL`
- `TEST_PASSWORD`
- `STAGING_BASE_URL` (E2E only)

### Azure DevOps

Create a variable group named `sdet-kit-test-secrets` in ADO Library containing the same variables. Mark sensitive values as secret.

---

## Adapting to Your Application

1. **Replace selectors** in `Pages/` with your app's `data-testid` attributes
2. **Replace models** in `IntegrationTests/Api/` with your actual API response shapes
3. **Replace endpoints** in `OrderApiTests.cs` with your real API routes
4. **Add page objects** for each screen you want to test
5. **Add builders** for each domain entity you need test data for

---

## Guide Reference

| Skeleton file | Guide section |
|---|---|
| `TestFramework/Builders/` | Section 01 (generics), Section 02 (Builder pattern) |
| `TestFramework/Helpers/WaitHelper.cs` | Section 01 (delegates, Func) |
| `TestFramework/Configuration/TestConfig.cs` | Section 02 (maintainability), Section 04 (CI config) |
| `E2ETests/Base/PlaywrightTestBase.cs` | Section 02 (POM), Section 04 (tracing) |
| `E2ETests/Pages/` | Section 02 (Page Object Model) |
| `IntegrationTests/Api/` | Section 05 (API testing, RestSharp) |
| `.github/workflows/ci.yml` | Section 04 (GitHub Actions) |
| `azure-pipelines.yml` | Section 04 (Azure DevOps) |

---

*Part of the C# SDET Interview Prep Kit — [get the full guide on Gumroad](https://ddorseyguides.gumroad.com/l/pbnujy)*
