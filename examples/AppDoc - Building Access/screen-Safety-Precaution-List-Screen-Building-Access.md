# Power App Documentation \- Building Access

| Property                   | Value                                   |
| -------------------------- | --------------------------------------- |
| App Name                   | Building Access                         |
| App Logo                   | ![App Logo](resources/appLogoSmall.png) |
| Documentation generated at | Wednesday, 30 November 2022 10:20 am    |

- [Overview](index-Building-Access.md)
- [App Details](appdetails-Building-Access.md)
- [Variables](variables-Building-Access.md)
- [DataSources](datasources-Building-Access.md)
- [Resources](resources-Building-Access.md)
- [Controls](controls-Building-Access.md)

## Safety Precaution List Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| --------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | \/\/Get published safety precations if they havent already Clear(colSafetyPrecautions); If(IsEmpty(colSafetyPrecautions), ClearCollect(colSafetyPrecautions,Filter(BAR\_SafetyPrecautions,Status.Value \= "Published" ))); \/\/ClearCollect(colTranslation,{ReadMoreValue:varString.safetyPrecautionReadMore,UpdatedValue:varString.UpdatedLbl,SharedValue:varString.SharedLbl,AgoValue:varString.AgoLbl,yearLbl:varStringExt.yearLbl,monthLbl:varStringExt.monthLbl,dayLbl:varStringExt.dayLbl,hourLbl:varStringExt.hourLbl,minuteLbl:varStringExt.minuteLbl,secondLbl:varStringExt.secondLbl}); |

### Design

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| Height              | Max(App.Height, App.MinScreenHeight)                                                                                  |
| ImagePosition       | ImagePosition.Fit                                                                                                     |
| LoadingSpinner      | LoadingSpinner.None                                                                                                   |
| LoadingSpinnerColor | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| Orientation         | If(Self.Width \< Self.Height, Layout.Vertical, Layout.Horizontal)                                                     |
| Size                | 1 + CountRows(App.SizeBreakpoints) \- CountIf(App.SizeBreakpoints, Value \>\= Self.Width)                             |
| Width               | Max(App.Width, App.MinScreenWidth)                                                                                    |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property      | Value                                              |
| ------------- | -------------------------------------------------- |
| Child Control | shpRectangleBackGround\_SafetyPrecautionListScreen |
| Child Control | shpUpperRectangle\_SafetyPrecautionListScreen      |
| Child Control | HeaderControlSafetyPrecautions                     |
| Child Control | txtSearchBox                                       |
| Child Control | icnSearch                                          |
| Child Control | glrySafetyPrecautions.                             |

## galleryTemplate5\_3

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Behavior

| Property | Value                                                                                                                                                                                          |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ClearCollect(colSelectedRequests, ThisItem); Set(varSelectedRequest, First(colSelectedRequests)); Set(varSelectedRequest, LookUp(BAR\_Requests,AccessKey \= varSelectedRequest.AccessKey));    |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## glrySafetyPrecautions.

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                                                            |
| --------- | ---------------------------------------------------------------------------------------------------------------- |
| Items     | SortByColumns( Search( colSafetyPrecautions, txtSearchBox.Text, "Title", "Description" ), "Created",Descending ) |
| WrapCount | 1                                                                                                                |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 0                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Height                 | Parent.Height \- 250                                                         |
| Layout                 | Layout.Vertical                                                              |
| LoadingSpinner         | LoadingSpinner.None                                                          |
| LoadingSpinnerColor    | Self.BorderColor                                                             |
| ShowScrollbar          | false                                                                        |
| TemplatePadding        | 8                                                                            |
| TemplateSize           | 440                                                                          |
| Transition             | Transition.None                                                              |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-36, (App.DesignWidth\*2)\-36 ) |
| X                      | ((Parent.Width\-Self.Width) \/2)                                             |
| Y                      | txtSearchBox.Y+txtSearchBox.Height+16                                        |
| ZIndex                 | 3                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Child Control  | galleryTemplate5\_3           |
| Child Control  | shpRectangleBackground        |
| Child Control  | shpRectangleBackground\_1     |
| Child Control  | lblSafetyPrecautionTitle      |
| Child Control  | lblCountry                    |
| Child Control  | lblDescription                |
| Child Control  | htmlLblDescription            |
| Child Control  | imgClock                      |
| Child Control  | lblLastUpdated                |
| Child Control  | imgRightArrow                 |
| Child Control  | precautionSeparator           |
| Parent Control | Safety Precaution List Screen |

