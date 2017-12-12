# EastBanc-Test-Assignment
Test assignment from EastBanc Technology

---
### Task
Need to implement a web application that can solve the problem of knapsack.
Can be done by brute force

---
### UI
The UI application should consist of the following pages:

* The "List of running tasks" page, which contains the columns:
  * The name of the task (the link to the page of details if status "Complete")
  * Maximum item price (if the task is in status "Complete")
  * Percentage of complete (if the task is in status "In Progress")
  * Status "In Progress" / "Complete"
  * Action "Delete" (should interrupt the running task)
  * Action "New Task"
* Page with the form of creating a new task:
  * Task name
  * Capacity of backpack
  * Table with the name, price and weight of objects
* Page, with details of the completed task:
  * Task name
  * Capacity of backpack
  * Table with the name, price and weight of objects
  * Maximum price
  * Calculation time  
  
---
### IMPORTANT

1. It is necessary to start the calculation of several tasks simultaneously
2. The solution should display the progress of the task in percent
3. The calculation should be able to "survive" the launch of the web server under which it operates, i.e. When restarting the process should continue from the place where it stopped

---
### TECHNICAL CONDITIONS

1. The solution should be Visual Studio (2015) Solution
2. The solution must be run under IIS Express
3. The solution should be implemented either using ASP.NET MVC, or in the form of SPA, using one of their technologies: Angular 1, Angular 2, Knockout, or React.



# Solution
---
Task done by using ASP.NET MVC 5 with 3x layer architecture (DAL, BLL, UI).
For running tasks simultaneously **QueueBackgroundWorkItem** is used to run task in the background, independent of any request. Also it give you ability to save task progress by delaying AppDomain shutdown.
For saving tasks **EF6** code first is used.
Hadling task progress and client notification is done by **signalR**.
Deleting done perfome by **Jquery AJAX** call and wepApi end point.
Add simple validation in creating new task form.

List of libraries\technology used:
* ASP.NET MVC 5
* Entity Framework 6 (Code First)
* SignalR
* Automapper
* Jquery
* Jquery.validation
