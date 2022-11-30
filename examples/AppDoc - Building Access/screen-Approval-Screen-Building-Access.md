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

## Approval Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                             |
| --------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | ClearCollect( colMyApprovals, Filter( BAR\_Requests, IsSlotBooked, IsRequired, ApproverGuid \= varUser.id, Status.Value \= "Pending Approval" ) ) |

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

| Property | Value                                                                                                              |
| -------- | ------------------------------------------------------------------------------------------------------------------ |
| Fill     | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property      | Value                                  |
| ------------- | -------------------------------------- |
| Child Control | shpRectangleBackGround\_ApprovalScreen |
| Child Control | shpUpperRectangle\_ApprovalScreen      |
| Child Control | AlertBackDrop                          |
| Child Control | HeaderControlApproval                  |
| Child Control | lblErrorBackRectangle\_1               |
| Child Control | imgApprovalBack                        |
| Child Control | txtApprovalsSearch                     |
| Child Control | icnSearchApproval                      |
| Child Control | icnFilter                              |
| Child Control | chkSelectAll                           |
| Child Control | lblSelectAll                           |
| Child Control | imgGalleryApprovalsBorder              |
| Child Control | glryApprovals                          |
| Child Control | ctrlRejectionReason                    |
| Child Control | lblErrorRectangle\_1                   |
| Child Control | icnErrorCancel\_1                      |
| Child Control | imgErrorInfo\_1                        |
| Child Control | lblErrorMessage\_1                     |
| Child Control | btnCancel                              |
| Child Control | btnRejectApproval                      |
| Child Control | rctRejectApproved                      |
| Child Control | SeperatorFeedApproval                  |
| Child Control | btnRejectSelected                      |
| Child Control | btnApproveSelected                     |
| Child Control | grpErrorMessage\_1                     |

## AlertBackDrop

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value              |
| ---------------------- | ------------------ |
| BorderStyle            | BorderStyle.None   |
| BorderThickness        | 2                  |
| DisplayMode            | DisplayMode.Edit   |
| FocusedBorderThickness | 4                  |
| Height                 | Parent.Height      |
| Visible                | varShowRejectPopup |
| Width                  | Parent.Width       |
| X                      | 0                  |
| Y                      | 0                  |
| ZIndex                 | 11                 |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | ColorFade(RGBA(0,0,0,0.79),10%)                                                                                       |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | ColorFade(RGBA(0,0,0,0.79),10%)                                                                                       |
| PressedFill        | ColorFade(RGBA(0,0,0,0.79),10%)                                                                                       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## btnApproveSelected

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                   |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ForAll( RenameColumns( colSelectedApprovals, "ID", "ID1" ), Patch( BAR\_Requests, LookUp( colMyApprovals, ID1 \= ID ), { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 1, Value: "Approved" }, RequestorNotificationSent: false } ); RemoveIf( colMyApprovals, ID1 \= ID ) ); Reset(chkSelectAll); Clear(colSelectedApprovals); Set( varErrorMessageApproval, true ); |

### Data

| Property | Value                      |
| -------- | -------------------------- |
| Text     | varStringNew.ApproveBtnTxt |

### Design

| Property               | Value                                                                |
| ---------------------- | -------------------------------------------------------------------- |
| Align                  | Align.Center                                                         |
| BorderStyle            | BorderStyle.Solid                                                    |
| BorderThickness        | 0                                                                    |
| DisplayMode            | If(IsEmpty(colSelectedApprovals) \|\| varShowApproved,Disabled,Edit) |
| FocusedBorderThickness | 4                                                                    |
| Font                   | Font.'Segoe UI'                                                      |
| FontWeight             | FontWeight.Semibold                                                  |
| Height                 | 60                                                                   |
| Italic                 | false                                                                |
| RadiusBottomLeft       | 4                                                                    |
| RadiusBottomRight      | 4                                                                    |
| RadiusTopLeft          | 4                                                                    |
| RadiusTopRight         | 4                                                                    |
| Size                   | 22.5                                                                 |
| Strikethrough          | false                                                                |
| TabIndex               |                                                                      |
| Underline              | false                                                                |
| VerticalAlign          | VerticalAlign.Middle                                                 |
| Width                  | (glryApprovals.Width\/2)\-48                                         |
| X                      | rctRejectApproved.X+rctRejectApproved.Width\-Self.Width\-24          |
| Y                      | Parent.Height\-Self.Height\-20                                       |
| ZIndex                 | 18                                                                   |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | btnApproveSelected.BorderColor                                                                                        |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## btnCancel

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                            |
| -------- | -------------------------------- |
| OnSelect | Set( varShowRejectPopup, false ) |

