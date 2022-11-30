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

## Approval Detail Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                               |
| --------- | ----------------------------------- |
| OnHidden  | Set(varBuildingCardVisible,false)   |
| OnVisible | Set( varBuildingCardVisible, true ) |

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

| Property      | Value                                        |
| ------------- | -------------------------------------------- |
| Child Control | shpRectangleBackGround\_ApprovalDetailScreen |
| Child Control | shpUpperRectangle\_ApprovalDetailScreen      |
| Child Control | rctAlertBackDropApprovalDetail               |
| Child Control | HeaderControlApprovalRequestDetails          |
| Child Control | lblErrorBackRectangle\_3                     |
| Child Control | BuildingCardApprovalDetail                   |
| Child Control | imgBuildingDetailsBorder                     |
| Child Control | imgApprovalBackApprovalDetail                |
| Child Control | ctrlRejectionReasonApprovalDetail            |
| Child Control | lblErrorRectangle\_3                         |
| Child Control | icnErrorCancel\_3                            |
| Child Control | imgErrorInfo\_3                              |
| Child Control | lblErrorMessage\_3                           |
| Child Control | btnCancelReject                              |
| Child Control | btnReject                                    |
| Child Control | SeperatorFeedApprovalDetail                  |
| Child Control | imgReasonAccessBorder                        |
| Child Control | btnRequestReject                             |
| Child Control | btnRequestApprove                            |
| Child Control | grpErrorMessage\_3                           |

## btnCancelReject

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

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                 |
| BorderStyle            | BorderStyle.Solid                                                            |
| BorderThickness        | 1                                                                            |
| DisplayMode            | Edit                                                                         |
| FocusedBorderThickness | 4                                                                            |
| Font                   | Font.'Segoe UI'                                                              |
| FontWeight             | FontWeight.Semibold                                                          |
| Height                 | 60                                                                           |
| Italic                 | false                                                                        |
| RadiusBottomLeft       | 4                                                                            |
| RadiusBottomRight      | 4                                                                            |
| RadiusTopLeft          | 4                                                                            |
| RadiusTopRight         | 4                                                                            |
| Size                   | 22.5                                                                         |
| Strikethrough          | false                                                                        |
| TabIndex               |                                                                              |
| Underline              | false                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                         |
| Visible                | varShowRejectPopup                                                           |
| Width                  | ctrlRejectionReasonApprovalDetail.Width\/2                                   |
| X                      | ctrlRejectionReasonApprovalDetail.X                                          |
| Y                      | ctrlRejectionReasonApprovalDetail.Y+ctrlRejectionReasonApprovalDetail.Height |
| ZIndex                 | 13                                                                           |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | btnCancelReject.BorderColor                                                                                           |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## btnReject

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { Active: 0, RejectionReason: ctrlRejectionReasonApprovalDetail.Text, RequestorNotificationSent: false, Status: If( varSelectedRequest.Status.Value \= "Pending Approval", { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 2, Value: "Rejected" }, { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 4, Value: "Revoked" } ) } ); Remove( colMyApprovals, varSelectedRequest ); Notify( If( varSelectedRequest.Status.Value \= "Pending Approval", varString.Rejected, varString.Revoked ), NotificationType.Success, 3000 ); Set( varShowRejectPopup, false ); Back(UnCover); |

### Data

| Property | Value                                                                                                   |
| -------- | ------------------------------------------------------------------------------------------------------- |
| Text     | If(varSelectedRequest.Status.Value \=varString.PendingApproval,varString.Reject,varString.RevokeAccess) |

### Design

