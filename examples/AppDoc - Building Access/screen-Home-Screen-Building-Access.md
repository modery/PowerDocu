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

## Home Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| --------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | Refresh(BAR\_Requests); Refresh(BAR\_AppSettings); Set( varUser, Office365Users.MyProfileV2() ); \/\/ Get the locale of the logged\-in user. \/\/ Use the Language() function if you would like to automatically pull in the locale. \/\/ Or type your locale in between the quotation marks below. \/\/Set(varLanguage,Coalesce("",Param("locale"))); Set( varLanguage, Coalesce( "", Language() ) ); \/\/ Set English as a fallback if the language of the logged\-in user is not a currently supported language. If( IsEmpty( Filter( colTranslationsUser, IsSupported \= true, Locale \= varLanguage ) ), Set( varLanguage, "en\-US" ) ); If( IsEmpty( Filter( colTranslationUserNew, IsSupported \= true, Locale \= varLanguage ) ), Set( varLanguage, "en\-US" ) ); \/\/ Get all translations. \/\/ Substitute your own table of translations as desired provided the columns align. \/\/ Get the translations of the selected language. Set( varString, LookUp( colTranslationsUser, Locale \= varLanguage ) ); Set( varStringNew, LookUp( colTranslationUserNew, Locale \= varLanguage ) ); Set( varStringExt, LookUp( colTranslationsUserExtended, Locale \= varLanguage ) ); \/\/Get todays date formated as a number eg 20200527 , we use this to query sharepoint to avoid delegation Set( varTodayFormated, Value( Year(Today()) & Text( Month(Today()), "\[$\-en\-GB\]00" ) & Text( Day(Today()), "\[$\-en\-GB\]00" ) ) ); Concurrent( ClearCollect( colMyApprovals, Filter( BAR\_Requests, IsSlotBooked, IsRequired, ApproverGuid \= varUser.id, Status.Value \= "Pending Approval" ) ), \/\/Get todays request ClearCollect( colSelectedRequest, SortByColumns( Filter( BAR\_Requests, IsSlotBooked, IsRequired, RequestorGuid \= varUser.id, DateValue \>\= varTodayFormated, Active \= 1 ), "DateValue", Ascending ) ), If( IsEmpty(BAR\_AppSettings), Patch( BAR\_AppSettings, Defaults(BAR\_AppSettings), {Title: "Settings"} ) ); Set( varAppSettings, First(BAR\_AppSettings) ); ); ClearCollect( colNav, { Id: 0, ScreenName: varString.NewRequest, Description: varString.NewRequestDesc, Screen: If( varAppSettings.KeyQuestions, 'New Request Key Questions Screen', 'Building Screen' ), Image: 'building Copy 2@2x' }, { Id: 5, ScreenName: varString.MyRequestsTitle, Description: varString.MyRequestsDesc, Screen: 'My Request List Screen', Image: 'building Copy@2x' } ); \/\/If we use safety precautions add to the Nav If( varAppSettings.SafetyPrecautions, Collect( colNav, { Id: 4, ScreenName: varString.SafetyPrecautionsTitle, Description: varString.SafetyPrecautionsDesc, Screen: 'Safety Precaution List Screen', Image: 'building Copy 3@2x' } ) ); If( \!IsEmpty(colMyApprovals), Collect( colNav, { Id: 6, ScreenName: varString.ApprovalsTitle, Description: varString.ApprovalsDesc, Screen: 'Approval Screen', Image: 'building@2x', ShowNotification: true } ) ); Concurrent( \/\/Get todays request ClearCollect( colUserRequests, SortByColumns( Filter( BAR\_Requests, IsSlotBooked, IsRequired, RequestorGuid \= varUser.id, Status.Value \<\> "Withdrawn", RequestDate \>\= Today() ), "DateValue", Ascending ) ), ClearCollect( colUserRequestsToday, SortByColumns( Filter( BAR\_Requests, IsSlotBooked, IsRequired, RequestorGuid \= varUser.id, RequestDate \= Today(), Status.Value \= "Approved", 'CheckIn Status'.Value \<\> "Disqualified", 'CheckIn Status'.Value \<\> "Checked\-Out", Active \= 1 ), "DateValue", Ascending ) ), \/\/Get my approvals for count ClearCollect( colMyApprovals, Filter( BAR\_Requests, IsSlotBooked, IsRequired, ApproverGuid \= varUser.id, Status.Value \= "Pending Approval" ) ) ); Set( varShowApproved, false ); Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colDatesNotOccupied); Clear(colNewDatesNotOccupied); Clear(colAddRequestDates); Clear(colReservedSpaces); Reset(cmbSelectBuilding); Reset(txtBusinessReason); |

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

