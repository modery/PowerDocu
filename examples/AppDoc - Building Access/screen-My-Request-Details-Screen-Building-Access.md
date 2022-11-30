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

## My Request Details Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                       |
| --------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | Set( varSelectedRequest, First(colSelectedRequest) ); Set( varSelectedBuilding, LookUp( BAR\_Buildings, ID \= varSelectedRequest.BuildingID ) ); Set( varBuildingCardVisible, true ); Set( VarRequestIstoday, varSelectedRequest.DateValue \= varTodayFormated ); Clear(colAlertComponent); |

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

| Property      | Value                                          |
| ------------- | ---------------------------------------------- |
| Child Control | shpRectangleBackGround\_MyRequestDetailsScreen |
| Child Control | shpUpperRectangle\_MyRequestDetailsScreen      |
| Child Control | HeaderControlRequestDetails                    |
| Child Control | rectBackDrop                                   |
| Child Control | lblErrorBackRectangle\_5                       |
| Child Control | lblErrorBackRectangle\_KeyquestionsScreen\_1   |
| Child Control | RequestDetailsCanvas                           |
| Child Control | RequestQRCode                                  |
| Child Control | lblErrorRectangle\_KeyquestionsScreen\_1       |
| Child Control | lblErrorRectangle\_5                           |
| Child Control | rectPopupBox                                   |
| Child Control | lblAlertTitle                                  |
| Child Control | icnErrorCancel\_5                              |
| Child Control | icnErrorCancel\_KeyquestionsScreen\_1          |
| Child Control | imgErrorInfo\_5                                |
| Child Control | imgErrorInfo\_KeyquestionsScreen\_1            |
| Child Control | lblAlertText                                   |
| Child Control | lblErrorMessage\_KeyquestionsScreen\_1         |
| Child Control | lblErrorMessage\_5                             |
| Child Control | rectOptionsBox                                 |
| Child Control | btnNo                                          |
| Child Control | btnYes                                         |
| Child Control | lblCheckInTime                                 |
| Child Control | lblCheckOutTime                                |
| Child Control | btnCheckIn                                     |
| Child Control | btnCheckOut                                    |
| Child Control | btnWithdraw                                    |
| Child Control | GrpCheckInCheckOut                             |
| Child Control | GrpWithdrawAlert                               |
| Child Control | grpCheckOutSuccessMessage                      |
| Child Control | grpCheckInSuccessMessage                       |

## btnCheckIn

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If( varAppSettings.KeyQuestions, Navigate('Key Questions Screen'); ClearCollect( colBuildingRequests, Filter( BAR\_Requests, Active \= 1, BuildingID \= varSelectedBuilding.ID, DateValue \= varTodayFormated ) ); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ), Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { CheckInTime: Now(), 'CheckIn Status': { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 0, Value: "Checked\-In" } } ); ClearCollect( colBuildingRequests, Filter( BAR\_Requests, Active \= 1, BuildingID \= varSelectedBuilding.ID, DateValue \= varTodayFormated ) ); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varSuccessCheckInMessage, true ) ); Refresh(BAR\_Requests); |

### Data

| Property | Value                      |
| -------- | -------------------------- |
| Text     | varStringNew.CheckInBtnTxt |

### Design

| Property               | Value                                                                                                                                                                                                                                                                |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                                                                         |
| BorderStyle            | BorderStyle.None                                                                                                                                                                                                                                                     |
| BorderThickness        | 2                                                                                                                                                                                                                                                                    |
| DisplayMode            | If(IsBlank(varSelectedRequest.CheckInTime) && varSelectedRequest.Status.Value \="Approved" && varSelectedRequest.CheckIn\_x0020\_Status.Value \<\> "Disqualified" && varSelectedRequest.CheckIn\_x0020\_Status.Value \<\> "Checked\-In", DisplayMode.Edit, Disabled) |
| FocusedBorderThickness | 4                                                                                                                                                                                                                                                                    |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                                                                                      |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                                                                                                  |
| Height                 | 60                                                                                                                                                                                                                                                                   |
| Italic                 | false                                                                                                                                                                                                                                                                |
| RadiusBottomLeft       | 4                                                                                                                                                                                                                                                                    |
| RadiusBottomRight      | 4                                                                                                                                                                                                                                                                    |
| RadiusTopLeft          | 4                                                                                                                                                                                                                                                                    |
| RadiusTopRight         | 4                                                                                                                                                                                                                                                                    |
| Size                   | 22.5                                                                                                                                                                                                                                                                 |
| Strikethrough          | false                                                                                                                                                                                                                                                                |
| Underline              | false                                                                                                                                                                                                                                                                |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                                                                                 |
| Visible                | If ( varSelectedBuilding.'Building Type'.Value \= "Unmonitored", If( varSelectedRequest.RequestDate \= Today(), true, false), false )                                                                                                                                |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40                                                                                                                                                                                          |
| X                      | btnWithdraw.X                                                                                                                                                                                                                                                        |
| Y                      | btnWithdraw.Y\-20\-Self.Height                                                                                                                                                                                                                                       |
| ZIndex                 | 6                                                                                                                                                                                                                                                                    |

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
| HoverBorderColor    | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## btnCheckOut

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { CheckOutTime: Now(), 'CheckIn Status': { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 1, Value: "Checked\-Out" } } ); ClearCollect( colBuildingRequests, Filter( BAR\_Requests, Active \= 1, BuildingID \= varSelectedBuilding.ID, DateValue \= varTodayFormated ) ); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varSuccessCheckoutMessage, true ); |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | varStringNew.CheckOutBtnTxt |

### Design

| Property               | Value                                                                                                                                                                                                                                                                                                                |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                                                                                                                         |
| BorderStyle            | BorderStyle.None                                                                                                                                                                                                                                                                                                     |
| BorderThickness        | 2                                                                                                                                                                                                                                                                                                                    |
| DisplayMode            | If(\!IsBlank(varSelectedRequest.CheckInTime) && IsBlank(varSelectedRequest.CheckOutTime) && varSelectedRequest.Status.Value \="Approved" && varSelectedRequest.CheckIn\_x0020\_Status.Value \<\> "Disqualified" && varSelectedRequest.CheckIn\_x0020\_Status.Value \<\> "Checked\-Out" , DisplayMode.Edit, Disabled) |
| FocusedBorderThickness | 4                                                                                                                                                                                                                                                                                                                    |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                                                                                                                                      |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                                                                                                                                                  |
| Height                 | 60                                                                                                                                                                                                                                                                                                                   |
| Italic                 | false                                                                                                                                                                                                                                                                                                                |
| RadiusBottomLeft       | 4                                                                                                                                                                                                                                                                                                                    |
| RadiusBottomRight      | 4                                                                                                                                                                                                                                                                                                                    |
| RadiusTopLeft          | 4                                                                                                                                                                                                                                                                                                                    |
| RadiusTopRight         | 4                                                                                                                                                                                                                                                                                                                    |
| Size                   | 22.5                                                                                                                                                                                                                                                                                                                 |
| Strikethrough          | false                                                                                                                                                                                                                                                                                                                |
| Underline              | false                                                                                                                                                                                                                                                                                                                |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                                                                                                                                 |
| Visible                | If ( varSelectedBuilding.'Building Type'.Value \= "Unmonitored", If( varSelectedRequest.RequestDate \= Today(), true, false), false )                                                                                                                                                                                |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40                                                                                                                                                                                                                                          |
| X                      | btnCheckIn.Width + btnCheckIn.X + 20                                                                                                                                                                                                                                                                                 |
| Y                      | btnWithdraw.Y\-20\-Self.Height                                                                                                                                                                                                                                                                                       |
| ZIndex                 | 7                                                                                                                                                                                                                                                                                                                    |

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
| PressedBorderColor  | ColorFade(RGBA(135, 100, 184, 1), \-50%)                                                                              |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(69, 75, 146, 1)</td></tr><tr><td style="background-color:#454B92"></td></tr></table>   |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## btnNo

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                             |
| -------- | ----------------------------------------------------------------- |
| OnSelect | ClearCollect( colAlertComponent, { Result: 1, Visible: false } ); |

