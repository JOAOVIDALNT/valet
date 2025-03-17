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