## HeaderControlSafetyPrecautions

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                            |
| ------------------- | -------------------------------- |
| backLabel           | "back"                           |
| IsBackButtonVisible | false                            |
| IsHomeButtonVisible | true                             |
| NavigateHomeScreen  | 'Home Screen'                    |
| NavigateScreen      | 'Home Screen'                    |
| Text                | varString.SafetyPrecautionsTitle |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width) \/2                                     |
| Y        | 0                                                                  |
| ZIndex   | 4                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Parent Control | Safety Precaution List Screen |

## htmlLblDescription

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                                             |
| -------- | --------------------------------------------------------------------------------------------------------------------------------- |
| HtmlText | \/\*If( Len(ThisItem.Description) \> 100, Left(ThisItem.Description, 100) & "...", ThisItem.Description)\*\/ ThisItem.Description |
| Tooltip  | ThisItem.Description                                                                                                              |

### Design

| Property        | Value                               |
| --------------- | ----------------------------------- |
| AutoHeight      | false                               |
| BorderStyle     | BorderStyle.Solid                   |
| BorderThickness | 2                                   |
| DisplayMode     | DisplayMode.Edit                    |
| Font            | Font.'Segoe UI'                     |
| Height          | 225                                 |
| PaddingLeft     | 16                                  |
| PaddingTop      | 0                                   |
| Size            | 22.5                                |
| Width           | shpRectangleBackground.Width\-36    |
| X               | 18                                  |
| Y               | lblCountry.Y + lblCountry.Height+12 |
| ZIndex          | 10                                  |

### Color Properties

| Property            | Value                                                                                                                  |
| ------------------- | ---------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table>  |
| Color               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>        |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>     |
| DisabledFill        | <table border="0"><tr><td>RGBA(119, 119, 119, .4)</td></tr><tr><td style="background-color:#777777"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 0)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>  |
| HoverBorderColor    | Self.BorderColor                                                                                                       |
| PressedBorderColor  | Self.BorderColor                                                                                                       |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## icnSearch

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Design

| Property               | Value                                             |
| ---------------------- | ------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                 |
| BorderThickness        | 0                                                 |
| DisplayMode            | DisplayMode.View                                  |
| FocusedBorderThickness | 4                                                 |
| Height                 | 42                                                |
| Icon                   | Icon.Search                                       |
| Rotation               | 0                                                 |
| Width                  | 42                                                |
| X                      | txtSearchBox.X+txtSearchBox.Width\-Self.Width\-12 |
| Y                      | txtSearchBox.Y+16                                 |
| ZIndex                 | 6                                                 |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 36, 1)</td></tr><tr><td style="background-color:#252424"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledColor       | <table border="0"><tr><td>RGBA(220, 220, 220, 1)</td></tr><tr><td style="background-color:#DCDCDC"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(37, 36, 36, 1)</td></tr><tr><td style="background-color:#252424"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Parent Control | Safety Precaution List Screen |

## imgClock

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value |
| -------- | ----- |
| Image    | clock |

### Design

| Property               | Value                                                |
| ---------------------- | ---------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                     |
| BorderThickness        | 2                                                    |
| DisplayMode            | DisplayMode.Edit                                     |
| FocusedBorderThickness | 4                                                    |
| Height                 | 48                                                   |
| ImagePosition          | ImagePosition.Fit                                    |
| ImageRotation          | ImageRotation.None                                   |
| PaddingBottom          | 0                                                    |
| PaddingLeft            | 0                                                    |
| PaddingRight           | 0                                                    |
| PaddingTop             | 0                                                    |
| RadiusBottomLeft       | 0                                                    |
| RadiusBottomRight      | 0                                                    |
| RadiusTopLeft          | 0                                                    |
| RadiusTopRight         | 0                                                    |
| Width                  | 48                                                   |
| X                      | 10                                                   |
| Y                      | htmlLblDescription.Y + htmlLblDescription.Height + 9 |
| ZIndex                 | 7                                                    |

