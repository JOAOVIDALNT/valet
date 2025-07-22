# VALET

Valet is a simple library designed to help developers streamline their application setup. It consists of two main modules:

## INTRODUCTION

Valet simplifies authentication and data persistence in .NET applications by providing pre-configured tools for repositories, unit of work, and authentication management. It reduces boilerplate code, enforces best practices, and integrates seamlessly with Entity Framework Core.

## INSTALLATION

To install Valet, add the NuGet package:

```sh
Install-Package valet
```

Or via .NET CLI:

```sh
dotnet add package valet
```

🔗 **NuGet Package:** [Valet on NuGet](https://www.nuget.org/packages/valet/)


## VALET.CORE

Valet Core provides three key features:

### Generic Repository and Unit of Work
Developers can extend the generic repository to access common repository operations. A Unit of Work repository is also available, pre-configured with a commit operation.

### Base Entity
A default base entity is provided for extension, including essential properties for any entity. This base entity can be utilized within your application, just as it is in all entities of the "Auth" module.

### Base Exception
An abstract base class is also included, designed to simplify the creation of custom exceptions. It extends System.Exception and provides a clean structure for handling exception details.

## VALET.AUTH

Valet Auth offers a comprehensive authentication toolkit, including the following features:

### Entities
Three entities are provided: `User`, `Role`, and `UserRole`. These entities can be extended and customized as needed.

### AuthDbContext
An authentication database context that can be extended, with pre-configured user logic. The AuthDbContext came with our authentication schema pattern set, for `User`, `Role` and `UserRole`. All those with mapping configured.

### Password Hasher
A password hasher that securely encrypts user passwords using BCrypt.

### Token Managment
A JWT complete token managment that includes generation of tokens and claims for `User` and validation through `Attribute` on controllers.

## TECHNICAL SPECIFICATIONS

### Configuration Setup
All necessary configuration options for using Valet's auth module are provided in the configuration module.

#### Example Usage
Below is an example of how to configure Valet services in your application:

```csharp
builder.Services.AddValet<AppDbContext>(builder.Configuration, options => 
{
  options.EnableAuthRepositories = true;
  options.EnablePasswordHasher = true;
  options.EnableTokenJwtManagment = true;
  options.EnableValetSwaggerGen = true;
})
```
You can decide how features you want to use or not. If you don't want to use any feature you don't need to pass options or any specific feature inside.


### DbContext Setup
If you want to use our authentication feature your local `DbContext` must extend `AuthDbContext`:

```csharp
public class AppDbContext(DbContextOptions options) : AuthDbContext(options)
```

#### OnModelCreating
To enable Valet's features, such as entity mapping for authentication module classes, override `OnModelCreating` and call the base implementation:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

After applying the base configurations, you can add your custom configurations, including new mappings. Note that `ApplyConfigurationsFromAssembly` automatically applies configuration mappings.

#### Customizing DbContext
You can add new `DbSet` properties to your `DbContext` and override existing ones when extending Valet's classes:

```csharp
public DbSet<Wallet> Wallets { get; set; }
public DbSet<Transaction> Transactions { get; set; }
public DbSet<Recurrency> Recurrencies { get; set; }
```

Example of extending `User` with additional properties:

```csharp
using valet.lib.Auth.Domain.Entities;

public class LocalUser : User
{
  public int Age { get; set; };
}
```

```csharp
public DbSet<LocalUser> LocalUsers { get; set; }
```

### Configuration Settings
For jwt managment setup, define your secret key and expiration time of token using the following pattern:

```json
"Settings": {
  "jwt": {
    "Secret": "YOURSECRET",
    "ExpirationMinutes": 60
  }
}
```
This allows Valet to access your secret key through `builder.Configuration`.

### Features Usage
Some examples of how to use our features:

#### CORE
##### BaseEntity
```csharp
using valet.lib.Core.Domain.Entities;

public class Wallet : BaseEntity
{
  public decimal Balance {get; set;}
  public Guid UserId {get; set;}
  public virtual ICollection<Transaction> Transactions { get; set; }
}
```
Extend BaseEntity with three default properties: (Guid) Id, (DateTime) CreatedAt, (DateTime) UpdatedAt.
All those properties are `private set` and only `UpdatedAt` could be changed through `Touch()` method, wich is protected and could be accessible only into the entity that extends BaseEntity.

##### BaseException
```csharp
using valet.lib.Core.Exception;

public class InsufficientFundsException : BaseException
{
    public InsufficientFundsException() : base("Insuficient funds for this operation.") { }

    public override IList<string> GetErrorMessages() => [Message];
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}
```
BaseException is a abstract class that extends SystemException and define two methods that will help you to handle exceptions, you can extend and customize your exception class as you want, in cases where there's a list of exceptions, you can receive at constructor and pass `string.Empty` to base constructor.

##### Generic Repository
You can extends valet generic repository, set your class repository and add methods with these two steps:

```csharp
using valet.lib.Core.Domain.Interfaces;

public interface IWalletRepository : IRepository<Wallet>
{
    Task<bool> WalletBelongsToUser(Guid walletId, Guid userId);
    Task<decimal> GetWalletBalance(Guid walletId);
    Task<decimal> GetTotalBalanceForUser(Guid userId);
}
```

```csharp
using valet.lib.Core.Data.Repositories;

public class WalletRepository(AppDbContext db) :  Repository<Wallet>(db), IWalletRepository
{
    public async Task<decimal> GetWalletBalance(Guid walletId) => ...
    public async Task<bool> WalletBelongsToUser(Guid walletId, Guid userId) => ..
    public async Task<decimal> GetTotalBalanceForUser(Guid userId) => ...
    
}
```
That's the example with primary constructor, notice that we extend our Interface previously configured and pass our dbcontext to valet Generic Repository implementation, wich has the main operations: `GetAllAsync` (with filter and pagination option), `GetAsync` (with filter and track option), `CreateAsync`, `Delete`, `Update`.

##### Unit of Work
This feature is configured by default on `AddValet` to service collection and is intended to give you the responsability on commiting changes to the databse. So always remember to use when inserting or updating.

```csharp
public class ExampleService
{
  private readonly IUnitOfWork _uow;
  private readonly IExampleRepository _repo;

  public ExampleService(IUnitOfWork uow, IExampleRepository repo)
  {
    _uow = uow;
    _repo = repo;
  }

  public async Task Execute(ExampleRequest req)
  {
    ...
    await _repo.CreateAsync(entity);
    // any other db change
    await _uow.CommitAsync();
  }
}
```
This logic allow you to create or update more than one propertie in databse per transaction and give you freedom to use strategically the `SaveChanges()`.
Note that our unit of work isn't a IDisposable, 'cause it's intended to use on api request lifecycle and doesn't need to handle dispose.

##### Error Response
This class is intended to be used directly as your default error response for http calls. Theres an example on how to use on a ExceptionFilter:

```csharp
using valet.lib.Core.Exception;
using valet.lib.Core.Exception.Response;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseException baseException)
            HandleProjectException(context, baseException);
        else
            HandleUnknowException(context);       
    }

    private void HandleProjectException(ExceptionContext context, BaseException baseException)
    {
        context.HttpContext.Response.StatusCode = (int)baseException.GetStatusCode();
        context.Result = new ObjectResult(new ErrorResponse(baseException.GetErrorMessages()));
    }

    ...
}
```

It's also common to specify in reponse type attributes on controller:
```csharp
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
```
#### AUTH

##### Password Hasher
A simple feature that aim's to abstract BCrypt hashing with salt. Here we have two main methods:

```csharp
string HashPassword(string password);
bool VerifyPassword(string givenPassword, string storedPassword);
```
the `HashPassword` to encrypt the parameter given string, and the `VerifyPassword` to validate if stored pass matches with the given one.
This operation is intended to be completed always using DI accessing the hasher by his interface.

##### Token Managment
This section provides fully token jwt managment, since generation through validation by token and validate user extracting token from `HttpContext`.

There's and example of an login logic with token generation:

```csharp
public UserLoginService(IUserLoginValidation loginValidation, IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
{
    _loginValidation = loginValidation;
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _tokenGenerator = tokenGenerator;
}
public async Task<UserLoginResponse> Login(UserLoginRequest request)
{
    await ValidateAsync(request);

    var user = await _userRepository.GetAsync(x => x.Email == request.Email);

    if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
        throw new InvalidLoginException();

    var token = _tokenGenerator.GenerateToken(user);

    return new UserLoginResponse(token);
}
```

Another key feature of token managment is our `ValidateUserAttribute` that can validate the access of users directly on controller.
You can use either on controller or individually at methods.

```csharp
using valet.lib.Auth.Service.Token.Middlewares;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    [ValidateUser]
    [HttpGet("example")]
    public async Task<IActionResult> GetExample() ...
}
```
Notice that we can use with roles as well:
```csharp
using valet.lib.Auth.Service.Token.Middlewares;


[ApiController]
[Route("[controller]")]
[ValidateUser]
public class TransactionController : ControllerBase
{
    [ValidateUser("admin")]
    [HttpPost("create")]
    public async Task<IActionResult> Create() ...
}
```
In these example we also use the attribute directly in controller, with this you have to be a valid user to access any method, and be a admin to create.

##### Auth Properties and Repostories
At last but as important, we have our auth repositories, so if you configure to use all these features our repositories implementation will be really useful for you.

We have default support methods on `UserRepository` and `RoleRepository` but feel free to create you local methods by extending valet's implementation.

```csharp
public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExists(string email);
    Task<bool> UserExists(Guid identifier);
    Task<User> GetUserWithRolesAsync(string email);
}
```

```csharp
public interface IRoleRepository : IRepository<Role>
{
    Task<bool> RoleExistsAsync(string name);
}
```
You can continue using `UserRepository` methods even with a custom `LocalUser` on your application for example, since this one extend valet one.


## API Reference

### Repositories
- **`UserRepository`** – Extends the generic repository with additional operations:
  - `UserExists(string email)`: Checks if a user with the given email exists.
  - `GetUserWithRoleAsync(string email)`: Retrieves a user along with their assigned roles.
- **`RoleRepository`** – Provides default repository operations plus:
  - `RoleExistsAsync(string name)`: Checks if a role with the given name exists.
- **`UserRoleRepository`** – Manages user-role assignments with standard repository operations.

### Interfaces
- `IRepository<T>` – Base interface for repositories
- `IUnitOfWork` – Manages transactions
- `IPasswordHasher` – Handles password hashing
- `ITokenGenerator` – Generates JWT tokens

### Classes
- `Repository<T>` – Implements repository operations
- `UnitOfWork` – Manages commits
- `PasswordHasher` – Implements password hashing
- `TokenGenerator` – Generates JWT 

### Dependency Injection
All these repositories are automatically registered in the Dependency Injection container when you configure `AddValet()` with true for `UseAuthentication`. If you set it to false, only the UnitOfWork will be available in the DI container.

Similarly, other modules can be added individually to the DI container, such as `UsePasswordHasher()`.

### Database Commit
Nothing in the repositories uses EF Core’s `SaveChanges()` method to commit database operations. The responsibility of committing changes lies with the user, providing greater flexibility to manage database transactions.

With this pattern, operations do not automatically force a database commit, allowing users to control when changes are persisted.

## FAQs & Troubleshooting

### Why is my token not being generated?
Ensure you have configured the secret key correctly in `appsettings.json`.

### How can I override default User properties?
Extend the `User` entity and update your `DbContext` to use the new class.

### Can I use Valet without Entity Framework?
Yes, but you must implement custom repository and `UnitOfWork` patterns. You can replace the default `GenericRepository<T>` and `UnitOfWork` with your own implementations that use another data access approach, such as Dapper or raw SQL queries.

### What if I forget to call Commit()?
Since repositories do not automatically call `SaveChanges()`, you must explicitly invoke `Commit()` on the UnitOfWork to persist changes. Failing to do so will result in data modifications not being saved to the database.

### Conclusion

Valet simplifies authentication and data management in .NET applications. With its generic repositories, authentication tools, and extensible architecture, it reduces development time and enforces best practices.