### Data

| Property | Value            |
| -------- | ---------------- |
| Text     | varString.Cancel |

### Design

| Property               | Value                                            |
| ---------------------- | ------------------------------------------------ |
| Align                  | Align.Center                                     |
| BorderStyle            | BorderStyle.Solid                                |
| BorderThickness        | 1                                                |
| DisplayMode            | Edit                                             |
| FocusedBorderThickness | 4                                                |
| Font                   | Font.'Segoe UI'                                  |
| FontWeight             | FontWeight.Semibold                              |
| Height                 | 60                                               |
| Italic                 | false                                            |
| RadiusBottomLeft       | 0                                                |
| RadiusBottomRight      | 0                                                |
| RadiusTopLeft          | 0                                                |
| RadiusTopRight         | 0                                                |
| Size                   | 22.5                                             |
| Strikethrough          | false                                            |
| TabIndex               |                                                  |
| Underline              | false                                            |
| VerticalAlign          | VerticalAlign.Middle                             |
| Visible                | varShowRejectPopup                               |
| Width                  | ctrlRejectionReason.Width\/2                     |
| X                      | ctrlRejectionReason.X                            |
| Y                      | ctrlRejectionReason.Y+ctrlRejectionReason.Height |
| ZIndex                 | 14                                               |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | btnCancel.BorderColor                                                                                                 |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## btnRejectApproval

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ForAll( RenameColumns( colSelectedApprovals, "ID", "ID1" ), Patch( BAR\_Requests, LookUp( colMyApprovals, ID1 \= ID ), { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 2, Value: "Rejected" }, RejectionReason: ctrlRejectionReason.Text, Active: 0, RequestorNotificationSent: false } ); RemoveIf( colMyApprovals, ID1 \= ID ) ); Clear(colSelectedApprovals); Notify( varString.AlertRejected, NotificationType.Success, 3000 ); Set( varShowRejectPopup, false ) |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | \/\/varString.Reject "Deny" |

### Design

| Property               | Value                                                |
| ---------------------- | ---------------------------------------------------- |
| Align                  | Align.Center                                         |
| BorderStyle            | BorderStyle.None                                     |
| BorderThickness        | 2                                                    |
| DisplayMode            | If(IsBlank(ctrlRejectionReason.Text),Disabled,Edit)  |
| FocusedBorderThickness | 4                                                    |
| Font                   | Font.'Segoe UI'                                      |
| FontWeight             | FontWeight.Semibold                                  |
| Height                 | 60                                                   |
| Italic                 | false                                                |
| RadiusBottomLeft       | 0                                                    |
| RadiusBottomRight      | 0                                                    |
| RadiusTopLeft          | 0                                                    |
| RadiusTopRight         | 0                                                    |
| Size                   | 22.5                                                 |
| Strikethrough          | false                                                |
| TabIndex               |                                                      |
| Underline              | false                                                |
| VerticalAlign          | VerticalAlign.Middle                                 |
| Visible                | varShowRejectPopup                                   |
| Width                  | ctrlRejectionReason.Width\/2                         |
| X                      | ctrlRejectionReason.X + ctrlRejectionReason.Width\/2 |
| Y                      | ctrlRejectionReason.Y+ctrlRejectionReason.Height     |
| ZIndex                 | 13                                                   |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | btnRejectApproval.BorderColor                                                                                         |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | ColorFade(RGBA(135, 100, 184, 1), \-50%)                                                                              |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(69, 75, 146, 1)</td></tr><tr><td style="background-color:#454B92"></td></tr></table>   |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## btnRejectSelected

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | Reset(ctrlRejectionReason); Reset(RejectionReasonComponent); Navigate('Reject Reason Screen'); Set( isNavigatedFromApprovalsScreen, "Yes" ); Set( varErrorMessageRejection, false ); |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Text     | varStringNew.DenyBtnText |

### Design