### Data

| Property | Value            |
| -------- | ---------------- |
| Text     | varString.Cancel |

### Design

| Property               | Value                                                                                                            |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                     |
| BorderStyle            | BorderStyle.Solid                                                                                                |
| BorderThickness        | 1                                                                                                                |
| DisplayMode            | DisplayMode.Edit                                                                                                 |
| FocusedBorderThickness | 4                                                                                                                |
| Font                   | Font.'Segoe UI'                                                                                                  |
| FontWeight             | FontWeight.Semibold                                                                                              |
| Height                 | 50                                                                                                               |
| Italic                 | false                                                                                                            |
| RadiusBottomLeft       | 4                                                                                                                |
| RadiusBottomRight      | 4                                                                                                                |
| RadiusTopLeft          | 4                                                                                                                |
| RadiusTopRight         | 4                                                                                                                |
| Size                   | 21                                                                                                               |
| Strikethrough          | false                                                                                                            |
| Underline              | false                                                                                                            |
| VerticalAlign          | VerticalAlign.Middle                                                                                             |
| Visible                | First(colAlertComponent).Visible                                                                                 |
| Width                  | \/\/rectPopupBox.Width\*30% If( Parent.Size\=ScreenSize.Small, rectPopupBox.Width, rectPopupBox.Width )\/2 \- 40 |
| X                      | rectPopupBox.X +25                                                                                               |
| Y                      | (rectPopupBox.Y +rectPopupBox.Height) \- Self.Height \- 20                                                       |
| ZIndex                 | 17                                                                                                               |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## btnWithdraw

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                    |
| -------- | -------------------------------------------------------------------------------------------------------- |
| OnSelect | Set( varRequestAction, "Withdraw" ); ClearCollect( colAlertComponent, { Result: \-22, Visible: true } ); |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | varStringNew.WithDrawBtnTxt |

### Design

| Property               | Value                                                                                                                                                                                                                                                                                               |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                                                                                                        |
| BorderStyle            | BorderStyle.Solid                                                                                                                                                                                                                                                                                   |
| BorderThickness        | 1                                                                                                                                                                                                                                                                                                   |
| DisplayMode            | If(varSelectedRequest.Status.Value in \["Rejected","Withdrawn"\]\|\|varSelectedRequest.CheckIn\_x0020\_Status.Value\="Checked\-In"\|\|varSelectedRequest.CheckIn\_x0020\_Status.Value\="Checked\-Out"\|\|varSelectedRequest.CheckIn\_x0020\_Status.Value\="Disqualified",DisplayMode.Disabled,Edit) |
| FocusedBorderThickness | 4                                                                                                                                                                                                                                                                                                   |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                                                                                                                     |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                                                                                                                                 |
| Height                 | 60                                                                                                                                                                                                                                                                                                  |
| Italic                 | false                                                                                                                                                                                                                                                                                               |
| RadiusBottomLeft       | 4                                                                                                                                                                                                                                                                                                   |
| RadiusBottomRight      | 4                                                                                                                                                                                                                                                                                                   |
| RadiusTopLeft          | 4                                                                                                                                                                                                                                                                                                   |
| RadiusTopRight         | 4                                                                                                                                                                                                                                                                                                   |
| Size                   | 22.5                                                                                                                                                                                                                                                                                                |
| Strikethrough          | false                                                                                                                                                                                                                                                                                               |
| Underline              | false                                                                                                                                                                                                                                                                                               |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                                                                                                                |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) \- 60                                                                                                                                                                                                                            |
| X                      | (Parent.Width\-Self.Width) \/2                                                                                                                                                                                                                                                                      |
| Y                      | (Parent.Height\-Self.Height\-20)                                                                                                                                                                                                                                                                    |
| ZIndex                 | 8                                                                                                                                                                                                                                                                                                   |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(189, 189, 230, 1)</td></tr><tr><td style="background-color:#BDBDE6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| HoverColor          | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| HoverFill           | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(200, 198, 196, 1)</td></tr><tr><td style="background-color:#C8C6C4"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedFill         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## btnYes

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | ClearCollect( colAlertComponent, { Result: 1, Visible: false } ); Set( varShowLoading, true ); \/\/Patches the selected request based on which action is being performed If( varSelectedRequest.TimeSlot \= varStringNew.FullDayLbl2, UpdateIf( BAR\_Requests, varSelectedRequest.Request\_x0020\_Collection\_x0020\_I in 'Request Collection ID' && varSelectedRequest.DateValue in Text(DateValue), { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 3, Value: "Withdrawn" }, Active: 0 } ); , Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { Status: { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 3, Value: "Withdrawn" }, Active: 0 } ) ); Set( varShowLoading, false ); Navigate( \[@'My Request List Screen'\], ScreenTransition.Fade ); |

### Data

| Property | Value                                                                                                                    |
| -------- | ------------------------------------------------------------------------------------------------------------------------ |
| Text     | Switch( varRequestAction, "Withdraw", varString.Withdraw, "CheckIn", varString.CheckIn, "CheckOut", varString.CheckOut ) |

### Design

| Property               | Value                                                                                                            |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                     |
| BorderStyle            | BorderStyle.None                                                                                                 |
| BorderThickness        | 0                                                                                                                |
| DisplayMode            | DisplayMode.Edit                                                                                                 |
| FocusedBorderThickness | 4                                                                                                                |
| Font                   | Font.'Segoe UI'                                                                                                  |
| FontWeight             | FontWeight.Semibold                                                                                              |
| Height                 | 50                                                                                                               |
| Italic                 | false                                                                                                            |
| RadiusBottomLeft       | 4                                                                                                                |
| RadiusBottomRight      | 4                                                                                                                |
| RadiusTopLeft          | 4                                                                                                                |
| RadiusTopRight         | 4                                                                                                                |
| Size                   | 21                                                                                                               |
| Strikethrough          | false                                                                                                            |
| Underline              | false                                                                                                            |
| VerticalAlign          | VerticalAlign.Middle                                                                                             |
| Visible                | First(colAlertComponent).Visible                                                                                 |
| Width                  | \/\/rectPopupBox.Width\*30% If( Parent.Size\=ScreenSize.Small, rectPopupBox.Width, rectPopupBox.Width )\/2 \- 40 |
| X                      | btnNo.X+btnNo.Width + 24                                                                                         |
| Y                      | (rectPopupBox.Y +rectPopupBox.Height) \- Self.Height \- 20                                                       |
| ZIndex                 | 12                                                                                                               |

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
| PressedBorderColor  | ColorFade(RGBA(135, 100, 184, 1), \-50%)                                                                              |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## GrpCheckInCheckOut

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![group](resources/group.png) | Type: group |

