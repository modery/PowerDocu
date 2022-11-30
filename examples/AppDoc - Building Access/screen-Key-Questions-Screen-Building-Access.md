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

## Key Questions Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| --------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | If( IsEmpty(colKeyQuestions), ClearCollect( colKeyQuestions, Filter( BAR\_KeyQuestions, State.Value \= "Published" ) ) ); Clear(colLocalQuestionAnswers); Clear(colQuestionAnswersResponse); ForAll( colKeyQuestions, Collect( colLocalQuestionAnswers, { QuestionID: ID, Question: Title, Answer: \-1 } ); Collect( colQuestionAnswersResponse, { QuestionID: ID, Question: Title, Answer: \-1 } ) ); Reset(KeyQuestionsListComponent); Set( varKeyQuestionsPass, true ); Set( varKeyQuestionsRequiredPass, true ); |

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
| Child Control | shpRectangleBackGround\_KeyQuestionsScreen |
| Child Control | shpUpperRectangle\_KeyQuestionsScreen      |
| Child Control | HeaderControlKeyEligibilityQuestions       |
| Child Control | lblErrorBackRectangle\_KeyquestionsScreen  |
| Child Control | KeyQuestionsListComponent                  |
| Child Control | lblErrorRectangle\_KeyquestionsScreen      |
| Child Control | icnErrorCancel\_KeyquestionsScreen         |
| Child Control | imgErrorInfo\_KeyquestionsScreen           |
| Child Control | htmlErrorMessage\_KeyquestionsScreens      |
| Child Control | lblErrorMessage\_KeyquestionsScreen        |
| Child Control | btnSubmitKeyQuestions                      |
| Child Control | grpErrorMessage\_KeyquestionsScreen        |

## btnSubmitKeyQuestions

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0, Set( varErrorMessageKeyquestionsScreen, true ), CountRows( Filter( colQuestionAnswersResponse, Answer \= 1 ) ) \> 0, Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { CheckInTime: Now(), Active: 0, 'CheckIn Status': { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 3, Value: "Disqualified" } } ); ClearCollect( colBuildingRequests, Filter( BAR\_Requests, Active \= 1, BuildingID \= varSelectedBuilding.ID, DateValue \= varTodayFormated ) ); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varErrorMessageKeyquestionsScreen, true ), Patch( BAR\_Requests, {ID: varSelectedRequest.ID}, { CheckInTime: Now(), 'CheckIn Status': { '@odata.type': "\#Microsoft.Azure.Connectors.SharePoint.SPListExpandedReference", Id: 0, Value: "Checked\-In" } } ); ClearCollect( colBuildingRequests, Filter( BAR\_Requests, Active \= 1, BuildingID \= varSelectedBuilding.ID, DateValue \= varTodayFormated ) ); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varErrorMessageKeyquestionsScreen, true ) ); |

### Data

| Property | Value                                 |
| -------- | ------------------------------------- |
| Text     | \/\/varString.Submit varString.Submit |

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
| ZIndex                 | 7                                                                             |

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
| HoverBorderColor    | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| HoverColor          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| HoverFill           | <table border="0"><tr><td>RGBA(70, 71, 117, 1)</td></tr><tr><td style="background-color:#464775"></td></tr></table>   |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(51, 52, 74, 1)</td></tr><tr><td style="background-color:#33344A"></td></tr></table>    |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## grpErrorMessage\_KeyquestionsScreen

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
| ZIndex   | 13    |

### Color Properties

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## HeaderControlKeyEligibilityQuestions

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                       |
| ------------------- | --------------------------- |
| backLabel           | "back"                      |
| IsBackButtonVisible | true                        |
| IsHomeButtonVisible | true                        |
| NavigateHomeScreen  | 'Home Screen'               |
| NavigateScreen      | 'My Request Details Screen' |
| Text                | varString.KeyQuestions      |

### Design

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Height   | 80                                                                  |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width , App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                       |
| Y        | 0                                                                   |
| ZIndex   | 6                                                                   |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## htmlErrorMessage\_KeyquestionsScreens

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Data

| Property | Value                                                                                                                                                                                                                                          |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| HtmlText | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0,varStringNew.PleaseAnsQueTxt, CountRows( Filter( colQuestionAnswersResponse, Answer \= 1 ) ) \> 0,varAppSettings.KeyQuestionsFailMessage, varString.CheckInSuccess) |

### Design