### Color Properties

| Property            | Value                                                                                                           |
| ------------------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## imgRightArrow

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ClearCollect(colSelectedSafetyPrecation,ThisItem); Navigate('Safety Precaution Details Screen',ScreenTransition.None) |

### Data

| Property | Value |
| -------- | ----- |
| Image    | go    |

### Design

| Property               | Value                                                 |
| ---------------------- | ----------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                      |
| BorderThickness        | 2                                                     |
| DisplayMode            | DisplayMode.Edit                                      |
| FocusedBorderThickness | 4                                                     |
| Height                 | 32                                                    |
| ImagePosition          | ImagePosition.Fit                                     |
| ImageRotation          | ImageRotation.None                                    |
| PaddingBottom          | 0                                                     |
| PaddingLeft            | 0                                                     |
| PaddingRight           | 0                                                     |
| PaddingTop             | 0                                                     |
| RadiusBottomLeft       | 0                                                     |
| RadiusBottomRight      | 0                                                     |
| RadiusTopLeft          | 0                                                     |
| RadiusTopRight         | 0                                                     |
| TabIndex               | 0                                                     |
| Width                  | 32                                                    |
| X                      | shpRectangleBackground.Width \- Self.Width \- 20      |
| Y                      | htmlLblDescription.Y + htmlLblDescription.Height + 17 |
| ZIndex                 | 9                                                     |

### Color Properties

| Property            | Value                                                                                                           |
| ------------------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## lblCountry

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | ThisItem.Country |

### Design

| Property               | Value                                                          |
| ---------------------- | -------------------------------------------------------------- |
| Align                  | Align.Left                                                     |
| AutoHeight             | false                                                          |
| BorderStyle            | BorderStyle.None                                               |
| BorderThickness        | 2                                                              |
| DisplayMode            | DisplayMode.Edit                                               |
| FocusedBorderThickness | 4                                                              |
| Font                   | Font.'Segoe UI'                                                |
| FontWeight             | FontWeight.Normal                                              |
| Height                 | 40                                                             |
| Italic                 | false                                                          |
| LineHeight             | 1.2                                                            |
| Overflow               | Overflow.Hidden                                                |
| PaddingBottom          | 5                                                              |
| PaddingLeft            | 0                                                              |
| PaddingRight           | 5                                                              |
| PaddingTop             | 5                                                              |
| Size                   | 18                                                             |
| Strikethrough          | false                                                          |
| Underline              | false                                                          |
| VerticalAlign          | VerticalAlign.Middle                                           |
| Width                  | shpRectangleBackground.Width\-36                               |
| X                      | 18                                                             |
| Y                      | lblSafetyPrecautionTitle.X + lblSafetyPrecautionTitle.Height+8 |
| ZIndex                 | 4                                                              |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## lblDescription

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                              |
| -------- | -------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                           |
| Role     | TextRole.Default                                                                                   |
| Text     | If( Len(ThisItem.Description) \> 80, Left(ThisItem.Description, 80) & "...", ThisItem.Description) |
| Tooltip  | ThisItem.Description                                                                               |

### Design