| Property               | Value                                                                            |
| ---------------------- | -------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                     |
| BorderStyle            | BorderStyle.None                                                                 |
| BorderThickness        | 2                                                                                |
| DisplayMode            | If(IsBlank(ctrlRejectionReasonApprovalDetail.Text),Disabled,Edit)                |
| FocusedBorderThickness | 4                                                                                |
| Font                   | Font.'Segoe UI'                                                                  |
| FontWeight             | FontWeight.Semibold                                                              |
| Height                 | 60                                                                               |
| Italic                 | false                                                                            |
| RadiusBottomLeft       | 4                                                                                |
| RadiusBottomRight      | 4                                                                                |
| RadiusTopLeft          | 4                                                                                |
| RadiusTopRight         | 4                                                                                |
| Size                   | 22.5                                                                             |
| Strikethrough          | false                                                                            |
| TabIndex               |                                                                                  |
| Underline              | false                                                                            |
| VerticalAlign          | VerticalAlign.Middle                                                             |
| Visible                | varShowRejectPopup                                                               |
| Width                  | ctrlRejectionReasonApprovalDetail.Width\/2                                       |
| X                      | ctrlRejectionReasonApprovalDetail.X + ctrlRejectionReasonApprovalDetail.Width\/2 |
| Y                      | ctrlRejectionReasonApprovalDetail.Y+ctrlRejectionReasonApprovalDetail.Height     |
| ZIndex                 | 12                                                                               |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | btnReject.BorderColor                                                                                                 |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | ColorFade(Self.Fill, \-10%)                                                                                           |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## btnRequestApprove

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                       |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 1, Value: "Approved" } } ); Remove( colMyApprovals, varSelectedRequest ); Set( varErrorMessageApprovalDetails, true ); |

### Data

| Property | Value                |
| -------- | -------------------- |
| Text     | varString.ApproveLbl |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                  |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 0                                                                             |
| DisplayMode            | If(varSelectedRequest.Status.Value \<\> "Approved",DisplayMode.Edit,Disabled) |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Semibold                                                           |
| Height                 | 60                                                                            |
| Italic                 | false                                                                         |
| RadiusBottomLeft       | 4                                                                             |
| RadiusBottomRight      | 4                                                                             |
| RadiusTopLeft          | 4                                                                             |
| RadiusTopRight         | 4                                                                             |
| Size                   | 22.5                                                                          |
| Strikethrough          | false                                                                         |
| TabIndex               |                                                                               |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Width                  | BuildingCardApprovalDetail.Width\/2 \- 25                                     |
| X                      | btnRequestReject.X + btnRequestReject.Width + 25                              |
| Y                      | Parent.Height \- Self.Height \- 15                                            |
| ZIndex                 | 6                                                                             |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| FocusedBorderColor  | btnRequestApprove.BorderColor                                                                                         |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## btnRequestReject

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                            |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Reset(ctrlRejectionReasonApprovalDetail); Navigate('Reject Reason Screen'); Set( isNavigatedFromApprovalsScreen, "No" ); Set( varErrorMessageRejection, false ); |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Text     | varStringNew.DenyBtnText |

### Design