| Property      | Value                              |
| ------------- | ---------------------------------- |
| Child Control | shpRectangleBackGround\_HomeScreen |
| Child Control | shpUpperRectangle\_HomeScreen      |
| Child Control | lblUserTitle                       |
| Child Control | icnReload                          |
| Child Control | lblTodaysReservation               |
| Child Control | glryReservation                    |
| Child Control | shpSeparatorRectangle              |
| Child Control | lblUserDescription                 |
| Child Control | HomePageMenus                      |

## galleryTemplate7\_1

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect | false |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | glryReservation |

## glryReservation

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                |
| --------- | -------------------- |
| Items     | colUserRequestsToday |
| WrapCount | 1                    |

### Design

| Property               | Value                                                                                      |
| ---------------------- | ------------------------------------------------------------------------------------------ |
| BorderStyle            | BorderStyle.None                                                                           |
| BorderThickness        | 1                                                                                          |
| DisplayMode            | DisplayMode.Edit                                                                           |
| FocusedBorderThickness | 4                                                                                          |
| Height                 | If(\!IsEmpty(colUserRequestsToday), If(CountRows(colUserRequestsToday) \> 1, 232, 116), 0) |
| Layout                 | Layout.Vertical                                                                            |
| LoadingSpinner         | LoadingSpinner.None                                                                        |
| LoadingSpinnerColor    | Self.BorderColor                                                                           |
| TemplatePadding        | 6                                                                                          |
| TemplateSize           | 110                                                                                        |
| Transition             | Transition.None                                                                            |
| Visible                | If(IsEmpty(colUserRequestsToday), false, true)                                             |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 )               |
| X                      | (Parent.Width\-Self.Width) \/2                                                             |
| Y                      | lblTodaysReservation.Y+lblTodaysReservation.Height+8                                       |
| ZIndex                 | 5                                                                                          |

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

| Property       | Value               |
| -------------- | ------------------- |
| Child Control  | galleryTemplate7\_1 |
| Child Control  | rectReservation     |
| Child Control  | lblTimeSlot         |
| Child Control  | imgArrow            |
| Child Control  | lblBuildingInfo     |
| Parent Control | Home Screen         |

## HomePageMenus

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property     | Value                                                                                                              |
| ------------ | ------------------------------------------------------------------------------------------------------------------ |
| IsSmall      | false                                                                                                              |
| Items        | Sort(colNav,Id)                                                                                                    |
| PrimaryColor | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table> |
| ShowSubtitle | true                                                                                                               |

### Design