| Property               | Value                               |
| ---------------------- | ----------------------------------- |
| Align                  | Align.Left                          |
| AutoHeight             | false                               |
| BorderStyle            | BorderStyle.None                    |
| BorderThickness        | 2                                   |
| DisplayMode            | DisplayMode.Edit                    |
| FocusedBorderThickness | 4                                   |
| Font                   | Font.'Segoe UI'                     |
| FontWeight             | FontWeight.Normal                   |
| Height                 | 80                                  |
| Italic                 | false                               |
| LineHeight             | 1.2                                 |
| Overflow               | Overflow.Hidden                     |
| PaddingBottom          | 5                                   |
| PaddingLeft            | 0                                   |
| PaddingRight           | 5                                   |
| PaddingTop             | 5                                   |
| Size                   | 22.5                                |
| Strikethrough          | false                               |
| Underline              | false                               |
| VerticalAlign          | VerticalAlign.Top                   |
| Visible                | false                               |
| Width                  | shpRectangleBackground.Width\-36    |
| Wrap                   | true                                |
| X                      | 18                                  |
| Y                      | lblCountry.Y + lblCountry.Height+12 |
| ZIndex                 | 5                                   |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## lblLastUpdated

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                              |
| -------- | ------------------------------------------------------------------------------------------------------------------ |
| Live     | Live.Off                                                                                                           |
| Role     | TextRole.Default                                                                                                   |
| Text     | varString.UpdatedLbl &" "& DateDiff(ThisItem.Modified, Today()) &" "& varStringNew.DaysLbl &" "& varString.AgoLbl  |

### Design

| Property               | Value                                               |
| ---------------------- | --------------------------------------------------- |
| Align                  | Align.Left                                          |
| AutoHeight             | false                                               |
| BorderStyle            | BorderStyle.None                                    |
| BorderThickness        | 2                                                   |
| DisplayMode            | DisplayMode.Edit                                    |
| FocusedBorderThickness | 4                                                   |
| Font                   | Font.'Segoe UI'                                     |
| FontWeight             | FontWeight.Normal                                   |
| Height                 | 32                                                  |
| Italic                 | false                                               |
| LineHeight             | 1.2                                                 |
| Overflow               | Overflow.Hidden                                     |
| PaddingBottom          | 5                                                   |
| PaddingLeft            | 5                                                   |
| PaddingRight           | 5                                                   |
| PaddingTop             | 5                                                   |
| Size                   | 18                                                  |
| Strikethrough          | false                                               |
| Underline              | false                                               |
| VerticalAlign          | VerticalAlign.Middle                                |
| Width                  | 420                                                 |
| X                      | imgClock.X+imgClock.Width                           |
| Y                      | htmlLblDescription.Y + htmlLblDescription.Height+17 |
| ZIndex                 | 6                                                   |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## lblSafetyPrecautionTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | ThisItem.Title   |

### Design

| Property               | Value                            |
| ---------------------- | -------------------------------- |
| Align                  | Align.Left                       |
| AutoHeight             | false                            |
| BorderStyle            | BorderStyle.None                 |
| BorderThickness        | 2                                |
| DisplayMode            | DisplayMode.Edit                 |
| FocusedBorderThickness | 4                                |
| Font                   | Font.'Segoe UI'                  |
| FontWeight             | FontWeight.Bold                  |
| Height                 | 80                               |
| Italic                 | false                            |
| LineHeight             | 1.2                              |
| Overflow               | Overflow.Hidden                  |
| PaddingBottom          | 5                                |
| PaddingLeft            | 0                                |
| PaddingRight           | 5                                |
| PaddingTop             | 5                                |
| Size                   | 25.5                             |
| Strikethrough          | false                            |
| Underline              | false                            |
| VerticalAlign          | VerticalAlign.Middle             |
| Width                  | shpRectangleBackground.Width\-36 |
| X                      | 18                               |
| Y                      | 12                               |
| ZIndex                 | 3                                |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## precautionSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property        | Value                    |
| --------------- | ------------------------ |
| BorderStyle     | BorderStyle.Solid        |
| BorderThickness | 0                        |
| DisplayMode     | DisplayMode.Edit         |
| Height          | 1                        |
| Visible         | false                    |
| Width           | Parent.TemplateWidth     |
| X               | 0                        |
| Y               | Parent.TemplateHeight\-1 |
| ZIndex          | 2                        |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## shpRectangleBackground

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value           |
| -------- | --------------- |
| OnSelect | Select(Parent); |

### Design

