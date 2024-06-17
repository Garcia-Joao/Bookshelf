# BookShelf

## Introduction
Avaliation project to handle books, genres and authors.

## Settings
- The default database creation is made into the localhost SQL Server, to a custom connection string you can find the configuration file into __"Configuration/appsettings.json"__.
- Into the __"Presentation/app.xaml.cs"__ you can find a comment __"//InsertMockup(host)"__, if this section is not commented, the project will insert some mockup, books, genres and authors, everytime that the project is executed

## Features
- Visualize, create, remove, and edit books, genres and authors.

## Project Images
![Imgur](https://i.imgur.com/P9LGOQJ.png)
![Imgur](https://i.imgur.com/mcrGGKP.png)

## Project Structure
-The classes of the project are constructed at the app.xaml.cs, using the dependency injection logic.

![Imgur](https://i.imgur.com/mx6RRz2.jpg)

### Presentation Layer
![Imgur](https://i.imgur.com/CKflsXK.jpg)

### Domain Layer
![Imgur](https://i.imgur.com/TDigJsI.jpg)

### Infrastructure Layer
![Imgur](https://i.imgur.com/ts33hu0.jpg)
