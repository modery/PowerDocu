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

## Reject Reason Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                          |
| --------- | ---------------------------------------------- |
| OnVisible | ClearCollect( colMyApprovals, BAR\_Requests ); |

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

| Property      | Value                                      |
| ------------- | ------------------------------------------ |
| Child Control | shpRectangleBackGround\_RejectReasonScreen |
| Child Control | shpUpperRectangle\_RejectReasonScreen      |
| Child Control | HeaderControlRejectionReason               |
| Child Control | lblErrorBackRectangle\_2                   |
| Child Control | RejectionReasonComponent                   |
| Child Control | lblErrorRectangle\_2                       |
| Child Control | icnErrorCancel\_2                          |
| Child Control | imgErrorInfo\_2                            |
| Child Control | lblErrorMessage\_2                         |
| Child Control | btnCancelRequest                           |
| Child Control | btnRejectRequest                           |
| Child Control | grpErrorMessage\_2                         |

## btnCancelRequest

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If( isNavigatedFromApprovalsScreen \= "Yes", Navigate( 'Approval Screen', ScreenTransition.UnCover ) ); If( isNavigatedFromApprovalsScreen \= "No", Navigate( 'Approval Detail Screen', ScreenTransition.UnCover ) ); |

### Data

| Property | Value            |
| -------- | ---------------- |
| Text     | varString.Cancel |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| Align                  | Align.Center                            |
| BorderStyle            | BorderStyle.Solid                       |
| BorderThickness        | 1                                       |
| DisplayMode            | Edit                                    |
| FocusedBorderThickness | 4                                       |
| Font                   | Font.'Segoe UI'                         |
| FontWeight             | FontWeight.Semibold                     |
| Height                 | 60                                      |
| Italic                 | false                                   |
| RadiusBottomLeft       | 4                                       |
| RadiusBottomRight      | 4                                       |
| RadiusTopLeft          | 4                                       |
| RadiusTopRight         | 4                                       |
| Size                   | 22.5                                    |
| Strikethrough          | false                                   |
| TabIndex               |                                         |
| Underline              | false                                   |
| VerticalAlign          | VerticalAlign.Middle                    |
| Visible                | true                                    |
| Width                  | RejectionReasonComponent.Width\/2 \- 25 |
| X                      | RejectionReasonComponent.X + 15         |
| Y                      | Parent.Height \- Self.Height \- 15      |
| ZIndex                 | 4                                       |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | btnCancelRequest.BorderColor                                                                                          |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## btnRejectRequest

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If( isNavigatedFromApprovalsScreen \= "No", If( varSelectedRequest.TimeSlot \= varStringNew.FullDayLbl2, UpdateIf( BAR\_Requests, varSelectedRequest.Request\_x0020\_Collection\_x0020\_I in 'Request Collection ID' && varSelectedRequest.DateValue in Text(DateValue), { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 2, Value: "Rejected" }, RejectionReason: RejectionReasonComponent.Text, Active: 0, RequestorNotificationSent: false } ); , Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 2, Value: "Rejected" }, RejectionReason: RejectionReasonComponent.Text, Active: 0, RequestorNotificationSent: false } ) ); Remove( colMyApprovals, varSelectedRequest ); Set( varErrorMessageRejection, true ), ForAll( RenameColumns( colSelectedApprovals, "ID", "ID1" ), Patch( BAR\_Requests, LookUp( colMyApprovals, ID1 \= ID ), { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 2, Value: "Rejected" }, RejectionReason: RejectionReasonComponent.Text, Active: 0, RequestorNotificationSent: false } ); RemoveIf( colMyApprovals, ID1 \= ID ) ); Clear(colSelectedApprovals); Set( varErrorMessageRejection, true ) ); |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Text     | varStringNew.DenyBtnText |

### Design

| Property               | Value                                                    |
| ---------------------- | -------------------------------------------------------- |
| Align                  | Align.Center                                             |
| BorderStyle            | BorderStyle.None                                         |
| BorderThickness        | 0                                                        |
| DisplayMode            | If(IsBlank(RejectionReasonComponent.Text),Disabled,Edit) |
| FocusedBorderThickness | 4                                                        |
| Font                   | Font.'Segoe UI'                                          |
| FontWeight             | FontWeight.Semibold                                      |
| Height                 | 60                                                       |
| Italic                 | false                                                    |
| RadiusBottomLeft       | 4                                                        |
| RadiusBottomRight      | 4                                                        |
| RadiusTopLeft          | 4                                                        |
| RadiusTopRight         | 4                                                        |
| Size                   | 22.5                                                     |
| Strikethrough          | false                                                    |
| TabIndex               |                                                          |
| Underline              | false                                                    |
| VerticalAlign          | VerticalAlign.Middle                                     |
| Visible                | true                                                     |
| Width                  | RejectionReasonComponent.Width\/2 \- 25                  |
| X                      | btnCancelRequest.X + btnCancelRequest.Width + 20         |
| Y                      | Parent.Height \- Self.Height \- 15                       |
| ZIndex                 | 3                                                        |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | btnRejectRequest.BorderColor                                                                                          |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## grpErrorMessage\_2

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![group](resources/group.png) | Type: group |

### Design

| Property | Value |
| -------- | ----- |
| Height   | 5     |
| Width    | 5     |
| X        | 40    |
| Y        | 40    |
| ZIndex   | 12    |