### Design

| Property | Value |
| -------- | ----- |
| Height   | 5     |
| Width    | 5     |
| X        | 20    |
| Y        | 20    |
| ZIndex   | 37    |

### Color Properties

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## grpCheckInSuccessMessage

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
| ZIndex   | 28    |

### Color Properties

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## grpCheckOutSuccessMessage

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
| ZIndex   | 23    |

### Color Properties

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## GrpWithdrawAlert

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
| ZIndex   | 33    |

### Color Properties

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## HeaderControlRequestDetails

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
| NavigateScreen      | 'My Request List Screen'  |
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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## htmlOnsiteText

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Data

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| HtmlText | If( IsBlank(varSelectedBuilding.OnsiteAccessInstructions), "\-", varSelectedBuilding.OnsiteAccessInstructions ) |

### Design

| Property        | Value                                                      |
| --------------- | ---------------------------------------------------------- |
| AutoHeight      | false                                                      |
| BorderStyle     | BorderStyle.Solid                                          |
| BorderThickness | 2                                                          |
| DisplayMode     | DisplayMode.Edit                                           |
| Font            | Font.'Segoe UI'                                            |
| Height          | 225                                                        |
| Size            | 22.5                                                       |
| Width           | Parent.Width\-20                                           |
| X               | 0                                                          |
| Y               | lblMyRequestInstructions.Y+lblMyRequestInstructions.Height |
| ZIndex          | 19                                                         |

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
| Parent Control | RequestDetailsDataCard |

## icnErrorCancel\_5

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                    |
| -------- | ---------------------------------------- |
| OnSelect | Set( varSuccessCheckoutMessage, false ); |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                 |
| BorderThickness        | 0                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Height                 | 32                                                                |
| Icon                   | Icon.Cancel                                                       |
| Visible                | varSuccessCheckoutMessage                                         |
| Width                  | 32                                                                |
| X                      | lblErrorRectangle\_5.X+lblErrorRectangle\_5.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_5.Y+32                                         |
| ZIndex                 | 21                                                                |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## icnErrorCancel\_KeyquestionsScreen\_1

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                            |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | Refresh(BAR\_Requests); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varSuccessCheckInMessage, false ); |

### Design

| Property               | Value                                                                                                     |
| ---------------------- | --------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                                         |
| BorderThickness        | 0                                                                                                         |
| DisplayMode            | DisplayMode.Edit                                                                                          |
| FocusedBorderThickness | 4                                                                                                         |
| Height                 | 32                                                                                                        |
| Icon                   | Icon.Cancel                                                                                               |
| Visible                | varSuccessCheckInMessage                                                                                  |
| Width                  | 32                                                                                                        |
| X                      | lblErrorRectangle\_KeyquestionsScreen\_1.X+lblErrorRectangle\_KeyquestionsScreen\_1.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_KeyquestionsScreen\_1.Y+32                                                             |
| ZIndex                 | 26                                                                                                        |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## imgErrorInfo\_5

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
| Visible                | varSuccessCheckoutMessage     |
| Width                  | 48                            |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | lblErrorRectangle\_5.Y+64     |
| ZIndex                 | 22                            |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## imgErrorInfo\_KeyquestionsScreen\_1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Image    | 'Task Complete Copy 4' |

### Design

| Property               | Value                                         |
| ---------------------- | --------------------------------------------- |
| BorderStyle            | BorderStyle.None                              |
| BorderThickness        | 2                                             |
| DisplayMode            | DisplayMode.Edit                              |
| FocusedBorderThickness | 4                                             |
| Height                 | 48                                            |
| ImagePosition          | ImagePosition.Fit                             |
| ImageRotation          | ImageRotation.None                            |
| PaddingBottom          | 0                                             |
| PaddingLeft            | 0                                             |
| PaddingRight           | 0                                             |
| PaddingTop             | 0                                             |
| RadiusBottomLeft       | 0                                             |
| RadiusBottomRight      | 0                                             |
| RadiusTopLeft          | 0                                             |
| RadiusTopRight         | 0                                             |
| Visible                | varSuccessCheckInMessage                      |
| Width                  | 48                                            |
| X                      | (Parent.Width\-Self.Width)\/2                 |
| Y                      | lblErrorRectangle\_KeyquestionsScreen\_1.Y+64 |
| ZIndex                 | 27                                            |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblAlertText

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                                             |
| -------- | --------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                          |
| Role     | TextRole.Default                                                                                                                  |
| Text     | Switch( varRequestAction, "Withdraw", varString.WithdrawMsg, "CheckIn", varString.CheckinMsg, "CheckOut", varString.CheckoutMsg ) |

### Design

| Property               | Value                                    |
| ---------------------- | ---------------------------------------- |
| Align                  | Align.Left                               |
| BorderStyle            | BorderStyle.None                         |
| BorderThickness        | 2                                        |
| DisplayMode            | DisplayMode.Edit                         |
| FocusedBorderThickness | 4                                        |
| Font                   | Font.'Segoe UI'                          |
| FontWeight             | FontWeight.Normal                        |
| Height                 | 72                                       |
| Italic                 | false                                    |
| LineHeight             | 1.2                                      |
| Overflow               | Overflow.Hidden                          |
| PaddingBottom          | 5                                        |
| PaddingLeft            | 5                                        |
| PaddingRight           | 5                                        |
| PaddingTop             | 5                                        |
| Size                   | 21                                       |
| Strikethrough          | false                                    |
| Underline              | false                                    |
| VerticalAlign          | Top                                      |
| Visible                | First(colAlertComponent).Visible         |
| Width                  | rectPopupBox.Width \- 30 \-30            |
| X                      | lblAlertTitle.X                          |
| Y                      | (lblAlertTitle.Y + lblAlertTitle.Height) |
| ZIndex                 | 14                                       |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblAlertTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value                                                    |
| -------- | -------------------------------------------------------- |
| OnSelect | ClearCollect(colAlertComponent,{Result:0,Visble:false }) |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | varString.Alert  |

### Design

| Property               | Value                            |
| ---------------------- | -------------------------------- |
| Align                  | Align.Left                       |
| BorderStyle            | BorderStyle.None                 |
| BorderThickness        | 2                                |
| DisplayMode            | DisplayMode.Edit                 |
| FocusedBorderThickness | 4                                |
| Font                   | Font.'Segoe UI'                  |
| FontWeight             | Bold                             |
| Height                 | 64                               |
| Italic                 | false                            |
| LineHeight             | 1.2                              |
| Overflow               | Overflow.Hidden                  |
| PaddingBottom          | 5                                |
| PaddingLeft            | 5                                |
| PaddingRight           | 5                                |
| PaddingTop             | 5                                |
| Size                   | 21                               |
| Strikethrough          | false                            |
| Underline              | false                            |
| VerticalAlign          | VerticalAlign.Middle             |
| Visible                | First(colAlertComponent).Visible |
| Width                  | rectPopupBox.Width \- 30         |
| X                      | rectPopupBox.X +25               |
| Y                      | rectPopupBox.Y                   |
| ZIndex                 | 13                               |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblCheckInTime

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                       |
| -------- | ----------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                    |
| Role     | TextRole.Default                                                                                            |
| Text     | If( IsBlankOrError(varSelectedRequest.CheckInTime), "", Text( varSelectedRequest.CheckInTime, ShortTime ) ) |

