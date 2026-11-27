# Plan: Complete Subject and Department Endpoints (Student-style)

**Status:** Implemented. `dotnet build schoolproject.slnx` succeeds with 0 errors, and `dotnet ef migrations has-pending-model-changes` reports no model changes.

## 1. Goal

Add complete CRUD + paginated/list endpoints for **Department** and **Subject** using the same architecture, naming, response format, validation, localization, and layering already used by the **Student** feature.

This plan is based on a read-only review of the whole solution.

---

## 2. Current architecture overview

### Projects

| Project | Responsibility |
|---|---|
| `schoolproject.api` | ASP.NET Core controllers, middleware, localization, DI composition |
| `schoolproject.core` | MediatR commands/queries, handlers, FluentValidation validators, DTOs, AutoMapper profiles, `ResponseHandler`, wrappers |
| `schoolproject.service` | Service abstractions and implementations called by handlers |
| `schoolproject.infrastructure` | `applicationdbcontext`, generic/specialized repositories, EF migrations |
| `schoolproject.data` | Entities, enums, metadata/route constants, shared localization base |

Reference direction:

```text
schoolproject.api -> schoolproject.core
schoolproject.core -> schoolproject.data / infrastructure / service
schoolproject.service -> schoolproject.data / infrastructure
schoolproject.infrastructure -> schoolproject.data
```

The API references only `schoolproject.core` directly.

### Request pipeline used by Student

```text
HTTP request
  -> appcontrollerbases / studentController
  -> MediatR command or query
  -> ValidationBehavior (FluentValidation validators)
  -> command/query handler (: ResponseHandler)
  -> service abstraction (istudentservice)
  -> specialized repository (istudentrepository : igenericrepo<Student>)
  -> applicationdbcontext
  -> Response<T> / PaginatedResult<T>
  -> NewResult<T>(Response<T>) in appcontrollerbases
  -> HTTP status code + JSON
```

Two important pipeline details:

- `ValidationBehavior` collects all failures but throws a `ValidationException` containing only the **first** error message; middleware maps it to HTTP 422.
- `appcontrollerbases.NewResult<T>` maps `Response<T>.StatusCode` to `Ok` / `Created` / `BadRequest` / `NotFound` / `UnprocessableEntity` / etc. List, specific-attribute, and paginated Student actions currently use `Ok(result)` instead, so their `Response<T>` metadata is bypassed.

Core DI is assembly-scanned in `schoolproject.core/modulecoredependencies.cs`:

```csharp
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
services.AddAutoMapper(Assembly.GetExecutingAssembly());
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
```

Therefore new handlers, validators, and mapping profiles in `schoolproject.core` are registered automatically. Only repository and service registrations must be added manually.

### Existing Student endpoints

| Verb | Route constant | Request |
|---|---|---|
| GET | `studentrouting.getstudentlist` | `getstudentlistquery` |
| GET | `studentrouting.getstudentbyarr` | `getspecificattributefromstdudent` |
| GET | `studentrouting.getstudentbyid` | `getstudentbyidquery` |
| GET | `studentrouting.getstudentpaginited` | `getstudentpaginatedlistquery` |
| POST | `studentrouting.addstd` | `addstudentcommand` |
| PUT | `studentrouting.editstd` | `editstudent` |
| DELETE | `studentrouting.deletestd` | `deletcommand` |

Routing caveat: Student action templates do not start with `/`, while base controller route is `[Route("api/[controller]")]`. This produces effective templates like `api/student/api/v1/student/...`. The same caveat will apply to Department/Subject unless the route constants are changed to leading `/`. See decision D4 below.

---

## 3. Current state: Department and Subject

Already present:

- Entities:
  - `schoolproject.data/entites/department.cs` → `DID`, `DNamear`, `DNameen`
  - `schoolproject.data/entites/subjects.cs` → `SubID`, `SubjectNamear`, `SubjectNameen`, `Period`
