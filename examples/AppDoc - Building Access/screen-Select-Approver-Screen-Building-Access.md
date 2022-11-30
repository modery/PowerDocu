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

## Select Approver Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value |
| --------- | ----- |
| OnVisible |       |

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
| Child Control | shpRectangleBackGround\_SelectApproverScreen |
| Child Control | shpUpperRectangle\_SelectApproverScreen      |
| Child Control | HeaderControlSelectApprover                  |
| Child Control | lblErrorBackRectangle\_ApproverScreen        |
| Child Control | lblHiddenApproverName                        |
| Child Control | lblApproverName                              |
| Child Control | NavigationComponentSelectApprover            |
| Child Control | selectApproverLbl\_1                         |
| Child Control | selectApproverLbl                            |
| Child Control | selectApproverLbl\_2                         |
| Child Control | lblNoApprover                                |
| Child Control | chkSetDefaultApprover                        |
| Child Control | lblErrorRectangle\_ApproverScreen            |
| Child Control | selectApproverLbl\_3                         |
| Child Control | rctApproverComboBoxUnderline                 |
| Child Control | selectAlternateApproverCombobox              |
| Child Control | icnErrorCancel\_ApproverScreen               |
| Child Control | rctBckRectangleApproverScreen                |
| Child Control | lblRequestSummary                            |
| Child Control | imgErrorInfo\_ApproverScreen                 |
| Child Control | lblBuildingName                              |
| Child Control | lblErrorMessage\_ApproverScreen              |
| Child Control | lblBuildingValue                             |
| Child Control | glryRequiredDate&Space                       |
| Child Control | lblApproverGUID                              |
| Child Control | btnBackApproverScreen                        |
| Child Control | btnSave&NextApproverScreen                   |
| Child Control | grpDefaultApprover                           |
| Child Control | grpAlternateApprover                         |
| Child Control | grpErrorMessage\_ApproverScreen              |

## btnBackApproverScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                         |
| -------- | ----------------------------- |
| OnSelect | Navigate('Date Space Screen') |

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
| ZIndex                 | 6                                                                           |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## btnSave&NextApproverScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | ClearCollect( colAddRequestDates, AddColumns( colNewDatesNotOccupied, "ApproverNameText", lblApproverName.Text, "Approver", lblHiddenApproverName.Text, "ApproverGuid", lblApproverGUID.Text ) ); Clear(colFlowResponse); If( \!IsEmpty(colAddRequestDates), ClearCollect( colFlowResponse, BARCreateRequests.Run( JSON(colAddRequestDates), JSON(colAddRequestDates), txtBusinessReason.Text ) ); If( \!IsEmpty(colFlowResponse), Set( varErrorMessageAppover, true ) ); Clear(colNewDatesNotOccupied) ); Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colNewDatesNotOccupied); Clear(colAddRequestDates); Reset(selectAlternateApproverCombobox); Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colReservedSpaces); Clear(colRequiredSlots); Clear(colDatesNotOccupied); Clear(colNewDatesNotOccupied); |

### Data

| Property | Value            |
| -------- | ---------------- |
| Text     | varString.Submit |

### Design

| Property               | Value                                                                                                                                                                                       |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                |
| BorderStyle            | BorderStyle.None                                                                                                                                                                            |
| BorderThickness        | 2                                                                                                                                                                                           |
| DisplayMode            | If((chkSetDefaultApprover.Value && CountRows(selectAlternateApproverCombobox.SelectedItems)\<1)\|\|selectAlternateApproverCombobox.Selected.DisplayName\=varUser.displayName,Disabled,Edit) |
| FocusedBorderThickness | 4                                                                                                                                                                                           |
| Font                   | Font.'Segoe UI'                                                                                                                                                                             |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                         |
| Height                 | 60                                                                                                                                                                                          |
| Italic                 | false                                                                                                                                                                                       |
| RadiusBottomLeft       | 4                                                                                                                                                                                           |
| RadiusBottomRight      | 4                                                                                                                                                                                           |
| RadiusTopLeft          | 4                                                                                                                                                                                           |
| RadiusTopRight         | 4                                                                                                                                                                                           |
| Size                   | 22.5                                                                                                                                                                                        |
| Strikethrough          | false                                                                                                                                                                                       |
| Underline              | false                                                                                                                                                                                       |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40                                                                                                                 |
| X                      | btnBackDateScreen.Width + btnBackDateScreen.X + 30                                                                                                                                          |
| Y                      | Parent.Height\-20\-Self.Height                                                                                                                                                              |
| ZIndex                 | 7                                                                                                                                                                                           |

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
| Parent Control | Select Approver Screen |