### Design

| Property               | Value                                                                                                                                                                                                       |
| ---------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                |
| BorderStyle            | BorderStyle.None                                                                                                                                                                                            |
| BorderThickness        | 2                                                                                                                                                                                                           |
| DisplayMode            | DisplayMode.Edit                                                                                                                                                                                            |
| FocusedBorderThickness | 4                                                                                                                                                                                                           |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                             |
| FontWeight             | FontWeight.Normal                                                                                                                                                                                           |
| Height                 | 40                                                                                                                                                                                                          |
| Italic                 | false                                                                                                                                                                                                       |
| LineHeight             | 1.2                                                                                                                                                                                                         |
| Overflow               | Overflow.Hidden                                                                                                                                                                                             |
| PaddingBottom          | 5                                                                                                                                                                                                           |
| PaddingLeft            | 5                                                                                                                                                                                                           |
| PaddingRight           | 5                                                                                                                                                                                                           |
| PaddingTop             | 5                                                                                                                                                                                                           |
| Size                   | 22.5                                                                                                                                                                                                        |
| Strikethrough          | false                                                                                                                                                                                                       |
| Underline              | false                                                                                                                                                                                                       |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                        |
| Visible                | If ( varSelectedBuilding.'Building Type'.Value \= "Unmonitored", If( varSelectedRequest.RequestDate \= Today()&& varSelectedRequest.CheckIn\_x0020\_Status.Value \<\> "Disqualified", true, false), false ) |
| Width                  | btnCheckIn.Width                                                                                                                                                                                            |
| X                      | btnCheckIn.X                                                                                                                                                                                                |
| Y                      | btnCheckIn.Y\-Self.Height\-10                                                                                                                                                                               |
| ZIndex                 | 16                                                                                                                                                                                                          |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblCheckOutTime

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                         |
| -------- | ------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                      |
| Role     | TextRole.Default                                                                                              |
| Text     | If( IsBlankOrError(varSelectedRequest.CheckOutTime), "", Text( varSelectedRequest.CheckOutTime, ShortTime ) ) |

### Design

| Property               | Value                                                                                                                                 |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                          |
| BorderStyle            | BorderStyle.None                                                                                                                      |
| BorderThickness        | 2                                                                                                                                     |
| DisplayMode            | DisplayMode.Edit                                                                                                                      |
| FocusedBorderThickness | 4                                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                                     |
| Height                 | 40                                                                                                                                    |
| Italic                 | false                                                                                                                                 |
| LineHeight             | 1.2                                                                                                                                   |
| Overflow               | Overflow.Hidden                                                                                                                       |
| PaddingBottom          | 5                                                                                                                                     |
| PaddingLeft            | 5                                                                                                                                     |
| PaddingRight           | 5                                                                                                                                     |
| PaddingTop             | 5                                                                                                                                     |
| Size                   | 22.5                                                                                                                                  |
| Strikethrough          | false                                                                                                                                 |
| Underline              | false                                                                                                                                 |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                  |
| Visible                | If ( varSelectedBuilding.'Building Type'.Value \= "Unmonitored", If( varSelectedRequest.RequestDate \= Today(), true, false), false ) |
| Width                  | btnCheckOut.Width                                                                                                                     |
| X                      | btnCheckOut.X                                                                                                                         |
| Y                      | btnCheckOut.Y\-Self.Height\-10                                                                                                        |
| ZIndex                 | 15                                                                                                                                    |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorBackRectangle\_5

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                     |
| ---------------------- | ------------------------- |
| BorderStyle            | BorderStyle.None          |
| BorderThickness        | 0                         |
| DisplayMode            | DisplayMode.Edit          |
| FocusedBorderThickness | 4                         |
| Height                 | Parent.Height             |
| Visible                | varSuccessCheckoutMessage |
| Width                  | Parent.Width              |
| X                      | 0                         |
| Y                      | 0                         |
| ZIndex                 | 18                        |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorBackRectangle\_KeyquestionsScreen\_1

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
| Visible                | varSuccessCheckInMessage |
| Width                  | Parent.Width             |
| X                      | 0                        |
| Y                      | 0                        |
| ZIndex                 | 23                       |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorMessage\_5

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                     |
| -------- | ------------------------- |
| Live     | Live.Off                  |
| Role     | TextRole.Default          |
| Text     | varString.CheckOutSuccess |

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
| Visible                | varSuccessCheckoutMessage                   |
| Width                  | lblErrorRectangle\_5.Width                  |
| X                      | lblErrorRectangle\_5.X                      |
| Y                      | imgErrorInfo\_5.Y+imgErrorInfo\_5.Height+18 |
| ZIndex                 | 20                                          |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorMessage\_KeyquestionsScreen\_1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varString.CheckInSuccess |

### Design

| Property               | Value                                                                               |
| ---------------------- | ----------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                        |
| BorderStyle            | BorderStyle.None                                                                    |
| BorderThickness        | 2                                                                                   |
| DisplayMode            | DisplayMode.Edit                                                                    |
| FocusedBorderThickness | 4                                                                                   |
| Font                   | Font.'Segoe UI'                                                                     |
| FontWeight             | FontWeight.Normal                                                                   |
| Height                 | 40                                                                                  |
| Italic                 | false                                                                               |
| LineHeight             | 1.2                                                                                 |
| Overflow               | Overflow.Hidden                                                                     |
| PaddingBottom          | 5                                                                                   |
| PaddingLeft            | 5                                                                                   |
| PaddingRight           | 5                                                                                   |
| PaddingTop             | 5                                                                                   |
| Size                   | 21                                                                                  |
| Strikethrough          | false                                                                               |
| Underline              | false                                                                               |
| VerticalAlign          | VerticalAlign.Middle                                                                |
| Visible                | varSuccessCheckInMessage                                                            |
| Width                  | lblErrorRectangle\_KeyquestionsScreen\_1.Width                                      |
| X                      | lblErrorRectangle\_KeyquestionsScreen\_1.X                                          |
| Y                      | imgErrorInfo\_KeyquestionsScreen\_1.Y+imgErrorInfo\_KeyquestionsScreen\_1.Height+18 |
| ZIndex                 | 25                                                                                  |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorRectangle\_5

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
| Visible                | varSuccessCheckoutMessage                                                     |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 19                                                                            |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblErrorRectangle\_KeyquestionsScreen\_1

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
| Visible                | varSuccessCheckInMessage                                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-32 , (App.DesignWidth\*2)\-32 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 24                                                                            |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## lblMyRequestApprover

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varString.Approver & ":" |

### Design

| Property               | Value                                                      |
| ---------------------- | ---------------------------------------------------------- |
| Align                  | Align.Left                                                 |
| BorderStyle            | BorderStyle.None                                           |
| BorderThickness        | 2                                                          |
| DisplayMode            | DisplayMode.Edit                                           |
| FocusedBorderThickness | 4                                                          |
| Font                   | Font.'Segoe UI'                                            |
| FontWeight             | FontWeight.Bold                                            |
| Height                 | 32                                                         |
| Italic                 | false                                                      |
| LineHeight             | 1.2                                                        |
| Overflow               | Overflow.Hidden                                            |
| PaddingBottom          | 5                                                          |
| PaddingLeft            | 5                                                          |
| PaddingRight           | 5                                                          |
| PaddingTop             | 5                                                          |
| Size                   | 18                                                         |
| Strikethrough          | false                                                      |
| Underline              | false                                                      |
| VerticalAlign          | VerticalAlign.Middle                                       |
| Width                  | Parent.Width                                               |
| X                      | 0                                                          |
| Y                      | lblMyRequestStatusValue.Y+lblMyRequestStatusValue.Height+8 |
| ZIndex                 | 11                                                         |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestApproverValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value                                                                                                     |
| -------- | --------------------------------------------------------------------------------------------------------- |
| OnSelect | \/\/Launch("https:\/\/teams.microsoft.com\/l\/chat\/0\/0?users\=" & lblMyRequestApproverValue.Text) false |