- `DbSet<Department> departments` and `DbSet<Subjects> subjects` in `applicationdbcontext.cs`
- EF migrations and snapshot already include localized columns and data transforms:
  - `20261127021817_secondcreate`
  - `20261127023300_MoveDepartmentSubjectDataToEnglish`
  - `20261127023326_TranslateDepartmentSubjectFields`
- Generic repository `igenericrepo<T>` / `genericrepo<T>`
- `ResponseHandler`, `Response<T>`, `PaginatedResult<T>`, `QueryableExtensions.ToPaginatedListAsync`
- Shared localization keys for generic messages (`Required`, `NotFound`, `IsExist`, `NotEmpty`, `MaxLengthis15`, `Success`, `Updated`, `Deleted`, `BadRequest`)

Missing:

- Specialized repositories and services
- DI registrations for those repositories/services
- Route constants and ordering enums
- Core `feature/department` and `feature/subject` folders
- Commands, queries, validators, DTOs, handlers
- AutoMapper profiles/mappings
- API controllers and endpoints

**No new EF migration is needed.**

---

## 4. Design decisions

### D1 — Keep the same layering as Student

Use:

```text
Controller -> MediatR -> Validator -> Handler -> Service -> Repository -> DbContext
```

Do not skip the service layer and do not inject repositories directly into handlers.

### D2 — Use specialized repositories

Create:

- `idepartmentrepository : igenericrepo<Department>`
- `isubjectrepository : igenericrepo<Subjects>`

with implementations `departmentrepository` / `subjectrepository` following `studentrepository`.

This keeps parity with Student and leaves room for `Include(...)` methods.

### D3 — Localize response names with `GeneralLocaliztionEntity.Localize`

Student mappings use:

```csharp
src.Localize(src.Nameen, src.Namear)
```

Department/Subject mappings should use:

```csharp
src.Localize(src.DNameen, src.DNamear)
src.Localize(src.SubjectNameen, src.SubjectNamear)
```

Commands should accept both `...en` and `...ar` fields, like Student.

### D4 — Routing: preserve Student style by default, but flag the double-prefix issue

Default plan: add `departmentrouting` / `subjectrouting` constants using the exact same shape as `studentrouting`.

Recommended cleanup, done deliberately and applied to all three controllers if approved:

```csharp
public const string rule = "/" + root + "/" + version + "/";
```

or prefix each action route constant with `/`. That gives intended URLs:

```text
api/v1/department/list
api/v1/subject/list
```

Do not silently fix only Department/Subject; that would make them inconsistent with Student.

### D5 — Preserve or fix the service result-string pattern consistently

Student services return literal strings such as `"added successrfully"` and handlers compare against them. Two options:

1. **Parity option:** use the same typo-style literals for Department/Subject and compare exactly.
2. **Cleanup option:** introduce an enum/constants for operation results and migrate Student, Department, and Subject together.

Default for this task: **parity** with Student to minimize scope. Record the cleanup as a follow-up.

### D6 — Delete-safety checks are required

`Student.DID -> Department` has no explicit cascade in the snapshot, so deleting a Department referenced by Students will fail.

Define service checks analogous to `checkondidusingid`:

- Department: block delete when students or department-subject join rows reference it
- Subject: block delete when student-subject or department-subject join rows reference it

Return `BadRequest` with an existing localized key (`BadRequest`) or add specific keys if desired.

### D7 — Validators use existing generic resource keys

No mandatory new resource keys are needed:

| Rule | Key |
|---|---|
| `NotEmpty` | `SharedResourcesKey.NotEmpty` |
| `NotNull` | `SharedResourcesKey.Required` |
| `MaximumLength(15)` | `SharedResourcesKey.MaxLengthis15` |
| duplicate name | `SharedResourcesKey.IsExist` |

If the project decides to allow longer Department/Subject names, add a `MaxLengthis500` key instead of copying `MaxLengthis15`.