### Color Properties

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## HeaderControlRejectionReason

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                    |
| ------------------- | ------------------------ |
| backLabel           | "back"                   |
| IsBackButtonVisible | true                     |
| IsHomeButtonVisible | true                     |
| NavigateHomeScreen  | 'Home Screen'            |
| NavigateScreen      | 'Approval Screen'        |
| Text                | varString.ApprovalsTitle |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0                                                                  |
| ZIndex   | 6                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## icnErrorCancel\_2

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                           |
| -------- | ------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Set(varErrorMessageRejection,false); Set(varErrorMessageApproval,false); Navigate('Approval Screen', ScreenTransition.UnCover); |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                 |
| BorderThickness        | 0                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Height                 | 32                                                                |
| Icon                   | Icon.Cancel                                                       |
| Visible                | varErrorMessageRejection                                          |
| Width                  | 32                                                                |
| X                      | lblErrorRectangle\_2.X+lblErrorRectangle\_2.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_2.Y+32                                         |
| ZIndex                 | 10                                                                |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledColor       | <table border="0"><tr><td>RGBA(220, 220, 220, 1)</td></tr><tr><td style="background-color:#DCDCDC"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## imgErrorInfo\_2

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value     |
| -------- | --------- |
| Image    | InfoSolid |

### Design

| Property               | Value                         |
| ---------------------- | ----------------------------- |
| BorderStyle            | BorderStyle.None              |
| BorderThickness        | 2                             |
| DisplayMode            | DisplayMode.Edit              |
| FocusedBorderThickness | 4                             |
| Height                 | 48                            |
| ImagePosition          | ImagePosition.Fit             |
| ImageRotation          | ImageRotation.None            |
| PaddingBottom          | 0                             |
| PaddingLeft            | 0                             |
| PaddingRight           | 0                             |
| PaddingTop             | 0                             |
| RadiusBottomLeft       | 0                             |
| RadiusBottomRight      | 0                             |
| RadiusTopLeft          | 0                             |
| RadiusTopRight         | 0                             |
| Visible                | varErrorMessageRejection      |
| Width                  | 48                            |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | lblErrorRectangle\_2.Y+64     |
| ZIndex                 | 11                            |

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

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## lblErrorBackRectangle\_2

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                    |
| ---------------------- | ------------------------ |
| BorderStyle            | BorderStyle.None         |
| BorderThickness        | 0                        |
| DisplayMode            | DisplayMode.Edit         |
| FocusedBorderThickness | 4                        |
| Height                 | Parent.Height            |
| Visible                | varErrorMessageRejection |
| Width                  | Parent.Width             |
| X                      | 0                        |
| Y                      | 0                        |
| ZIndex                 | 7                        |

### Color Properties

| Property           | Value                                                                                                             |
| ------------------ | ----------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>   |
| DisabledFill       | <table border="0"><tr><td>RGBA(0, 0, 0, 0.7)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(0, 0, 0, 0.7)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                  |
| HoverFill          | <table border="0"><tr><td>RGBA(0, 0, 0, 0.7)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0.7)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## lblErrorMessage\_2

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                   |
| -------- | ----------------------- |
| Live     | Live.Off                |
| Role     | TextRole.Default        |
| Text     | varString.AlertRejected |

### Design

| Property               | Value                                       |
| ---------------------- | ------------------------------------------- |
| Align                  | Align.Center                                |
| BorderStyle            | BorderStyle.None                            |
| BorderThickness        | 2                                           |
| DisplayMode            | DisplayMode.Edit                            |
| FocusedBorderThickness | 4                                           |
| Font                   | Font.'Segoe UI'                             |
| FontWeight             | FontWeight.Normal                           |
| Height                 | 40                                          |
| Italic                 | false                                       |
| LineHeight             | 1.2                                         |
| Overflow               | Overflow.Hidden                             |
| PaddingBottom          | 5                                           |
| PaddingLeft            | 5                                           |
| PaddingRight           | 5                                           |
| PaddingTop             | 5                                           |
| Size                   | 21                                          |
| Strikethrough          | false                                       |
| Underline              | false                                       |
| VerticalAlign          | VerticalAlign.Middle                        |
| Visible                | varErrorMessageRejection                    |
| Width                  | lblErrorRectangle\_2.Width                  |
| X                      | lblErrorRectangle\_2.X                      |
| Y                      | imgErrorInfo\_2.Y+imgErrorInfo\_2.Height+18 |
| ZIndex                 | 9                                           |

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

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## lblErrorRectangle\_2

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 0                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Height                 | 240                                                                           |
| Visible                | varErrorMessageRejection                                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 8                                                                             |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## RejectionReasonComponent

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value             |
| -------- | ----------------- |
| OnReset  | Reset(txtReason)  |

### Data

| Property       | Value                              |
| -------------- | ---------------------------------- |
| DisplayMode    | DisplayMode.Edit                   |
| HintText       | varStringNew.RequestReasonHintText |
| LabelSize      | 20                                 |
| Margin         | 40                                 |
| Padding        | 5                                  |
| Requred        | true                               |
| ResetTextField | true                               |
| Text           | txtReason.Text                     |
| TextSize       | 20                                 |
| Title          | varString.RejectionReason          |

### Design

| Property | Value                                                                         |
| -------- | ----------------------------------------------------------------------------- |
| Height   | Parent.Height\*80%                                                            |
| Visible  | true                                                                          |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 ) |
| X        | (Parent.Width\-Self.Width)\/2                                                 |
| Y        | HeaderControlRejectionReason.Y+HeaderControlRejectionReason.Height+25         |
| ZIndex   | 5                                                                             |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## shpRectangleBackGround\_RejectReasonScreen

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

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |

## shpUpperRectangle\_RejectReasonScreen

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

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Reject Reason Screen |