| Property               | Value                                     |
| ---------------------- | ----------------------------------------- |
| Align                  | Align.Center                              |
| BorderStyle            | BorderStyle.Solid                         |
| BorderThickness        | 1                                         |
| DisplayMode            | DisplayMode.Edit                          |
| FocusedBorderThickness | 2                                         |
| Font                   | Font.'Segoe UI'                           |
| FontWeight             | FontWeight.Semibold                       |
| Height                 | 60                                        |
| Italic                 | false                                     |
| RadiusBottomLeft       | 4                                         |
| RadiusBottomRight      | 4                                         |
| RadiusTopLeft          | 4                                         |
| RadiusTopRight         | 4                                         |
| Size                   | 22.5                                      |
| Strikethrough          | false                                     |
| TabIndex               |                                           |
| Underline              | false                                     |
| VerticalAlign          | VerticalAlign.Middle                      |
| Visible                | true                                      |
| Width                  | BuildingCardApprovalDetail.Width\/2 \- 25 |
| X                      | BuildingCardApprovalDetail.X + 15         |
| Y                      | Parent.Height \- Self.Height \- 15        |
| ZIndex                 | 9                                         |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>    |
| FocusedBorderColor  | btnRequestReject.BorderColor                                                                                          |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## BuildingCardApprovalDetail

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property                    | Value                                                                                                                                             |
| --------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| Addresslbl                  | varString.Address                                                                                                                                 |
| ApprovedBy                  | varSelectedRequest.Approver.DisplayName                                                                                                           |
| Building                    | varSelectedBuilding                                                                                                                               |
| BuildingInfo                | Switch(varSelectedBuilding.'Building Type'.Value,"Monitored",varString.CheckinMonitored,"Unmonitored",varString.CheckinUnmonitored)               |
| BuildingTypelbl             | varString.BuildingType                                                                                                                            |
| CustomProperty1             | "Text"                                                                                                                                            |
| OnsiteAccessInstructionslbl | varString.OnsiteAccessInstructions                                                                                                                |
| Padding                     | 60                                                                                                                                                |
| RequestApprovedbylbl        | varString.Approver                                                                                                                                |
| RequestDate                 | varSelectedRequest.RequestDate                                                                                                                    |
| RequestDatelbl              | varString.RequestDate&":"                                                                                                                         |
| Requestor                   | varSelectedRequest.Requestor.DisplayName                                                                                                          |
| RequestReason               | If(Len(varSelectedRequest.RequestReason) \> 75, Concatenate(Left(varSelectedRequest.RequestReason, 72), "..."), varSelectedRequest.RequestReason) |
| RequestReasonLbl            | varString.BusinessReasonForAccess&":"                                                                                                             |
| ReuquestedBylbl             | varString.RequestedBy&":"                                                                                                                         |
| Space                       | varSelectedRequest.TimeSlot & " \- " & varSelectedRequest.SpaceName & " , " & varSelectedRequest.Title                                            |
| Spacelbl                    | varStringNew.TimeSlotNSpaceLbl2                                                                                                                   |
| SpaceReservedLbl            | varString.BuildingDetailsComponentSpaceReserved                                                                                                   |
| SubmittedDateValue          | varSelectedRequest.Created                                                                                                                        |

### Design

| Property | Value                                                                             |
| -------- | --------------------------------------------------------------------------------- |
| Height   | 966                                                                               |
| Visible  | varBuildingCardVisible                                                            |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width\-32, (App.DesignWidth\*2)\-32 )      |
| X        | (Parent.Width\-Self.Width)\/2                                                     |
| Y        | HeaderControlApprovalRequestDetails.Y+ HeaderControlApprovalRequestDetails.Height |
| ZIndex   | 5                                                                                 |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## ctrlRejectionReasonApprovalDetail

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value             |
| -------- | ----------------- |
| OnReset  | Reset(txtReason)  |

### Data

| Property       | Value                                                                                                                                                                                                                                                             |
| -------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DisplayMode    | DisplayMode.Edit                                                                                                                                                                                                                                                  |
| HintText       |                                                                                                                                                                                                                                                                   |
| LabelSize      | 20                                                                                                                                                                                                                                                                |
| Margin         | 40                                                                                                                                                                                                                                                                |
| Padding        | 5                                                                                                                                                                                                                                                                 |
| Requred        | true                                                                                                                                                                                                                                                              |
| ResetTextField | true                                                                                                                                                                                                                                                              |
| Text           | txtReason.Text                                                                                                                                                                                                                                                    |
| TextSize       | 20                                                                                                                                                                                                                                                                |
| Title          | \/\/If(varSelectedRequest.Status.Value \=varString.PendingApproval,varString.RejectionReasonMsg,varString.RevokeReasonMsg) If(varSelectedRequest.Status.Value \="Pending Approval","Please state reason for rejection","Please state reason for revoking access") |

### Design

