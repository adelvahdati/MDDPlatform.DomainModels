# MDDPlatform.DomainModels

1- Inside MDDPlatform.DomainModels.Api run this command to publish the project
dotnet publish -c Release -o app/publish

2- In the folder that contains your Dockerfile run this command to build your image
docker build -t domainmodelservice .


3- Run this command to create a container in a user-defined network
docker run -d --network mddplatform -p 5173:80 --name domainmodels-service domainmodelservice