### D8 — Response wrapping: choose parity or consistency

Student currently mixes two styles:

- Add/edit/delete/by-id use `NewResult(result)`.
- List, specific-attribute, and paginated use `Ok(result)`.

Default: mirror Student for exact parity. If improving consistency is allowed, use `NewResult` for all `Response<T>` endpoints and keep only `PaginatedResult<T>` as `Ok`.

### D9 — Add should return 201 if REST semantics are desired

Student add calls `Success(...)`, so it returns 200 even though `ResponseHandler.Created<T>` exists. Default: mirror Student. Recommended cleanup: use `Created<T>` for all three add endpoints together.

### D10 — Pagination must be deterministic and localization-safe

`studentservice.filterstudentpaginatedquerable` currently returns early when `search != null`, which skips ordering, and applies `Skip/Take` without a guaranteed `ORDER BY`.

For Department/Subject:

- always apply a stable final `OrderBy(x => x.DID)` / `OrderBy(x => x.SubID)` before pagination;
- apply search and ordering without early returns;
- do not call `Localize(...)` inside an EF `IQueryable` projection unless it can translate. Either project English columns like current Student or materialize first and localize in memory.

### D11 — Localized DTO mapping stays on the materialized path

`GeneralLocaliztionEntity.Localize` reads `Thread.CurrentThread.CurrentCulture`. It is safe in AutoMapper maps from already-loaded entities. For paginated queries, prefer:

```csharp
var page = await filtered.Select(e => new { e.DID, e.DNameen, e.DNamear })
                         .ToPaginatedListAsync(...);
var data = page.Data.Select(x => new getdepartmentpaginatedlist(
    x.DID, new Department().Localize(x.DNameen, x.DNamear))).ToList();
```

or a dedicated non-EF projection helper. Do not assume `Localize` translates to SQL.

### D12 — Culture fallback

`Program.cs` supports `en-US`, `de-DE`, `fr-FR`, and `ar-EG`, but only `en` and `ar` resx files exist and there is no neutral `SharedResources.resx`. `de-DE`/`fr-FR` fall back to key names. Either add resource files for those cultures, remove them from supported cultures, or accept the fallback explicitly.

---

## 5. Implementation phases

## Phase 1 — Infrastructure repositories

Create:

```text
schoolproject.infrastructure/abstract/idepartmentrepository.cs
schoolproject.infrastructure/abstract/isubjectrepository.cs
schoolproject.infrastructure/repositories/departmentrepository.cs
schoolproject.infrastructure/repositories/subjectrepository.cs
```

Signatures to mirror `istudentrepository`:

```csharp
public interface idepartmentrepository : igenericrepo<Department>
{
    Task<List<Department>> GetDepartmentsListAsync();
    Task<Department> GetDepartmentbyidtAsync(int id);
}

public interface isubjectrepository : igenericrepo<Subjects>
{
    Task<List<Subjects>> GetSubjectsListAsync();
    Task<Subjects> GetSubjectbyidtAsync(int id);
}
```

Implementations follow `studentrepository`: inject `applicationdbcontext`, expose `Set<T>()`, and use `Include(...)` only where needed.

Register in `schoolproject.infrastructure/moduleinfrastructuredependencies.cs`:

```csharp
services.AddTransient<idepartmentrepository, departmentrepository>();
services.AddTransient<isubjectrepository, subjectrepository>();
```

---

## Phase 2 — Services

Create:

```text
schoolproject.service/abstracts/idepartmentservice.cs
schoolproject.service/abstracts/isubjectservice.cs
schoolproject.service/immplemintaion/departmentservice.cs
schoolproject.service/immplemintaion/subjectservice.cs
```

Recommended Department methods:

```csharp
Task<List<Department>> GetDepartmentsListAsync();
Task<Department> GetDepartmentbyidAsync(int id);
Task<string> addsync(Department department);
Task<string> updatedepartment(Department department);
Task<string> deletedepartment(Department department);
Task<bool> nameexist(string name);
Task<bool> isnameisexistexcludeself(string name, int id);
Task<bool> cantdelete(int id);
IQueryable<Department> getdepartmentquerable();
IQueryable<Department> filterdepartmentpaginatedquerable(departmentorderingenum order, string search);
```

Recommended Subject methods:

```csharp
Task<List<Subjects>> GetSubjectsListAsync();
Task<Subjects> GetSubjectbyidAsync(int id);
Task<string> addsync(Subjects subject);
Task<string> updatesubject(Subjects subject);
Task<string> deletesubject(Subjects subject);
Task<bool> nameexist(string name);
Task<bool> isnameisexistexcludeself(string name, int id);
Task<bool> cantdelete(int id);
IQueryable<Subjects> getsubjectquerable();
IQueryable<Subjects> filtersubjectpaginatedquerable(subjectorderingenum order, string search);
```

Notes:

- `nameexist` in `studentservice` currently returns `true` when the name is **not** found. Decide whether to mirror that exact semantics or rename/fix all three services together. Default: mirror for parity, document it.
- Use the same transaction pattern as `studentservice.deletestudent`.
- Check both localized columns in name-existence queries.

Register in `schoolproject.service/moduleservicedependencies.cs`:

```csharp
services.AddTransient<idepartmentservice, departmentservice>();
services.AddTransient<isubjectservice, subjectservice>();
```

---

## Phase 3 — Route constants and order enums

Add to `schoolproject.data/appmetadata/router.cs`:

```csharp
public static class departmentrouting
{
    public const string prefix = rule + "department";
    public const string getdepartmentlist = prefix + "/list";
    public const string getdepartmentbyid = prefix + "/getbyid" + singleroute;
    public const string adddepartment = prefix + "/adddepartment";
    public const string editdepartment = prefix + "/editdepartment";
    public const string deletedepartment = prefix + "/deletedepartment" + singleroute;
    public const string getdepartmentpaginited = prefix + "/getdepartmentpaginited";
}

public static class subjectrouting
{
    public const string prefix = rule + "subject";
    public const string getsubjectlist = prefix + "/list";
    public const string getsubjectbyid = prefix + "/getbyid" + singleroute;
    public const string addsubject = prefix + "/addsubject";
    public const string editsubject = prefix + "/editsubject";
    public const string deletesubject = prefix + "/deletesubject" + singleroute;
    public const string getsubjectpaginited = prefix + "/getsubjectpaginited";
}
```

Add enums beside `schoolproject.data/helpers/studentorderingenum.cs`:

```csharp
public enum departmentorderingenum { DID, Name }
public enum subjectorderingenum { SubID, Name, Period }
```

Apply D4 before finalizing route constants.

---

## Phase 4 — Core CQRS features

Create these folder trees.

### Department

```text
schoolproject.core/feature/department/
  command/
    handel/departmentcommandhandler.cs
    model/adddepartmentcommand.cs
    model/editdepartment.cs
    model/deletedepartmentcommand.cs
    validation/adddepartmentvalidator.cs
    validation/editdepartmentvalidator.cs
    validation/deletedepartmentvalidator.cs
  query/
    handel/departmenthandler.cs
    model/getdepartmentlistquery.cs
    model/getdepartmentbyidquery.cs
    model/getdepartmentpaginatedlistquery.cs
    response/getsingledepartment.cs
    response/getdepartmentpaginatedlist.cs
```

### Subject

```text
schoolproject.core/feature/subject/
  command/
    handel/subjectcommandhandler.cs
    model/addsubjectcommand.cs
    model/editsubject.cs
    model/deletesubjectcommand.cs
    validation/addsubjectvalidator.cs
    validation/editsubjectvalidator.cs
    validation/deletesubjectvalidator.cs
  query/
    handel/subjecthandler.cs
    model/getsubjectlistquery.cs
    model/getsubjectbyidquery.cs
    model/getsubjectpaginatedlistquery.cs
    response/getsinglesubject.cs
    response/getsubjectpaginatedlist.cs
```