## chkSetDefaultApprover

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![checkbox](resources/checkbox.png) | Type: checkbox |

### Behavior

| Property  | Value                                       |
| --------- | ------------------------------------------- |
| OnUncheck | Reset(\[@selectAlternateApproverCombobox\]) |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Text     | varStringNew.SpecifyDiffAppLbl |

### Design

| Property               | Value                                                                                                                                    |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                                         |
| BorderThickness        | 2                                                                                                                                        |
| CheckboxBackgroundFill | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table>                    |
| CheckboxBorderColor    | <table border="0"><tr><td>RGBA(109, 110, 167, 1)</td></tr><tr><td style="background-color:#6D6EA7"></td></tr></table>                    |
| CheckboxSize           | 50                                                                                                                                       |
| CheckmarkFill          | <table border="0"><tr><td>RGBA(109, 110, 167, 1)</td></tr><tr><td style="background-color:#6D6EA7"></td></tr></table>                    |
| DisplayMode            | DisplayMode.Edit                                                                                                                         |
| FocusedBorderThickness | 4                                                                                                                                        |
| Font                   | Font.'Segoe UI'                                                                                                                          |
| FontWeight             | FontWeight.Normal                                                                                                                        |
| Height                 | 85                                                                                                                                       |
| Italic                 | false                                                                                                                                    |
| PaddingBottom          | 0                                                                                                                                        |
| PaddingLeft            | 0                                                                                                                                        |
| PaddingRight           | 0                                                                                                                                        |
| PaddingTop             | 0                                                                                                                                        |
| Size                   | 21                                                                                                                                       |
| Strikethrough          | false                                                                                                                                    |
| Underline              | false                                                                                                                                    |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                     |
| Visible                | If(varAppSettings.SelectAlternateApprover,true,false)                                                                                    |
| Width                  | selectApproverLbl\_1.Width                                                                                                               |
| X                      | selectApproverLbl\_2.X                                                                                                                   |
| Y                      | If(IsBlank(selectApproverLbl\_2.Text),lblNoApprover.Y + lblNoApprover.Height+24,selectApproverLbl\_2.Y + selectApproverLbl\_2.Height+24) |
| ZIndex                 | 8                                                                                                                                        |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## galleryTemplate4\_3

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## glryRequiredDate&Space

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                                                                                                                                                                                                                    |
| --------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Items     | If( togDay.Value, Filter( colNewDatesNotOccupied, TimeSlot \= varStringNew.FullDayLbl2, Not(RequestDate in colReservedSpaces.RequestDate) && Not(RequestDate in colDatesOccupied.RequestDate) ), Filter( colNewDatesNotOccupied, Slot in cmbSelectSlot.SelectedItems ) ) |
| WrapCount | 1                                                                                                                                                                                                                                                                        |

### Design

| Property               | Value                                                                                                                                  |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                                                                      |
| DisplayMode            | DisplayMode.Edit                                                                                                                       |
| FocusedBorderThickness | 4                                                                                                                                      |
| Height                 | Min('btnSave&NextApproverScreen'.Y\-20\-Self.Y, CountRows('glryRequiredDate&Space'.AllItems)\*'glryRequiredDate&Space'.TemplateHeight) |
| Layout                 | Layout.Vertical                                                                                                                        |
| LoadingSpinner         | LoadingSpinner.None                                                                                                                    |
| LoadingSpinnerColor    | Self.BorderColor                                                                                                                       |
| TemplatePadding        | 0                                                                                                                                      |
| TemplateSize           | lblSpaceName3.Y+lblSpaceName3.Height+8                                                                                                 |
| Transition             | Transition.None                                                                                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 )                                                          |
| X                      | selectAlternateApproverCombobox.X                                                                                                      |
| Y                      | lblBuildingValue.Y + lblBuildingValue.Height + 12                                                                                      |
| ZIndex                 | 16                                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                           |
| -------------- | ------------------------------- |
| Child Control  | galleryTemplate4\_3             |
| Child Control  | shpRequiredDateGalleryRectangle |
| Child Control  | lblBuildingTitle3               |
| Child Control  | lblDateExtra1                   |
| Child Control  | lblTimeSlotExtra1               |
| Child Control  | imgDown3                        |
| Child Control  | icnNextArrow3                   |
| Child Control  | lblSpaceName3                   |
| Parent Control | Select Approver Screen          |