| Property               | Value                                                                 |
| ---------------------- | --------------------------------------------------------------------- |
| Align                  | Align.Center                                                          |
| BorderStyle            | BorderStyle.Solid                                                     |
| BorderThickness        | 1                                                                     |
| DisplayMode            | If(IsEmpty(colSelectedApprovals)  \|\| varShowApproved,Disabled,Edit) |
| FocusedBorderThickness | 4                                                                     |
| Font                   | Font.'Segoe UI'                                                       |
| FontWeight             | FontWeight.Semibold                                                   |
| Height                 | 60                                                                    |
| Italic                 | false                                                                 |
| RadiusBottomLeft       | 4                                                                     |
| RadiusBottomRight      | 4                                                                     |
| RadiusTopLeft          | 4                                                                     |
| RadiusTopRight         | 4                                                                     |
| Size                   | 22.5                                                                  |
| Strikethrough          | false                                                                 |
| TabIndex               |                                                                       |
| Underline              | false                                                                 |
| VerticalAlign          | VerticalAlign.Middle                                                  |
| Width                  | (glryApprovals.Width\/2)\-48                                          |
| X                      | rctRejectApproved.X+16                                                |
| Y                      | Parent.Height\-Self.Height\-20                                        |
| ZIndex                 | 17                                                                    |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>    |
| FocusedBorderColor  | btnRejectSelected.BorderColor                                                                                         |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## chkSelectAll

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![checkbox](resources/checkbox.png) | Type: checkbox |

### Behavior

| Property  | Value                                                |
| --------- | ---------------------------------------------------- |
| OnCheck   | ClearCollect( colSelectedApprovals, colMyApprovals ) |
| OnSelect  |                                                      |
| OnUncheck | Clear(colSelectedApprovals)                          |

### Data

| Property | Value |
| -------- | ----- |
| Text     |       |

### Design

| Property               | Value                                                                                                                 |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                      |
| BorderThickness        | 0                                                                                                                     |
| CheckboxBackgroundFill | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CheckboxBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| CheckboxSize           | 50                                                                                                                    |
| CheckmarkFill          | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| DisplayMode            | If(\!varShowApproved,Edit,Disabled)                                                                                   |
| FocusedBorderThickness | 4                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                     |
| Height                 | 50                                                                                                                    |
| Italic                 | false                                                                                                                 |
| PaddingBottom          | 0                                                                                                                     |
| PaddingLeft            | 0                                                                                                                     |
| PaddingRight           | 0                                                                                                                     |
| PaddingTop             | 0                                                                                                                     |
| Size                   | 21                                                                                                                    |
| Strikethrough          | false                                                                                                                 |
| Underline              | false                                                                                                                 |
| VerticalAlign          | VerticalAlign.Top                                                                                                     |
| Visible                | true                                                                                                                  |
| Width                  | 50                                                                                                                    |
| X                      | txtApprovalsSearch.X                                                                                                  |
| Y                      | txtApprovalsSearch.Y +txtApprovalsSearch.Height+8                                                                     |
| ZIndex                 | 9                                                                                                                     |

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
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## chkSelectRequest

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![checkbox](resources/checkbox.png) | Type: checkbox |

### Behavior

| Property  | Value                                                                             |
| --------- | --------------------------------------------------------------------------------- |
| OnCheck   | Collect( colSelectedApprovals, ThisItem )                                         |
| OnSelect  |                                                                                   |
| OnUncheck | Remove( colSelectedApprovals, LookUp( colSelectedApprovals, ID \= ThisItem.ID ) ) |

### Data

| Property | Value                                                                  |
| -------- | ---------------------------------------------------------------------- |
| Default  | If(IsEmpty(Filter(colSelectedApprovals,ID \= ThisItem.ID)),false,true) |
| Text     |                                                                        |

### Design

| Property               | Value                                                                                                                 |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                      |
| BorderThickness        | 0                                                                                                                     |
| CheckboxBackgroundFill | <table border="0"><tr><td>RGBA(244, 243, 242, 1)</td></tr><tr><td style="background-color:#F4F3F2"></td></tr></table> |
| CheckboxBorderColor    | <table border="0"><tr><td>RGBA(231, 230, 230, 1)</td></tr><tr><td style="background-color:#E7E6E6"></td></tr></table> |
| CheckboxSize           | 50                                                                                                                    |
| CheckmarkFill          | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisplayMode            | If(varAppSettings.EnableInlineApprovals,DisplayMode.Edit,DisplayModeDisabled)                                         |
| FocusedBorderThickness | 4                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                     |
| Height                 | 50                                                                                                                    |
| Italic                 | false                                                                                                                 |
| PaddingBottom          | 0                                                                                                                     |
| PaddingLeft            | 0                                                                                                                     |
| PaddingRight           | 0                                                                                                                     |
| PaddingTop             | 0                                                                                                                     |
| Size                   | 21                                                                                                                    |
| Strikethrough          | false                                                                                                                 |
| Underline              | false                                                                                                                 |
| VerticalAlign          | VerticalAlign.Top                                                                                                     |
| Width                  | 50                                                                                                                    |
| X                      | 24                                                                                                                    |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2                                                                               |
| ZIndex                 | 4                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryApprovals |

## ctrlRejectionReason

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value             |
| -------- | ----------------- |
| OnReset  | Reset(txtReason)  |

### Data

| Property       | Value                                                                |
| -------------- | -------------------------------------------------------------------- |
| DisplayMode    | DisplayMode.Edit                                                     |
| HintText       |                                                                      |
| LabelSize      | 20                                                                   |
| Margin         | 40                                                                   |
| Padding        | 5                                                                    |
| Requred        | true                                                                 |
| ResetTextField | true                                                                 |
| Text           | txtReason.Text                                                       |
| TextSize       | 20                                                                   |
| Title          | \/\/varString.RejectionReasonMsg "Please state reason for rejection" |

### Design

| Property | Value                                  |
| -------- | -------------------------------------- |
| Height   | 308                                    |
| Visible  | varShowRejectPopup                     |
| Width    | glryApprovals.Width\-32                |
| X        | glryApprovals.X+16                     |
| Y        | Parent.Height \/2 \- Self.Height \*0.7 |
| ZIndex   | 12                                     |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## galleryTemplate1

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryApprovals |

## glryApprovals

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                                                                                   |
| --------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| Items     | SortByColumns( Search( colMyApprovals, txtApprovalsSearch.Text, "Title", "RequestorNameText", "SpaceName" ), "RequestDate", Ascending ) |
| WrapCount | 1                                                                                                                                       |

### Design

| Property               | Value                                                                |
| ---------------------- | -------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                     |
| BorderThickness        | 2                                                                    |
| DisplayMode            | DisplayMode.Edit                                                     |
| FocusedBorderThickness | 4                                                                    |
| Height                 | Parent.Height\-Self.Y\-100                                           |
| Layout                 | Layout.Vertical                                                      |
| LoadingSpinner         | LoadingSpinner.None                                                  |
| LoadingSpinnerColor    | Self.BorderColor                                                     |
| ShowNavigation         | true                                                                 |
| ShowScrollbar          | false                                                                |
| TemplatePadding        | 0                                                                    |
| TemplateSize           | lblRequestor.Y+lblRequestor.Height+12                                |
| Transition             | Transition.None                                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, (App.DesignWidth\*2) ) |
| X                      | (Parent.Width\-Self.Width) \/2                                       |
| Y                      | lblSelectAll.Y+lblSelectAll.Height+16                                |
| ZIndex                 | 5                                                                    |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 120, 212, 1)</td></tr><tr><td style="background-color:#0078D4"></td></tr></table>   |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(249, 248, 247, 1)</td></tr><tr><td style="background-color:#F9F8F7"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value            |
| -------------- | ---------------- |
| Child Control  | chkSelectRequest |
| Child Control  | icnInformation   |
| Child Control  | galleryTemplate1 |
| Child Control  | requestSeparator |
| Child Control  | lblTitle         |
| Child Control  | imgInfo          |
| Child Control  | lblRequestTitle  |
| Child Control  | lblRequestor     |
| Parent Control | Approval Screen  |

## grpErrorMessage\_1

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
| ZIndex   | 25    |

### Color Properties

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## HeaderControlApproval

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
| IsBackButtonVisible | false                    |
| IsHomeButtonVisible | true                     |
| NavigateHomeScreen  | 'Home Screen'            |
| NavigateScreen      | 'Home Screen'            |
| Text                | varString.ApprovalsTitle |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0                                                                  |
| ZIndex   | 19                                                                 |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## icnErrorCancel\_1

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                               |
| -------- | ----------------------------------- |
| OnSelect | Set(varErrorMessageApproval,false); |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                 |
| BorderThickness        | 0                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Height                 | 32                                                                |
| Icon                   | Icon.Cancel                                                       |
| Visible                | varErrorMessageApproval                                           |
| Width                  | 32                                                                |
| X                      | lblErrorRectangle\_1.X+lblErrorRectangle\_1.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_1.Y+32                                         |
| ZIndex                 | 23                                                                |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## icnFilter

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                          |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | Set( varShowApproved, true ); ClearCollect( colMyApprovals, Filter( BAR\_Requests, ApproverGuid \= varUser.id, Status.Value \= "Approved", DateValue \>\= varTodayFormated ) ) |

