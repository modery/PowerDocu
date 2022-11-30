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

## Building Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                                                                                                                                                                                                                                                               |
| --------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnVisible | ClearCollect( colBuildings, Filter( BAR\_Buildings, Status.Value \= "Published" ) ); Reset(cmbSelectSpace); Reset(cmbSelectSlot); Reset(datepkEndDate); Reset(datepkStartDate); Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colReservedSpaces); Clear(colRequiredSlots); Clear(colDatesNotOccupied); Clear(colNewDatesNotOccupied); |

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

| Property      | Value                                  |
| ------------- | -------------------------------------- |
| Child Control | shpRectangleBackGround\_BuildingScreen |
| Child Control | shpUpperRectangle\_BuildingScreen      |
| Child Control | HeaderControlSelectBuilding            |
| Child Control | NavigationComponentSelectBuilding      |
| Child Control | lblSelectBuildingHeader                |
| Child Control | lblSelectBuilding                      |
| Child Control | cmbSelectBuilding                      |
| Child Control | shpBuildingSeparator                   |
| Child Control | lblBusinessReason                      |
| Child Control | txtBusinessReason                      |
| Child Control | shpReasonSeparator                     |
| Child Control | lblEligibility                         |
| Child Control | txtEligibilityCriteria                 |
| Child Control | htmlEligibilityText                    |
| Child Control | btnBackBuildingScreen                  |
| Child Control | btnSave&NextBuildingScreen             |
| Child Control | grpSelectBuilding                      |
| Child Control | grpBusinessReason                      |
| Child Control | grpEligibilityCriteria.                |

## btnBackBuildingScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                              |
| -------- | ------------------------------------------------------------------------------------------------------------------ |
| OnSelect | If( varAppSettings.KeyQuestions, Navigate(\[@'New Request Key Questions Screen'\]), Navigate(\[@'Home Screen'\]) ) |

### Data

| Property | Value                   |
| -------- | ----------------------- |
| Text     | varStringNew.BackBtnLbl |

### Design

| Property               | Value                                                                       |
| ---------------------- | --------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                |
| BorderStyle            | BorderStyle.Solid                                                           |
| BorderThickness        | 1                                                                           |
| DisplayMode            | DisplayMode.Edit                                                            |
| FocusedBorderThickness | 4                                                                           |
| Font                   | Font.'Segoe UI'                                                             |
| FontWeight             | FontWeight.Semibold                                                         |
| Height                 | 60                                                                          |
| Italic                 | false                                                                       |
| RadiusBottomLeft       | 4                                                                           |
| RadiusBottomRight      | 4                                                                           |
| RadiusTopLeft          | 4                                                                           |
| RadiusTopRight         | 4                                                                           |
| Size                   | 22.5                                                                        |
| Strikethrough          | false                                                                       |
| Underline              | false                                                                       |
| VerticalAlign          | VerticalAlign.Middle                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40 |
| X                      | HeaderControlSelectBuilding.X + 25                                          |
| Y                      | Parent.Height\-20\-Self.Height                                              |
| ZIndex                 | 14                                                                          |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## btnSave&NextBuildingScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                          |
| -------- | ------------------------------------------------------------------------------------------------------------------------------ |
| OnSelect | Set( varSelectedBuilding, First(colSelectedBuilding) ); If( \!IsBlank(txtBusinessReason), Navigate(\[@'Date Space Screen'\]) ) |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | varStringNew.SaveAndNextBtn |

### Design

| Property               | Value                                                                                                        |
| ---------------------- | ------------------------------------------------------------------------------------------------------------ |
| Align                  | Align.Center                                                                                                 |
| BorderStyle            | BorderStyle.None                                                                                             |
| BorderThickness        | 2                                                                                                            |
| DisplayMode            | If(IsBlank(txtBusinessReason.Text) \|\| IsBlank(cmbSelectBuilding.Selected.Title),Disabled,DisplayMode.Edit) |
| FocusedBorderThickness | 4                                                                                                            |
| Font                   | Font.'Segoe UI'                                                                                              |
| FontWeight             | FontWeight.Semibold                                                                                          |
| Height                 | 60                                                                                                           |
| Italic                 | false                                                                                                        |
| RadiusBottomLeft       | 4                                                                                                            |
| RadiusBottomRight      | 4                                                                                                            |
| RadiusTopLeft          | 4                                                                                                            |
| RadiusTopRight         | 4                                                                                                            |
| Size                   | 22.5                                                                                                         |
| Strikethrough          | false                                                                                                        |
| Underline              | false                                                                                                        |
| VerticalAlign          | VerticalAlign.Middle                                                                                         |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40                                  |
| X                      | btnBackBuildingScreen.Width + btnBackBuildingScreen.X + 30                                                   |
| Y                      | Parent.Height\-20\-Self.Height                                                                               |
| ZIndex                 | 15                                                                                                           |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## cmbSelectBuilding

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![combobox](resources/combobox.png) | Type: combobox |

