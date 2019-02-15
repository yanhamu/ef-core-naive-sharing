# Naive Sharding using Entity Framework Core

- make sure to create databases
```sql
create database shard_0
go
create database shard_1
go
use shard_0
create table Customers(Id uniqueidentifier primary key)
go
use shard_1
create table Customers(Id uniqueidentifier primary key)
go
```

- make sure to update connection string template in appsettings.json 
- run