### Design

| Property               | Value                                            |
| ---------------------- | ------------------------------------------------ |
| BorderStyle            | BorderStyle.Solid                                |
| BorderThickness        | 0                                                |
| DisplayMode            | DisplayMode.Edit                                 |
| FocusedBorderThickness | 4                                                |
| Height                 | txtApprovalsSearch.Height                        |
| Icon                   | Icon.Filter                                      |
| PaddingLeft            | icnSearchApproval.Height\*20%                    |
| PaddingRight           | icnFilter.PaddingLeft                            |
| Width                  | 0                                                |
| X                      | txtApprovalsSearch.X+txtApprovalsSearch.Width+28 |
| Y                      | txtApprovalsSearch.Y                             |
| ZIndex                 | 15                                               |

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
| HoverColor          | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## icnInformation

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                                                                                      |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | Set( varSelectedRequest, ThisItem ); Set( varSelectedBuilding, LookUp( BAR\_Buildings, ID \= ThisItem.BuildingID ) ); Reset(BuildingCardApprovalDetail); Navigate('Approval Detail Screen'); Set( varErrorMessageApprovalDetails, false ); |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| BorderStyle            | BorderStyle.Solid                       |
| BorderThickness        | 0                                       |
| DisplayMode            | DisplayMode.Edit                        |
| FocusedBorderThickness | 4                                       |
| Height                 | 40                                      |
| Icon                   | Icon.Information                        |
| Width                  | 40                                      |
| X                      | Parent.TemplateWidth \- Self.Width\-30  |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 8                                       |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(164, 163, 162, 1)</td></tr><tr><td style="background-color:#A4A3A2"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledColor       | <table border="0"><tr><td>RGBA(220, 220, 220, 1)</td></tr><tr><td style="background-color:#DCDCDC"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryApprovals |

## icnSearchApproval

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Design

| Property               | Value                                                         |
| ---------------------- | ------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                             |
| BorderThickness        | 0                                                             |
| DisplayMode            | DisplayMode.Edit                                              |
| FocusedBorderThickness | 4                                                             |
| Height                 | txtApprovalsSearch.Height                                     |
| Icon                   | Icon.Search                                                   |
| PaddingBottom          | icnSearchApproval.PaddingLeft                                 |
| PaddingLeft            | icnSearchApproval.Height\*20%                                 |
| PaddingRight           | icnSearchApproval.PaddingLeft                                 |
| PaddingTop             | icnSearchApproval.PaddingLeft                                 |
| Rotation               | 90                                                            |
| Width                  | 55                                                            |
| X                      | txtApprovalsSearch.X+txtApprovalsSearch.Width\-Self.Width\-12 |
| Y                      | txtApprovalsSearch.Y                                          |
| ZIndex                 | 8                                                             |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 36, 1)</td></tr><tr><td style="background-color:#252424"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledColor       | <table border="0"><tr><td>RGBA(220, 220, 220, 1)</td></tr><tr><td style="background-color:#DCDCDC"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | icnSearchApproval.BorderColor                                                                                         |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## imgApprovalBack

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value |
| -------- | ----- |
| Image    |       |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 0                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Height                 | 940                                                                          |
| ImagePosition          | ImagePosition.Fit                                                            |
| ImageRotation          | ImageRotation.None                                                           |
| PaddingBottom          | 0                                                                            |
| PaddingLeft            | 0                                                                            |
| PaddingRight           | 0                                                                            |
| PaddingTop             | 0                                                                            |
| RadiusBottomLeft       | 11.2                                                                         |
| RadiusBottomRight      | 11.2                                                                         |
| RadiusTopLeft          | 11.2                                                                         |
| RadiusTopRight         | 11.2                                                                         |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-20, (App.DesignWidth\*2)\-20 ) |
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | HeaderControlApproval.Y+72                                                   |
| ZIndex                 | 3                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | imgApprovalBack.BorderColor                                                                                           |
| HoverFill           | imgApprovalBack.Fill                                                                                                  |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## imgErrorInfo\_1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Image    | 'Task Complete Copy 4' |

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
| Visible                | varErrorMessageApproval       |
| Width                  | 48                            |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | lblErrorRectangle\_1.Y+64     |
| ZIndex                 | 24                            |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## imgGalleryApprovalsBorder

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value |
| -------- | ----- |
| Image    | ""    |

