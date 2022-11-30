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

## My Request List Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                              |
| --------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | \/\/ Get Users requests ClearCollect( colUserRequests, SortByColumns( Filter( BAR\_Requests, IsSlotBooked, IsRequired, RequestorGuid \= varUser.id, Status.Value \<\> "Withdrawn", RequestDate \>\= Today() ), "RequestDate", Ascending ) ); \/\/clear selection variables Set( varSelectedRequest, Blank() ); Set( varSelectedBuilding, Blank() ) |

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

| Property      | Value                                       |
| ------------- | ------------------------------------------- |
| Child Control | shpRectangleBackGround\_MyRequestListScreen |
| Child Control | shpUpperRectangle\_MyRequestListScreen      |
| Child Control | HeaderControlMyRequestList                  |
| Child Control | txtSearchRequestBox                         |
| Child Control | icnSearchRequests                           |
| Child Control | glryMyRequest                               |
| Child Control | btnSaveKeyQuestions\_1                      |

## btnSaveKeyQuestions\_1

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                        |
| -------- | ------------------------------------------------------------------------------------------------------------ |
| OnSelect | If( varAppSettings.KeyQuestions, Navigate('New Request Key Questions Screen'), Navigate('Building Screen') ) |

### Data

| Property | Value                         |
| -------- | ----------------------------- |
| Text     | varStringNew.NewRequestBtnTxt |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                  |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Semibold                                                           |
| Height                 | 64                                                                            |
| Italic                 | false                                                                         |
| RadiusBottomLeft       | 4                                                                             |
| RadiusBottomRight      | 4                                                                             |
| RadiusTopLeft          | 4                                                                             |
| RadiusTopRight         | 4                                                                             |
| Size                   | 22.5                                                                          |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | Parent.Height\-16\-Self.Height                                                |
| ZIndex                 | 3                                                                             |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |

## galleryTemplate6\_1

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Behavior

| Property | Value                                                                                                   |
| -------- | ------------------------------------------------------------------------------------------------------- |
| OnSelect | ClearCollect(colSelectedRequest,ThisItem); Navigate('My Request Details Screen' ,ScreenTransition.None) |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## glryMyRequest

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                                                                |
| --------- | -------------------------------------------------------------------------------------------------------------------- |
| Items     | SortByColumns( Search( colUserRequests, txtSearchRequestBox.Text, "Title", "SpaceName" ), "RequestDate", Ascending ) |
| WrapCount | 1                                                                                                                    |

### Design

| Property               | Value                                                                                                     |
| ---------------------- | --------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                          |
| BorderThickness        | 0                                                                                                         |
| DisplayMode            | DisplayMode.Edit                                                                                          |
| FocusedBorderThickness | 4                                                                                                         |
| Height                 | Min(btnSaveKeyQuestions\_1.Y\-Self.Y\-20,glryMyRequest.TemplateHeight\*CountRows(glryMyRequest.AllItems)) |
| Layout                 | Layout.Vertical                                                                                           |
| LoadingSpinner         | LoadingSpinner.None                                                                                       |
| LoadingSpinnerColor    | Self.BorderColor                                                                                          |
| TemplatePadding        | 0                                                                                                         |
| TemplateSize           | 97                                                                                                        |
| Transition             | Transition.None                                                                                           |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, (App.DesignWidth\*2) )                                      |
| X                      | (Parent.Width\-Self.Width) \/2                                                                            |
| Y                      | 192                                                                                                       |
| ZIndex                 | 4                                                                                                         |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Child Control  | galleryTemplate6\_1    |
| Child Control  | rctUserRequest         |
| Child Control  | lblUserRequestTitle    |
| Child Control  | imgInformation         |
| Child Control  | imgRequests            |
| Child Control  | lblRequestStatus       |
| Parent Control | My Request List Screen |

## HeaderControlMyRequestList

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                     |
| ------------------- | ------------------------- |
| backLabel           | "back"                    |
| IsBackButtonVisible | false                     |
| IsHomeButtonVisible | true                      |
| NavigateHomeScreen  | 'Home Screen'             |
| NavigateScreen      | 'Home Screen'             |
| Text                | varString.MyRequestsTitle |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width) \/2                                     |
| Y        | 0                                                                  |
| ZIndex   | 5                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |

## icnSearchRequests

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Design

| Property               | Value                                                           |
| ---------------------- | --------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                               |
| BorderThickness        | 0                                                               |
| DisplayMode            | DisplayMode.View                                                |
| FocusedBorderThickness | 4                                                               |
| Height                 | 42                                                              |
| Icon                   | Icon.Search                                                     |
| Rotation               | 0                                                               |
| TabIndex               | 0                                                               |
| Width                  | 42                                                              |
| X                      | txtSearchRequestBox.X+txtSearchRequestBox.Width\-Self.Width\-12 |
| Y                      | txtSearchRequestBox.Y+16                                        |
| ZIndex                 | 7                                                               |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(151, 149, 147, 1)</td></tr><tr><td style="background-color:#979593"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledColor       | <table border="0"><tr><td>RGBA(220, 220, 220, 1)</td></tr><tr><td style="background-color:#DCDCDC"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(151, 149, 147, 1)</td></tr><tr><td style="background-color:#979593"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |

## imgInformation

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Image    | icn\_fluent\_information |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| BorderStyle            | BorderStyle.None                        |
| BorderThickness        | 2                                       |
| DisplayMode            | DisplayMode.Edit                        |
| FocusedBorderThickness | 4                                       |
| Height                 | 72                                      |
| ImagePosition          | ImagePosition.Fit                       |
| ImageRotation          | ImageRotation.None                      |
| PaddingBottom          | 0                                       |
| PaddingLeft            | 0                                       |
| PaddingRight           | 0                                       |
| PaddingTop             | 0                                       |
| RadiusBottomLeft       | 0                                       |
| RadiusBottomRight      | 0                                       |
| RadiusTopLeft          | 0                                       |
| RadiusTopRight         | 0                                       |
| TabIndex               | 0                                       |
| Width                  | 72                                      |
| X                      | Parent.Width \- Self.Width\-12          |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 5                                       |

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

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## imgRequests

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                                                                                                                            |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Image    | If(ThisItem.Status.Value\="Pending Approval",'Pending@2x', ThisItem.Status.Value\="Approved",'Approved@2x', ThisItem.Status.Value\="Rejected",'Rejected@2x', ThisItem.Status.Value\="Withdrawn",'Withdrawn@2x' ) |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| BorderStyle            | BorderStyle.None                        |
| BorderThickness        | 2                                       |
| DisplayMode            | DisplayMode.Edit                        |
| FocusedBorderThickness | 4                                       |
| Height                 | 48                                      |
| ImagePosition          | ImagePosition.Fit                       |
| ImageRotation          | ImageRotation.None                      |
| PaddingBottom          | 0                                       |
| PaddingLeft            | 0                                       |
| PaddingRight           | 0                                       |
| PaddingTop             | 0                                       |
| RadiusBottomLeft       | 0                                       |
| RadiusBottomRight      | 0                                       |
| RadiusTopLeft          | 0                                       |
| RadiusTopRight         | 0                                       |
| Width                  | 48                                      |
| X                      | 24                                      |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 4                                       |

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

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## lblRequestStatus

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| Role     | TextRole.Default                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| Text     | \/\/ThisItem.RequestDate & " \| " & ThisItem.Status.Value If(ThisItem.Status.Value\="Pending Approval",ThisItem.RequestDate & " \| "& varString.PendingApproval, ThisItem.Status.Value\="Approved",ThisItem.RequestDate & " \| "& varString.Approved, ThisItem.Status.Value\="Rejected",ThisItem.RequestDate & " \| "& varString.Rejected, ThisItem.Status.Value\="Revoked",ThisItem.RequestDate & " \| "& varString.Revoked, ThisItem.Status.Value\="Withdrawn",ThisItem.RequestDate & " \| "& varStringNew.WithdrawnStatus) |

### Design

| Property               | Value                                            |
| ---------------------- | ------------------------------------------------ |
| Align                  | Align.Left                                       |
| BorderStyle            | BorderStyle.None                                 |
| BorderThickness        | 2                                                |
| DisplayMode            | DisplayMode.Edit                                 |
| FocusedBorderThickness | 4                                                |
| Font                   | Font.'Segoe UI'                                  |
| FontWeight             | FontWeight.Normal                                |
| Height                 | 32                                               |
| Italic                 | false                                            |
| LineHeight             | 1.2                                              |
| Overflow               | Overflow.Hidden                                  |
| PaddingBottom          | 5                                                |
| PaddingLeft            | 5                                                |
| PaddingRight           | 5                                                |
| PaddingTop             | 5                                                |
| Size                   | 18                                               |
| Strikethrough          | false                                            |
| Underline              | false                                            |
| VerticalAlign          | VerticalAlign.Middle                             |
| Width                  | lblUserRequestTitle.Width                        |
| X                      | lblUserRequestTitle.X                            |
| Y                      | lblUserRequestTitle.Y+lblUserRequestTitle.Height |
| ZIndex                 | 3                                                |

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

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## lblUserRequestTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                        |
| -------- | -------------------------------------------- |
| Live     | Live.Off                                     |
| Role     | TextRole.Default                             |
| Text     | ThisItem.SpaceName & " \| " & ThisItem.Title |
| Tooltip  | ThisItem.SpaceName & " \| " & ThisItem.Title |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| AutoHeight             | false                |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Bold      |
| Height                 | 40                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 22.5                 |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Width                  | 484                  |
| Wrap                   | false                |
| X                      | 91                   |
| Y                      | 12                   |
| ZIndex                 | 2                    |

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

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## rctUserRequest

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property               | Value             |
| ---------------------- | ----------------- |
| BorderStyle            | BorderStyle.Solid |
| BorderThickness        | 1                 |
| DisplayMode            | DisplayMode.Edit  |
| FocusedBorderThickness | 4                 |
| Height                 | 96                |
| Width                  | Parent.Width      |
| X                      | 0                 |
| Y                      | 0                 |
| ZIndex                 | 1                 |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryMyRequest |

## shpRectangleBackGround\_MyRequestListScreen

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |

## shpUpperRectangle\_MyRequestListScreen

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |

## txtSearchRequestBox

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
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | HeaderControlMyRequestList.Y+HeaderControlMyRequestList.Height+20            |
| ZIndex                 | 6                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | My Request List Screen |