## grpAlternateApprover

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![group](resources/group.png) | Type: group |

### Design

| Property | Value |
| -------- | ----- |
| Height   | 5     |
| Width    | 5     |
| X        | 40    |
| Y        | \-365 |
| ZIndex   | 17    |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## grpDefaultApprover

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![group](resources/group.png) | Type: group |

### Design

| Property | Value |
| -------- | ----- |
| Height   | 5     |
| Width    | 5     |
| X        | 40    |
| Y        | \-365 |
| ZIndex   | 14    |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## grpErrorMessage\_ApproverScreen

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
| ZIndex   | 27    |

### Color Properties

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## HeaderControlSelectApprover

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
| NavigateScreen      | 'Date Space Screen'               |
| Text                | varStringNew.NewRequestHeaderText |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0+0                                                                |
| ZIndex   | 4                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## icnErrorCancel\_ApproverScreen

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                      |
| -------- | ------------------------------------------------------------------------------------------ |
| OnSelect | Refresh(BAR\_Requests); Set( varErrorMessage, false ); Navigate('My Request List Screen'); |

### Design

| Property               | Value                                                                                       |
| ---------------------- | ------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                           |
| BorderThickness        | 0                                                                                           |
| DisplayMode            | DisplayMode.Edit                                                                            |
| FocusedBorderThickness | 4                                                                                           |
| Height                 | 32                                                                                          |
| Icon                   | Icon.Cancel                                                                                 |
| Visible                | varErrorMessageAppover                                                                      |
| Width                  | 32                                                                                          |
| X                      | lblErrorRectangle\_ApproverScreen.X+lblErrorRectangle\_ApproverScreen.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_ApproverScreen.Y+32                                                      |
| ZIndex                 | 25                                                                                          |

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
| Parent Control | Select Approver Screen |

## icnNextArrow3

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | If( ThisItem.TimeSlot \= varStringNew.FullDayLbl2, RemoveIf( colNewDatesNotOccupied, DateValue \= ThisItem.DateValue ), Remove( colNewDatesNotOccupied, ThisItem ) ) |

### Data

| Property        | Value                |
| --------------- | -------------------- |
| AccessibleLabel | Self.Tooltip         |
| Tooltip         | "Remove the request" |

### Design

| Property               | Value                                   |
| ---------------------- | --------------------------------------- |
| BorderStyle            | BorderStyle.Solid                       |
| BorderThickness        | 0                                       |
| DisplayMode            | DisplayMode.Edit                        |
| FocusedBorderThickness | 4                                       |
| Height                 | 48                                      |
| Icon                   | Icon.Cancel                             |
| PaddingBottom          | 10                                      |
| PaddingLeft            | 10                                      |
| PaddingRight           | 10                                      |
| PaddingTop             | 10                                      |
| TabIndex               | 0                                       |
| Width                  | 48                                      |
| X                      | Parent.TemplateWidth\-Self.Width\-24    |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 5                                       |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## imgDown3

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value               |
| -------- | ------------------- |
| Image    | 'Auto\-approved@2x' |

### Design

| Property               | Value                                              |
| ---------------------- | -------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                   |
| BorderThickness        | 2                                                  |
| DisplayMode            | DisplayMode.Edit                                   |
| FocusedBorderThickness | 4                                                  |
| Height                 | 60                                                 |
| ImagePosition          | ImagePosition.Fit                                  |
| ImageRotation          | ImageRotation.None                                 |
| PaddingBottom          | 0                                                  |
| PaddingLeft            | 0                                                  |
| PaddingRight           | 0                                                  |
| PaddingTop             | 0                                                  |
| RadiusBottomLeft       | 0                                                  |
| RadiusBottomRight      | 0                                                  |
| RadiusTopLeft          | 0                                                  |
| RadiusTopRight         | 0                                                  |
| Width                  | 60                                                 |
| X                      | 12                                                 |
| Y                      | (Parent.TemplateHeight \/ 2) \- (Self.Height \/ 2) |
| ZIndex                 | 2                                                  |

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
| Parent Control | glryRequiredDate&Space |