### Design

| Property               | Value                                  |
| ---------------------- | -------------------------------------- |
| BorderStyle            | BorderStyle.None                       |
| BorderThickness        | 0                                      |
| DisplayMode            | DisplayMode.Edit                       |
| FocusedBorderThickness | 4                                      |
| Height                 | 777                                    |
| ImagePosition          | ImagePosition.Fit                      |
| ImageRotation          | ImageRotation.None                     |
| PaddingBottom          | 0                                      |
| PaddingLeft            | 0                                      |
| PaddingRight           | 0                                      |
| PaddingTop             | 0                                      |
| RadiusBottomLeft       | 11.2                                   |
| RadiusBottomRight      | 11.2                                   |
| RadiusTopLeft          | 11.2                                   |
| RadiusTopRight         | 11.2                                   |
| Width                  | glryApprovals.Width                    |
| X                      | (Parent.Width\-Self.Width) \/2         |
| Y                      | chkSelectAll.Y +chkSelectAll.Height+10 |
| ZIndex                 | 4                                      |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## imgInfo

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value                                                                                                                                                                                  |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Set(varSelectedRequest,ThisItem) ; Set(varSelectedBuilding,LookUp(BAR\_Buildings,ID \= ThisItem.BuildingID)) ;   Reset(BuildingCardApprovalDetail); Navigate('Approval Detail Screen') |

### Data

| Property | Value                         |
| -------- | ----------------------------- |
| Image    | ic\_fluent\_info\_24\_regular |

### Design

| Property               | Value                                  |
| ---------------------- | -------------------------------------- |
| BorderStyle            | BorderStyle.None                       |
| BorderThickness        | 2                                      |
| DisplayMode            | DisplayMode.Edit                       |
| FocusedBorderThickness | 4                                      |
| Height                 | 70                                     |
| ImagePosition          | ImagePosition.Fit                      |
| ImageRotation          | ImageRotation.None                     |
| PaddingBottom          | 10                                     |
| PaddingLeft            | 10                                     |
| PaddingRight           | 10                                     |
| PaddingTop             | 10                                     |
| RadiusBottomLeft       | 0                                      |
| RadiusBottomRight      | 0                                      |
| RadiusTopLeft          | 0                                      |
| RadiusTopRight         | 0                                      |
| Visible                | false                                  |
| Width                  | 55                                     |
| X                      | Parent.TemplateWidth \- Self.Width\-30 |
| Y                      | glryApprovals.X+30                     |
| ZIndex                 | 6                                      |

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
| Parent Control | glryApprovals |

## lblErrorBackRectangle\_1

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                   |
| ---------------------- | ----------------------- |
| BorderStyle            | BorderStyle.None        |
| BorderThickness        | 0                       |
| DisplayMode            | DisplayMode.Edit        |
| FocusedBorderThickness | 4                       |
| Height                 | Parent.Height           |
| Visible                | varErrorMessageApproval |
| Width                  | Parent.Width            |
| X                      | 0                       |
| Y                      | 0                       |
| ZIndex                 | 20                      |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## lblErrorMessage\_1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                   |
| -------- | ----------------------- |
| Live     | Live.Off                |
| Role     | TextRole.Default        |
| Text     | varString.AlertApproved |

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
| Visible                | varErrorMessageApproval                     |
| Width                  | lblErrorRectangle\_1.Width                  |
| X                      | lblErrorRectangle\_1.X                      |
| Y                      | imgErrorInfo\_1.Y+imgErrorInfo\_1.Height+18 |
| ZIndex                 | 22                                          |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## lblErrorRectangle\_1

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
| Visible                | varErrorMessageApproval                                                       |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 21                                                                            |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## lblRequestor

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                 |
| -------- | ----------------------------------------------------- |
| Live     | Live.Off                                              |
| Role     | TextRole.Default                                      |
| Text     | Concatenate(ThisItem.Title, " : ",ThisItem.SpaceName) |

### Design

