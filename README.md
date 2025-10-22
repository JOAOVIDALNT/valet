![alt text](valet_logo.png)

**Valet** is a lightweight library designed to streamline setup and configuration for .NET applications.

## INTRODUCTION

Valet simplifies authentication and data persistence in .NET by offering pre-configured tools for repositories, unit of work, and authentication management. It helps reduce boilerplate, enforces best practices, and integrates seamlessly with Entity Framework Core.

## INSTALLATION

Install via NuGet:

```sh
Install-Package valet
```

Or using the .NET CLI:

```sh
dotnet add package valet
```

🔗 **NuGet Package:** [Valet on NuGet](https://www.nuget.org/packages/valet/)

<br/>

## VALET.CORE

Valet Core module includes three key features:

### Generic Repository & Unit of Work

Extend the generic repository to access common data operations. The Unit of Work pattern is included to group transactions and commit changes with a single call.

### Base Entity

A base class providing default properties (`Id`, `CreatedAt`, `UpdatedAt`) for entity inheritance. These are used across all Auth entities and can be reused in your application.

### Base Exception

An abstract base class that extends `System.Exception`, designed for structured custom exception handling. It provides a clean foundation for building and managing domain-level exceptions.

<br/>

## VALET.AUTH

Valet Auth offers a complete set of tools for user authentication:

### Entities

Predefined entities: `User`, `Role`, and `UserRole`. These are fully extendable for your application needs.

### AuthDbContext

A specialized DbContext with pre-configured mappings and logic for user, role, and role-assignment entities. You can extend it just like a standard `DbContext`.

### Password Hasher

Provides secure password hashing using BCrypt, including hashing and verification utilities.

### Token Management

A complete JWT implementation for token creation and validation, including built-in controller attributes for user validation.

<br/>

## TECHNICAL SETUP

### Configuration

Configure Valet using the following example:

```csharp
builder.Services.AddValet<AppDbContext>(builder.Configuration, options => 
{
  options.EnableAuthRepositories = true;
  options.EnablePasswordHasher = true;
  options.EnableTokenJwtManagment = true;
  options.EnableValetSwaggerGen = true;
});
```

Omitting a configuration will exclude the respective feature — all are optional.

### DbContext Setup

If using authentication, your context should inherit from `AuthDbContext`:

```csharp
public class AppDbContext(DbContextOptions options) : AuthDbContext(options)
```

Then override `OnModelCreating`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

This ensures Valet's entity mappings are applied. You can still apply your own configurations as needed.

### Custom DbContext Properties

You're free to add custom `DbSet`s:

```csharp
public DbSet<Wallet> Wallets { get; set; }
public DbSet<Transaction> Transactions { get; set; }
```

Extend the `User` entity:

```csharp
public class LocalUser : User
{
  public int Age { get; set; }
}

public DbSet<LocalUser> LocalUsers { get; set; }
```

<br/>

### JWT Configuration

If you're intended to use token managment define your JWT settings in your `appsettings.json`:

```json
"Settings": {
  "jwt": {
    "Secret": "YOURSECRET",
    "ExpirationMinutes": 60
  }
}
```

<br/>


## FEATURE USAGE

### CORE

#### BaseEntity

```csharp
public class Wallet : BaseEntity
{
  public decimal Balance { get; set; }
  public Guid UserId { get; set; }
  public virtual ICollection<Transaction> Transactions { get; set; }
}
```

* `Id`, `CreatedAt`, and `UpdatedAt` are auto-handled.
* `UpdatedAt` can only be updated via the protected `Touch()` method.

#### BaseException

```csharp
public class InsufficientFundsException : BaseException
{
    public InsufficientFundsException() : base("Insufficient funds for this operation.") {}

    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}
```

Designed for clean and structured exception handling. You can customize exceptions and aggregate error messages.

#### Generic Repository

Create custom repository interfaces:

```csharp
public interface IWalletRepository : IRepository<Wallet>
{
    Task<bool> WalletBelongsToUser(Guid walletId, Guid userId);
}
```

And implement them:

```csharp
public class WalletRepository(AppDbContext db) : Repository<Wallet>(db), IWalletRepository
{
    public async Task<bool> WalletBelongsToUser(Guid walletId, Guid userId) => ...
}
```

Includes default operations:

* `GetAllAsync()`, `GetAsync()`
* `CreateAsync()`, `Update()`, `Delete()`

#### Unit of Work

Used to commit changes manually:

```csharp
await _repo.CreateAsync(entity);
await _otherRepo.Update(prop);
await _uow.CommitAsync();
```

Transactions are not automatically committed. You control the lifecycle.

#### Error Response

Recommended use in exception handling middleware or filters:

```csharp
public void OnException(ExceptionContext context)
{
    if (context.Exception is BaseException baseEx)
    {
      context.HttpContext.Response.StatusCode = (int)baseException.GetStatusCode();
      context.Result = new ObjectResult(new ErrorResponse(baseException.GetErrorMessages()));
    }
}
```

<br/>

### AUTH

#### Password Hasher

Use via DI:

```csharp
string hash = _hasher.HashPassword("secret");
bool valid = _hasher.VerifyPassword("input", storedHash);
```

#### Token Management

Example login logic:

```csharp
var user = await _userRepo.GetAsync(x => x.Email == request.Email);
if (!_hasher.VerifyPassword(request.Password, user.Password))
    throw new InvalidLoginException();

var token = _tokenGenerator.GenerateToken(user);
return new UserLoginResponse(token);
```

Use attribute-based access control:

```csharp
[ValidateUser] // Validates any authenticated user
[ValidateUser("admin")] // Validates specific roles
```
could be added either to controller class and individually to controller methods.

#### Auth Repositories

Built-in interfaces:

```csharp
public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserWithRolesAsync(string email);
}
```
Those could be also extended and incremented localy.

## API Reference

### Repositories

* **UserRepository**: Get user by email, check existence, get roles.
* **RoleRepository**: Check role existence.
* **UserRoleRepository**: Manage assignments.

### Interfaces

* `IRepository<T>` – Generic repository contract
* `IUnitOfWork` – Handles commit logic
* `IPasswordHasher`, `ITokenGenerator`

### Services

* `Repository<T>` – Concrete implementation
* `UnitOfWork` – Commit unit
* `PasswordHasher`, `TokenGenerator`

<br/>

## DEPENDENCY INJECTION

When calling `AddValet()`, you control what gets injected via options. Only enabled features will be added to the DI container.

<br/>

## DATABASE COMMIT

Valet repositories never call `SaveChanges()` internally. You must call `Commit()` via `IUnitOfWork`, giving you full transaction control.

<br/>

## FAQs

### Why isn't my token being generated?

Check your JWT configuration in `appsettings.json`.

### Can I override the `User` entity?

Yes — extend `User` and use your custom class in your `DbContext`.

### Can I use Valet without Entity Framework?

Yes, but you'll need to implement your own `Repository` and `UnitOfWork`.

### What if I forget to call `Commit()`?

Changes won't be persisted. Always call `Commit()` after modifying data.

<br/>

## CONCLUSION

Valet helps you build secure, scalable .NET applications with minimal boilerplate. From core infrastructure to full authentication, its modular design adapts to your needs while enforcing clean architecture principles.
