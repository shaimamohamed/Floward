* ensure that multiple startup project of 3 following projects :
1) CatalogService.API project.
1) CatalogService.APIgateway project.
1) EmailService.Console project.

*Use SwaggerUI For Test the Catalog Service in Product Section
*  In the api project change database connection string in 
    the assessmentproject.API/appsettings.json file ,
 and in assessmentproject.Data/Database/AssessmentProjectDbDbContext File.

*  In the data project run Update-Database command to create the database
*  Database will be seeded with dummy data for test , 

so you can use below customer information for basic authentication:

* first customer :- username: CustomerA , password: password123
* second customer :- username: CustomerB , password: password123

*Change the default Email Address in EmailService.Console/appsetting.json file 

