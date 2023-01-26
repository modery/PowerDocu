# PowerDocu Installation & Usage

## Requirements

PowerDocu is running on .NET on Windows

## Download the latest Release
Download the latest release from (https://github.com/modery/PowerDocu/releases). Currently, two versions are provided as part of the releases:
1. The default app that requires [.NET 5.0 to be installed](https://dotnet.microsoft.com/download)
2. The standalone package that contains everything you need to run it (including .NET Core runtime and libraries).

Alternatively, you can also download the current source code and compile it (e.g. in Visual Studio Code).


## Use PowerDocu

Run **PowerDocu.GUI.exe** to launch PowerDocu. If you run it for the first time, or generally if you want to ensure that you have the latest set of connector icons, press the green download icon in the "Other Options" section to refresh your local copy of connector icons.

![PowerDocu GUI](Images/PowerDocu.GUI.png)


In the **Output Format** section, select your desired format (currently supported are Word and Markdown). For Word documents, you can also select a template to be used.
![PowerDocu GUI](Images/PowerDocu.GUI%20-%20Output%20Format%20Selection.png)

Under **Documentation Options** select if you want to document only those Canvas App properties that have been modified, or all properties (including those that are still set to their default values). "Canvas Apps: Document default values" allows you to document the default values for those properties that were modified:
![PowerDocu GUI](Images/PowerDocu.GUI%20-%20Documentation%20Options.png)

Click the "Next" Button to select the App(s), Flow(s), or Solution(s) to be documented. Once selected, PowerDocu will generate their respective documentation inside a named subfolder in the folder where the source files are located.
![PowerDocu GUI](Images/PowerDocu.GUI%20running.png)

## Export the Flow, App, or Solution you want to document
To generate documentation for a Flow, you need to export it from the web as a .ZIP package. 

1. Open your Flow's detail page
2. Select *Export Flow*
![Export Flow](Images/Export-Flow.png)
3. Provide a name for the file and export the package
![Export Flow](Images/Export-Flow-Package.png)

To generate documentation for a Power Apps canvas app, you need to export it from the web as a .msapp package or .ZIP solution package. 

1. Open your Power Apps canvas app
2. Select *File* and *Save As*, then choose *This Computer*
![Export app](Images/Export-App-Package.png)
3. The exported package will be downloaded to your computer

## Use a Word template
In the GUI, you can select a Word document (regular Word files with extension .docx, Macro-enabled Word documents with extension .docm, and Word templates with extension .dotx) to be used as the template for the generated documentation. The document that you select will not be modified in any way, but rather will be used as a template for any generated documentation as part of the current session.

PowerDocu simply appends the documented content as headings (H1 to H4), normal text, and tables. It is recommended to have the Heading styles available in your Word template.