# Steps to configure the service application

## Run BoldReports embed service application

1. Move to the csproject folder location `embedded-services\boldreportservice`
![Shows the csproject folder location](/readme/images/project-folder-location.png)

2. Open the command prompt from the csproject location.
![Shows the command prompt open location](/readme/images/command-prompt-location.png)

3. run the command `dotnet build` to build the project
![Shows the build command execution](/readme/images/build-command.png)

4. once the build succeed, run the command `dotnet run`
![Shows the run command execution](/readme/images/run-command.png)
 
5. Copy the localhost url from the console window and use it in the browser. When configuring the URL for the service, choose between `HTTP or HTTPS` based on your requirements
![Shows the service url in console](/readme/images/service-url.png)

6. Use this url in your front-end application as service url.
![Shows the service url configuration](/readme/images/service-url-configuration.png)

## Configure the designed report in service application

1. Move the designed report to the location `embedded-services\boldreportservice\wwwroot\Reports`
![Shows the report folder location](/readme/images/reports-folder-location.png)

2. Configure the report path in the front-end applications.
![Shows the report path configuration](/readme/images/report-path-configuration.png)

## Embed service application docker file location

You can find the docker file for the application in below paths.

For Windows - embedded-services\boldreportservice\Window_dockerfile</br>
For Linux   - embedded-services\boldreportservice\Linux_dockerfile