## imgErrorInfo\_ApproverScreen

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![image](resources/image.png) | Type: image |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Image    | 'Task Complete Copy 4' |

### Design

| Property               | Value                                  |
| ---------------------- | -------------------------------------- |
| BorderStyle            | BorderStyle.None                       |
| BorderThickness        | 2                                      |
| DisplayMode            | DisplayMode.Edit                       |
| FocusedBorderThickness | 4                                      |
| Height                 | 48                                     |
| ImagePosition          | ImagePosition.Fit                      |
| ImageRotation          | ImageRotation.None                     |
| PaddingBottom          | 0                                      |
| PaddingLeft            | 0                                      |
| PaddingRight           | 0                                      |
| PaddingTop             | 0                                      |
| RadiusBottomLeft       | 0                                      |
| RadiusBottomRight      | 0                                      |
| RadiusTopLeft          | 0                                      |
| RadiusTopRight         | 0                                      |
| Visible                | varErrorMessageAppover                 |
| Width                  | 48                                     |
| X                      | (Parent.Width\-Self.Width)\/2          |
| Y                      | lblErrorRectangle\_ApproverScreen.Y+64 |
| ZIndex                 | 26                                     |

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
| Parent Control | Select Approver Screen |

## lblApproverGUID

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                    |
| -------- | -------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                 |
| Role     | TextRole.Default                                                                                         |
| Text     | If( IsBlank(lblHiddenApproverName.Text), "", Office365Users.UserProfile(lblHiddenApproverName.Text).Id ) |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Normal    |
| Height                 | 70                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 21                   |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Visible                | false                |
| Width                  | 560                  |
| X                      | 28                   |
| Y                      | 946                  |
| ZIndex                 | 21                   |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblApproverName

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                             |
| -------- | ----------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                          |
| Role     | TextRole.Default                                                                                                  |
| Text     | If( IsBlank(lblHiddenApproverName.Text), "", Office365Users.UserProfile(lblHiddenApproverName.Text).DisplayName ) |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Normal    |
| Height                 | 70                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 21                   |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Visible                | false                |
| Width                  | 560                  |
| X                      | 60                   |
| Y                      | 60                   |
| ZIndex                 | 20                   |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblBuildingName

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                             |
| -------- | --------------------------------- |
| Live     | Live.Off                          |
| Role     | TextRole.Default                  |
| Text     | varStringNew.SelectedBuildingsLbl |

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
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 ) |
| X                      | selectAlternateApproverCombobox.X                                             |
| Y                      | lblRequestSummary.Y + lblRequestSummary.Height + 16                           |
| ZIndex                 | 18                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblBuildingTitle3

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                            |
| -------- | -------------------------------- |
| Live     | Live.Off                         |
| Role     | TextRole.Default                 |
| Text     | cmbSelectBuilding.Selected.Title |

### Design

| Property      | Value                                         |
| ------------- | --------------------------------------------- |
| Align         | Align.Left                                    |
| BorderStyle   | BorderStyle.Solid                             |
| DisplayMode   | DisplayMode.Edit                              |
| Font          | Font.'Segoe UI'                               |
| FontWeight    | FontWeight.Bold                               |
| Height        | 40                                            |
| Overflow      | Overflow.Hidden                               |
| PaddingBottom | 0                                             |
| PaddingLeft   | 0                                             |
| PaddingRight  | 0                                             |
| PaddingTop    | 0                                             |
| Size          | 22.5                                          |
| VerticalAlign | VerticalAlign.Top                             |
| Width         | Parent.TemplateWidth \- imgDown3.Width \- 104 |
| X             | 88                                            |
| Y             | 8                                             |
| ZIndex        | 3                                             |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## lblBuildingValue

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                           |
| -------- | ------------------------------- |
| Live     | Live.Off                        |
| Role     | TextRole.Default                |
| Text     | cmbSelectBuilding.Selected.Name |

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
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 ) |
| X                      | selectAlternateApproverCombobox.X                                             |
| Y                      | lblBuildingName.Y + lblBuildingName.Height                                    |
| ZIndex                 | 19                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblDateExtra1

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
| Text     | ThisItem.RequestDate |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Normal    |
| Height                 | 14                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 21                   |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Width                  | 0                    |
| X                      | 320                  |
| Y                      | 14                   |
| ZIndex                 | 7                    |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## lblErrorBackRectangle\_ApproverScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                  |
| ---------------------- | ---------------------- |
| BorderStyle            | BorderStyle.None       |
| BorderThickness        | 0                      |
| DisplayMode            | DisplayMode.Edit       |
| FocusedBorderThickness | 4                      |
| Height                 | Parent.Height          |
| Visible                | varErrorMessageAppover |
| Width                  | Parent.Width           |
| X                      | 0                      |
| Y                      | 0                      |
| ZIndex                 | 22                     |

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
| Parent Control | Select Approver Screen |

