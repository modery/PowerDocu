# PowerDocu Installation & Usage

Please review the steps below to start using **PowerDocu**. Also, please note that the current version has only been tested on Windows.

## Requirements

PowerDocu has been tested on Windows only. It is running on .NET 5.0

## Download the latest Release
Download the latest release from (https://github.com/modery/PowerDocu/releases). Currently, two versions are provided as part of the releases:
1. The default app that requires [.NET 5.0 to be installed](https://dotnet.microsoft.com/download)
2. The standalone package that contains everything you need to run it (including .NET Core runtime and libraries).

Alternatively, you can also download the current source code and compile it (e.g. in Visual Studio Code).

## Prepare the Connector Icons
By default, this solution contains some general information about the currently available connectors inside the **[Resources\ConnectorIcons\connectors.json](PowerDocu.Common\Resources\ConnectorIcons\connectors.json)** file. As there are regular updates to the set of connectors available, a script to get the latest set is provided. Running the script will regenerate the JSON file that contains the connector details, and it also downloads the connector icons (which will then be used inside the generated Word document).

To retrieve the latest list of connector details and icons, simply run the PowerShell script **GenerateConnectorMapping.ps1** inside the *Resources\ConnectorIcons* folder.
![Generate Connector Mappings](Images/GenerateConnectorMapping.png)

You do not need to run this script everytime before you generate documentation. After a first, initial run you would only need to run it again if there are new connectors available that you use inside your Flow.

## Export the Flow, App, or Solution you want to document
To generate documentation for a Flow, you need to export it from the web as a ZIP package. 

1. Open your Flow's detail page
2. Select *Export Flow*
![Export Flow](Images/Export-Flow.png)
3. Provide a name for the file and export the package
![Export Flow](Images/Export-Flow-Package.png)

To generate documentation for a Power Apps canvas app, you need to export it from the web as a .msapp package. 

1. Open your Power Apps canvas app
2. Select *File* and *Save As*, then choose *This Computer*
![Export app](Images/Export-App-Package.png)
3. The exported package will be downloaded to your computer

## Run PowerDocu FlowDocumenter & AppDocumenter

You can run FlowDocumenter & AppDocumenter by either using the PowerDocu.GUI Windows application, or the PowerDocu.FlowDocumenter/PowerDocu.AppDocumenter console application respectively. Both applications generate the same set of documentation, but using the PowerDocu.GUI application allows you to generate documentation for apps and Flows contained inside a solution in one run.

**Flow Package**

A new folder will be created within the current folder, using the exported Flow's name as folder name. Inside this folder you can find the generated diagrams as well as the generated documentation
![Email me with a list of upcoming Calendar events documentation](Images/PowerDocu.FlowDocumenter-generated-output.png)

**App Package**

A new folder will be created within the current folder, using the exported Apps's name as folder name. Inside this folder you can find the generated documentation
![Meeting capture docu](Images/PowerDocu.AppDocumenter-generated-output.png)

**Solution Package**

A folder with the Solution package file's name will be created, and each individual Flow and Power app will be processed and its generated files stored inside this folder
![audit components](Images/Solution-CoEStartKit-AuditComponents.png)

### Use the PowerDocu.GUI
1. Open the PowerDocu.GUI.exe application
2. Optionally, select a Word document (.docx) as template
3. Select a Flow, App or Solution package to havve it converted
![Execute FlowDocumenter](Images/PowerDocu.GUI.png)

### Run the PowerDocu.FlowDocumenter/PowerDocu.AppDocumenter console application
1. Open a command prompt or PowerShell window in the folder that contains FlowDocumenter and AppDocumenter
2. Execute FlowDocumenter/AppDocumenter and provide an exported Solution, Flow or App package as parameter. For example: *PowerDocu.FlowDocumenter.exe FlowPackage.zip*, or *PowerDocu.AppDocumenter.exe AppPackage.msapp*
3. To use a Word document (.docx) as template, use it as an additional parameter: *PowerDocu.FlowDocumenter.exe ExportedFlow.zip WordTemplate.docx*
![Execute FlowDocumenter](Images/PowerDocu.FlowDocumenter-run.png)

## Use custom Word style formatting

Inside the Resources folder, you can find a *styles.xml* file which provides the default styling options for the Headings used in the generated documentation. If wanted, you can replace it with your own version. If you have an existing Word document, open it as a ZIP archive (e.g. in 7-Zip), and extract the styles.xml found inside the folder *word*
![styles.xml inside Word document](Images/7zip-WordDocu-styles.xml.png)