| Property      | Value                                      |
| ------------- | ------------------------------------------ |
| Align         | Align.Left                                 |
| BorderStyle   | BorderStyle.Solid                          |
| DisplayMode   | DisplayMode.Edit                           |
| Font          | Font.'Segoe UI'                            |
| FontWeight    | FontWeight.Normal                          |
| Height        | 32                                         |
| Overflow      | Overflow.Hidden                            |
| PaddingBottom | 0                                          |
| PaddingLeft   | 0                                          |
| PaddingRight  | 0                                          |
| PaddingTop    | 0                                          |
| Size          | 18                                         |
| VerticalAlign | VerticalAlign.Top                          |
| Width         | Parent.TemplateWidth\- 120                 |
| X             | lblTitle.X                                 |
| Y             | lblRequestTitle.Y+lblRequestTitle.Height+4 |
| ZIndex        | 2                                          |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>    |
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
| Parent Control | glryApprovals |

## lblRequestTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                 |
| -------- | ----------------------------------------------------- |
| Live     | Live.Off                                              |
| Role     | TextRole.Default                                      |
| Text     | Text(ThisItem.RequestDate,"\[$\-en\-US\]dd mmm yyyy") |

### Design

| Property      | Value                                        |
| ------------- | -------------------------------------------- |
| Align         | Align.Left                                   |
| BorderStyle   | BorderStyle.Solid                            |
| DisplayMode   | DisplayMode.Edit                             |
| Font          | Font.'Segoe UI'                              |
| FontWeight    | FontWeight.Normal                            |
| Height        | 32                                           |
| Overflow      | Overflow.Hidden                              |
| PaddingBottom | 0                                            |
| PaddingLeft   | 0                                            |
| PaddingRight  | 0                                            |
| PaddingTop    | 0                                            |
| Size          | 18                                           |
| VerticalAlign | VerticalAlign.Top                            |
| Width         | Parent.TemplateWidth\- 170                   |
| X             | chkSelectRequest.X +chkSelectRequest.Width+8 |
| Y             | lblTitle.Y+lblTitle.Height+4                 |
| ZIndex        | 7                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>    |
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
| Parent Control | glryApprovals |

## lblSelectAll

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value               |
| -------- | ------------------- |
| Live     | Live.Off            |
| Role     | TextRole.Default    |
| Text     | varString.SelectAll |

### Design

| Property               | Value                                 |
| ---------------------- | ------------------------------------- |
| Align                  | Align.Left                            |
| BorderStyle            | BorderStyle.None                      |
| BorderThickness        | 2                                     |
| DisplayMode            | If(\!varShowApproved,Edit,Disabled)   |
| FocusedBorderThickness | 4                                     |
| Font                   | Font.'Segoe UI'                       |
| FontWeight             | FontWeight.Semibold                   |
| Height                 | 50                                    |
| Italic                 | false                                 |
| LineHeight             | 1.2                                   |
| Overflow               | Overflow.Hidden                       |
| PaddingBottom          | 5                                     |
| PaddingLeft            | 5                                     |
| PaddingRight           | 5                                     |
| PaddingTop             | 5                                     |
| Size                   | 21                                    |
| Strikethrough          | false                                 |
| Underline              | false                                 |
| VerticalAlign          | VerticalAlign.Middle                  |
| Visible                | true                                  |
| Width                  | txtApprovalsSearch.Width              |
| X                      | chkSelectAll.X +chkSelectAll.Width +8 |
| Y                      | chkSelectAll.Y                        |
| ZIndex                 | 10                                    |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## lblTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Live     | Live.Off                       |
| Role     | TextRole.Default               |
| Text     | ThisItem.Requestor.DisplayName |

### Design

| Property      | Value                                        |
| ------------- | -------------------------------------------- |
| Align         | Align.Left                                   |
| BorderStyle   | BorderStyle.Solid                            |
| DisplayMode   | DisplayMode.Edit                             |
| Font          | Font.'Segoe UI'                              |
| FontWeight    | FontWeight.Bold                              |
| Height        | 40                                           |
| Overflow      | Overflow.Hidden                              |
| PaddingBottom | 0                                            |
| PaddingLeft   | 0                                            |
| PaddingRight  | 0                                            |
| PaddingTop    | 0                                            |
| Size          | 22.5                                         |
| VerticalAlign | VerticalAlign.Top                            |
| Width         | Parent.TemplateWidth\- 120                   |
| X             | chkSelectRequest.X +chkSelectRequest.Width+8 |
| Y             | 12                                           |
| ZIndex        | 1                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>    |
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
| Parent Control | glryApprovals |