## lblErrorMessage\_ApproverScreen

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                          |
| -------- | ------------------------------ |
| Live     | Live.Off                       |
| Role     | TextRole.Default               |
| Text     | varStringNew.RequestSuccessMsg |

### Design

| Property               | Value                                                                 |
| ---------------------- | --------------------------------------------------------------------- |
| Align                  | Align.Center                                                          |
| BorderStyle            | BorderStyle.None                                                      |
| BorderThickness        | 2                                                                     |
| DisplayMode            | DisplayMode.Edit                                                      |
| FocusedBorderThickness | 4                                                                     |
| Font                   | Font.'Segoe UI'                                                       |
| FontWeight             | FontWeight.Normal                                                     |
| Height                 | 40                                                                    |
| Italic                 | false                                                                 |
| LineHeight             | 1.2                                                                   |
| Overflow               | Overflow.Hidden                                                       |
| PaddingBottom          | 5                                                                     |
| PaddingLeft            | 5                                                                     |
| PaddingRight           | 5                                                                     |
| PaddingTop             | 5                                                                     |
| Size                   | 21                                                                    |
| Strikethrough          | false                                                                 |
| Underline              | false                                                                 |
| VerticalAlign          | VerticalAlign.Middle                                                  |
| Visible                | varErrorMessageAppover                                                |
| Width                  | lblErrorRectangle\_ApproverScreen.Width                               |
| X                      | lblErrorRectangle\_ApproverScreen.X                                   |
| Y                      | imgErrorInfo\_ApproverScreen.Y+imgErrorInfo\_ApproverScreen.Height+18 |
| ZIndex                 | 24                                                                    |

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
| Parent Control | Select Approver Screen |

## lblErrorRectangle\_ApproverScreen

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
| Visible                | varErrorMessageAppover                                                        |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 23                                                                            |

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
| Parent Control | Select Approver Screen |

## lblHiddenApproverName

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                                                                                                                                                                    |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                                                                 |
| Role     | TextRole.Default                                                                                                                                                                                                                                         |
| Text     | If( CountRows(selectAlternateApproverCombobox.SelectedItems) \> 0, selectAlternateApproverCombobox.Selected.UserPrincipalName, If( IsBlank(selectApproverLbl\_2.Text), "", Office365Users.UserProfileV2(selectApproverLbl\_2.Text).userPrincipalName ) ) |

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
| Height                 | 30                                                                            |
| Italic                 | false                                                                         |
| LineHeight             | 1.2                                                                           |
| Overflow               | Overflow.Hidden                                                               |
| PaddingBottom          | 5                                                                             |
| PaddingLeft            | 5                                                                             |
| PaddingRight           | 5                                                                             |
| PaddingTop             | 5                                                                             |
| Size                   | 21                                                                            |
| Strikethrough          | false                                                                         |
| Underline              | false                                                                         |
| VerticalAlign          | VerticalAlign.Middle                                                          |
| Visible                | false                                                                         |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | 40                                                                            |
| ZIndex                 | 10                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblNoApprover

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                             |
| -------- | --------------------------------- |
| Live     | Live.Off                          |
| Role     | TextRole.Default                  |
| Text     | varStringNew.NoDefaultApproverLbl |

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
| Height                 | 106                                                                           |
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
| Visible                | If(IsBlank(selectApproverLbl\_2.Text),true,false)                             |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | selectApproverLbl.Y + selectApproverLbl.Height+8                              |
| ZIndex                 | 27                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblRequestSummary

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varString.RequestSummary |

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
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-32, (App.DesignWidth\*2)\-32 ) |
| X                      | selectAlternateApproverCombobox.X                                             |
| Y                      | rctBckRectangleApproverScreen.Y + 8                                           |
| ZIndex                 | 17                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## lblSpaceName3

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                          |
| -------- | ---------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                       |
| Role     | TextRole.Default                                                                               |
| Text     | cmbSelectSpace.Selected.Title & " \| " & ThisItem.RequestDate & " (" & ThisItem.TimeSlot & ")" |

