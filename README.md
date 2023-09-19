# This is a PetStore Client Application
In this application try to follow clean architecture and focus on separation of concern, exception handling and error logging.

PetStore has docs, src and test folders 

docs has ReadMe.file src has following ConsoleApp folder and Models folder

ConsoleApp include PetStore.ConsoleApp models to maintain, PetStore.Models C# Library project 

##### What are the efforts to taken to make it indepentant and loosely couple manner
###### appsettings.json file defined configuration details
###### PetStoreApiClient introduced, later its injected in main method
###### OutputHandler for custom logics Print a list of available Pets to the console sorted in Categories and displayed in reverse orderby Name
###### <T> classes introduced to pass class time when its call

##### further steps that I would take to ensure that this application is fit for the enterprise


##### ChatGPT Queries

###### 1. Injecting the class file  
###### 2. How to Write Unit Test for the API 
###### 2. How to Write Unit Test for the API 