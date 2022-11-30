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

## Safety Precaution Details Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

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

| Property      | Value                                                 |
| ------------- | ----------------------------------------------------- |
| Child Control | shpRectangleBackGround\_SafetyPrecautionDetailsScreen |
| Child Control | shpUpperRectangle\_SafetyPrecautionDetailsScreen      |
| Child Control | HeaderControlSafetyPrecautionsDetails                 |
| Child Control | glrySafetyPrecautionDetails                           |

## galleryTemplate4\_1

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## glrySafetyPrecautionDetails

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                      |
| --------- | -------------------------- |
| Items     | colSelectedSafetyPrecation |
| WrapCount | 1                          |

### Design

| Property               | Value                                                                                   |
| ---------------------- | --------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                       |
| DisplayMode            | DisplayMode.Edit                                                                        |
| FocusedBorderThickness | 4                                                                                       |
| Height                 | Parent.Height \- Self.Y\-20                                                             |
| Layout                 | Layout.Vertical                                                                         |
| LoadingSpinner         | LoadingSpinner.None                                                                     |
| LoadingSpinnerColor    | Self.BorderColor                                                                        |
| TemplateSize           | lblDescriptionSafetyPrecaution.Y+lblDescriptionSafetyPrecaution.Height                  |
| Transition             | Transition.None                                                                         |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 )            |
| X                      | (Parent.Width\-Self.Width) \/2                                                          |
| Y                      | HeaderControlSafetyPrecautionsDetails.Y+HeaderControlSafetyPrecautionsDetails.Height+12 |
| ZIndex                 | 3                                                                                       |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                                    |
| -------------- | ---------------------------------------- |
| Child Control  | galleryTemplate4\_1                      |
| Child Control  | lblSafetyPrecautionTitleSafetyPrecaution |
| Child Control  | imgUpdatedStatusClock                    |
| Child Control  | lblCountrySafetyPrecaution               |
| Child Control  | lblLastUpdatedSafetyPrecaution           |
| Child Control  | lblDescriptionSafetyPrecaution           |
| Child Control  | lblDescriptionSafetyPrecautions          |
| Parent Control | Safety Precaution Details Screen         |

## HeaderControlSafetyPrecautionsDetails

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
| IsBackButtonVisible | true                             |
| IsHomeButtonVisible | true                             |
| NavigateHomeScreen  | 'Home Screen'                    |
| NavigateScreen      | 'Safety Precaution List Screen'  |
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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | Safety Precaution Details Screen |

## imgUpdatedStatusClock

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

| Property               | Value                                                                                      |
| ---------------------- | ------------------------------------------------------------------------------------------ |
| BorderStyle            | BorderStyle.None                                                                           |
| BorderThickness        | 2                                                                                          |
| DisplayMode            | DisplayMode.Edit                                                                           |
| FocusedBorderThickness | 4                                                                                          |
| Height                 | 48                                                                                         |
| ImagePosition          | ImagePosition.Fit                                                                          |
| ImageRotation          | ImageRotation.None                                                                         |
| PaddingBottom          | 0                                                                                          |
| PaddingLeft            | 0                                                                                          |
| PaddingRight           | 0                                                                                          |
| PaddingTop             | 0                                                                                          |
| RadiusBottomLeft       | 0                                                                                          |
| RadiusBottomRight      | 0                                                                                          |
| RadiusTopLeft          | 0                                                                                          |
| RadiusTopRight         | 0                                                                                          |
| Width                  | 48                                                                                         |
| X                      | Parent.Width \- lblLastUpdatedSafetyPrecaution.Width \- Self.Width \- 20                   |
| Y                      | lblSafetyPrecautionTitleSafetyPrecaution.Y+lblSafetyPrecautionTitleSafetyPrecaution.Height |
| ZIndex                 | 6                                                                                          |

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

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## lblCountrySafetyPrecaution

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
| Tooltip  | ThisItem.Country |

### Design

