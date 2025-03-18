# VALET

Valet is a simple library designed to help developers streamline their application setup. It consists of two main modules:

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