### Design

| Property      | Value                                        |
| ------------- | -------------------------------------------- |
| Align         | Align.Left                                   |
| BorderStyle   | BorderStyle.Solid                            |
| DisplayMode   | DisplayMode.Edit                             |
| Font          | Font.'Segoe UI'                              |
| FontWeight    | FontWeight.Normal                            |
| Height        | 40                                           |
| Overflow      | Overflow.Hidden                              |
| PaddingBottom | 0                                            |
| PaddingLeft   | 0                                            |
| PaddingRight  | 0                                            |
| PaddingTop    | 0                                            |
| Size          | 22.5                                         |
| VerticalAlign | VerticalAlign.Top                            |
| Width         | lblBuildingTitle3.Width+90                   |
| X             | 88                                           |
| Y             | lblBuildingTitle3.Y+lblBuildingTitle3.Height |
| ZIndex        | 4                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## lblTimeSlotExtra1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value             |
| -------- | ----------------- |
| Live     | Live.Off          |
| Role     | TextRole.Default  |
| Text     | ThisItem.TimeSlot |

### Design

| Property               | Value                |
| ---------------------- | -------------------- |
| Align                  | Align.Left           |
| BorderStyle            | BorderStyle.None     |
| BorderThickness        | 2                    |
| DisplayMode            | DisplayMode.Edit     |
| FocusedBorderThickness | 4                    |
| Font                   | Font.'Segoe UI'      |
| FontWeight             | FontWeight.Normal    |
| Height                 | 14                   |
| Italic                 | false                |
| LineHeight             | 1.2                  |
| Overflow               | Overflow.Hidden      |
| PaddingBottom          | 5                    |
| PaddingLeft            | 5                    |
| PaddingRight           | 5                    |
| PaddingTop             | 5                    |
| Size                   | 21                   |
| Strikethrough          | false                |
| Underline              | false                |
| VerticalAlign          | VerticalAlign.Middle |
| Width                  | 0                    |
| X                      | 320                  |
| Y                      | 14                   |
| ZIndex                 | 8                    |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## NavigationComponentSelectApprover

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property           | Value                                                                                                              |
| ------------------ | ------------------------------------------------------------------------------------------------------------------ |
| CircleFill1        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill2        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill3        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill4        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ConnectorLineWidth | If( Parent.Size\<\=ScreenSize.Small, 140, 328 )                                                                    |
| InnerCircle1       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle2       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle3       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle4       | <table border="0"><tr><td>RGBA(248,210,42,1)</td></tr><tr><td style="background-color:#F8D22A"></td></tr></table>  |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 72                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | HeaderControlSelectApprover.Height+12                              |
| ZIndex   | 3                                                                  |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## rctApproverComboBoxUnderline

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                    |
| ---------------------- | ------------------------------------------------------------------------ |
| BorderStyle            | BorderStyle.Solid                                                        |
| BorderThickness        | 1                                                                        |
| DisplayMode            | DisplayMode.Edit                                                         |
| FocusedBorderThickness | 4                                                                        |
| Height                 | 1                                                                        |
| Visible                | If(chkSetDefaultApprover.Value\=true,true,false)                         |
| Width                  | selectAlternateApproverCombobox.Width                                    |
| X                      | selectAlternateApproverCombobox.X                                        |
| Y                      | selectAlternateApproverCombobox.Y+selectAlternateApproverCombobox.Height |
| ZIndex                 | 9                                                                        |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(230, 230, 230, 1)</td></tr><tr><td style="background-color:#E6E6E6"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(128, 128, 128, 1)</td></tr><tr><td style="background-color:#808080"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## rctBckRectangleApproverScreen

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                           |
| ---------------------- | ------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                |
| BorderThickness        | 2                                                                               |
| DisplayMode            | DisplayMode.Edit                                                                |
| FocusedBorderThickness | 4                                                                               |
| Height                 | 'btnSave&NextApproverScreen'.Y\-20\-Self.Y                                      |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, (App.DesignWidth\*2) )            |
| X                      | (Parent.Width\-Self.Width)\/2                                                   |
| Y                      | selectAlternateApproverCombobox.Y + selectAlternateApproverCombobox.Height + 28 |
| ZIndex                 | 5                                                                               |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## selectAlternateApproverCombobox

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![combobox](resources/combobox.png) | Type: combobox |

