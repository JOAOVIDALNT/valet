# VALET
Valet is a simple library designed to help developers on their application setup. There are 2 main modules to use in your applications.

### VALET.CORE
Valet Core brings you 2 main features:

#### GENERIC REPOSITORY AND UNIT OF WORK
Developers can extend our generic repository to have all of the most common repository operations. There's also a Unit of Work repository already configured and with only a commit operation.

#### BASE ENTITY
A default base entity to be extended, providing key properties for any entity. Developers can use inside your own application like we use on all entities of our "Auth" module.

### VALET.AUTH
Valet Auth brings you a full authentication tool set, displaying all these features:

#### PASSWORD HASHER
A password hasher to encrypt Users passwords using BCrypt.

#### TOKEN GENERATOR
A token generator that offers a JWT Token with relevant User information. 

#### CONTEXT
An AuthDbContext to be extended, with all User logic configured.

#### ENTITIES
There are 3 entities: User, Role and UserRole. All of those can be extended and customized for your best use.


## TECHNICAL ESPECIFICATION

### CONFIGURATION SETUP
All needed configuration to use the modules of valet are provided at our configuration module.

### EXAMPLE
Here's and example on how to fully configurate valet services on your application:

```csharp
builder.Services.AddValet<AppDbContext>(builder.Configuration, true)
    .UsePasswordHasher()
    .UseTokenGenerator(builder.Configuration)
    .UseValetJwt(builder.Configuration)
    .UseValetSwaggerGen();
```

### DbContext
It has to extend from valet AuthDbContext.

```csharp
public class AppDbContext(DbContextOptions options) : AuthDbContext(options)
```

### OnModelCreating
If you want all the features from valet, like entity mapping for Auth module classes. You should override OnModelCreating passing base OnModelCreating with the actual builder.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
```
Right after you can apply your own configuration, such as new mappings or stuff like that. Note that ApplyConfigurationFromAssembly already defines configuration from mappings.

You could also add new DbSet's on your DbContext, as well as you can override our DbSet when extending from our classes.
```csharp
public DbSet<Wallet> Wallets { get; set; }
public DbSet<Transaction> Transactions { get; set; }
public DbSet<Recurrency> Recurrencies { get; set; }
```
```csharp
public DbSet<LocalUser> LocalUsers { get; set; } // In case of adding relevant properties that will be persisted
```

### Builder Configuration
For configuration setup is very important to follow up an specific pattern for your secret key:
```json
"Settings": {
  "Secret":  "YOURSECRETKEY"
}
```
With that, our application can access your secret key through builder.Configuration.

