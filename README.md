# This is a PetStore Client Application
In this application try to follow clean architecture and focus on separation of concern, exception handling and error logging.

PetStore has docs, src and test folders 

docs has ReadMe.file src has following ConsoleApp folder and Models folder

ConsoleApp include PetStore.ConsoleApp models to maintain, PetStore.Models C# Library project 

API - https://petstore.swagger.io/

---

##### What are the efforts to taken to make it indepentant and loosely couple manner
###### appsettings.json file defined configuration details
###### PetStoreApiClient introduced, later its injected in main method
###### OutputHandler for custom logics Print a list of available Pets to the console sorted in Categories and displayed in reverse orderby Name
###### <T> classes introduced to pass class time when its call
###### Unit Testing intriduced fail test case FetchAvailablePetsAsync_Failure with incorrect base url and success test case as FetchAvailablePetsAsync_Success 

----

##### further steps that I would take to ensure that this application is fit for the enterprise
######  1. Implement a global error handling middleware that catches unhandled exceptions and logs them centrally.
######  2. Consider using a more robust logging framework like Serilog or NLog.
######  3. Implement proper authentication and authorization mechanisms if required.
######  4. Implement rate limiting to protect this application and the external API from abuse.
######  5. in future Consider containerizing this application using Docker.
######  6. Set up a CI/CD pipeline to automate the build, testing, and deployment processes. Tools like Jenkins, Azure DevOps, or GitHub Actions can be used for this purpose.

---
##### ChatGPT Queries

###### 1. Injecting the class file  
###### 2. How to Write Unit Test for the API 
###### 3. How to access API from Nunit
###### 4. How to enahnce enterprice level application 
###### 5. How to get values from from appsettings.json
###### 6. how to access an API from console app 