| Property    | Value                                                                                                                                                       |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| AutoHeight  | true                                                                                                                                                        |
| BorderStyle | BorderStyle.None                                                                                                                                            |
| DisplayMode | DisplayMode.Edit                                                                                                                                            |
| Font        | Font.'Segoe UI'                                                                                                                                             |
| Height      | If(lblErrorMessage\_KeyquestionsScreen.Text \= varString.CheckInSuccess \|\| lblErrorMessage\_KeyquestionsScreen.Text \= varStringNew.PleaseAnsQueTxt,0,70) |
| PaddingLeft | 90                                                                                                                                                          |
| Size        | 21                                                                                                                                                          |
| Visible     | varErrorMessageKeyquestionsScreen                                                                                                                           |
| Width       | lblErrorRectangle\_KeyquestionsScreen.Width \- 64                                                                                                           |
| X           | lblErrorRectangle\_KeyquestionsScreen.X+(lblErrorRectangle\_KeyquestionsScreen.Width\-Self.Width)\/2                                                        |
| Y           | imgErrorInfo\_KeyquestionsScreen.Y+imgErrorInfo\_KeyquestionsScreen.Height                                                                                  |
| ZIndex      | 13                                                                                                                                                          |

### Color Properties

| Property            | Value                                                                                                                                                                              |
| ------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>                                                                    |
| Color               | If(lblErrorMessage\_KeyquestionsScreen.Text \= varString.CheckInSuccess \|\| lblErrorMessage\_KeyquestionsScreen.Text \= varStringNew.PleaseAnsQueTxt,RGBA(0,0,0,0),RGBA(0,0,0,1)) |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(56, 56, 56, 1)</td></tr><tr><td style="background-color:#383838"></td></tr></table>                                                                 |
| DisabledFill        | <table border="0"><tr><td>RGBA(119, 119, 119, .4)</td></tr><tr><td style="background-color:#777777"></td></tr></table>                                                             |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 0)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>                                                              |
| HoverBorderColor    | Self.BorderColor                                                                                                                                                                   |
| PressedBorderColor  | Self.BorderColor                                                                                                                                                                   |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## icnErrorCancel\_KeyquestionsScreen

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                     |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Refresh(BAR\_Requests); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); Set( varErrorMessageKeyquestionsScreen, false ); Refresh(BAR\_Requests); Navigate('My Request Details Screen'); Set( varSelectedRequest, LookUp( BAR\_Requests, ID \= varSelectedRequest.ID ) ); |

### Design

| Property               | Value                                                                                               |
| ---------------------- | --------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                                   |
| BorderThickness        | 0                                                                                                   |
| DisplayMode            | DisplayMode.Edit                                                                                    |
| FocusedBorderThickness | 4                                                                                                   |
| Height                 | 32                                                                                                  |
| Icon                   | Icon.Cancel                                                                                         |
| Visible                | varErrorMessageKeyquestionsScreen                                                                   |
| Width                  | 32                                                                                                  |
| X                      | lblErrorRectangle\_KeyquestionsScreen.X+lblErrorRectangle\_KeyquestionsScreen.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_KeyquestionsScreen.Y+32                                                          |
| ZIndex                 | 11                                                                                                  |

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
| Parent Control | Key Questions Screen |

## imgErrorInfo\_KeyquestionsScreen

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value                                                                                                                                                                                        |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Image    | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0,InfoSolid, CountRows( Filter( colQuestionAnswersResponse, Answer \= 1 ) ) \> 0,InfoSolid, 'Task Complete Copy 4') |

### Design

| Property               | Value                                      |
| ---------------------- | ------------------------------------------ |
| BorderStyle            | BorderStyle.None                           |
| BorderThickness        | 2                                          |
| DisplayMode            | DisplayMode.Edit                           |
| FocusedBorderThickness | 4                                          |
| Height                 | 48                                         |
| ImagePosition          | ImagePosition.Fit                          |
| ImageRotation          | ImageRotation.None                         |
| PaddingBottom          | 0                                          |
| PaddingLeft            | 0                                          |
| PaddingRight           | 0                                          |
| PaddingTop             | 0                                          |
| RadiusBottomLeft       | 0                                          |
| RadiusBottomRight      | 0                                          |
| RadiusTopLeft          | 0                                          |
| RadiusTopRight         | 0                                          |
| Visible                | varErrorMessageKeyquestionsScreen          |
| Width                  | 48                                         |
| X                      | (Parent.Width\-Self.Width)\/2              |
| Y                      | lblErrorRectangle\_KeyquestionsScreen.Y+64 |
| ZIndex                 | 12                                         |

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
| Parent Control | Key Questions Screen |

## KeyQuestionsListComponent

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value                   |
| -------- | ----------------------- |
| OnReset  | Reset(glryKeyQuestions) |

### Data

| Property     | Value               |
| ------------ | ------------------- |
| Translations | {Yes:"Yes",No:"No"} |

