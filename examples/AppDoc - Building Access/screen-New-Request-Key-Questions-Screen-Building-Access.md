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

## New Request Key Questions Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| --------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | If( IsEmpty(colKeyQuestions), ClearCollect( colKeyQuestions, Filter( BAR\_KeyQuestions, State.Value \= "Published" ) ) ); Clear(colLocalQuestionAnswers); Clear(colQuestionAnswersResponse); ForAll( colKeyQuestions, Collect( colLocalQuestionAnswers, { QuestionID: ID, Question: Title, Answer: \-1 } ); Collect( colQuestionAnswersResponse, { QuestionID: ID, Question: Title, Answer: \-1 } ) ); Reset(KeyQuestionAnswers); Set( varKeyQuestionsPass, true ); Set( varKeyQuestionsRequiredPass, true ); Reset(cmbSelectBuilding); Reset(txtBusinessReason); |

### Design

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| Height              | App.Height                                                                                                            |
| ImagePosition       | ImagePosition.Fit                                                                                                     |
| LoadingSpinner      | LoadingSpinner.None                                                                                                   |
| LoadingSpinnerColor | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| Orientation         | If(Self.Width \< Self.Height, Layout.Vertical, Layout.Horizontal)                                                     |
| Size                | 1 + CountRows(App.SizeBreakpoints) \- CountIf(App.SizeBreakpoints, Value \>\= Self.Width)                             |
| Width               | App.Width                                                                                                             |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property      | Value                                                |
| ------------- | ---------------------------------------------------- |
| Child Control | shpRectangleBackGround\_NewRequestKeyQuestionsScreen |
| Child Control | shpUpperRectangle\_NewRequestKeyQuestionsScreen      |
| Child Control | HeaderControlKeyQuestions                            |
| Child Control | lblErrorBackRectangle                                |
| Child Control | NavigationKeyQuestions                               |
| Child Control | lblKeyQuestionsHeader                                |
| Child Control | KeyQuestionAnswers                                   |
| Child Control | lblErrorRectangle                                    |
| Child Control | icnErrorCancel                                       |
| Child Control | imgErrorInfo                                         |
| Child Control | htmlLblErrorMsg                                      |
| Child Control | btnSaveKeyQuestions                                  |
| Child Control | grpErrorMessage                                      |

## btnSaveKeyQuestions

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                               |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0, Set(varErrorMessage,true), CountRows( Filter( colQuestionAnswersResponse, Answer \= 1 ) ) \> 0, Set(varErrorMessage,true), Navigate('Building Screen')) |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | varStringNew.SaveAndNextBtn |

### Design

| Property               | Value                                                                                                                                    |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                             |
| BorderStyle            | BorderStyle.None                                                                                                                         |
| BorderThickness        | 2                                                                                                                                        |
| DisplayMode            | \/\/If(IsEmpty(Filter(colQuestionAnswersResponse,Answer \=\-1 \|\| Answer \=1)), DisplayMode.Edit,DisplayMode.Disabled) DisplayMode.Edit |
| FocusedBorderThickness | 4                                                                                                                                        |
| Font                   | Font.'Segoe UI'                                                                                                                          |
| FontWeight             | FontWeight.Semibold                                                                                                                      |
| Height                 | 64                                                                                                                                       |
| Italic                 | false                                                                                                                                    |
| RadiusBottomLeft       | 4                                                                                                                                        |
| RadiusBottomRight      | 4                                                                                                                                        |
| RadiusTopLeft          | 4                                                                                                                                        |
| RadiusTopRight         | 4                                                                                                                                        |
| Size                   | 22.5                                                                                                                                     |
| Strikethrough          | false                                                                                                                                    |
| Underline              | false                                                                                                                                    |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                     |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )                                                            |
| X                      | (Parent.Width\-Self.Width)\/2                                                                                                            |
| Y                      | Parent.Height\-16\-Self.Height                                                                                                           |
| ZIndex                 | 3                                                                                                                                        |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## grpErrorMessage

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![group](resources/group.png) | Type: group |

### Design

| Property | Value |
| -------- | ----- |
| Height   | 5     |
| Width    | 5     |
| X        | 60    |
| Y        | 60    |
| ZIndex   | 16    |

### Color Properties

### Child & Parent Controls

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## HeaderControlKeyQuestions

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                             |
| ------------------- | --------------------------------- |
| backLabel           | "back"                            |
| IsBackButtonVisible | false                             |
| IsHomeButtonVisible | true                              |
| NavigateHomeScreen  | 'Home Screen'                     |
| NavigateScreen      | 'Home Screen'                     |
| Text                | varStringNew.NewRequestHeaderText |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0+0                                                                |
| ZIndex   | 9                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## htmlLblErrorMsg

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Data