| Property | Value                                     |
| -------- | ----------------------------------------- |
| Height   | 308                                       |
| Visible  | varShowRejectPopup                        |
| Width    | imgApprovalBackApprovalDetail.Width \- 20 |
| X        | imgApprovalBackApprovalDetail.X+10        |
| Y        | Parent.Height \/2 \- Self.Height \*0.7    |
| ZIndex   | 11                                        |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## grpErrorMessage\_3

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
| ZIndex   | 20    |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## HeaderControlApprovalRequestDetails

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
| NavigateScreen      | 'Approval Screen'        |
| Text                | varString.ApprovalsTitle |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0                                                                  |
| ZIndex   | 14                                                                 |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## icnErrorCancel\_3

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                 |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Set(varErrorMessageApprovalDetails,false); Set(varErrorMessageApproval,false); Navigate('Approval Screen', ScreenTransition.UnCover); |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                 |
| BorderThickness        | 0                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Height                 | 32                                                                |
| Icon                   | Icon.Cancel                                                       |
| Visible                | varErrorMessageApprovalDetails                                    |
| Width                  | 32                                                                |
| X                      | lblErrorRectangle\_3.X+lblErrorRectangle\_3.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_3.Y+32                                         |
| ZIndex                 | 18                                                                |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## imgApprovalBackApprovalDetail

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
| Height                 | SeperatorFeedApproval.Y \- Self.Y \-10                                       |
| ImagePosition          | ImagePosition.Fit                                                            |
| ImageRotation          | ImageRotation.None                                                           |
| PaddingBottom          | 0                                                                            |
| PaddingLeft            | 0                                                                            |
| PaddingRight           | 0                                                                            |
| PaddingTop             | 0                                                                            |
| RadiusBottomLeft       | 0                                                                            |
| RadiusBottomRight      | 0                                                                            |
| RadiusTopLeft          | 0                                                                            |
| RadiusTopRight         | 0                                                                            |
| Transparency           | 0                                                                            |
| Visible                | false                                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-32, (App.DesignWidth\*2)\-32 ) |
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | HeaderControlApproval.Height+10                                              |
| ZIndex                 | 3                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | White                                                                                                                 |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | imgApprovalBackApprovalDetail.BorderColor                                                                             |
| HoverFill           | imgApprovalBackApprovalDetail.Fill                                                                                    |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## imgBuildingDetailsBorder

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value |
| -------- | ----- |
| Image    | ""    |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| BorderStyle            | BorderStyle.Solid                       |
| BorderThickness        | 2                                       |
| DisplayMode            | DisplayMode.Edit                        |
| FocusedBorderThickness | 4                                       |
| Height                 | BuildingCardApprovalDetail.Height+10    |
| ImagePosition          | ImagePosition.Fit                       |
| ImageRotation          | ImageRotation.None                      |
| PaddingBottom          | 0                                       |
| PaddingLeft            | 0                                       |
| PaddingRight           | 0                                       |
| PaddingTop             | 0                                       |
| RadiusBottomLeft       | 11.2                                    |
| RadiusBottomRight      | 11.2                                    |
| RadiusTopLeft          | 11.2                                    |
| RadiusTopRight         | 11.2                                    |
| Visible                | false                                   |
| Width                  | imgApprovalBackApprovalDetail.Width\-20 |
| X                      | imgApprovalBackApprovalDetail.X+10      |
| Y                      | imgApprovalBack.Y+10                    |
| ZIndex                 | 4                                       |

### Color Properties

| Property            | Value                                                                                                               |
| ------------------- | ------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 120, 212, 1)</td></tr><tr><td style="background-color:#0078D4"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| FocusedBorderColor  | Self.BorderColor                                                                                                    |
| HoverBorderColor    | Self.BorderColor                                                                                                    |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| PressedBorderColor  | Self.BorderColor                                                                                                    |
| PressedFill         | Self.Fill                                                                                                           |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## imgErrorInfo\_3

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Image    | 'Task Complete Copy 4' |

### Design

| Property               | Value                          |
| ---------------------- | ------------------------------ |
| BorderStyle            | BorderStyle.None               |
| BorderThickness        | 2                              |
| DisplayMode            | DisplayMode.Edit               |
| FocusedBorderThickness | 4                              |
| Height                 | 48                             |
| ImagePosition          | ImagePosition.Fit              |
| ImageRotation          | ImageRotation.None             |
| PaddingBottom          | 0                              |
| PaddingLeft            | 0                              |
| PaddingRight           | 0                              |
| PaddingTop             | 0                              |
| RadiusBottomLeft       | 0                              |
| RadiusBottomRight      | 0                              |
| RadiusTopLeft          | 0                              |
| RadiusTopRight         | 0                              |
| Visible                | varErrorMessageApprovalDetails |
| Width                  | 48                             |
| X                      | (Parent.Width\-Self.Width)\/2  |
| Y                      | lblErrorRectangle\_3.Y+64      |
| ZIndex                 | 19                             |

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
| Parent Control | Approval Detail Screen |