| Property | Value                                                                         |
| -------- | ----------------------------------------------------------------------------- |
| Height   | 520                                                                           |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X        | (Parent.Width\-Self.Width) \/2\-6                                             |
| Y        | lblUserDescription.Height + lblUserDescription.Y + 8                          |
| ZIndex   | 3                                                                             |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## icnReload

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Set( varUser, Office365Users.MyProfileV2() ); \/\/Get todays date formated as a number eg 20200527 , we use this to query sharepoint to avoid delegation Set( varTodayFormated, Value( Year(Today()) & Text( Month(Today()), "\[$\-en\-GB\]00" ) & Text( Day(Today()), "\[$\-en\-GB\]00" ) ) ); Concurrent( ClearCollect( colMyApprovals, Filter( BAR\_Requests, IsSlotBooked, IsRequired, ApproverGuid \= varUser.id, Status.Value \= "Pending Approval" ) ), \/\/Get todays request ClearCollect( colSelectedRequest, SortByColumns( Filter( BAR\_Requests, IsSlotBooked, IsRequired, RequestorGuid \= varUser.id, DateValue \>\= varTodayFormated, Active \= 1 ), "DateValue", Ascending ) ), If( IsEmpty(BAR\_AppSettings), Patch( BAR\_AppSettings, Defaults(BAR\_AppSettings), {Title: "Settings"} ) ); Set( varAppSettings, First(BAR\_AppSettings) ); ); ClearCollect( colNav, { Id: 0, ScreenName: varString.NewRequest, Description: varString.NewRequestDesc, Screen: If( varAppSettings.KeyQuestions, 'New Request Key Questions Screen', 'Building Screen' ), Image: 'building Copy 2@2x' }, { Id: 5, ScreenName: varString.MyRequestsTitle, Description: varString.MyRequestsDesc, Screen: 'My Request List Screen', Image: 'building Copy@2x' } ); \/\/If we use safety precautions add to the Nav If( varAppSettings.SafetyPrecautions, Collect( colNav, { Id: 4, ScreenName: varString.SafetyPrecautionsTitle, Description: varString.SafetyPrecautionsDesc, Screen: 'Safety Precaution List Screen', Image: 'building Copy 3@2x' } ) ); If( \!IsEmpty(colMyApprovals), Collect( colNav, { Id: 6, ScreenName: varString.ApprovalsTitle, Description: varString.ApprovalsDesc, Screen: 'Approval Screen', Image: 'building@2x', ShowNotification: true } ) ); |

### Design

| Property               | Value                                 |
| ---------------------- | ------------------------------------- |
| BorderStyle            | BorderStyle.Solid                     |
| BorderThickness        | 0                                     |
| DisplayMode            | DisplayMode.Edit                      |
| FocusedBorderThickness | 4                                     |
| Height                 | 36                                    |
| Icon                   | Icon.Reload                           |
| Width                  | 36                                    |
| X                      | lblUserTitle.X+lblUserTitle.Width\-48 |
| Y                      | 52                                    |
| ZIndex                 | 9                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
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

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## imgArrow

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value                                                                                                              |
| -------- | ------------------------------------------------------------------------------------------------------------------ |
| OnSelect | ClearCollect( colSelectedRequest, ThisItem ); Navigate( \[@'My Request Details Screen'\], ScreenTransition.None ); |

### Data

| Property | Value   |
| -------- | ------- |
| Image    | 'go@2x' |

### Design

| Property               | Value                                                      |
| ---------------------- | ---------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                           |
| BorderThickness        | 2                                                          |
| DisplayMode            | DisplayMode.Edit                                           |
| FocusedBorderThickness | 4                                                          |
| Height                 | 36                                                         |
| ImagePosition          | ImagePosition.Fit                                          |
| ImageRotation          | ImageRotation.None                                         |
| PaddingBottom          | 0                                                          |
| PaddingLeft            | 0                                                          |
| PaddingRight           | 0                                                          |
| PaddingTop             | 0                                                          |
| RadiusBottomLeft       | 0                                                          |
| RadiusBottomRight      | 0                                                          |
| RadiusTopLeft          | 0                                                          |
| RadiusTopRight         | 0                                                          |
| TabIndex               | 0                                                          |
| Width                  | 36                                                         |
| X                      | rectReservation.Width \- Self.Width \- 12                  |
| Y                      | rectReservation.Y+(rectReservation.Height\-Self.Height)\/2 |
| ZIndex                 | 6                                                          |

### Color Properties

| Property            | Value                                                                                                           |
| ------------------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| HoverFill           | Self.Fill                                                                                                       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| PressedFill         | Self.Fill                                                                                                       |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | glryReservation |

## lblBuildingInfo

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

### Design

