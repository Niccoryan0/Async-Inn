# Async Inn
Author: *Nicco Ryan*

---
## Overview
This web app currently exists to simulate database information regarding a hotel chain, specifically it's locations, rooms, and amenities. It showcases the creation of SQL databases in C#, including creating models, migrating the database, and seeding data. It also has controllers to set up an API for the data. It now also showcases Dependency Injection via abstraction of the three tables in Repositorys and Interfaces that the controller utilizes. Additionally, it utilizes Data Transfer Objects in order to control the flow of data to and from the user. The app also has a user database to store users using the .NET Identity framework and allow specific authorization on routes based on user roles.



---

Date: *7/20/2020*
![ERD Diagram](Assets/Async-Inn-ERD.png)
[Diagram Description](https://docs.google.com/document/d/1nppbXbjYCOY2yeuyXozYDU2KQVrGXcFB8_k19UAyZWs/edit)

---

### Change Log
1.6: *Added Authorization to Routes and Defined Roles* - 30 July 2020

1.5: *Added User account Database* - 28 July 2020

1.4: *Added DTOs* - 27 July 2020

1.3: *Added HotelRoom Joint Entry Table* - 24 July 2020

1.2: *Adding join tables* - 23 July 2020

1.1: *Applied dependency injection* - 22 July 2020

1.0: *Initial release* - 21 July 2020
