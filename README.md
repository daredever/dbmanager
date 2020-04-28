# dbmanager

## Summary

Simple Database Manager with .Net Core 3.1, React and Bootstrap.
Tested on docker container with mssql/server:2017-latest-ubuntu image.

## Examples

To connect with Sql Server DB instance, enter connection string and press "Load data".

For example, 'Data Source=localhost;Initial Catalog=master;User Id=USER;Password=PASSWORD'.

Base layout:

![base](ui-base-layout.png)

Press "Create" to show "create table" script:

![createtable](ui-create-table.png)

## Build

Run command at sln folder:
1. 'docker build -f Infra/DbManager.Infra.WebApi/dockerfile .'
1. 'docker run -d -p 8080:80 [ContainerId]'

Open browser on page [http://localhost:8080/](http://localhost:8080/).