| Property               | Value                                                                                        |
| ---------------------- | -------------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                                   |
| BorderStyle            | BorderStyle.None                                                                             |
| BorderThickness        | 2                                                                                            |
| DisplayMode            | DisplayMode.Edit                                                                             |
| FocusedBorderThickness | 4                                                                                            |
| Font                   | Font.'Segoe UI'                                                                              |
| FontWeight             | FontWeight.Normal                                                                            |
| Height                 | 32                                                                                           |
| Italic                 | false                                                                                        |
| LineHeight             | 1.2                                                                                          |
| Overflow               | Overflow.Hidden                                                                              |
| PaddingBottom          | 5                                                                                            |
| PaddingLeft            | 0                                                                                            |
| PaddingRight           | 5                                                                                            |
| PaddingTop             | 5                                                                                            |
| Size                   | 18                                                                                           |
| Strikethrough          | false                                                                                        |
| Underline              | false                                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                                         |
| Width                  | 260                                                                                          |
| Wrap                   | false                                                                                        |
| X                      | 0                                                                                            |
| Y                      | lblSafetyPrecautionTitleSafetyPrecaution.Y+lblSafetyPrecautionTitleSafetyPrecaution.Height+8 |
| ZIndex                 | 3                                                                                            |

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

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## lblDescriptionSafetyPrecaution

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                |
| -------- | -------------------- |
| Live     | Live.Off             |
| Role     | TextRole.Default     |
| Text     | ThisItem.Description |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| Align                  | Align.Left                                                        |
| AutoHeight             | true                                                              |
| BorderStyle            | BorderStyle.None                                                  |
| BorderThickness        | 2                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Font                   | Font.'Segoe UI'                                                   |
| FontWeight             | FontWeight.Normal                                                 |
| Height                 | Parent.Height\-Self.Y\-20                                         |
| Italic                 | false                                                             |
| LineHeight             | 1.2                                                               |
| Overflow               | Overflow.Hidden                                                   |
| PaddingBottom          | 5                                                                 |
| PaddingLeft            | 0                                                                 |
| PaddingRight           | 5                                                                 |
| PaddingTop             | 5                                                                 |
| Size                   | 22.5                                                              |
| Strikethrough          | false                                                             |
| Underline              | false                                                             |
| VerticalAlign          | VerticalAlign.Middle                                              |
| Visible                | false                                                             |
| Width                  | Parent.Width                                                      |
| Wrap                   | true                                                              |
| X                      | 0                                                                 |
| Y                      | lblCountrySafetyPrecaution.Y+lblCountrySafetyPrecaution.Height+16 |
| ZIndex                 | 4                                                                 |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
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

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## lblDescriptionSafetyPrecautions

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                |
| -------- | -------------------- |
| HtmlText | ThisItem.Description |

### Design

| Property    | Value                                                             |
| ----------- | ----------------------------------------------------------------- |
| AutoHeight  | false                                                             |
| BorderStyle | BorderStyle.None                                                  |
| DisplayMode | DisplayMode.Edit                                                  |
| Font        | Font.'Segoe UI'                                                   |
| Height      | Parent.Height\-Self.Y\-20                                         |
| PaddingLeft | 0                                                                 |
| Size        | 22.5                                                              |
| Width       | Parent.Width                                                      |
| X           | 0                                                                 |
| Y           | lblCountrySafetyPrecaution.Y+lblCountrySafetyPrecaution.Height+16 |
| ZIndex      | 7                                                                 |

### Color Properties

| Property            | Value                                                                                                                  |
| ------------------- | ---------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>        |
| Color               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>        |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>     |
| DisabledFill        | <table border="0"><tr><td>RGBA(119, 119, 119, .4)</td></tr><tr><td style="background-color:#777777"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 0)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>  |
| HoverBorderColor    | Self.BorderColor                                                                                                       |
| PressedBorderColor  | Self.BorderColor                                                                                                       |

### Child & Parent Controls

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## lblLastUpdatedSafetyPrecaution

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                              |
| Role     | TextRole.Default                                                                                                      |
| Text     | varString.UpdatedLbl &" "& DateDiff(ThisItem.Modified, Today()) & " " & varStringNew.DaysLbl & " " & varString.AgoLbl |

### Design

| Property               | Value                                                                                        |
| ---------------------- | -------------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                                   |
| AutoHeight             | false                                                                                        |
| BorderStyle            | BorderStyle.None                                                                             |
| BorderThickness        | 2                                                                                            |
| DisplayMode            | DisplayMode.Edit                                                                             |
| FocusedBorderThickness | 4                                                                                            |
| Font                   | Font.'Segoe UI'                                                                              |
| FontWeight             | FontWeight.Normal                                                                            |
| Height                 | 32                                                                                           |
| Italic                 | false                                                                                        |
| LineHeight             | 1.2                                                                                          |
| Overflow               | Overflow.Hidden                                                                              |
| PaddingBottom          | 5                                                                                            |
| PaddingLeft            | 5                                                                                            |
| PaddingRight           | 5                                                                                            |
| PaddingTop             | 5                                                                                            |
| Size                   | 18                                                                                           |
| Strikethrough          | false                                                                                        |
| Underline              | false                                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                                         |
| Width                  | 260                                                                                          |
| X                      | Parent.Width \- Self.Width \- 20                                                             |
| Y                      | lblSafetyPrecautionTitleSafetyPrecaution.Y+lblSafetyPrecautionTitleSafetyPrecaution.Height+8 |
| ZIndex                 | 5                                                                                            |

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

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## lblSafetyPrecautionTitleSafetyPrecaution

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

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| AutoHeight             | true                 |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Bold      |
| Height                 | 70                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 0                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 25.5                 |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Width                  | Parent.Width         |
| X                      | 0                    |
| Y                      | 0                    |
| ZIndex                 | 2                    |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
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

| Property       | Value                       |
| -------------- | --------------------------- |
| Parent Control | glrySafetyPrecautionDetails |

## shpRectangleBackGround\_SafetyPrecautionDetailsScreen

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | Safety Precaution Details Screen |

## shpUpperRectangle\_SafetyPrecautionDetailsScreen

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | Safety Precaution Details Screen |
