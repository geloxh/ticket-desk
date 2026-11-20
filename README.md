

```
ticket-desk/                                   (solution root)
├── TicketDesk.sln                             📦 dotnet new sln
├── .gitignore                                 📦 dotnet new gitignore
├── .editorconfig                              📦 dotnet new editorconfig
├── .env                                       ✅ Postgres password (never commit)
├── docker-compose.yml                         ✅ runs PostgreSQL
├── requests.http                              ✅ test calls (REST Client)
├── README.md                                  🔜 setup + runbook
├── .vscode/
│   ├── launch.json                            📦 Generate Assets for Build and Debug
│   └── tasks.json                             📦 same
│
├── src/
│   ├── TicketDesk.Shared/                     contracts shared with the desktop client
│   │   ├── TicketDesk.Shared.csproj           📦
│   │   ├── Tickets/
│   │   │   ├── TicketEnums.cs                 ✅ TicketStatus, TicketPriority
│   │   │   ├── CreateTicketRequest.cs         ✅
│   │   │   ├── TicketDto.cs                   ✅
│   │   │   ├── ChangeStatusRequest.cs         🔜
│   │   │   └── AssignTicketRequest.cs         🔜
│   │   ├── Comments/
│   │   │   ├── CommentDto.cs                  🔜
│   │   │   └── AddCommentRequest.cs           🔜
│   │   └── Users/
│   │       └── UserRole.cs                    ✅
│   │
│   └── TicketDesk.Server/                     the API
│       ├── TicketDesk.Server.csproj           📦 (packages listed inside)
│       ├── Program.cs                         ✅ composition root
│       ├── appsettings.json                   📦
│       ├── appsettings.Development.json       📦
│       ├── Dockerfile                         🔜 for deployment
│       ├── Properties/launchSettings.json     📦 ports
│       │
│       ├── Common/
│       │   ├── Result.cs                      ✅ success-or-error type
│       │   └── Extensions/                    🔜 move DI/security setup out of Program.cs
│       │       ├── ServiceCollectionExtensions.cs
│       │       └── WebApplicationExtensions.cs  (security headers, rate limiting)
│       │
│       ├── Infrastructure/                    technical plumbing
│       │   ├── Persistence/
│       │   │   ├── AppDbContext.cs            ✅
│       │   │   └── Migrations/                📦 dotnet ef migrations add
│       │   ├── Auth/
│       │   │   ├── ICurrentUser + CurrentUser.cs   ✅ resolves logged-in user
│       │   │   ├── DevAuthHandler.cs          ✅ development only
│       │   │   └── EntraAuthExtensions.cs     🔜 real JWT/Entra login
│       │   ├── Storage/                       🔜 attachment file storage
│       │   │   └── IFileStore.cs, LocalFileStore.cs
│       │   └── Email/                         🔜 notifications
│       │       └── IEmailSender.cs, SmtpEmailSender.cs
│       │
│       └── Features/                          one folder per business feature
│           ├── Tickets/
│           │   ├── Ticket.cs                  ✅ entity + status rules
│           │   ├── TicketAccess.cs            ✅ who can see what
│           │   ├── TicketService.cs           ✅ create / list / get
│           │   ├── TicketEndpoints.cs         ✅ HTTP routes
│           │   ├── TicketMappings.cs          ✅ entity to DTO
│           │   ├── CreateTicketValidator.cs   ✅
│           │   └── ChangeStatusValidator.cs   🔜
│           ├── Comments/                      🔜
│           │   ├── Comment.cs                 (IsInternal flag)
│           │   ├── CommentService.cs
│           │   ├── CommentEndpoints.cs
│           │   └── AddCommentValidator.cs
│           ├── Attachments/                   🔜
│           │   ├── Attachment.cs
│           │   ├── AttachmentService.cs
│           │   └── AttachmentEndpoints.cs
│           ├── Users/
│           │   ├── AppUser.cs                 ✅
│           │   └── UserEndpoints.cs           🔜 admin: list users, change role
│           └── Audit/
│               ├── AuditLog.cs                ✅
│               └── AuditService.cs            ✅
│
└── tests/
    └── TicketDesk.Server.Tests/
        ├── TicketDesk.Server.Tests.csproj     📦
        ├── Tickets/
        │   ├── TicketVisibilityTests.cs       🔜 first priority
        │   └── TicketStatusTests.cs           🔜
        └── Comments/
            └── InternalNoteVisibilityTests.cs 🔜
```