### Behavior

| Property | Value                                                         |
| -------- | ------------------------------------------------------------- |
| OnChange | SetFocus(txtBusinessReason);                                  |
| OnSelect | ClearCollect(colSelectedBuilding,cmbSelectBuilding.Selected)  |

### Data

| Property             | Value                                                                                                                                                         |
| -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DisplayFields        | \["Title"\]                                                                                                                                                   |
| InputTextPlaceholder | varStringNew.SelectBuildingHintlbl                                                                                                                            |
| Items                | FirstN( Sort( Search(colBuildings, cmbSelectBuilding.SearchText, "Title"), "Created", Descending ), 5)                                                        |
| NavigateFields       | \[\]                                                                                                                                                          |
| SearchFields         | \["Title", "Address"\]                                                                                                                                        |
| SearchItems          | Search(FirstN( Sort( Search(colBuildings, cmbSelectBuilding.SearchText, "Title"), "Created", Descending ), 5),cmbSelectBuilding.SearchText,"Title","Address") |
| SelectMultiple       | false                                                                                                                                                         |

### Design

| Property                  | Value                                                                                                                 |
| ------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle               | BorderStyle.Solid                                                                                                     |
| BorderThickness           | 2                                                                                                                     |
| ChevronBackground         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ChevronDisabledBackground | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| ChevronDisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| ChevronFill               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| ChevronHoverBackground    | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ChevronHoverFill          | <table border="0"><tr><td>RGBA(33, 33, 33, 1)</td></tr><tr><td style="background-color:#212121"></td></tr></table>    |
| DisplayMode               | DisplayMode.Edit                                                                                                      |
| FocusedBorderThickness    | 4                                                                                                                     |
| Font                      | Font.'Segoe UI'                                                                                                       |
| FontWeight                | FontWeight.Normal                                                                                                     |
| Height                    | 70                                                                                                                    |
| MoreItemsButtonColor      | Self.ChevronBackground                                                                                                |
| SelectionTagColor         | Self.HoverColor                                                                                                       |
| SelectionTagFill          | Self.HoverFill                                                                                                        |
| Size                      | 25.5                                                                                                                  |
| Template                  | ListItemTemplate.Single                                                                                               |
| Width                     | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )                                         |
| X                         | (Parent.Width\-Self.Width)\/2                                                                                         |
| Y                         | lblSelectBuilding.Y+lblSelectBuilding.Height+8                                                                        |
| ZIndex                    | 8                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverColor          | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(212, 212, 212, 1)</td></tr><tr><td style="background-color:#D4D4D4"></td></tr></table> |
| PressedBorderColor  | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| SelectionColor      | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| SelectionFill       | <table border="0"><tr><td>RGBA(179, 179, 179, 1)</td></tr><tr><td style="background-color:#B3B3B3"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## grpBusinessReason

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## grpEligibilityCriteria.

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## grpSelectBuilding

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## HeaderControlSelectBuilding

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Behavior

| Property | Value |
| -------- | ----- |
| OnReset  |       |

### Data

| Property            | Value                              |
| ------------------- | ---------------------------------- |
| backLabel           | "back"                             |
| IsBackButtonVisible | false                              |
| IsHomeButtonVisible | true                               |
| NavigateHomeScreen  | 'Home Screen'                      |
| NavigateScreen      | 'New Request Key Questions Screen' |
| Text                | varStringNew.NewRequestHeaderText  |

### Design

| Property | Value                                                               |
| -------- | ------------------------------------------------------------------- |
| Height   | 80                                                                  |
| Width    |  If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                       |
| Y        | 0+0                                                                 |
| ZIndex   | 6                                                                   |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## htmlEligibilityText

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![htmlViewer](resources/htmlViewer.png) | Type: htmlViewer |

### Data

| Property | Value                                                                                                                                                          |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| HtmlText | If( IsBlank(cmbSelectBuilding.Selected.KeyEligibilityCriteria), varString.EligibilityCriteriaNotAvailable, cmbSelectBuilding.Selected.KeyEligibilityCriteria ) |

### Design