## imgReasonAccessBorder

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value |
| -------- | ----- |
| Image    | ""    |

### Design

| Property               | Value                                                          |
| ---------------------- | -------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                              |
| BorderThickness        | 2                                                              |
| DisplayMode            | DisplayMode.Edit                                               |
| FocusedBorderThickness | 4                                                              |
| Height                 | BuildingCardApprovalDetail.Height+20                           |
| ImagePosition          | ImagePosition.Fit                                              |
| ImageRotation          | ImageRotation.None                                             |
| PaddingBottom          | 0                                                              |
| PaddingLeft            | 0                                                              |
| PaddingRight           | 0                                                              |
| PaddingTop             | 0                                                              |
| RadiusBottomLeft       | 11.2                                                           |
| RadiusBottomRight      | 11.2                                                           |
| RadiusTopLeft          | 11.2                                                           |
| RadiusTopRight         | 11.2                                                           |
| Visible                | false                                                          |
| Width                  | imgApprovalBackApprovalDetail.Width\-20                        |
| X                      | imgApprovalBackApprovalDetail.X+10                             |
| Y                      | BuildingCardApprovalDetail.Y+BuildingCardApprovalDetail.Height |
| ZIndex                 | 8                                                              |

### Color Properties

| Property            | Value                                                                                                               |
| ------------------- | ------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 120, 212, 1)</td></tr><tr><td style="background-color:#0078D4"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| FocusedBorderColor  | Self.BorderColor                                                                                                    |
| HoverBorderColor    | Self.BorderColor                                                                                                    |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>     |
| PressedBorderColor  | Self.BorderColor                                                                                                    |
| PressedFill         | Self.Fill                                                                                                           |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## lblErrorBackRectangle\_3

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                          |
| ---------------------- | ------------------------------ |
| BorderStyle            | BorderStyle.None               |
| BorderThickness        | 0                              |
| DisplayMode            | DisplayMode.Edit               |
| FocusedBorderThickness | 4                              |
| Height                 | Parent.Height                  |
| Visible                | varErrorMessageApprovalDetails |
| Width                  | Parent.Width                   |
| X                      | 0                              |
| Y                      | 0                              |
| ZIndex                 | 15                             |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## lblErrorMessage\_3

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
| Visible                | varErrorMessageApprovalDetails              |
| Width                  | lblErrorRectangle\_3.Width                  |
| X                      | lblErrorRectangle\_3.X                      |
| Y                      | imgErrorInfo\_3.Y+imgErrorInfo\_3.Height+18 |
| ZIndex                 | 17                                          |

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
| Parent Control | Approval Detail Screen |

## lblErrorRectangle\_3

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
| Visible                | varErrorMessageApprovalDetails                                                |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 16                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## rctAlertBackDropApprovalDetail

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
| ZIndex                 | 10                 |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## SeperatorFeedApprovalDetail

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Design

| Property               | Value                     |
| ---------------------- | ------------------------- |
| BorderStyle            | BorderStyle.None          |
| BorderThickness        | 2                         |
| DisplayMode            | DisplayMode.Edit          |
| FocusedBorderThickness | 4                         |
| Height                 | 1                         |
| Visible                | false                     |
| Width                  | Parent.Width              |
| X                      | 0                         |
| Y                      | btnRequestApprove.Y \- 15 |
| ZIndex                 | 7                         |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(230, 230, 230, 1)</td></tr><tr><td style="background-color:#E6E6E6"></td></tr></table> |
| FocusedBorderColor | SeperatorFeedApprovalDetail.BorderColor                                                                               |
| HoverFill          | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Approval Detail Screen |

## shpRectangleBackGround\_ApprovalDetailScreen

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
| Parent Control | Approval Detail Screen |

## shpUpperRectangle\_ApprovalDetailScreen

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
| Parent Control | Approval Detail Screen |