| Property | Value                                                                                                                                              |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------- |
| HtmlText | If ( CountRows( Filter( colQuestionAnswersResponse, Answer \= \-1 ) ) \> 0, varStringNew.PleaseAnsQueTxt, varAppSettings.KeyQuestionsFailMessage ) |

### Design

| Property    | Value                                                                          |
| ----------- | ------------------------------------------------------------------------------ |
| AutoHeight  | true                                                                           |
| BorderStyle | BorderStyle.None                                                               |
| DisplayMode | DisplayMode.Edit                                                               |
| Font        | Font.'Segoe UI'                                                                |
| Height      | 70                                                                             |
| PaddingLeft | \/\/If(htmlLblErrorMsg.HtmlText \= "Please answer all the questions.",50,50) 5 |
| Size        | 22.5                                                                           |
| Visible     | varErrorMessage                                                                |
| Width       | lblErrorRectangle.Width\-80                                                    |
| X           | lblErrorRectangle.X+(lblErrorRectangle.Width\-Self.Width)\/2                   |
| Y           | imgErrorInfo.Y+imgErrorInfo.Height                                             |
| ZIndex      | 15                                                                             |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## icnErrorCancel

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                       |
| -------- | --------------------------- |
| OnSelect | Set(varErrorMessage,false); |

### Design

| Property               | Value                                                       |
| ---------------------- | ----------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                           |
| BorderThickness        | 0                                                           |
| DisplayMode            | DisplayMode.Edit                                            |
| FocusedBorderThickness | 4                                                           |
| Height                 | 32                                                          |
| Icon                   | Icon.Cancel                                                 |
| Visible                | varErrorMessage                                             |
| Width                  | 32                                                          |
| X                      | lblErrorRectangle.X+lblErrorRectangle.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle.Y+32                                      |
| ZIndex                 | 13                                                          |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## imgErrorInfo

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
| Visible                | varErrorMessage               |
| Width                  | 48                            |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | lblErrorRectangle.Y+64        |
| ZIndex                 | 14                            |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## KeyQuestionAnswers

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

| Property | Value                                                                         |
| -------- | ----------------------------------------------------------------------------- |
| Height   | btnSaveKeyQuestions.Y\-Self.Y\-20                                             |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X        | (Parent.Width\-Self.Width)\/2                                                 |
| Y        | lblKeyQuestionsHeader.Y+lblKeyQuestionsHeader.Height+16                       |
| ZIndex   | 6                                                                             |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## lblErrorBackRectangle

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value            |
| ---------------------- | ---------------- |
| BorderStyle            | BorderStyle.None |
| BorderThickness        | 0                |
| DisplayMode            | DisplayMode.Edit |
| FocusedBorderThickness | 4                |
| Height                 | Parent.Height    |
| Visible                | varErrorMessage  |
| Width                  | Parent.Width     |
| X                      | 0                |
| Y                      | 0                |
| ZIndex                 | 10               |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## lblErrorRectangle

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
| Height                 | htmlLblErrorMsg.Height + imgErrorInfo.Height + icnErrorCancel.Height + 50     |
| Visible                | varErrorMessage                                                               |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 11                                                                            |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## lblKeyQuestionsHeader

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Live     | Live.Off               |
| Role     | TextRole.Default       |
| Text     | varString.KeyQuestions |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Bold                                                               |
| Height                 | 40                                                                            |
| Italic                 | false                                                                         |
| LineHeight             | 1.2                                                                           |
| Overflow               | Overflow.Hidden                                                               |
| PaddingBottom          | 5                                                                             |
| PaddingLeft            | 5                                                                             |
| PaddingRight           | 5                                                                             |
| PaddingTop             | 5                                                                             |
| Size                   | 22.5                                                                          |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | NavigationKeyQuestions.Y+NavigationKeyQuestions.Height+12                     |
| ZIndex                 | 7                                                                             |

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

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## NavigationKeyQuestions

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property           | Value                                                                                                                |
| ------------------ | -------------------------------------------------------------------------------------------------------------------- |
| CircleFill1        | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill2        | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill3        | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill4        | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ConnectorLineWidth | If( Parent.Size\<\=ScreenSize.Small, 140, 328 )                                                                      |
| InnerCircle1       | <table border="0"><tr><td>RGBA(248,210,42,1)</td></tr><tr><td style="background-color:#F8D22A"></td></tr></table>    |
| InnerCircle2       | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| InnerCircle3       | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| InnerCircle4       | <table border="0"><tr><td>RGBA(255,255,255,100)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 72                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | HeaderControlKeyQuestions.Height+12                                |
| ZIndex   | 8                                                                  |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                            |
| -------------- | -------------------------------- |
| Parent Control | New Request Key Questions Screen |

## shpRectangleBackGround\_NewRequestKeyQuestionsScreen

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
| Parent Control | New Request Key Questions Screen |

## shpUpperRectangle\_NewRequestKeyQuestionsScreen

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
| Parent Control | New Request Key Questions Screen |