| Property      | Value                                                                         |
| ------------- | ----------------------------------------------------------------------------- |
| AutoHeight    | false                                                                         |
| BorderStyle   | BorderStyle.None                                                              |
| DisplayMode   | DisplayMode.Edit                                                              |
| Font          | Font.'Segoe UI'                                                               |
| Height        | 225                                                                           |
| PaddingBottom | 5                                                                             |
| PaddingTop    | 0                                                                             |
| Size          | 18                                                                            |
| Visible       | If(CountRows(cmbSelectBuilding.SelectedItems)\>0,true,false)                  |
| Width         | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X             | (Parent.Width\-Self.Width)\/2                                                 |
| Y             | lblEligibility.Y+lblEligibility.Height+8                                      |
| ZIndex        | 16                                                                            |

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

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## lblBusinessReason

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                    |
| -------- | ---------------------------------------- |
| Live     | Live.Off                                 |
| Role     | TextRole.Default                         |
| Text     | "\*" & varString.BusinessReasonForAccess |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Height                 | 32                                                                            |
| Italic                 | false                                                                         |
| LineHeight             | 1.2                                                                           |
| Overflow               | Overflow.Hidden                                                               |
| PaddingBottom          | 5                                                                             |
| PaddingLeft            | 5                                                                             |
| PaddingRight           | 5                                                                             |
| PaddingTop             | 5                                                                             |
| Size                   | 18                                                                            |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | shpBuildingSeparator.Y+shpBuildingSeparator.Height+25                         |
| ZIndex                 | 4                                                                             |

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
| HoverColor          | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## lblEligibility

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                    |
| -------- | ---------------------------------------- |
| Live     | Live.Off                                 |
| Role     | TextRole.Default                         |
| Text     | varString.ScreenBuildingEligibilityLabel |

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
| Height                 | 32                                                                            |
| Italic                 | false                                                                         |
| LineHeight             | 1.2                                                                           |
| Overflow               | Overflow.Hidden                                                               |
| PaddingBottom          | 5                                                                             |
| PaddingLeft            | 5                                                                             |
| PaddingRight           | 5                                                                             |
| PaddingTop             | 5                                                                             |
| Size                   | 18                                                                            |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Visible                | If(CountRows(cmbSelectBuilding.SelectedItems)\>0,true,false)                  |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | shpReasonSeparator.Y+shpReasonSeparator.Height+20                             |
| ZIndex                 | 9                                                                             |

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
| HoverColor          | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## lblSelectBuilding

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                             |
| -------- | --------------------------------- |
| Live     | Live.Off                          |
| Role     | TextRole.Default                  |
| Text     | "\*"&varString.NewBuildingRequest |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Height                 | 32                                                                            |
| Italic                 | false                                                                         |
| LineHeight             | 1.2                                                                           |
| Overflow               | Overflow.Hidden                                                               |
| PaddingBottom          | 5                                                                             |
| PaddingLeft            | 5                                                                             |
| PaddingRight           | 5                                                                             |
| PaddingTop             | 5                                                                             |
| Size                   | 18                                                                            |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | lblSelectBuildingHeader.Y+lblSelectBuildingHeader.Height+16                   |
| ZIndex                 | 5                                                                             |

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
| HoverColor          | <table border="0"><tr><td>RGBA(72, 70, 68, 1)</td></tr><tr><td style="background-color:#484644"></td></tr></table>    |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedColor        | Self.Color                                                                                                            |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## lblSelectBuildingHeader

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                           |
| -------- | ------------------------------- |
| Live     | Live.Off                        |
| Role     | TextRole.Default                |
| Text     | varStringNew.BuildingDetailsLbl |

### Design

| Property               | Value                                                                           |
| ---------------------- | ------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                      |
| BorderStyle            | BorderStyle.None                                                                |
| BorderThickness        | 2                                                                               |
| DisplayMode            | DisplayMode.Edit                                                                |
| FocusedBorderThickness | 4                                                                               |
| Font                   | Font.'Segoe UI'                                                                 |
| FontWeight             | FontWeight.Bold                                                                 |
| Height                 | 40                                                                              |
| Italic                 | false                                                                           |
| LineHeight             | 1.2                                                                             |
| Overflow               | Overflow.Hidden                                                                 |
| PaddingBottom          | 5                                                                               |
| PaddingLeft            | 5                                                                               |
| PaddingRight           | 5                                                                               |
| PaddingTop             | 5                                                                               |
| Size                   | 22.5                                                                            |
| Strikethrough          | false                                                                           |
| Underline              | false                                                                           |
| VerticalAlign          | VerticalAlign.Middle                                                            |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )   |
| X                      | (Parent.Width\-Self.Width)\/2                                                   |
| Y                      | NavigationComponentSelectBuilding.Y+NavigationComponentSelectBuilding.Height+12 |
| ZIndex                 | 13                                                                              |

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
| Parent Control | Building Screen |