### Design

| Property | Value                                                                                 |
| -------- | ------------------------------------------------------------------------------------- |
| Height   | btnSubmitKeyQuestions.Y\-Self.Y\-20                                                   |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )         |
| X        | (Parent.Width\-Self.Width)\/2                                                         |
| Y        | HeaderControlKeyEligibilityQuestions.Y+HeaderControlKeyEligibilityQuestions.Height+25 |
| ZIndex   | 5                                                                                     |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## lblErrorBackRectangle\_KeyquestionsScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                             |
| ---------------------- | --------------------------------- |
| BorderStyle            | BorderStyle.None                  |
| BorderThickness        | 0                                 |
| DisplayMode            | DisplayMode.Edit                  |
| FocusedBorderThickness | 4                                 |
| Height                 | Parent.Height                     |
| Visible                | varErrorMessageKeyquestionsScreen |
| Width                  | Parent.Width                      |
| X                      | 0                                 |
| Y                      | 0                                 |
| ZIndex                 | 8                                 |

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
| Parent Control | Key Questions Screen |

## lblErrorMessage\_KeyquestionsScreen

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                                                                                                                                                          |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                                                       |
| Role     | TextRole.Default                                                                                                                                                                                                                               |
| Text     | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0,varStringNew.PleaseAnsQueTxt, CountRows( Filter( colQuestionAnswersResponse, Answer \= 1 ) ) \> 0,varAppSettings.KeyQuestionsFailMessage, varString.CheckInSuccess) |

### Design

| Property               | Value                                                                                                                                                       |
| ---------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                |
| BorderStyle            | BorderStyle.None                                                                                                                                            |
| BorderThickness        | 2                                                                                                                                                           |
| DisplayMode            | DisplayMode.Edit                                                                                                                                            |
| FocusedBorderThickness | 4                                                                                                                                                           |
| Font                   | Font.'Segoe UI'                                                                                                                                             |
| FontWeight             | FontWeight.Normal                                                                                                                                           |
| Height                 | If(lblErrorMessage\_KeyquestionsScreen.Text \= varString.CheckInSuccess \|\| lblErrorMessage\_KeyquestionsScreen.Text \= varStringNew.PleaseAnsQueTxt,70,0) |
| Italic                 | false                                                                                                                                                       |
| LineHeight             | 1.2                                                                                                                                                         |
| Overflow               | Overflow.Hidden                                                                                                                                             |
| PaddingBottom          | 5                                                                                                                                                           |
| PaddingLeft            | 5                                                                                                                                                           |
| PaddingRight           | 5                                                                                                                                                           |
| PaddingTop             | 5                                                                                                                                                           |
| Size                   | 21                                                                                                                                                          |
| Strikethrough          | false                                                                                                                                                       |
| Underline              | false                                                                                                                                                       |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                        |
| Visible                | varErrorMessageKeyquestionsScreen                                                                                                                           |
| Width                  | lblErrorRectangle\_KeyquestionsScreen.Width\-24                                                                                                             |
| X                      | lblErrorRectangle\_KeyquestionsScreen.X+(lblErrorRectangle\_KeyquestionsScreen.Width\-Self.Width)\/2                                                        |
| Y                      | imgErrorInfo\_KeyquestionsScreen.Y+imgErrorInfo\_KeyquestionsScreen.Height+22                                                                               |
| ZIndex                 | 10                                                                                                                                                          |

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

| Property       | Value                |
| -------------- | -------------------- |
| Parent Control | Key Questions Screen |

## lblErrorRectangle\_KeyquestionsScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                                                                                    |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                                         |
| BorderThickness        | 0                                                                                                                                        |
| DisplayMode            | DisplayMode.Edit                                                                                                                         |
| FocusedBorderThickness | 4                                                                                                                                        |
| Height                 | htmlErrorMessage\_KeyquestionsScreens.Height + icnErrorCancel\_KeyquestionsScreen.Height + imgErrorInfo\_KeyquestionsScreen.Height + 100 |
| Visible                | varErrorMessageKeyquestionsScreen                                                                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-32 , (App.DesignWidth\*2)\-32 )                                                            |
| X                      | (Parent.Width\-Self.Width)\/2                                                                                                            |
| Y                      | (Parent.Height\-Self.Height)\/2                                                                                                          |
| ZIndex                 | 9                                                                                                                                        |

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
| Parent Control | Key Questions Screen |

## shpRectangleBackGround\_KeyQuestionsScreen

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
| Parent Control | Key Questions Screen |

## shpUpperRectangle\_KeyQuestionsScreen

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
| Parent Control | Key Questions Screen |