| Property               | Value                            |
| ---------------------- | -------------------------------- |
| Align                  | Align.Left                       |
| BorderStyle            | BorderStyle.None                 |
| BorderThickness        | 2                                |
| DisplayMode            | DisplayMode.Edit                 |
| FocusedBorderThickness | 4                                |
| Font                   | Font.'Segoe UI'                  |
| FontWeight             | FontWeight.Normal                |
| Height                 | 40                               |
| Italic                 | false                            |
| LineHeight             | 1.2                              |
| Overflow               | Overflow.Hidden                  |
| PaddingBottom          | 5                                |
| PaddingLeft            | 5                                |
| PaddingRight           | 5                                |
| PaddingTop             | 5                                |
| Size                   | 18                               |
| Strikethrough          | false                            |
| Underline              | false                            |
| VerticalAlign          | VerticalAlign.Middle             |
| Width                  | 500                              |
| X                      | 18                               |
| Y                      | lblTimeSlot.Y+lblTimeSlot.Height |
| ZIndex                 | 5                                |

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
| HoverColor          | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | glryReservation |

## lblTimeSlot

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                        |
| -------- | ---------------------------------------------------------------------------- |
| Live     | Live.Off                                                                     |
| Role     | TextRole.Default                                                             |
| Text     | If( IsBlank(ThisItem.TimeSlot), varStringNew.FullDayLbl, ThisItem.TimeSlot ) |

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
| Width                  | 500                  |
| X                      | 18                   |
| Y                      | 15                   |
| ZIndex                 | 4                    |

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
| HoverColor          | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | glryReservation |

## lblTodaysReservation

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                    |
| -------- | ---------------------------------------- |
| Live     | Live.Off                                 |
| Role     | TextRole.Default                         |
| Text     | varString.ScreenAvailabilityRequestToday |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                   |
| AutoHeight             | false                                                                        |
| BorderStyle            | BorderStyle.None                                                             |
| BorderThickness        | 2                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Font                   | Font.'Segoe UI'                                                              |
| FontWeight             | FontWeight.Semibold                                                          |
| Height                 | 32                                                                           |
| Italic                 | false                                                                        |
| LineHeight             | 1.2                                                                          |
| Overflow               | Overflow.Hidden                                                              |
| PaddingBottom          | 5                                                                            |
| PaddingLeft            | 5                                                                            |
| PaddingRight           | 5                                                                            |
| PaddingTop             | 5                                                                            |
| Size                   | 18                                                                           |
| Strikethrough          | false                                                                        |
| Underline              | false                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                         |
| Visible                | If(IsEmpty(colUserRequestsToday), false, true)                               |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | lblUserTitle.Y+lblUserTitle.Height+4                                         |
| ZIndex                 | 6                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(96, 94, 92, 1)</td></tr><tr><td style="background-color:#605E5C"></td></tr></table>    |
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

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## lblUserDescription

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                           |
| -------- | ------------------------------- |
| Live     | Live.Off                        |
| Role     | TextRole.Default                |
| Text     | varStringNew.AppDescriptiveText |

### Design

| Property               | Value                                                                                                                           |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                                                                      |
| BorderStyle            | BorderStyle.None                                                                                                                |
| BorderThickness        | 2                                                                                                                               |
| DisplayMode            | DisplayMode.Edit                                                                                                                |
| FocusedBorderThickness | 4                                                                                                                               |
| Font                   | Font.'Segoe UI'                                                                                                                 |
| FontWeight             | FontWeight.Normal                                                                                                               |
| Height                 | 60                                                                                                                              |
| Italic                 | false                                                                                                                           |
| LineHeight             | 1.2                                                                                                                             |
| Overflow               | Overflow.Hidden                                                                                                                 |
| PaddingBottom          | 5                                                                                                                               |
| PaddingLeft            | 5                                                                                                                               |
| PaddingRight           | 5                                                                                                                               |
| PaddingTop             | 5                                                                                                                               |
| Size                   | 18                                                                                                                              |
| Strikethrough          | false                                                                                                                           |
| Underline              | false                                                                                                                           |
| VerticalAlign          | VerticalAlign.Middle                                                                                                            |
| Width                  | lblUserTitle.Width                                                                                                              |
| X                      | lblUserTitle.X                                                                                                                  |
| Y                      | If(IsEmpty(colUserRequestsToday), lblUserTitle.Y+lblUserTitle.Height+4,shpSeparatorRectangle.Y+shpSeparatorRectangle.Height+12) |
| ZIndex                 | 7                                                                                                                               |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(96, 94, 92, 1)</td></tr><tr><td style="background-color:#605E5C"></td></tr></table>    |
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

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## lblUserTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                           |
| -------- | ----------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                        |
| Role     | TextRole.Default                                                                                |
| Text     | Concatenate( varString.HomeComponentGreeting, " , ", Office365Users.MyProfileV2().displayName ) |