## rctRejectApproved

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 2                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Height                 | 100                                                                          |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-20, (App.DesignWidth\*2)\-20 ) |
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | Parent.Height\-100                                                           |
| ZIndex                 | 16                                                                           |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## requestSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property        | Value                      |
| --------------- | -------------------------- |
| BorderStyle     | BorderStyle.Solid          |
| BorderThickness | 0                          |
| DisplayMode     | DisplayMode.Edit           |
| Height          | 1                          |
| Width           | Parent.TemplateWidth       |
| X               | 0                          |
| Y               | Parent.TemplateHeight \- 1 |
| ZIndex          | 3                          |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | Self.Fill                                                                                                             |
| Fill               | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | Self.Fill                                                                                                             |
| PressedFill        | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value         |
| -------------- | ------------- |
| Parent Control | glryApprovals |

## SeperatorFeedApproval

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Design

| Property               | Value                      |
| ---------------------- | -------------------------- |
| BorderStyle            | BorderStyle.None           |
| BorderThickness        | 2                          |
| DisplayMode            | DisplayMode.Edit           |
| FocusedBorderThickness | 4                          |
| Height                 | 1                          |
| Visible                | false                      |
| Width                  | Parent.Width               |
| X                      | 0                          |
| Y                      | btnApproveSelected.Y \- 15 |
| ZIndex                 | 6                          |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(230, 230, 230, 1)</td></tr><tr><td style="background-color:#E6E6E6"></td></tr></table> |
| FocusedBorderColor | SeperatorFeedApproval.BorderColor                                                                                     |
| HoverFill          | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## shpRectangleBackGround\_ApprovalScreen

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## shpUpperRectangle\_ApprovalScreen

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |

## txtApprovalsSearch

| Property                    | Value      |
| --------------------------- | ---------- |
| ![text](resources/text.png) | Type: text |

### Behavior

| Property | Value |
| -------- | ----- |
| OnChange |       |

### Data

| Property        | Value            |
| --------------- | ---------------- |
| AccessibleLabel | "Search links"   |
| Default         | ""               |
| DelayOutput     | true             |
| HintText        | varString.Search |

### Design

| Property               | Value                                                   |
| ---------------------- | ------------------------------------------------------- |
| Align                  | Align.Left                                              |
| BorderStyle            | BorderStyle.None                                        |
| BorderThickness        | 2                                                       |
| DisplayMode            | DisplayMode.Edit                                        |
| FocusedBorderThickness | 4                                                       |
| Font                   | Font.'Segoe UI'                                         |
| FontWeight             | FontWeight.Normal                                       |
| Format                 | TextFormat.Text                                         |
| Height                 | 55                                                      |
| Italic                 | false                                                   |
| Mode                   | TextMode.SingleLine                                     |
| PaddingLeft            | 10                                                      |
| RadiusBottomLeft       | txtApprovalsSearch.RadiusTopLeft                        |
| RadiusBottomRight      | txtApprovalsSearch.RadiusTopLeft                        |
| RadiusTopLeft          | 5                                                       |
| RadiusTopRight         | txtApprovalsSearch.RadiusTopLeft                        |
| Size                   | 21                                                      |
| Strikethrough          | false                                                   |
| Underline              | false                                                   |
| VirtualKeyboardMode    | VirtualKeyboardMode.Auto                                |
| Width                  | glryApprovals.Width\-48                                 |
| X                      | glryApprovals.X+24                                      |
| Y                      | HeaderControlApproval.Y+HeaderControlApproval.Height+16 |
| ZIndex                 | 7                                                       |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 120, 212, 1)</td></tr><tr><td style="background-color:#0078D4"></td></tr></table>   |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(151, 149, 147, 1)</td></tr><tr><td style="background-color:#979593"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| FocusedBorderColor  | txtApprovalsSearch.BorderColor                                                                                        |
| HoverBorderColor    | ColorFade(txtApprovalsSearch.BorderColor, \-30%)                                                                      |
| HoverColor          | txtApprovalsSearch.Color                                                                                              |
| HoverFill           | txtApprovalsSearch.Fill                                                                                               |
| PressedBorderColor  | txtApprovalsSearch.BorderColor                                                                                        |
| PressedColor        | txtApprovalsSearch.Color                                                                                              |
| PressedFill         | txtApprovalsSearch.Fill                                                                                               |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Approval Screen |
