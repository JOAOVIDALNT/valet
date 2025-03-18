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

Valet Core provides two key features:

### Generic Repository and Unit of Work
Developers can extend the generic repository to access common repository operations. A Unit of Work repository is also available, pre-configured with a commit operation.

### Base Entity
A default base entity is provided for extension, including essential properties for any entity. This base entity can be utilized within your application, just as it is in all entities of the "Auth" module.

## VALET.AUTH

Valet Auth offers a comprehensive authentication toolkit, including the following features:

### Password Hasher
A password hasher that securely encrypts user passwords using BCrypt.

### Token Generator
A JWT token generator that includes relevant user information.

### AuthDbContext
An authentication database context that can be extended, with pre-configured user logic.

### Entities
Three entities are provided: `User`, `Role`, and `UserRole`. These entities can be extended and customized as needed.

## TECHNICAL SPECIFICATIONS

### Configuration Setup
All necessary configuration options for using Valet's modules are provided in the configuration module.

### Example Usage
Below is an example of how to configure Valet services in your application:

```csharp
builder.Services.AddValet<AppDbContext>(builder.Configuration, true)
    .UsePasswordHasher()
    .UseTokenGenerator(builder.Configuration)
    .UseValetJwt(builder.Configuration)
    .UseValetSwaggerGen();
```

### DbContext Setup
Your `DbContext` must extend `AuthDbContext`:

```csharp
public class AppDbContext(DbContextOptions options) : AuthDbContext(options)
```

### OnModelCreating
To enable Valet's features, such as entity mapping for authentication module classes, override `OnModelCreating` and call the base implementation:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```

After applying the base configurations, you can add your custom configurations, including new mappings. Note that `ApplyConfigurationsFromAssembly` automatically applies configuration mappings.

### Customizing DbContext
You can add new `DbSet` properties to your `DbContext` and override existing ones when extending Valet's classes:

```csharp
public DbSet<Wallet> Wallets { get; set; }
public DbSet<Transaction> Transactions { get; set; }
public DbSet<Recurrency> Recurrencies { get; set; }
```

Example of extending `User` with additional properties:

```csharp
public DbSet<LocalUser> LocalUsers { get; set; } // Adds new persistent properties
```

### Configuration Settings
For secure setup, define your secret key using the following pattern:

```json
"Settings": {
  "Secret": "YOURSECRETKEY"
}
```

This allows Valet to access your secret key through `builder.Configuration`.

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
Since repositories do not automatically call SaveChanges(), you must explicitly invoke Commit() on the UnitOfWork to persist changes. Failing to do so will result in data modifications not being saved to the database.

### Conclusion

Valet simplifies authentication and data management in .NET applications. With its generic repositories, authentication tools, and extensible architecture, it reduces development time and enforces best practices.