### Design

| Property               | Value                                                                        |
| ---------------------- | ---------------------------------------------------------------------------- |
| Align                  |                                                                              |
| AutoHeight             | false                                                                        |
| BorderStyle            | If(lblUserTitle.TabIndex\=0,Solid,BorderStyle.None)                          |
| BorderThickness        | 0                                                                            |
| DisplayMode            | DisplayMode.Edit                                                             |
| FocusedBorderThickness | 4                                                                            |
| Font                   | Font.'Segoe UI'                                                              |
| FontWeight             | Bold                                                                         |
| Height                 | 60                                                                           |
| Italic                 | false                                                                        |
| LineHeight             | 1.2                                                                          |
| Overflow               | Overflow.Hidden                                                              |
| PaddingBottom          | 5                                                                            |
| PaddingLeft            | 5                                                                            |
| PaddingRight           | 5                                                                            |
| PaddingTop             | 5                                                                            |
| Size                   | 22.5                                                                         |
| Strikethrough          | false                                                                        |
| TabIndex               | 0                                                                            |
| Underline              | false                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                         |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width) \/2                                               |
| Y                      | 40                                                                           |
| ZIndex                 | 8                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Fill                | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| FocusedBorderColor  | lblUserTitle.BorderColor                                                                                              |
| HoverBorderColor    | lblUserTitle.BorderColor                                                                                              |
| HoverColor          | lblUserTitle.Color                                                                                                    |
| HoverFill           | lblUserTitle.Fill                                                                                                     |
| PressedBorderColor  | lblUserTitle.BorderColor                                                                                              |
| PressedColor        | lblUserTitle.Color                                                                                                    |
| PressedFill         | lblUserTitle.Fill                                                                                                     |

### Child & Parent Controls

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## rectReservation

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value |
| -------- | ----- |
| OnSelect | false |

### Design

| Property               | Value             |
| ---------------------- | ----------------- |
| BorderStyle            | BorderStyle.Solid |
| BorderThickness        | 2                 |
| DisplayMode            | DisplayMode.Edit  |
| FocusedBorderThickness | 4                 |
| Height                 | 110               |
| Width                  | Parent.Width\-20  |
| X                      | 0                 |
| Y                      | 0                 |
| ZIndex                 | 3                 |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | glryReservation |

## shpRectangleBackGround\_HomeScreen

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

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## shpSeparatorRectangle

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                            |
| ---------------------- | -------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                 |
| BorderThickness        | 0                                                                                |
| DisplayMode            | DisplayMode.Edit                                                                 |
| FocusedBorderThickness | 4                                                                                |
| Height                 | 1                                                                                |
| Visible                | If(IsEmpty(colUserRequestsToday), false, true)                                   |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48, (App.DesignWidth\*2)\-48 )\-16 |
| X                      | (Parent.Width\-Self.Width)\/2 \-4                                                |
| Y                      | glryReservation.Y+glryReservation.Height+16                                      |
| ZIndex                 | 4                                                                                |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |

## shpUpperRectangle\_HomeScreen

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

| Property       | Value       |
| -------------- | ----------- |
| Parent Control | Home Screen |