### Data

| Property | Value                                                                                                                                                                                                                |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                             |
| Role     | TextRole.Default                                                                                                                                                                                                     |
| Text     | If( IsBlank(varSelectedRequest.Approver), If(\!IsBlank(varSelectedRequest.SecurityApprover), varSelectedRequest.SecurityApprover.DisplayName, varStringNew.NotListedLbl ), varSelectedRequest.Approver.DisplayName ) |

### Design

| Property               | Value                                              |
| ---------------------- | -------------------------------------------------- |
| Align                  | Align.Left                                         |
| BorderStyle            | BorderStyle.None                                   |
| BorderThickness        | 2                                                  |
| DisplayMode            | DisplayMode.Edit                                   |
| FocusedBorderThickness | 4                                                  |
| Font                   | Font.'Segoe UI'                                    |
| FontWeight             | FontWeight.Normal                                  |
| Height                 | 40                                                 |
| Italic                 | false                                              |
| LineHeight             | 1.2                                                |
| Overflow               | Overflow.Hidden                                    |
| PaddingBottom          | 5                                                  |
| PaddingLeft            | 5                                                  |
| PaddingRight           | 5                                                  |
| PaddingTop             | 5                                                  |
| Size                   | 22.5                                               |
| Strikethrough          | false                                              |
| Underline              | false                                              |
| VerticalAlign          | VerticalAlign.Middle                               |
| Width                  | Parent.Width                                       |
| X                      | 0                                                  |
| Y                      | lblMyRequestApprover.Y+lblMyRequestApprover.Height |
| ZIndex                 | 12                                                 |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestBusinessJustification

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                   |
| -------- | --------------------------------------- |
| Live     | Live.Off                                |
| Role     | TextRole.Default                        |
| Text     | varString.BusinessReasonForAccess & ":" |

### Design

| Property               | Value                                                          |
| ---------------------- | -------------------------------------------------------------- |
| Align                  | Align.Left                                                     |
| BorderStyle            | BorderStyle.None                                               |
| BorderThickness        | 2                                                              |
| DisplayMode            | DisplayMode.Edit                                               |
| FocusedBorderThickness | 4                                                              |
| Font                   | Font.'Segoe UI'                                                |
| FontWeight             | FontWeight.Bold                                                |
| Height                 | 32                                                             |
| Italic                 | false                                                          |
| LineHeight             | 1.2                                                            |
| Overflow               | Overflow.Hidden                                                |
| PaddingBottom          | 5                                                              |
| PaddingLeft            | 5                                                              |
| PaddingRight           | 5                                                              |
| PaddingTop             | 5                                                              |
| Size                   | 18                                                             |
| Strikethrough          | false                                                          |
| Underline              | false                                                          |
| VerticalAlign          | VerticalAlign.Middle                                           |
| Width                  | Parent.Width                                                   |
| X                      | 0                                                              |
| Y                      | lblMyRequestApproverValue.Y+lblMyRequestApproverValue.Height+8 |
| ZIndex                 | 13                                                             |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestBusinessJustificationValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                   |
| -------- | --------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                |
| Role     | TextRole.Default                                                                        |
| Text     | If( IsBlank(varSelectedRequest.RequestReason), "\-", varSelectedRequest.RequestReason ) |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                   |
| AutoHeight             | true                                                                         |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 2                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Font                   | Font.'Segoe UI'                                                              |
| FontWeight             | FontWeight.Normal                                                            |
| Height                 | 40                                                                           |
| Italic                 | false                                                                        |
| LineHeight             | 1.2                                                                          |
| Overflow               | Overflow.Hidden                                                              |
| PaddingBottom          | 5                                                                            |
| PaddingLeft            | 5                                                                            |
| PaddingRight           | 5                                                                            |
| PaddingTop             | 5                                                                            |
| Size                   | 22.5                                                                         |
| Strikethrough          | false                                                                        |
| Underline              | false                                                                        |
| VerticalAlign          | VerticalAlign.Top                                                            |
| Width                  | Parent.Width                                                                 |
| X                      | 0                                                                            |
| Y                      | lblMyRequestBusinessJustification.Y+lblMyRequestBusinessJustification.Height |
| ZIndex                 | 14                                                                           |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestCheckInStatus

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                                                         |
| -------- | ----------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                      |
| Role     | TextRole.Default                                                                                                              |
| Text     | If( varSelectedRequest.Status.Value \= "Rejected", Mid( varString.RejectionReason, 2 ) & ":", varStringNew.ChecKInStatusLbl ) |

### Design

| Property               | Value                                                                                    |
| ---------------------- | ---------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                               |
| BorderStyle            | BorderStyle.None                                                                         |
| BorderThickness        | 2                                                                                        |
| DisplayMode            | DisplayMode.Edit                                                                         |
| FocusedBorderThickness | 4                                                                                        |
| Font                   | Font.'Segoe UI'                                                                          |
| FontWeight             | FontWeight.Bold                                                                          |
| Height                 | 32                                                                                       |
| Italic                 | false                                                                                    |
| LineHeight             | 1.2                                                                                      |
| Overflow               | Overflow.Hidden                                                                          |
| PaddingBottom          | 5                                                                                        |
| PaddingLeft            | 5                                                                                        |
| PaddingRight           | 5                                                                                        |
| PaddingTop             | 5                                                                                        |
| Size                   | 18                                                                                       |
| Strikethrough          | false                                                                                    |
| Underline              | false                                                                                    |
| VerticalAlign          | VerticalAlign.Middle                                                                     |
| Width                  | Parent.Width                                                                             |
| X                      | 0                                                                                        |
| Y                      | lblMyRequestBusinessJustificationValue.Y+lblMyRequestBusinessJustificationValue.Height+8 |
| ZIndex                 | 16                                                                                       |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestCheckInStatusValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Live     | Live.Off                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| Role     | TextRole.Default                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| Text     | If( varSelectedRequest.Status.Value \= "Rejected", varSelectedRequest.RejectionReason, If( IsBlank(varSelectedRequest.CheckIn\_x0020\_Status.Value), " \- ", If(varSelectedRequest.CheckIn\_x0020\_Status.Value\="Checked\-In",varString.CheckinMsg, varSelectedRequest.CheckIn\_x0020\_Status.Value\="Checked\-Out",varString.CheckoutMsg, varSelectedRequest.CheckIn\_x0020\_Status.Value\="Disqualified",varStringNew.DisqualifiedStatus) ) ) |

### Design