### Command model fields

Department add/edit:

```csharp
public int DID { get; set; }          // edit only
public string DNameen { get; set; }
public string DNamear { get; set; }
```

Subject add/edit:

```csharp
public int SubID { get; set; }        // edit only
public string SubjectNameen { get; set; }
public string SubjectNamear { get; set; }
public DateTime Period { get; set; }
```

Delete commands only carry the id, like `deletcommand`.

### Handlers

- Command handlers derive from `ResponseHandler` and inject `IStringLocalizer<SharedResources>` through `base(localizer)`.
- Query handlers derive from `ResponseHandler` where they return `Response<T>`.
- Use `Success`, `Updated`, `Deleted`, `NotFound`, `BadRequest` from `ResponseHandler`.
- For edit, map onto the tracked entity with `_mapper.Map(request, entity)` like Student.
- Paginated handlers project directly with `ToPaginatedListAsync`, using localized names.

Paginated projection (D10/D11): project raw localized columns in the EF query, then localize in memory:

```csharp
// Department
var page = await filtered
    .Select(e => new { e.DID, e.DNameen, e.DNamear })
    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

var data = page.Data
    .Select(e => new getdepartmentpaginatedlist(
        e.DID,
        new Department().Localize(e.DNameen, e.DNamear)))
    .ToList();
```

For Subject, also project `e.Period`. Add a stable final `OrderBy(e => e.DID)` / `OrderBy(e => e.SubID)` before `ToPaginatedListAsync`.

Entity-to-DTO AutoMapper maps for by-id/list-over-DTO endpoints remain safe because they operate on materialized entities:

```csharp
src.Localize(src.DNameen, src.DNamear)
src.Localize(src.SubjectNameen, src.SubjectNamear)
```

### Validators

- Use `AbstractValidator<T>`.
- Inject the relevant service and `IStringLocalizer<SharedResources>`.
- Validate both `...en` and `...ar` fields.
- Use `MustAsync` with `nameexist` / `isnameisexistexcludeself`.
- Delete validators use the `cantdelete` check.
- Replace all hardcoded messages with `_localizer[SharedResourcesKey.X]`.

---

## Phase 5 — AutoMapper profiles

Create:

```text
schoolproject.core/mapping/departmentmapping/departmentprofile.cs
schoolproject.core/mapping/departmentmapping/commandmapping/adddepartmentmapping.cs
schoolproject.core/mapping/departmentmapping/commandmapping/editdepartmentmapping.cs
schoolproject.core/mapping/departmentmapping/commandmapping/deletedepartmentmapping.cs
schoolproject.core/mapping/departmentmapping/querymapping/getdepartmentbyidmapping.cs

schoolproject.core/mapping/subjectmapping/subjectprofile.cs
schoolproject.core/mapping/subjectmapping/commandmapping/addsubjectmapping.cs
schoolproject.core/mapping/subjectmapping/commandmapping/editsubjectmapping.cs
schoolproject.core/mapping/subjectmapping/commandmapping/deletesubjectmapping.cs
schoolproject.core/mapping/subjectmapping/querymapping/getsubjectbyidmapping.cs
```

Rules:

- `partial class departmentprofile : Profile`, `partial class subjectprofile : Profile`
- Constructor calls each partial mapping method, like `studentprofile`.
- Command→entity maps request id to `DID` / `SubID`.
- Entity→DTO maps use `Localize(...)`.
- No DI change needed because of assembly scanning.

---

## Phase 6 — API controllers

Create:

```text
schoolproject.api/Controllers/departmentController.cs
schoolproject.api/Controllers/subjectController.cs
```

Both derive from `appcontrollerbases` and use `IMediator` via the base-class service locator:

```csharp
private IMediator _mediatorInstance;
protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
```

Default: mirror Student exactly (see D8/D9). Recommended cleanup: use `NewResult` for every `Response<T>` endpoint and `Created<T>` for add.

### Department endpoints

| Verb | Route constant | Request | Result helper |
|---|---|---|---|
| GET | `getdepartmentlist` | `getdepartmentlistquery` | `Ok` |
| GET | `getdepartmentbyid` | `getdepartmentbyidquery(id)` | `NewResult` |
| GET | `getdepartmentpaginited` | `getdepartmentpaginatedlistquery` | `Ok` |
| POST | `adddepartment` | `adddepartmentcommand` | `NewResult` |
| PUT | `editdepartment` | `editdepartment` | `NewResult` |
| DELETE | `deletedepartment` | `deletedepartmentcommand(id)` | `NewResult` |

### Subject endpoints

| Verb | Route constant | Request | Result helper |
|---|---|---|---|
| GET | `getsubjectlist` | `getsubjectlistquery` | `Ok` |
| GET | `getsubjectbyid` | `getsubjectbyidquery(id)` | `NewResult` |
| GET | `getsubjectpaginited` | `getsubjectpaginatedlistquery` | `Ok` |
| POST | `addsubject` | `addsubjectcommand` | `NewResult` |
| PUT | `editsubject` | `editsubject` | `NewResult` |
| DELETE | `deletesubject` | `deletesubjectcommand(id)` | `NewResult` |

Optional Student parity item: add a `listwithanotherattribute` query for Department/Subject if the specific-attribute projection is required.

---

## Phase 7 — Localization/resources

Existing generic keys are enough for a direct Student-style implementation.

Optional additions if more precise messages are wanted:

- `MaxLengthis500`
- `DepartmentIsUsed`
- `SubjectIsUsed`
- `DepartmentNotFound`
- `SubjectNotFound`

If added, update all four files:

```text
schoolproject.core/resources/SharedResourcesKey.cs
schoolproject.core/resources/SharedResources.en.resx
schoolproject.core/resources/SharedResources.ar.resx
schoolproject.core/resources/SharedResources.en.Designer.cs
schoolproject.core/resources/SharedResources.ar.Designer.cs
```

Then regenerate designers via `ResXFileCodeGenerator` or the existing generation script.

---

## 6. File-by-file checklist

### Infrastructure

- [x] `abstract/idepartmentrepository.cs`
- [x] `abstract/isubjectrepository.cs`
- [x] `repositories/departmentrepository.cs`
- [x] `repositories/subjectrepository.cs`
- [x] `moduleinfrastructuredependencies.cs` updated

### Service

- [x] `abstracts/idepartmentservice.cs`
- [x] `abstracts/isubjectservice.cs`
- [x] `immplemintaion/departmentservice.cs`
- [x] `immplemintaion/subjectservice.cs`
- [x] `moduleservicedependencies.cs` updated

### Data

- [x] `appmetadata/router.cs` updated
- [x] `helpers/departmentorderingenum.cs`
- [x] `helpers/subjectorderingenum.cs`

### Core — Department

- [x] all `feature/department/command/**` files
- [x] all `feature/department/query/**` files
- [x] all `mapping/departmentmapping/**` files

### Core — Subject

- [x] all `feature/subject/command/**` files
- [x] all `feature/subject/query/**` files
- [x] all `mapping/subjectmapping/**` files

### API

- [x] `Controllers/departmentController.cs`
- [x] `Controllers/subjectController.cs`

### Optional cleanup

- [ ] Decide D4 route-prefix fix for all controllers
- [ ] Decide D5 result-string cleanup for all services/handlers
- [ ] Add specific resource keys if generic keys are insufficient

---

## 7. Verification plan

### Build

Use the .NET 10 SDK already installed at `$env:USERPROFILE\.dotnet\dotnet.exe`:

```powershell
$env:PATH = "$env:USERPROFILE\.dotnet;$env:PATH"
dotnet build schoolproject.slnx --no-restore
```

### EF

No migration should be generated for this feature:

```powershell
dotnet ef migrations list --project schoolproject.infrastructure --startup-project schoolproject.api
```

Expected: the latest migration remains `20261127023326_TranslateDepartmentSubjectFields` (or newer only if intentionally added).

### Endpoint smoke tests

For each of Department and Subject:

1. POST add with both `...en` and `...ar` names → `201`/success response
2. POST duplicate name → validation error via `ValidationBehavior`
3. GET list → returns localized names
4. GET by id → returns localized name
5. GET paginated with `PageNumber`, `PageSize`, `orderby`, `search`
6. PUT edit, including exclude-self duplicate check
7. DELETE unused row → success
8. DELETE referenced row → `BadRequest` from `cantdelete`
9. Repeat with `Accept-Language: ar` and `en-US` to verify localized messages and names

### Localization checks

- No hardcoded `WithMessage("...")` remains in new validators
- No hardcoded response `Message = "..."` remains in new handlers
- Both `.resx` files remain valid XML with matching key counts
- Designer classes match `.resx` keys
- `Accept-Language: de-DE` / `fr-FR` behavior is explicitly accepted (see D12)
- Validation failures surface only the first error (matching `ValidationBehavior`) and return 422
- Paginated results are stable across repeated calls with identical parameters

---

## 8. Known quirks / risks

1. **Route double-prefix** — existing Student routes likely resolve as `api/student/api/v1/student/...`. Copying blindly reproduces the issue.
2. **Inverted `nameexist` semantics** — Student currently returns `true` when the name is free. Mirror deliberately or fix all services together.
3. **Typo result strings** — `"added successrfully"`, `"updated successrfully"`, `"deleted successrfully"`. Mirror or refactor consistently.
4. **Delete constraints** — Department deletion can fail due to `Student.DID` no-action FK; Subject deletion cascades join rows but still needs a clear rule.
5. **`Period` type** — `Subjects.Period` is `DateTime`; keep it in add/edit DTOs and mapping.
6. **Connection string** — `appsettings.json` has an empty `ConnectionStrings` object; local runs currently rely on user secrets/environment variables.
7. **No unit of work** — transactions are started directly from the generic repository, matching Student.
8. **Mixed response wrapping** — some Student actions bypass `NewResult`; some handlers hardcode messages that `ResponseHandler` already localizes.
9. **Add returns 200** — `Created<T>` exists but is unused by the Student add flow.
10. **List leaks entities** — `getstudentlistquery` returns raw `Student` with both `Nameen` and `Namear`; prefer localized DTOs for new features if parity is not required.
11. **Search disables ordering** — the Student filter returns early on `search`, so `orderby` is ignored and pages are nondeterministic.
12. **`Localize` in EF projections** — cannot be assumed to translate to SQL; use the in-memory pattern in D11.
13. **Unsupported culture resources** — `de-DE` and `fr-FR` are configured but have no `.resx` files.
14. **Validation message detail** — only the first validation error is returned; `Response<T>.Errors` is never populated.
15. **Middleware catch-all overwrites `ApiException`** — that special branch is currently dead; error JSON is PascalCase while success JSON is camelCase.
16. **Column vs validator mismatch** — English name/address columns are `nvarchar(max)` while validators cap input at 15 characters; Department/Subject Arabic columns are `nvarchar(500)`.

---

## 9. Definition of done

- Department and Subject have working list, by-id, paginated, add, edit, and delete endpoints
- Requests/responses and validation follow Student conventions
- Names are returned using `Localize(en, ar)`
- All validation and response messages use `IStringLocalizer<SharedResources>`
- Repositories and services are registered in DI
- Build succeeds with 0 errors
- No EF migration is generated solely for this feature
- Localized endpoint smoke tests pass for English and Arabic