### Behavior

| Property | Value                                   |
| -------- | --------------------------------------- |
| OnChange | SetFocus('btnSave&NextApproverScreen'); |

### Data

| Property             | Value                                                                                                                                                                                                 |
| -------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DisplayFields        | \["DisplayName"\]                                                                                                                                                                                     |
| InputTextPlaceholder | varStringNew.SelectAlternateApproverHintTxt                                                                                                                                                           |
| IsSearchable         | true                                                                                                                                                                                                  |
| Items                | Filter( Office365Users.SearchUser( { searchTerm: selectAlternateApproverCombobox.SearchText, top: 5 } ), Not(Mail in User().Email) )                                                                  |
| NavigateFields       | \[\]                                                                                                                                                                                                  |
| NoSelectionText      | varStringNew.SelectAlternateApproverHintTxt                                                                                                                                                           |
| Reset                | true                                                                                                                                                                                                  |
| SearchFields         | \["DisplayName"\]                                                                                                                                                                                     |
| SearchItems          | Search(Filter( Office365Users.SearchUser( { searchTerm: selectAlternateApproverCombobox.SearchText, top: 5 } ), Not(Mail in User().Email) ),selectAlternateApproverCombobox.SearchText,"DisplayName") |
| SelectMultiple       | false                                                                                                                                                                                                 |

### Design

| Property                  | Value                                                                                                                 |
| ------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle               | BorderStyle.None                                                                                                      |
| BorderThickness           | 2                                                                                                                     |
| ChevronBackground         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ChevronDisabledBackground | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| ChevronDisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| ChevronFill               | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| ChevronHoverBackground    | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ChevronHoverFill          | <table border="0"><tr><td>RGBA(33, 33, 33, 1)</td></tr><tr><td style="background-color:#212121"></td></tr></table>    |
| DisplayMode               | If(chkSetDefaultApprover.Value,DisplayMode.Edit,Disabled)                                                             |
| FocusedBorderThickness    | 4                                                                                                                     |
| Font                      | Font.'Segoe UI'                                                                                                       |
| FontWeight                | FontWeight.Normal                                                                                                     |
| Height                    | If(chkSetDefaultApprover.Value\=true,60,0)                                                                            |
| MoreItemsButtonColor      | Self.ChevronBackground                                                                                                |
| SelectionTagColor         | Self.HoverColor                                                                                                       |
| SelectionTagFill          | Self.HoverFill                                                                                                        |
| Size                      | 25.5                                                                                                                  |
| Template                  | ListItemTemplate.Single                                                                                               |
| Visible                   | If(chkSetDefaultApprover.Value\=true,true,false)                                                                      |
| Width                     | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )                                         |
| X                         | (Parent.Width\-Self.Width)\/2                                                                                         |
| Y                         | selectApproverLbl\_3.Y+selectApproverLbl\_3.Height + 8                                                                |
| ZIndex                    | 14                                                                                                                    |

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
| HoverColor          | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| HoverFill           | <table border="0"><tr><td>RGBA(212, 212, 212, 1)</td></tr><tr><td style="background-color:#D4D4D4"></td></tr></table> |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(109, 110, 167, 1)</td></tr><tr><td style="background-color:#6D6EA7"></td></tr></table> |
| PressedColor        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill         | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| SelectionColor      | <table border="0"><tr><td>RGBA(37, 36, 35, 1)</td></tr><tr><td style="background-color:#252423"></td></tr></table>    |
| SelectionFill       | <table border="0"><tr><td>RGBA(179, 179, 179, 1)</td></tr><tr><td style="background-color:#B3B3B3"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## selectApproverLbl

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                           |
| -------- | ------------------------------- |
| Live     | Live.Off                        |
| Role     | TextRole.Default                |
| Text     | varStringNew.DefaultApproverLbl |

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
| Y                      | selectApproverLbl\_1.Y + selectApproverLbl\_1.Height + 16                     |
| ZIndex                 | 12                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## selectApproverLbl\_1

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                           |
| -------- | ------------------------------- |
| Live     | Live.Off                        |
| Role     | TextRole.Default                |
| Text     | varStringNew.ApproverDetailsLbl |