| Property               | Value                                                        |
| ---------------------- | ------------------------------------------------------------ |
| Align                  | Align.Left                                                   |
| BorderStyle            | BorderStyle.None                                             |
| BorderThickness        | 2                                                            |
| DisplayMode            | DisplayMode.Edit                                             |
| FocusedBorderThickness | 4                                                            |
| Font                   | Font.'Segoe UI'                                              |
| FontWeight             | FontWeight.Normal                                            |
| Height                 | 40                                                           |
| Italic                 | false                                                        |
| LineHeight             | 1.2                                                          |
| Overflow               | Overflow.Hidden                                              |
| PaddingBottom          | 5                                                            |
| PaddingLeft            | 5                                                            |
| PaddingRight           | 5                                                            |
| PaddingTop             | 5                                                            |
| Size                   | 22.5                                                         |
| Strikethrough          | false                                                        |
| Underline              | false                                                        |
| VerticalAlign          | VerticalAlign.Middle                                         |
| Width                  | Parent.Width                                                 |
| X                      | 0                                                            |
| Y                      | lblMyRequestCheckInStatus.Y+lblMyRequestCheckInStatus.Height |
| ZIndex                 | 17                                                           |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestDate

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Live     | Live.Off                    |
| Role     | TextRole.Default            |
| Text     | varString.RequestDate & ":" |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Bold      |
| Height                 | 32                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 18                   |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Width                  | Parent.Width         |
| X                      | 0                    |
| Y                      | 16                   |
| ZIndex                 | 1                    |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestDateValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Live     | Live.Off                       |
| Role     | TextRole.Default               |
| Text     | varSelectedRequest.RequestDate |

### Design

| Property               | Value                                      |
| ---------------------- | ------------------------------------------ |
| Align                  | Align.Left                                 |
| BorderStyle            | BorderStyle.None                           |
| BorderThickness        | 2                                          |
| DisplayMode            | DisplayMode.Edit                           |
| FocusedBorderThickness | 4                                          |
| Font                   | Font.'Segoe UI'                            |
| FontWeight             | FontWeight.Normal                          |
| Height                 | 40                                         |
| Italic                 | false                                      |
| LineHeight             | 1.2                                        |
| Overflow               | Overflow.Hidden                            |
| PaddingBottom          | 5                                          |
| PaddingLeft            | 5                                          |
| PaddingRight           | 5                                          |
| PaddingTop             | 5                                          |
| Size                   | 22.5                                       |
| Strikethrough          | false                                      |
| Underline              | false                                      |
| VerticalAlign          | VerticalAlign.Middle                       |
| Width                  | Parent.Width                               |
| X                      | 0                                          |
| Y                      | lblMyRequestDate.Y+lblMyRequestDate.Height |
| ZIndex                 | 5                                          |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestInstructions

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                   |
| -------- | --------------------------------------- |
| Live     | Live.Off                                |
| Role     | TextRole.Default                        |
| Text     | varString.OnsiteAccessInstructions& ":" |

### Design

| Property               | Value                                                    |
| ---------------------- | -------------------------------------------------------- |
| Align                  | Align.Left                                               |
| BorderStyle            | BorderStyle.None                                         |
| BorderThickness        | 2                                                        |
| DisplayMode            | DisplayMode.Edit                                         |
| FocusedBorderThickness | 4                                                        |
| Font                   | Font.'Segoe UI'                                          |
| FontWeight             | FontWeight.Bold                                          |
| Height                 | 32                                                       |
| Italic                 | false                                                    |
| LineHeight             | 1.2                                                      |
| Overflow               | Overflow.Hidden                                          |
| PaddingBottom          | 5                                                        |
| PaddingLeft            | 5                                                        |
| PaddingRight           | 5                                                        |
| PaddingTop             | 5                                                        |
| Size                   | 18                                                       |
| Strikethrough          | false                                                    |
| Underline              | false                                                    |
| VerticalAlign          | VerticalAlign.Middle                                     |
| Width                  | Parent.Width                                             |
| X                      | 0                                                        |
| Y                      | lblTypeOfFacilityValue.Y+lblTypeOfFacilityValue.Height+8 |
| ZIndex                 | 15                                                       |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestInstructionsValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                        |
| Role     | TextRole.Default                                                                                                |
| Text     | If( IsBlank(varSelectedBuilding.OnsiteAccessInstructions), "\-", varSelectedBuilding.OnsiteAccessInstructions ) |

### Design

| Property               | Value                                                      |
| ---------------------- | ---------------------------------------------------------- |
| Align                  | Align.Left                                                 |
| AutoHeight             | true                                                       |
| BorderStyle            | BorderStyle.None                                           |
| BorderThickness        | 2                                                          |
| DisplayMode            | DisplayMode.Edit                                           |
| FocusedBorderThickness | 4                                                          |
| Font                   | Font.'Segoe UI'                                            |
| FontWeight             | FontWeight.Normal                                          |
| Height                 | 100                                                        |
| Italic                 | false                                                      |
| LineHeight             | 1.2                                                        |
| Overflow               | Overflow.Hidden                                            |
| PaddingBottom          | 5                                                          |
| PaddingLeft            | 5                                                          |
| PaddingRight           | 5                                                          |
| PaddingTop             | 5                                                          |
| Size                   | 22.5                                                       |
| Strikethrough          | false                                                      |
| Underline              | false                                                      |
| VerticalAlign          | VerticalAlign.Top                                          |
| Visible                | false                                                      |
| Width                  | Parent.Width                                               |
| X                      | 0                                                          |
| Y                      | lblMyRequestInstructions.Y+lblMyRequestInstructions.Height |
| ZIndex                 | 18                                                         |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestStatus

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                      |
| -------- | -------------------------- |
| Live     | Live.Off                   |
| Role     | TextRole.Default           |
| Text     | varString.RequestStatuslbl |

### Design

| Property               | Value                                    |
| ---------------------- | ---------------------------------------- |
| Align                  | Align.Left                               |
| BorderStyle            | BorderStyle.None                         |
| BorderThickness        | 2                                        |
| DisplayMode            | DisplayMode.Edit                         |
| FocusedBorderThickness | 4                                        |
| Font                   | Font.'Segoe UI'                          |
| FontWeight             | FontWeight.Bold                          |
| Height                 | 32                                       |
| Italic                 | false                                    |
| LineHeight             | 1.2                                      |
| Overflow               | Overflow.Hidden                          |
| PaddingBottom          | 5                                        |
| PaddingLeft            | 5                                        |
| PaddingRight           | 5                                        |
| PaddingTop             | 5                                        |
| Size                   | 18                                       |
| Strikethrough          | false                                    |
| Underline              | false                                    |
| VerticalAlign          | VerticalAlign.Middle                     |
| Width                  | Parent.Width                             |
| X                      | 0                                        |
| Y                      | htmlOnsiteText.Y+htmlOnsiteText.Height+8 |
| ZIndex                 | 9                                        |

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
| Parent Control | RequestDetailsDataCard |

## lblMyRequestStatusValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Live     | Live.Off                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| Role     | TextRole.Default                                                                                                                                                                                                                                                                                                                                                                                                                           |
| Text     | If( varSelectedRequest.AutoApproved \= true, varStringNew.'AutoApproved lbl', If(varSelectedRequest.Status.Value\="Pending Approval",varString.PendingApproval, varSelectedRequest.Status.Value\="Approved",varString.Approved, varSelectedRequest.Status.Value\="Rejected",varString.Rejected, varSelectedRequest.Status.Value\="Revoked",varString.Revoked, varSelectedRequest.Status.Value\="Withdrawn",varStringNew.WithdrawnStatus) ) |

