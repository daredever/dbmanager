# dbmanager

## Summary

Simple Database Manager with .Net Core 3.1, React and Bootstrap.
The main goal is to use fewer third-party nuget packages.

Solution designed with Onion architecture.

Tested on docker container with mssql/server:2017-latest image.

## Examples

To connect with Sql Server DB instance, enter connection string and press "Load data".

For example, 'Data Source=sql-server;Initial Catalog=master;User Id=sa;Password=16F74A6C-F89D-4306-BD13-C972A86DE213'.

Base layout:

![base](ui-base-layout.png)

Press "Create" to show "create table" script:

![createtable](ui-create-table.png)

## Build and Run

Run command at sln folder 'docker-compose up -d --build'.

Open browser on page [http://localhost:8080/](http://localhost:8080/).