## NavigationComponentSelectBuilding

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property           | Value                                                                                                              |
| ------------------ | ------------------------------------------------------------------------------------------------------------------ |
| CircleFill1        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill2        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill3        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill4        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ConnectorLineWidth | If( Parent.Size\<\=ScreenSize.Small, 140, 328 )                                                                    |
| InnerCircle1       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle2       | <table border="0"><tr><td>RGBA(248,210,42,1)</td></tr><tr><td style="background-color:#F8D22A"></td></tr></table>  |
| InnerCircle3       | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| InnerCircle4       | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 72                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | HeaderControlSelectBuilding.Height+12                              |
| ZIndex   | 3                                                                  |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## shpBuildingSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                        |
| ---------------------- | -------------------------------------------- |
| BorderStyle            | BorderStyle.None                             |
| BorderThickness        | 0                                            |
| DisplayMode            | DisplayMode.Edit                             |
| FocusedBorderThickness | 4                                            |
| Height                 | 1                                            |
| Width                  | cmbSelectBuilding.Width                      |
| X                      | cmbSelectBuilding.X                          |
| Y                      | cmbSelectBuilding.Y+cmbSelectBuilding.Height |
| ZIndex                 | 12                                           |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## shpReasonSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                        |
| ---------------------- | -------------------------------------------- |
| BorderStyle            | BorderStyle.None                             |
| BorderThickness        | 0                                            |
| DisplayMode            | DisplayMode.Edit                             |
| FocusedBorderThickness | 4                                            |
| Height                 | 1                                            |
| Width                  | txtBusinessReason.Width                      |
| X                      | txtBusinessReason.X                          |
| Y                      | txtBusinessReason.Y+txtBusinessReason.Height |
| ZIndex                 | 11                                           |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(225, 223, 221, 1)</td></tr><tr><td style="background-color:#E1DFDD"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## shpRectangleBackGround\_BuildingScreen

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
| Parent Control | Building Screen |

## shpUpperRectangle\_BuildingScreen

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
| Parent Control | Building Screen |

## txtBusinessReason

| Property                    | Value      |
| --------------------------- | ---------- |
| ![text](resources/text.png) | Type: text |

### Data

| Property    | Value                              |
| ----------- | ---------------------------------- |
| Default     | ""                                 |
| DelayOutput | false                              |
| HintText    | varStringNew.RequestReasonHintText |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 0                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Format                 | TextFormat.Text                                                               |
| Height                 | 70                                                                            |
| Italic                 | false                                                                         |
| Mode                   | TextMode.SingleLine                                                           |
| RadiusBottomLeft       | 0                                                                             |
| RadiusBottomRight      | 0                                                                             |
| RadiusTopLeft          | 0                                                                             |
| RadiusTopRight         | 0                                                                             |
| Size                   | 25.5                                                                          |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VirtualKeyboardMode    | VirtualKeyboardMode.Auto                                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | lblBusinessReason.Y+lblBusinessReason.Height+8                                |
| ZIndex                 | 7                                                                             |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverColor          | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| PressedFill         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |

## txtEligibilityCriteria

| Property                    | Value      |
| --------------------------- | ---------- |
| ![text](resources/text.png) | Type: text |

### Data

| Property    | Value                                             |
| ----------- | ------------------------------------------------- |
| Default     | cmbSelectBuilding.Selected.KeyEligibilityCriteria |
| DelayOutput | false                                             |
| HintText    | varString.EligibilityCriteriaNotAvailable         |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 0                                                                             |
| DisplayMode            | DisplayMode.View                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Format                 | TextFormat.Text                                                               |
| Height                 | 70                                                                            |
| Italic                 | false                                                                         |
| Mode                   | TextMode.MultiLine                                                            |
| PaddingLeft            | 5                                                                             |
| RadiusBottomLeft       | 0                                                                             |
| RadiusBottomRight      | 0                                                                             |
| RadiusTopLeft          | 0                                                                             |
| RadiusTopRight         | 0                                                                             |
| Size                   | 18                                                                            |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VirtualKeyboardMode    | VirtualKeyboardMode.Auto                                                      |
| Visible                | \/\/If(CountRows(cmbSelectBuilding.SelectedItems)\>0,true,false) false        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | lblEligibility.Y+lblEligibility.Height+8                                      |
| ZIndex                 | 10                                                                            |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| HoverColor          | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| PressedFill         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value           |
| -------------- | --------------- |
| Parent Control | Building Screen |