### Design

| Property               | Value                                          |
| ---------------------- | ---------------------------------------------- |
| Align                  | Align.Left                                     |
| BorderStyle            | BorderStyle.None                               |
| BorderThickness        | 2                                              |
| DisplayMode            | DisplayMode.Edit                               |
| FocusedBorderThickness | 4                                              |
| Font                   | Font.'Segoe UI'                                |
| FontWeight             | FontWeight.Normal                              |
| Height                 | 40                                             |
| Italic                 | false                                          |
| LineHeight             | 1.2                                            |
| Overflow               | Overflow.Hidden                                |
| PaddingBottom          | 5                                              |
| PaddingLeft            | 5                                              |
| PaddingRight           | 5                                              |
| PaddingTop             | 5                                              |
| Size                   | 22.5                                           |
| Strikethrough          | false                                          |
| Underline              | false                                          |
| VerticalAlign          | VerticalAlign.Middle                           |
| Width                  | Parent.Width                                   |
| X                      | 0                                              |
| Y                      | lblMyRequestStatus.Y+lblMyRequestStatus.Height |
| ZIndex                 | 10                                             |

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
| Parent Control | RequestDetailsDataCard |

## lblReservedTime

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Live     | Live.Off                       |
| Role     | TextRole.Default               |
| Text     | varStringNew.TimeSlotNSpaceLbl |

### Design

| Property               | Value                                  |
| ---------------------- | -------------------------------------- |
| Align                  | Align.Left                             |
| BorderStyle            | BorderStyle.None                       |
| BorderThickness        | 2                                      |
| DisplayMode            | DisplayMode.Edit                       |
| FocusedBorderThickness | 4                                      |
| Font                   | Font.'Segoe UI'                        |
| FontWeight             | FontWeight.Bold                        |
| Height                 | 32                                     |
| Italic                 | false                                  |
| LineHeight             | 1.2                                    |
| Overflow               | Overflow.Hidden                        |
| PaddingBottom          | 5                                      |
| PaddingLeft            | 5                                      |
| PaddingRight           | 5                                      |
| PaddingTop             | 5                                      |
| Size                   | 18                                     |
| Strikethrough          | false                                  |
| Underline              | false                                  |
| VerticalAlign          | VerticalAlign.Middle                   |
| Width                  | Parent.Width                           |
| X                      | 0                                      |
| Y                      | lblSeatsValue.Y+lblSeatsValue.Height+8 |
| ZIndex                 | 3                                      |

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
| Parent Control | RequestDetailsDataCard |

## lblReservedTimeValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                                                                                                                   |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                |
| Role     | TextRole.Default                                                                                                                                                                        |
| Text     | If( IsBlank(varSelectedRequest.TimeSlot), varStringNew.FullDayLbl2 & " \- ", (varSelectedRequest.TimeSlot & " \- ") ) & varSelectedRequest.SpaceName & " , " & varSelectedRequest.Title |

### Design

| Property               | Value                                    |
| ---------------------- | ---------------------------------------- |
| Align                  | Align.Left                               |
| BorderStyle            | BorderStyle.None                         |
| BorderThickness        | 2                                        |
| DisplayMode            | DisplayMode.Edit                         |
| FocusedBorderThickness | 4                                        |
| Font                   | Font.'Segoe UI'                          |
| FontWeight             | FontWeight.Normal                        |
| Height                 | 40                                       |
| Italic                 | false                                    |
| LineHeight             | 1.2                                      |
| Overflow               | Overflow.Hidden                          |
| PaddingBottom          | 5                                        |
| PaddingLeft            | 5                                        |
| PaddingRight           | 5                                        |
| PaddingTop             | 5                                        |
| Size                   | 22.5                                     |
| Strikethrough          | false                                    |
| Underline              | false                                    |
| VerticalAlign          | VerticalAlign.Middle                     |
| Width                  | Parent.Width                             |
| Wrap                   | false                                    |
| X                      | 0                                        |
| Y                      | lblReservedTime.Y+lblReservedTime.Height |
| ZIndex                 | 7                                        |

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
| Parent Control | RequestDetailsDataCard |

## lblSeats

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varStringNew.NoOfSeatLbl |

### Design

| Property               | Value                                                  |
| ---------------------- | ------------------------------------------------------ |
| Align                  | Align.Left                                             |
| BorderStyle            | BorderStyle.None                                       |
| BorderThickness        | 2                                                      |
| DisplayMode            | DisplayMode.Edit                                       |
| FocusedBorderThickness | 4                                                      |
| Font                   | Font.'Segoe UI'                                        |
| FontWeight             | FontWeight.Bold                                        |
| Height                 | 32                                                     |
| Italic                 | false                                                  |
| LineHeight             | 1.2                                                    |
| Overflow               | Overflow.Hidden                                        |
| PaddingBottom          | 5                                                      |
| PaddingLeft            | 5                                                      |
| PaddingRight           | 5                                                      |
| PaddingTop             | 5                                                      |
| Size                   | 18                                                     |
| Strikethrough          | false                                                  |
| Underline              | false                                                  |
| VerticalAlign          | VerticalAlign.Middle                                   |
| Width                  | Parent.Width                                           |
| X                      | 0                                                      |
| Y                      | lblMyRequestDateValue.Y+lblMyRequestDateValue.Height+8 |
| ZIndex                 | 2                                                      |

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
| Parent Control | RequestDetailsDataCard |

## lblSeatsValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | "1"              |

### Design

| Property               | Value                      |
| ---------------------- | -------------------------- |
| Align                  | Align.Left                 |
| BorderStyle            | BorderStyle.None           |
| BorderThickness        | 2                          |
| DisplayMode            | DisplayMode.Edit           |
| FocusedBorderThickness | 4                          |
| Font                   | Font.'Segoe UI'            |
| FontWeight             | FontWeight.Normal          |
| Height                 | 40                         |
| Italic                 | false                      |
| LineHeight             | 1.2                        |
| Overflow               | Overflow.Hidden            |
| PaddingBottom          | 5                          |
| PaddingLeft            | 5                          |
| PaddingRight           | 5                          |
| PaddingTop             | 5                          |
| Size                   | 22.5                       |
| Strikethrough          | false                      |
| Underline              | false                      |
| VerticalAlign          | VerticalAlign.Middle       |
| Width                  | Parent.Width               |
| X                      | 0                          |
| Y                      | lblSeats.Y+lblSeats.Height |
| ZIndex                 | 6                          |

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
| Parent Control | RequestDetailsDataCard |

## lblTypeOfFacility

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Live     | Live.Off                       |
| Role     | TextRole.Default               |
| Text     | varStringNew.TyepOfFaciltiyLbl |

### Design