| Property               | Value                 |
| ---------------------- | --------------------- |
| BorderStyle            | BorderStyle.Solid     |
| BorderThickness        | 2                     |
| DisplayMode            | DisplayMode.Edit      |
| FocusedBorderThickness | 4                     |
| Height                 | Parent.TemplateHeight |
| Width                  | Parent.TemplateWidth  |
| X                      | 0                     |
| Y                      | 0                     |
| ZIndex                 | 1                     |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## shpRectangleBackground\_1

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ClearCollect(colSelectedSafetyPrecation,ThisItem); Navigate('Safety Precaution Details Screen',ScreenTransition.None) |

### Design

| Property               | Value                 |
| ---------------------- | --------------------- |
| BorderStyle            | BorderStyle.Solid     |
| BorderThickness        | 2                     |
| DisplayMode            | DisplayMode.Edit      |
| FocusedBorderThickness | 4                     |
| Height                 | Parent.TemplateHeight |
| Width                  | Parent.TemplateWidth  |
| X                      | 0                     |
| Y                      | 0                     |
| ZIndex                 | 8                     |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glrySafetyPrecautions. |

## shpRectangleBackGround\_SafetyPrecautionListScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value            |
| ---------------------- | ---------------- |
| BorderStyle            | BorderStyle.None |
| BorderThickness        | 2                |
| DisplayMode            | DisplayMode.Edit |
| FocusedBorderThickness | 4                |
| Height                 | Parent.Height    |
| Width                  | Parent.Width     |
| X                      | 0                |
| Y                      | 0                |
| ZIndex                 | 1                |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(229, 229, 241, 1)</td></tr><tr><td style="background-color:#E5E5F1"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(229, 229, 241, 1)</td></tr><tr><td style="background-color:#E5E5F1"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(229, 229, 241, 1)</td></tr><tr><td style="background-color:#E5E5F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Parent Control | Safety Precaution List Screen |

## shpUpperRectangle\_SafetyPrecautionListScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                              |
| ---------------------- | ------------------------------------------------------------------ |
| BorderStyle            | BorderStyle.Solid                                                  |
| BorderThickness        | 2                                                                  |
| DisplayMode            | DisplayMode.Edit                                                   |
| FocusedBorderThickness | 4                                                                  |
| Height                 | Parent.Height                                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                      |
| Y                      | 0                                                                  |
| ZIndex                 | 2                                                                  |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(204, 204, 204, 1)</td></tr><tr><td style="background-color:#CCCCCC"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Parent Control | Safety Precaution List Screen |

## txtSearchBox

| Property                    | Value      |
| --------------------------- | ---------- |
| ![text](resources/text.png) | Type: text |

### Data

| Property    | Value            |
| ----------- | ---------------- |
| Default     | ""               |
| DelayOutput | false            |
| HintText    | varString.Search |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                   |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 0                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Font                   | Font.'Segoe UI'                                                              |
| FontWeight             | FontWeight.Normal                                                            |
| Format                 | TextFormat.Text                                                              |
| Height                 | 67                                                                           |
| Italic                 | false                                                                        |
| Mode                   | TextMode.SingleLine                                                          |
| PaddingLeft            | 10                                                                           |
| RadiusBottomLeft       | 3                                                                            |
| RadiusBottomRight      | 3                                                                            |
| RadiusTopLeft          | 3                                                                            |
| RadiusTopRight         | 3                                                                            |
| Size                   | 21                                                                           |
| Strikethrough          | false                                                                        |
| Underline              | false                                                                        |
| VirtualKeyboardMode    | VirtualKeyboardMode.Auto                                                     |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 ) |
| X                      | 'glrySafetyPrecautions.'.X+6                                                 |
| Y                      | HeaderControlSafetyPrecautions.Y+HeaderControlSafetyPrecautions.Height+16    |
| ZIndex                 | 5                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(151, 149, 147, 1)</td></tr><tr><td style="background-color:#979593"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(151, 149, 147, 1)</td></tr><tr><td style="background-color:#979593"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverColor          | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| PressedFill         | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                         |
| -------------- | ----------------------------- |
| Parent Control | Safety Precaution List Screen |