### Design

| Property               | Value                                                                               |
| ---------------------- | ----------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                          |
| BorderStyle            | BorderStyle.None                                                                    |
| BorderThickness        | 2                                                                                   |
| DisplayMode            | DisplayMode.Edit                                                                    |
| FocusedBorderThickness | 4                                                                                   |
| Font                   | Font.'Segoe UI'                                                                     |
| FontWeight             | FontWeight.Bold                                                                     |
| Height                 | 40                                                                                  |
| Italic                 | false                                                                               |
| LineHeight             | 1.2                                                                                 |
| Overflow               | Overflow.Hidden                                                                     |
| PaddingBottom          | 5                                                                                   |
| PaddingLeft            | 5                                                                                   |
| PaddingRight           | 5                                                                                   |
| PaddingTop             | 5                                                                                   |
| Size                   | 22.5                                                                                |
| Strikethrough          | false                                                                               |
| Underline              | false                                                                               |
| VerticalAlign          | VerticalAlign.Middle                                                                |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 )       |
| X                      | (Parent.Width\-Self.Width)\/2                                                       |
| Y                      | NavigationComponentSelectApprover.Y + NavigationComponentSelectApprover.Height + 12 |
| ZIndex                 | 13                                                                                  |

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
| Parent Control | Select Approver Screen |

## selectApproverLbl\_2

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                                                                                                                                                                                                                                |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                                                                                                             |
| Role     | TextRole.Default                                                                                                                                                                                                                                                                                     |
| Text     | If( \!IsBlankOrError(cmbSelectBuilding.Selected.DefaultApproverEmailId), cmbSelectBuilding.Selected.DefaultApproverEmailId, If( IsBlankOrError(Office365Users.ManagerV2(varUser.userPrincipalName).userPrincipalName), "", Office365Users.ManagerV2(varUser.userPrincipalName).userPrincipalName ) ) |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| AutoHeight             | true                                                                          |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | DisplayMode.Edit                                                              |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Height                 | 46                                                                            |
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
| Y                      | selectApproverLbl.Y + selectApproverLbl.Height+8                              |
| ZIndex                 | 11                                                                            |

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
| Parent Control | Select Approver Screen |

## selectApproverLbl\_3

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                        |
| -------- | ---------------------------- |
| Live     | Live.Off                     |
| Role     | TextRole.Default             |
| Text     | varStringNew.SelectAltAppLbl |

### Design

| Property               | Value                                                                         |
| ---------------------- | ----------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                    |
| BorderStyle            | BorderStyle.None                                                              |
| BorderThickness        | 2                                                                             |
| DisplayMode            | If(chkSetDefaultApprover.Value,DisplayMode.Edit,Disabled)                     |
| FocusedBorderThickness | 4                                                                             |
| Font                   | Font.'Segoe UI'                                                               |
| FontWeight             | FontWeight.Normal                                                             |
| Height                 | If(chkSetDefaultApprover.Value\=true,32,0)                                    |
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
| Visible                | If(chkSetDefaultApprover.Value\=true,true,false)                              |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width \-48, (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | chkSetDefaultApprover.Y+chkSetDefaultApprover.Height + 32                     |
| ZIndex                 | 15                                                                            |

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

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | Select Approver Screen |

## shpRectangleBackGround\_SelectApproverScreen

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
| Parent Control | Select Approver Screen |

## shpRequiredDateGalleryRectangle

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property               | Value                    |
| ---------------------- | ------------------------ |
| BorderStyle            | BorderStyle.Solid        |
| BorderThickness        | 1                        |
| DisplayMode            | DisplayMode.Edit         |
| FocusedBorderThickness | 4                        |
| Height                 | Parent.TemplateHeight    |
| Width                  | Parent.TemplateWidth\-12 |
| X                      | 0                        |
| Y                      | 0                        |
| ZIndex                 | 1                        |

### Color Properties

| Property           | Value                                                                                                                 |
| ------------------ | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor        | <table border="0"><tr><td>RGBA(237, 235, 233, 1)</td></tr><tr><td style="background-color:#EDEBE9"></td></tr></table> |
| DisabledFill       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Fill               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor | Self.BorderColor                                                                                                      |
| HoverFill          | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PressedFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryRequiredDate&Space |

## shpUpperRectangle\_SelectApproverScreen

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
| Parent Control | Select Approver Screen |