| Property               | Value                                                |
| ---------------------- | ---------------------------------------------------- |
| Align                  | Align.Left                                           |
| BorderStyle            | BorderStyle.None                                     |
| BorderThickness        | 2                                                    |
| DisplayMode            | DisplayMode.Edit                                     |
| FocusedBorderThickness | 4                                                    |
| Font                   | Font.'Segoe UI'                                      |
| FontWeight             | FontWeight.Bold                                      |
| Height                 | 32                                                   |
| Italic                 | false                                                |
| LineHeight             | 1.2                                                  |
| Overflow               | Overflow.Hidden                                      |
| PaddingBottom          | 5                                                    |
| PaddingLeft            | 5                                                    |
| PaddingRight           | 5                                                    |
| PaddingTop             | 5                                                    |
| Size                   | 18                                                   |
| Strikethrough          | false                                                |
| Underline              | false                                                |
| VerticalAlign          | VerticalAlign.Middle                                 |
| Width                  | Parent.Width                                         |
| X                      | 0                                                    |
| Y                      | lblReservedTimeValue.Y+lblReservedTimeValue.Height+8 |
| ZIndex                 | 4                                                    |

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
| Parent Control | RequestDetailsDataCard |

## lblTypeOfFacilityValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect |       |

### Data

| Property | Value                                                                                                            |
| -------- | ---------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                         |
| Role     | TextRole.Default                                                                                                 |
| Text     | If( varSelectedBuilding.'Building Type'.Value \= "Monitored", varStringNew.Monitored, varStringNew.Unmonitored ) |

### Design

| Property               | Value                                        |
| ---------------------- | -------------------------------------------- |
| Align                  | Align.Left                                   |
| BorderStyle            | BorderStyle.None                             |
| BorderThickness        | 2                                            |
| DisplayMode            | DisplayMode.Edit                             |
| FocusedBorderThickness | 4                                            |
| Font                   | Font.'Segoe UI'                              |
| FontWeight             | FontWeight.Normal                            |
| Height                 | 40                                           |
| Italic                 | false                                        |
| LineHeight             | 1.2                                          |
| Overflow               | Overflow.Hidden                              |
| PaddingBottom          | 5                                            |
| PaddingLeft            | 5                                            |
| PaddingRight           | 5                                            |
| PaddingTop             | 5                                            |
| Size                   | 22.5                                         |
| Strikethrough          | false                                        |
| Underline              | false                                        |
| VerticalAlign          | VerticalAlign.Middle                         |
| Width                  | Parent.Width                                 |
| X                      | 0                                            |
| Y                      | lblTypeOfFacility.Y+lblTypeOfFacility.Height |
| ZIndex                 | 8                                            |

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
| Parent Control | RequestDetailsDataCard |

## rectBackDrop

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                |
| ---------------------- | -------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                     |
| BorderThickness        | 2                                                                    |
| DisplayMode            | DisplayMode.View                                                     |
| FocusedBorderThickness | 4                                                                    |
| Height                 | Parent.Height                                                        |
| Visible                | First(colAlertComponent).Visible                                     |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, (App.DesignWidth\*2) ) |
| X                      | (Parent.Width\-Self.Width)\/2                                        |
| Y                      | 0                                                                    |
| ZIndex                 | 9                                                                    |

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## rectOptionsBox

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                               |
| ---------------------- | --------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                    |
| BorderThickness        | 2                                                   |
| DisplayMode            | DisplayMode.Edit                                    |
| FocusedBorderThickness | 4                                                   |
| Height                 | 70                                                  |
| Visible                | \/\/First(colAlertComponent).Visible false          |
| Width                  | rectPopupBox.Width                                  |
| X                      | rectPopupBox.X                                      |
| Y                      | rectPopupBox.Y + rectPopupBox.Height \- Self.Height |
| ZIndex                 | 11                                                  |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | ColorValue("\#F0F0F0")                                                                                                |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | Self.Fill                                                                                                             |
| PressedFill        | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## rectPopupBox

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
| Height                 | 214                                                                          |
| Visible                | First(colAlertComponent).Visible                                             |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                |
| Y                      | (Parent.Height\-Self.Height)\/2                                              |
| ZIndex                 | 10                                                                           |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | White                                                                                                                 |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | Self.Fill                                                                                                             |
| PressedFill        | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## RequestDetailsCanvas

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![fluidGrid](resources/fluidGrid.png) | Type: fluidGrid |

### Data

| Property | Value |
| -------- | ----- |
| Reset    | false |

### Design

| Property        | Value                                                                        |
| --------------- | ---------------------------------------------------------------------------- |
| BorderStyle     | BorderStyle.Solid                                                            |
| BorderThickness | 0                                                                            |
| DisplayMode     | DisplayMode.Edit                                                             |
| Height          | lblCheckInTime.Y\-Self.Y\-12                                                 |
| Width           | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 ) |
| X               | (Parent.Width\-Self.Width) \/2                                               |
| Y               | RequestQRCode.Y + RequestQRCode.Height                                       |
| ZIndex          | 3                                                                            |

### Color Properties

| Property    | Value                                                                                                           |
| ----------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Child Control  | RequestDetailsDataCard    |
| Parent Control | My Request Details Screen |

## RequestDetailsDataCard

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![dataCard](resources/dataCard.png) | Type: dataCard |

### Design

| Property    | Value                                  |
| ----------- | -------------------------------------- |
| BorderStyle | BorderStyle.Solid                      |
| DisplayMode | DisplayMode.Edit                       |
| Height      | Parent.Height                          |
| Width       | Parent.Width                           |
| X           | 0                                      |
| Y           | RequestQRCode.Y + RequestQRCode.Height |
| ZIndex      | 2                                      |

### Color Properties

| Property    | Value                                                                                                           |
| ----------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                                  |
| -------------- | -------------------------------------- |
| Child Control  | lblMyRequestDate                       |
| Child Control  | lblMyRequestDateValue                  |
| Child Control  | lblSeats                               |
| Child Control  | lblSeatsValue                          |
| Child Control  | lblReservedTime                        |
| Child Control  | lblReservedTimeValue                   |
| Child Control  | lblTypeOfFacility                      |
| Child Control  | lblTypeOfFacilityValue                 |
| Child Control  | lblMyRequestInstructions               |
| Child Control  | lblMyRequestInstructionsValue          |
| Child Control  | htmlOnsiteText                         |
| Child Control  | lblMyRequestStatus                     |
| Child Control  | lblMyRequestStatusValue                |
| Child Control  | lblMyRequestApprover                   |
| Child Control  | lblMyRequestApproverValue              |
| Child Control  | lblMyRequestBusinessJustification      |
| Child Control  | lblMyRequestBusinessJustificationValue |
| Child Control  | lblMyRequestCheckInStatus              |
| Child Control  | lblMyRequestCheckInStatusValue         |
| Parent Control | RequestDetailsCanvas                   |

## RequestQRCode

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property | Value                        |
| -------- | ---------------------------- |
| Code     | varSelectedRequest.AccessKey |
| QRHeight | 300                          |
| QRWidth  | 300                          |

### Design

| Property | Value                                                                                                                               |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------- |
| Height   | If ( varSelectedBuilding.'Building Type'.Value \= "Monitored", If( varSelectedRequest.RequestDate \= Today(), 300, 0), 0 )          |
| Visible  | If ( varSelectedRequest.RequestDate \= Today(), If( varSelectedBuilding.'Building Type'.Value \= "Monitored", true, false), false ) |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 )                                                        |
| X        | (Parent.Width\-Self.Width) \/2                                                                                                      |
| Y        | 91                                                                                                                                  |
| ZIndex   | 4                                                                                                                                   |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## shpRectangleBackGround\_MyRequestDetailsScreen

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |

## shpUpperRectangle\_MyRequestDetailsScreen

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

| Property       | Value                     |
| -------------- | ------------------------- |
| Parent Control | My Request Details Screen |
