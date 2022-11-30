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

## Date Space Screen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![screen](resources/screen.png) | Type: screen |

### Behavior

| Property  | Value                                                                                                                 |
| --------- | --------------------------------------------------------------------------------------------------------------------- |
| OnVisible | ClearCollect( colFloors, Filter( BAR\_Spaces, BuildingID \= varSelectedBuilding.ID ) ); Reset(chkSetDefaultApprover); |

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

| Property      | Value                                   |
| ------------- | --------------------------------------- |
| Child Control | shpRectangleBackGround\_DateSpaceScreen |
| Child Control | shpUpperRectangle\_DateSpaceScreen      |
| Child Control | HeaderControlSelectDateAndSpace         |
| Child Control | lblErrorBackRectangle\_4                |
| Child Control | NavigationComponentSelectDateAndSpace   |
| Child Control | DateSpaceCanvas                         |
| Child Control | lblErrorRectangle\_4                    |
| Child Control | icnErrorCancel\_4                       |
| Child Control | imgErrorInfo\_4                         |
| Child Control | lblErrorMessage\_4                      |
| Child Control | btnBackDateScreen                       |
| Child Control | btnSave&NextDateScreen                  |

## btnBackDateScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                              |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Navigate('Building Screen'); Reset(cmbSelectSpace); Reset(cmbSelectSlot); Reset(datepkEndDate); Reset(datepkStartDate); Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colDatesNotOccupied); Clear(colNewDatesNotOccupied); Clear(colReservedSpaces); |

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
| ZIndex                 | 3                                                                           |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## btnCheckAvailability

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | Clear(colDateRanges); Clear(PossiblesSlots); Clear(colDatesOccupied); Clear(colReservedSpaces); Clear(colRequiredSlots); Clear(colDatesNotOccupied); Clear(colNewDatesNotOccupied); ForAll( FirstN( \[ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 \], DateDiff( datepkStartDate.SelectedDate, datepkEndDate.SelectedDate ) + 1 ), Collect( colDateRanges, DateAdd( datepkStartDate.SelectedDate, Value, Days ) ) ); ClearCollect( colDatesOccupied, Filter( BAR\_Requests, RequestDate \>\= datepkStartDate.SelectedDate && RequestDate \<\= datepkEndDate.SelectedDate, Active \= 1, IsSlotBooked, IsRequired, RequestorNameText \= varUser.displayName ) ); ClearCollect( colDatesOccupiedNew, AddColumns( colDatesOccupied, "Concat0", Concatenate( Text( ThisRecord.RequestDate, "\[$\-en\]mmddyyyy" ), ",", TimeSlot ) ) ); ClearCollect( colDatesFullyOccupied, Filter( colDatesOccupied, TimeSlot \= varStringNew.FullDayLbl2 ) ); ClearCollect( colTotalSeatsPerFloor, Filter( BAR\_Spaces, BuildingID \= cmbSelectSpace.Selected.BuildingID, Title \= cmbSelectSpace.Selected.Title, ID \= cmbSelectSpace.Selected.ID ) ); ClearCollect( colRecordsByDateFilter, Filter( BAR\_Requests, RequestDate \>\= datepkStartDate.SelectedDate && RequestDate \<\= datepkEndDate.SelectedDate, BuildingID \= cmbSelectSpace.Selected.BuildingID, FloorID \= cmbSelectSpace.Selected.ID ) ); ClearCollect( colA, Filter( colRecordsByDateFilter, Active \= 1, IsSlotBooked, RequestDate \>\= datepkStartDate.SelectedDate && RequestDate \<\= datepkEndDate.SelectedDate ) ); ClearCollect( colB, AddColumns( GroupBy( colA, "RequestDate", "TimeSlot", "XYZ" ), "CountFilled", CountRows(XYZ), "TotalSeats", LookUp( colTotalSeatsPerFloor, BuildingID \= cmbSelectSpace.Selected.BuildingID ).Capacity ) ); ClearCollect( colReservedSpaces, Filter( colB, CountFilled \>\= TotalSeats ) ); ClearCollect( colReservedSpacesNew, AddColumns( colReservedSpaces, "Concat1", Concatenate( Text( ThisRecord.RequestDate, "\[$\-en\]mmddyyyy" ), ",", TimeSlot ) ) ); ClearCollect( AllSlots, BAR\_TimeSlots.Title ); ClearCollect( AllSlot, Collect( AllSlots, {Title: varStringNew.FullDayLbl2} ) ); ForAll( colDateRanges As x, ForAll( AllSlot As y, Collect( PossiblesSlots, { RequestDate: x.Value, Slot: y.Title } ) ) ); If( togDay.Value, ClearCollect( colRequiredSlots, AddColumns( PossiblesSlots, "Concat2", Concatenate( Text( ThisRecord.RequestDate, "\[$\-en\]mmddyyyy" ), ",", Slot ), "IsSlotBooked", true, "IsRequired", If( ThisRecord.Slot \= varStringNew.FullDayLbl2, true, false ) ) ); , ClearCollect( colRequiredSlots, AddColumns( PossiblesSlots, "Concat2", Concatenate( Text( ThisRecord.RequestDate, "\[$\-en\]mmddyyyy" ), ",", Slot ), "IsSlotBooked", If( ThisRecord.Slot in cmbSelectSlot.SelectedItems, true, false ), "IsRequired", If( ThisRecord.Slot in cmbSelectSlot.SelectedItems, true, false ) ) ); ); If( togDay.Value, ClearCollect( colDatesNotOccupied, Filter( colRequiredSlots, Not(RequestDate in colReservedSpaces.RequestDate) && Not(RequestDate in colDatesOccupied.RequestDate) ) ); , ClearCollect( colDatesNotOccupied, Filter( colRequiredSlots, Not(ThisRecord.Concat2 in colReservedSpacesNew.Concat1) && Not(ThisRecord.Concat2 in colDatesOccupiedNew.Concat0) && Not(RequestDate in colDatesFullyOccupied.RequestDate) ) ); ); ClearCollect( colC, Filter( BAR\_Requests, BuildingID \= varSelectedBuilding.ID, IsSlotBooked, Active \= 1 ) ); ClearCollect( colD, AddColumns( GroupBy( colC, "RequestDate", "TimeSlot", "GHI" ), "C", CountRows(GHI), "OC", Value(varSelectedBuilding.MaxOccupancy) ) ); ClearCollect( colE, Filter( colD, C \>\= OC ) ); ClearCollect( colNewDatesNotOccupied, AddColumns( colDatesNotOccupied, "FloorID", cmbSelectSpace.Selected.ID, "BuildingID", cmbSelectSpace.Selected.BuildingID, "Title", varSelectedBuilding.Title, "TimeSlot", ThisRecord.Slot, "SpaceName", cmbSelectSpace.Selected.Title, "RequestorName", varUser.displayName, "DatePickerValue", ThisRecord.RequestDate, "DateValue", Value( Text( ThisRecord.RequestDate, "\[$\-en\-GB\]mmddyyyy" ) ), "BuildingMaxOccupancy", Value(varSelectedBuilding.MaxOccupancy), "BuldingName", varSelectedBuilding.Title, "RequestorEmail", varUser.mail, "OverCapacity", Switch( varSelectedBuilding.'Occupancy Threshold', 0, true, 100, false, If( togDay.Value, If( ThisRecord.RequestDate in colE.RequestDate, true, false ), If( ThisRecord.RequestDate in colE.RequestDate && ThisRecord.Slot in colE.TimeSlot, true, false ) ) ) ) ); |

### Data

| Property | Value                             |
| -------- | --------------------------------- |
| Text     | varStringNew.CheckAvailabilityBtn |

### Design

| Property               | Value                                                                                                                                                                                                                                                                                                                                                                                                                             |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                                                                                                                                                                                                                                      |
| BorderStyle            | BorderStyle.Solid                                                                                                                                                                                                                                                                                                                                                                                                                 |
| BorderThickness        | 1                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| DisplayMode            | If(datepkStartDate.SelectedDate\>\=Today()&&datepkEndDate.SelectedDate\>\=datepkStartDate.SelectedDate&&datepkStartDate.SelectedDate \<\= DateAdd(Today(),varAppSettings.BookingAdvance,Days)&&datepkEndDate.SelectedDate \<\= DateAdd(Today(),varAppSettings.BookingAdvance,Days)&&CountRows(cmbSelectSlot.SelectedItems) \< Count(BAR\_TimeSlots.ID)&&CountRows(cmbSelectSpace.SelectedItems) \> 0, DisplayMode.Edit, Disabled) |
| FocusedBorderThickness | 4                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                                                                                                                                                                                                                                                   |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                                                                                                                                                                                                                                                               |
| Height                 | 60                                                                                                                                                                                                                                                                                                                                                                                                                                |
| Italic                 | false                                                                                                                                                                                                                                                                                                                                                                                                                             |
| RadiusBottomLeft       | 4                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| RadiusBottomRight      | 4                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| RadiusTopLeft          | 4                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| RadiusTopRight         | 4                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| Size                   | 22.5                                                                                                                                                                                                                                                                                                                                                                                                                              |
| Strikethrough          | false                                                                                                                                                                                                                                                                                                                                                                                                                             |
| Underline              | false                                                                                                                                                                                                                                                                                                                                                                                                                             |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                                                                                                                                                                                                                                              |
| Width                  | cmbSelectSpace.Width                                                                                                                                                                                                                                                                                                                                                                                                              |
| X                      | cmbSelectSpace.X                                                                                                                                                                                                                                                                                                                                                                                                                  |
| Y                      | shpSpaceSeparator.Y+shpSpaceSeparator.Height+20                                                                                                                                                                                                                                                                                                                                                                                   |
| ZIndex                 | 12                                                                                                                                                                                                                                                                                                                                                                                                                                |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## btnSave&NextDateScreen

| Property                        | Value        |
| ------------------------------- | ------------ |
| ![button](resources/button.png) | Type: button |

### Behavior

| Property | Value                                                                     |
| -------- | ------------------------------------------------------------------------- |
| OnSelect | Set( varErrorMessageAppover, false ); Navigate('Select Approver Screen'); |

### Data

| Property | Value                       |
| -------- | --------------------------- |
| Text     | varStringNew.SaveAndNextBtn |

### Design

| Property               | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Center                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| BorderStyle            | BorderStyle.None                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| BorderThickness        | 2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| DisplayMode            | If(datepkStartDate.SelectedDate\>\=Today()&&datepkEndDate.SelectedDate\>\=datepkStartDate.SelectedDate&&datepkStartDate.SelectedDate \<\= DateAdd(Today(),varAppSettings.BookingAdvance,Days)&&datepkEndDate.SelectedDate \<\= DateAdd(Today(),varAppSettings.BookingAdvance,Days)&&CountRows(cmbSelectSlot.SelectedItems) \< Count(BAR\_TimeSlots.ID)&&CountRows(cmbSelectSpace.SelectedItems) \>0&&CountRows('glryAvailableDate&Space'.AllItems)\>0,DisplayMode.Edit,Disabled) |
| FocusedBorderThickness | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| Font                   | Font.'Segoe UI'                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| FontWeight             | FontWeight.Semibold                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| Height                 | 60                                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| Italic                 | false                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| RadiusBottomLeft       | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| RadiusBottomRight      | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| RadiusTopLeft          | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| RadiusTopRight         | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| Size                   | 22.5                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| Strikethrough          | false                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| Underline              | false                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 )\/2 \- 40                                                                                                                                                                                                                                                                                                                                                                                                      |
| X                      | btnBackDateScreen.Width + btnBackDateScreen.X + 30                                                                                                                                                                                                                                                                                                                                                                                                                               |
| Y                      | Parent.Height\-20\-Self.Height                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| ZIndex                 | 4                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## cmbSelectSlot

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![combobox](resources/combobox.png) | Type: combobox |

### Behavior

| Property | Value                           |
| -------- | ------------------------------- |
| OnChange | SetFocus(btnCheckAvailability); |

### Data

| Property             | Value                                                                                                  |
| -------------------- | ------------------------------------------------------------------------------------------------------ |
| DisplayFields        | \["Title"\]                                                                                            |
| InputTextPlaceholder | varStringNew.SelectTimeSlotLbl                                                                         |
| IsSearchable         | true                                                                                                   |
| Items                | \/\/\["Morning","Afternoon","Evening"\] BAR\_TimeSlots.Title                                           |
| NavigateFields       | \[\]                                                                                                   |
| NoSelectionText      | varStringNew.SelectTimeSlotLbl                                                                         |
| SearchFields         | \["Title"\]                                                                                            |
| SearchItems          | Search(\/\/\["Morning","Afternoon","Evening"\] BAR\_TimeSlots.Title ,cmbSelectSlot.SearchText,"Title") |
| SelectMultiple       | true                                                                                                   |
| Tooltip              | ""                                                                                                     |

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
| Visible                   | If(togDay.Value\=false,true,false)                                                                                    |
| Width                     | lblSelectSlot.Width\-14                                                                                               |
| X                         | lblSelectSlot.X                                                                                                       |
| Y                         | lblSelectSlot.Y+lblSelectSlot.Height+8                                                                                |
| ZIndex                    | 10                                                                                                                    |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## cmbSelectSpace

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![combobox](resources/combobox.png) | Type: combobox |

### Data

| Property             | Value                                                                                                                                       |
| -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------- |
| DisplayFields        | \["Title"\]                                                                                                                                 |
| InputTextPlaceholder | varStringNew.SelectSpaceHintText                                                                                                            |
| IsSearchable         | true                                                                                                                                        |
| Items                |  FirstN( Sort( Search(colFloors, cmbSelectSpace.SearchText, "Title"), "Created", Descending ), 5)                                           |
| NavigateFields       | \[\]                                                                                                                                        |
| NoSelectionText      | varStringNew.SelectSpaceHintText                                                                                                            |
| Reset                | false                                                                                                                                       |
| SearchFields         | \["Title"\]                                                                                                                                 |
| SearchItems          | Search( FirstN( Sort( Search(colFloors, cmbSelectSpace.SearchText, "Title"), "Created", Descending ), 5),cmbSelectSpace.SearchText,"Title") |
| SelectMultiple       | false                                                                                                                                       |

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
| Width                     | lblSelectSpace.Width\-14                                                                                              |
| X                         | lblSelectSpace.X                                                                                                      |
| Y                         | lblSelectSpace.Y+lblSelectSpace.Height+8                                                                              |
| ZIndex                    | 11                                                                                                                    |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## datepkEndDate

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![datepicker](resources/datepicker.png) | Type: datepicker |

### Behavior

| Property | Value                                                                                                                                                                                                        |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| OnChange | If(datepkEndDate.SelectedDate \> DateAdd(Today(),varAppSettings.BookingAdvance,Days),Set(varErrorMessageDayLimit,true)) \/\/Notify(varAppSettings.BookingAdvanceErrorMessage, NotificationType.Error, 3000)) |

### Data

| Property             | Value                                                                              |
| -------------------- | ---------------------------------------------------------------------------------- |
| DateTimeZone         | DateTimeZone.Local                                                                 |
| DefaultDate          | datepkStartDate.SelectedDate                                                       |
| EndYear              | 2050                                                                               |
| Format               | DateTimeFormat.ShortDate                                                           |
| InputTextPlaceholder | If(IsBlank(Self.SelectedDate), Text(Date(2001,12,31), Self.Format, Self.Language)) |
| StartOfWeek          | StartOfWeek.Sunday                                                                 |
| StartYear            | 1970                                                                               |

### Design

| Property               | Value                                                                                                                 |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                      |
| BorderThickness        | 0                                                                                                                     |
| CalendarHeaderFill     | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| CurrentDateFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DayColor               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisplayMode            | DisplayMode.Edit                                                                                                      |
| FocusedBorderThickness | 4                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                     |
| Height                 | 70                                                                                                                    |
| HoverDateFill          | <table border="0"><tr><td>RGBA(200, 200, 200, 1)</td></tr><tr><td style="background-color:#C8C8C8"></td></tr></table> |
| IconBackground         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| IconFill               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Italic                 | false                                                                                                                 |
| MonthColor             | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PaddingBottom          | 5                                                                                                                     |
| PaddingLeft            | 12                                                                                                                    |
| PaddingRight           | 5                                                                                                                     |
| PaddingTop             | 5                                                                                                                     |
| SelectedDateFill       | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| Size                   | 25.5                                                                                                                  |
| WeekColor              | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| Width                  | lblEndDate.Width\-14                                                                                                  |
| X                      | lblEndDate.X                                                                                                          |
| Y                      | lblEndDate.Y+lblEndDate.Height+8                                                                                      |
| ZIndex                 | 7                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## datepkStartDate

| Property                                | Value            |
| --------------------------------------- | ---------------- |
| ![datepicker](resources/datepicker.png) | Type: datepicker |

### Behavior

| Property | Value                                                                                                                                                                                                          |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnChange | If(datepkStartDate.SelectedDate \> DateAdd(Today(),varAppSettings.BookingAdvance,Days),Set(varErrorMessageDayLimit,true)) \/\/Notify(varAppSettings.BookingAdvanceErrorMessage, NotificationType.Error, 3000)) |

### Data

| Property             | Value                                                                              |
| -------------------- | ---------------------------------------------------------------------------------- |
| DateTimeZone         | DateTimeZone.Local                                                                 |
| DefaultDate          | Today()                                                                            |
| EndYear              | 2050                                                                               |
| Format               | DateTimeFormat.ShortDate                                                           |
| InputTextPlaceholder | If(IsBlank(Self.SelectedDate), Text(Date(2001,12,31), Self.Format, Self.Language)) |
| StartOfWeek          | StartOfWeek.Sunday                                                                 |
| StartYear            | 1970                                                                               |

### Design

| Property               | Value                                                                                                                 |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                      |
| BorderThickness        | 0                                                                                                                     |
| CalendarHeaderFill     | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| CurrentDateFill        | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DayColor               | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| DisplayMode            | DisplayMode.Edit                                                                                                      |
| FocusedBorderThickness | 4                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                     |
| Height                 | 70                                                                                                                    |
| HoverDateFill          | <table border="0"><tr><td>RGBA(200, 200, 200, 1)</td></tr><tr><td style="background-color:#C8C8C8"></td></tr></table> |
| IconBackground         | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| IconFill               | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Italic                 | false                                                                                                                 |
| MonthColor             | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| PaddingBottom          | 5                                                                                                                     |
| PaddingLeft            | 12                                                                                                                    |
| PaddingRight           | 5                                                                                                                     |
| PaddingTop             | 5                                                                                                                     |
| SelectedDateFill       | <table border="0"><tr><td>RGBA(135, 100, 184, 1)</td></tr><tr><td style="background-color:#8764B8"></td></tr></table> |
| Size                   | 25.5                                                                                                                  |
| WeekColor              | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| Width                  | lblStartDate.Width\-14                                                                                                |
| X                      | lblStartDate.X                                                                                                        |
| Y                      | lblStartDate.Y+lblStartDate.Height                                                                                    |
| ZIndex                 | 6                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledFill        | <table border="0"><tr><td>RGBA(244, 244, 244, 1)</td></tr><tr><td style="background-color:#F4F4F4"></td></tr></table> |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## DateSpaceCanvas

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![fluidGrid](resources/fluidGrid.png) | Type: fluidGrid |

### Data

| Property | Value |
| -------- | ----- |
| Reset    | false |

### Design

| Property        | Value                                                                                  |
| --------------- | -------------------------------------------------------------------------------------- |
| BorderStyle     | BorderStyle.Solid                                                                      |
| BorderThickness | 0                                                                                      |
| DisplayMode     | DisplayMode.Edit                                                                       |
| Height          | Parent.Height\-Self.Y\-'btnSave&NextDateScreen'.Height\-40                             |
| Visible         | true                                                                                   |
| Width           | If( Parent.Size\=ScreenSize.Small, App.Width , (App.DesignWidth\*2) )                  |
| X               | (Parent.Width\-Self.Width)\/2                                                          |
| Y               | NavigationComponentSelectDateAndSpace.Y + NavigationComponentSelectDateAndSpace.Height |
| ZIndex          | 5                                                                                      |

### Color Properties

| Property    | Value                                                                                                           |
| ----------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Child Control  | DateSpaceDataCard |
| Parent Control | Date Space Screen |

## DateSpaceDataCard

| Property                            | Value          |
| ----------------------------------- | -------------- |
| ![dataCard](resources/dataCard.png) | Type: dataCard |

### Design

| Property        | Value                         |
| --------------- | ----------------------------- |
| BorderStyle     | BorderStyle.Solid             |
| BorderThickness | 0                             |
| DisplayMode     | DisplayMode.Edit              |
| Height          | Parent.Height                 |
| Width           | Parent.Width                  |
| X               | (Parent.Width\-Self.Width)\/2 |
| Y               | Parent.Y                      |
| ZIndex          | 1                             |

### Color Properties

| Property    | Value                                                                                                           |
| ----------- | --------------------------------------------------------------------------------------------------------------- |
| BorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |
| Fill        | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value                      |
| -------------- | -------------------------- |
| Child Control  | lblSelectDate\/SpaceHeader |
| Child Control  | lblStartDate               |
| Child Control  | datepkStartDate            |
| Child Control  | shpStartDateSeparator      |
| Child Control  | lblEndDate                 |
| Child Control  | datepkEndDate              |
| Child Control  | shpEndDateSeparator        |
| Child Control  | lblToggleButton            |
| Child Control  | togDay                     |
| Child Control  | lblSelectSlot              |
| Child Control  | lblTimeSlotError           |
| Child Control  | cmbSelectSlot              |
| Child Control  | shpSlotSeparator           |
| Child Control  | lblSelectSpace             |
| Child Control  | cmbSelectSpace             |
| Child Control  | shpSpaceSeparator          |
| Child Control  | btnCheckAvailability       |
| Child Control  | lblDateAndSpaceError       |
| Child Control  | glryReservedDate&Space     |
| Child Control  | shpRectangleExtra          |
| Child Control  | lblReservedSpaces          |
| Child Control  | lblSelectedDateAndSpace    |
| Child Control  | glryReservedSpace          |
| Child Control  | glryAvailableDate&Space    |
| Child Control  | grpStartDate               |
| Child Control  | grpEndDate                 |
| Child Control  | grpSelectSlot              |
| Child Control  | grpSelectSpace             |
| Parent Control | DateSpaceCanvas            |

## galleryTemplate3

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | glryReservedSpace |

## galleryTemplate4\_2

| Property                                          | Value                 |
| ------------------------------------------------- | --------------------- |
| ![galleryTemplate](resources/galleryTemplate.png) | Type: galleryTemplate |

### Design

| Property     | Value                                                                                                           |
| ------------ | --------------------------------------------------------------------------------------------------------------- |
| TemplateFill | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Color Properties

### Child & Parent Controls

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## galleryTemplate5\_2

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
| Parent Control | glryReservedDate&Space |

## glryAvailableDate&Space

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                                                                                                             |
| --------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Items     | If( togDay.Value, Filter( colNewDatesNotOccupied, TimeSlot \= varStringNew.FullDayLbl2 ), Filter( colNewDatesNotOccupied, Slot in cmbSelectSlot.SelectedItems ) ) |
| WrapCount | 1                                                                                                                                                                 |

### Design

| Property               | Value                                                                                   |
| ---------------------- | --------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                       |
| DisplayMode            | DisplayMode.Edit                                                                        |
| FocusedBorderThickness | 4                                                                                       |
| Height                 | 'glryAvailableDate&Space'.TemplateHeight\*CountRows('glryAvailableDate&Space'.AllItems) |
| Layout                 | Layout.Vertical                                                                         |
| LoadingSpinner         | LoadingSpinner.None                                                                     |
| LoadingSpinnerColor    | Self.BorderColor                                                                        |
| TemplatePadding        | 0                                                                                       |
| TemplateSize           | 96                                                                                      |
| Transition             | Transition.None                                                                         |
| Width                  | Parent.Width\-48                                                                        |
| X                      | (Parent.Width\-Self.Width)\/2                                                           |
| Y                      | lblSelectedDateAndSpace.Y+lblSelectedDateAndSpace.Height+8                              |
| ZIndex                 | 18                                                                                      |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(166, 166, 166, 1)</td></tr><tr><td style="background-color:#A6A6A6"></td></tr></table> |
| DisabledBorderColor | Self.BorderColor                                                                                                      |
| DisabledFill        | Self.Fill                                                                                                             |
| Fill                | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | Self.BorderColor                                                                                                      |
| HoverFill           | Self.Fill                                                                                                             |
| PressedBorderColor  | Self.BorderColor                                                                                                      |
| PressedFill         | Self.Fill                                                                                                             |

### Child & Parent Controls

| Property       | Value                            |
| -------------- | -------------------------------- |
| Child Control  | galleryTemplate4\_2              |
| Child Control  | shpAvailableDateGalleryRectangle |
| Child Control  | lblBuildingTitle2                |
| Child Control  | imgDown2                         |
| Child Control  | icnNextArrow2                    |
| Child Control  | lblTimeSlotExtra                 |
| Child Control  | lblDateExtra                     |
| Child Control  | lblSpaceName2                    |
| Parent Control | DateSpaceDataCard                |

## glryReservedDate&Space

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                                             |
| --------- | --------------------------------------------------------------------------------- |
| Items     | SortByColumns( Filter( colDatesOccupied, IsRequired ), "RequestDate", Ascending ) |
| WrapCount | 1                                                                                 |

### Design

| Property               | Value                                                                                 |
| ---------------------- | ------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                                     |
| DisplayMode            | DisplayMode.Edit                                                                      |
| FocusedBorderThickness | 4                                                                                     |
| Height                 | 'glryReservedDate&Space'.TemplateHeight\*CountRows('glryReservedDate&Space'.AllItems) |
| Layout                 | Layout.Vertical                                                                       |
| LoadingSpinner         | LoadingSpinner.None                                                                   |
| LoadingSpinnerColor    | Self.BorderColor                                                                      |
| TemplatePadding        | 0                                                                                     |
| TemplateSize           | 96                                                                                    |
| Transition             | Transition.None                                                                       |
| Width                  | Parent.Width\-48                                                                      |
| X                      | (Parent.Width\-Self.Width)\/2                                                         |
| Y                      | lblDateAndSpaceError.Y+lblDateAndSpaceError.Height+4                                  |
| ZIndex                 | 19                                                                                    |

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

| Property       | Value                           |
| -------------- | ------------------------------- |
| Child Control  | galleryTemplate5\_2             |
| Child Control  | shpReservedDateGalleryRectangle |
| Child Control  | lblBuildingTitle                |
| Child Control  | lblTimeSlotValue                |
| Child Control  | imgDown                         |
| Child Control  | icnNextArrow                    |
| Child Control  | lblSpaceName                    |
| Parent Control | DateSpaceDataCard               |

## glryReservedSpace

| Property                          | Value         |
| --------------------------------- | ------------- |
| ![gallery](resources/gallery.png) | Type: gallery |

### Data

| Property  | Value                                                        |
| --------- | ------------------------------------------------------------ |
| Items     | SortByColumns( colReservedSpaces, "RequestDate", Ascending ) |
| WrapCount | 1                                                            |

### Design

| Property               | Value                                                                   |
| ---------------------- | ----------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                       |
| DisplayMode            | DisplayMode.Edit                                                        |
| FocusedBorderThickness | 4                                                                       |
| Height                 | glryReservedSpace.TemplateHeight\*CountRows(glryReservedSpace.AllItems) |
| Layout                 | Layout.Vertical                                                         |
| LoadingSpinner         | LoadingSpinner.None                                                     |
| LoadingSpinnerColor    | Self.BorderColor                                                        |
| TemplatePadding        | 0                                                                       |
| TemplateSize           | 42                                                                      |
| Transition             | Transition.None                                                         |
| Width                  | Parent.Width\-48                                                        |
| X                      | (Parent.Width\-Self.Width)\/2                                           |
| Y                      | lblReservedSpaces.Y+lblReservedSpaces.Height+8                          |
| ZIndex                 | 22                                                                      |

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

| Property       | Value             |
| -------------- | ----------------- |
| Child Control  | galleryTemplate3  |
| Child Control  | Title2            |
| Parent Control | DateSpaceDataCard |

## grpEndDate

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
| ZIndex   | 21    |

### Color Properties

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## grpSelectSlot

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
| ZIndex   | 22    |

### Color Properties

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## grpSelectSpace

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## grpStartDate

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## HeaderControlSelectDateAndSpace

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
| NavigateScreen      | 'Building Screen'                 |
| Text                | varStringNew.NewRequestHeaderText |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 80                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | 0+0                                                                |
| ZIndex   | 7                                                                  |

### Color Properties

| Property | Value                                                                                                                 |
| -------- | --------------------------------------------------------------------------------------------------------------------- |
| Color    | <table border="0"><tr><td>RGBA(80, 80, 80, 1)</td></tr><tr><td style="background-color:#505050"></td></tr></table>    |
| Fill     | <table border="0"><tr><td>RGBA(243, 242, 241, 1)</td></tr><tr><td style="background-color:#F3F2F1"></td></tr></table> |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## icnErrorCancel\_4

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                               |
| -------- | ----------------------------------- |
| OnSelect | Set(varErrorMessageDayLimit,false); |

### Design

| Property               | Value                                                             |
| ---------------------- | ----------------------------------------------------------------- |
| BorderStyle            | BorderStyle.Solid                                                 |
| BorderThickness        | 0                                                                 |
| DisplayMode            | DisplayMode.Edit                                                  |
| FocusedBorderThickness | 4                                                                 |
| Height                 | 32                                                                |
| Icon                   | Icon.Cancel                                                       |
| Visible                | varErrorMessageDayLimit                                           |
| Width                  | 32                                                                |
| X                      | lblErrorRectangle\_4.X+lblErrorRectangle\_4.Width\-32\-Self.Width |
| Y                      | lblErrorRectangle\_4.Y+32                                         |
| ZIndex                 | 11                                                                |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## icnNextArrow

| Property                    | Value      |
| --------------------------- | ---------- |
| ![icon](resources/icon.png) | Type: icon |

### Behavior

| Property | Value                                                                                                                                                                                                                                                                                                                             |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| OnSelect | \/\*Set(locRowId,ThisItem.ID); Set(locRowGUID,ThisItem.'Request Collection ID'); If(ThisItem.TimeSlot\="Full\-day", RemoveIf(BAR\_Requests,  Value(locRowGUID) in 'Request Collection ID' && ThisItem.DateValue in Text(DateValue));, RemoveIf(BAR\_Requests,ID \= Value(locRowId)); );\*\/ Remove( colDatesOccupied, ThisItem ); |

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
| X                      | Parent.TemplateWidth\-Self.Width\-12    |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 5                                       |

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
| HoverColor          | Self.Color                                                                                                            |
| HoverFill           | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedColor        | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| PressedFill         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value                  |
| -------------- | ---------------------- |
| Parent Control | glryReservedDate&Space |

## icnNextArrow2

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
| X                      | Parent.TemplateWidth\-Self.Width\-12    |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 5                                       |

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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## imgDown

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
| Height                 | 60                                      |
| ImagePosition          | ImagePosition.Fit                       |
| ImageRotation          | ImageRotation.None                      |
| PaddingBottom          | 0                                       |
| PaddingLeft            | 0                                       |
| PaddingRight           | 0                                       |
| PaddingTop             | 5                                       |
| RadiusBottomLeft       | 0                                       |
| RadiusBottomRight      | 0                                       |
| RadiusTopLeft          | 0                                       |
| RadiusTopRight         | 0                                       |
| Width                  | 60                                      |
| X                      | 12                                      |
| Y                      | (Parent.TemplateHeight\-Self.Height)\/2 |
| ZIndex                 | 2                                       |

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
| Parent Control | glryReservedDate&Space |

## imgDown2

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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## imgErrorInfo\_4

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
| Visible                | varErrorMessageDayLimit       |
| Width                  | 48                            |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | lblErrorRectangle\_4.Y+64     |
| ZIndex                 | 12                            |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## lblBuildingTitle

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | ThisItem.Title   |

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
| Size          | 21                                           |
| VerticalAlign | VerticalAlign.Top                            |
| Width         | Parent.TemplateWidth \- imgDown.Width \- 104 |
| X             | 88                                           |
| Y             | 8                                            |
| ZIndex        | 3                                            |

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
| Parent Control | glryReservedDate&Space |

## lblBuildingTitle2

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
| Size          | 21                                            |
| VerticalAlign | VerticalAlign.Top                             |
| Width         | Parent.TemplateWidth \- imgDown2.Width \- 104 |
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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## lblDateAndSpaceError

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                |
| -------- | ------------------------------------ |
| Live     | Live.Off                             |
| Role     | TextRole.Default                     |
| Text     | varStringNew.AlreadyReservedDatesLbl |

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
| Visible                | If(CountRows('glryReservedDate&Space'.AllItems)\>0,true,false) |
| Width                  | Parent.Width\-48                                               |
| X                      | (Parent.Width\-Self.Width)\/2                                  |
| Y                      | btnCheckAvailability.Y+btnCheckAvailability.Height+16          |
| ZIndex                 | 20                                                             |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(196, 49, 75, 1)</td></tr><tr><td style="background-color:#C4314B"></td></tr></table>   |
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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblDateExtra

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
| Height                 | 45                   |
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
| X                      | 560                  |
| Y                      | 40                   |
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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## lblEndDate

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                   |
| -------- | ----------------------- |
| Live     | Live.Off                |
| Role     | TextRole.Default        |
| Text     | varStringNew.EndDateLbl |

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
| Height                 | 32                                                      |
| Italic                 | false                                                   |
| LineHeight             | 1.2                                                     |
| Overflow               | Overflow.Hidden                                         |
| PaddingBottom          | 5                                                       |
| PaddingLeft            | 5                                                       |
| PaddingRight           | 5                                                       |
| PaddingTop             | 5                                                       |
| Size                   | 18                                                      |
| Strikethrough          | false                                                   |
| Underline              | false                                                   |
| VerticalAlign          | VerticalAlign.Middle                                    |
| Width                  | 'lblSelectDate\/SpaceHeader'.Width                      |
| X                      | 'lblSelectDate\/SpaceHeader'.X                          |
| Y                      | shpStartDateSeparator.Y+shpStartDateSeparator.Height+24 |
| ZIndex                 | 3                                                       |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblErrorBackRectangle\_4

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
| Visible                | varErrorMessageDayLimit |
| Width                  | Parent.Width            |
| X                      | 0                       |
| Y                      | 0                       |
| ZIndex                 | 8                       |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## lblErrorMessage\_4

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                      |
| -------- | ------------------------------------------------------------------------------------------ |
| Live     | Live.Off                                                                                   |
| Role     | TextRole.Default                                                                           |
| Text     | varStringNew.YouCanResText & varAppSettings.BookingAdvance & varStringNew.AdavanceDaysText |
| Tooltip  | varAppSettings.BookingAdvanceErrorMessage                                                  |

### Design

| Property               | Value                                       |
| ---------------------- | ------------------------------------------- |
| Align                  | Align.Center                                |
| AutoHeight             | true                                        |
| BorderStyle            | BorderStyle.None                            |
| BorderThickness        | 2                                           |
| DisplayMode            | DisplayMode.Edit                            |
| FocusedBorderThickness | 4                                           |
| Font                   | Font.'Segoe UI'                             |
| FontWeight             | FontWeight.Normal                           |
| Height                 |                                             |
| Italic                 | false                                       |
| LineHeight             | 1.2                                         |
| Overflow               | Overflow.Hidden                             |
| PaddingBottom          | 35                                          |
| PaddingLeft            | 5                                           |
| PaddingRight           | 5                                           |
| PaddingTop             | 0                                           |
| Size                   | 21                                          |
| Strikethrough          | false                                       |
| Underline              | false                                       |
| VerticalAlign          | VerticalAlign.Middle                        |
| Visible                | varErrorMessageDayLimit                     |
| Width                  | lblErrorRectangle\_4.Width \- 48            |
| Wrap                   | true                                        |
| X                      | lblErrorRectangle\_4.X+24                   |
| Y                      | imgErrorInfo\_4.Y+imgErrorInfo\_4.Height+18 |
| ZIndex                 | 10                                          |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## lblErrorRectangle\_4

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
| Height                 | lblErrorMessage\_4.Height + 130                                               |
| Visible                | varErrorMessageDayLimit                                                       |
| Width                  | If( Parent.Size\=ScreenSize.Small, App.Width\-48 , (App.DesignWidth\*2)\-48 ) |
| X                      | (Parent.Width\-Self.Width)\/2                                                 |
| Y                      | (Parent.Height\-Self.Height)\/2                                               |
| ZIndex                 | 9                                                                             |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## lblReservedSpaces

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                        |
| -------- | -------------------------------------------- |
| Live     | Live.Off                                     |
| Role     | TextRole.Default                             |
| Text     | varStringNew.AlreadyReservedSpaceerrorMsglbl |

### Design

| Property               | Value                                                        |
| ---------------------- | ------------------------------------------------------------ |
| Align                  | Align.Left                                                   |
| BorderStyle            | BorderStyle.None                                             |
| BorderThickness        | 2                                                            |
| DisplayMode            | DisplayMode.Edit                                             |
| FocusedBorderThickness | 4                                                            |
| Font                   | Font.'Segoe UI'                                              |
| FontWeight             | FontWeight.Bold                                              |
| Height                 | 32                                                           |
| Italic                 | false                                                        |
| LineHeight             | 1.2                                                          |
| Overflow               | Overflow.Hidden                                              |
| PaddingBottom          | 5                                                            |
| PaddingLeft            | 5                                                            |
| PaddingRight           | 5                                                            |
| PaddingTop             | 5                                                            |
| Size                   | 18                                                           |
| Strikethrough          | false                                                        |
| Underline              | false                                                        |
| VerticalAlign          | VerticalAlign.Middle                                         |
| Visible                | If(CountRows(glryReservedSpace.AllItems)\>0,true,false)      |
| Width                  | Parent.Width\-48                                             |
| X                      | (Parent.Width\-Self.Width)\/2                                |
| Y                      | 'glryReservedDate&Space'.Y+'glryReservedDate&Space'.Height+8 |
| ZIndex                 | 23                                                           |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(196, 49, 75, 1)</td></tr><tr><td style="background-color:#C4314B"></td></tr></table>   |
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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblSelectDate\/SpaceHeader

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varString.RequestDetails |

### Design

| Property               | Value                         |
| ---------------------- | ----------------------------- |
| Align                  | Align.Left                    |
| BorderStyle            | BorderStyle.None              |
| BorderThickness        | 2                             |
| DisplayMode            | DisplayMode.Edit              |
| FocusedBorderThickness | 4                             |
| Font                   | Font.'Segoe UI'               |
| FontWeight             | FontWeight.Bold               |
| Height                 | 40                            |
| Italic                 | false                         |
| LineHeight             | 1.2                           |
| Overflow               | Overflow.Hidden               |
| PaddingBottom          | 5                             |
| PaddingLeft            | 5                             |
| PaddingRight           | 5                             |
| PaddingTop             | 5                             |
| Size                   | 22.5                          |
| Strikethrough          | false                         |
| Underline              | false                         |
| VerticalAlign          | VerticalAlign.Middle          |
| Width                  | Parent.Width\-48              |
| X                      | (Parent.Width\-Self.Width)\/2 |
| Y                      | 12                            |
| ZIndex                 | 1                             |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblSelectedDateAndSpace

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                              |
| -------- | ---------------------------------- |
| Live     | Live.Off                           |
| Role     | TextRole.Default                   |
| Text     | varStringNew.SelectedDatenSpaceLbl |

### Design

| Property               | Value                                                                                                                                                      |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                                                                                                 |
| BorderStyle            | BorderStyle.None                                                                                                                                           |
| BorderThickness        | 2                                                                                                                                                          |
| DisplayMode            | DisplayMode.Edit                                                                                                                                           |
| FocusedBorderThickness | 4                                                                                                                                                          |
| Font                   | Font.'Segoe UI'                                                                                                                                            |
| FontWeight             | FontWeight.Bold                                                                                                                                            |
| Height                 | 32                                                                                                                                                         |
| Italic                 | false                                                                                                                                                      |
| LineHeight             | 1.2                                                                                                                                                        |
| Overflow               | Overflow.Hidden                                                                                                                                            |
| PaddingBottom          | 5                                                                                                                                                          |
| PaddingLeft            | 5                                                                                                                                                          |
| PaddingRight           | 5                                                                                                                                                          |
| PaddingTop             | 5                                                                                                                                                          |
| Size                   | 18                                                                                                                                                         |
| Strikethrough          | false                                                                                                                                                      |
| Underline              | false                                                                                                                                                      |
| VerticalAlign          | VerticalAlign.Middle                                                                                                                                       |
| Visible                | If(CountRows('glryAvailableDate&Space'.AllItems)\>0,true,false)                                                                                            |
| Width                  | Parent.Width\-48                                                                                                                                           |
| X                      | (Parent.Width\-Self.Width)\/2                                                                                                                              |
| Y                      | If(CountRows(glryReservedSpace.AllItems)\>0,glryReservedSpace.Y+glryReservedSpace.Height+12,'glryReservedDate&Space'.Y+'glryReservedDate&Space'.Height+12) |
| ZIndex                 | 21                                                                                                                                                         |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblSelectSlot

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                    |
| -------- | ------------------------ |
| Live     | Live.Off                 |
| Role     | TextRole.Default         |
| Text     | varStringNew.TimeSlotlbl |
| Tooltip  | ""                       |

### Design

| Property               | Value                                       |
| ---------------------- | ------------------------------------------- |
| Align                  | Align.Left                                  |
| BorderStyle            | BorderStyle.None                            |
| BorderThickness        | 2                                           |
| DisplayMode            | DisplayMode.Edit                            |
| FocusedBorderThickness | 4                                           |
| Font                   | Font.'Segoe UI'                             |
| FontWeight             | FontWeight.Normal                           |
| Height                 | 32                                          |
| Italic                 | false                                       |
| LineHeight             | 1.2                                         |
| Overflow               | Overflow.Hidden                             |
| PaddingBottom          | 5                                           |
| PaddingLeft            | 5                                           |
| PaddingRight           | 5                                           |
| PaddingTop             | 5                                           |
| Size                   | 18                                          |
| Strikethrough          | false                                       |
| Underline              | false                                       |
| VerticalAlign          | VerticalAlign.Middle                        |
| Visible                | If(togDay.Value\=false,true,false)          |
| Width                  | 'lblSelectDate\/SpaceHeader'.Width          |
| X                      | 'lblSelectDate\/SpaceHeader'.X              |
| Y                      | lblToggleButton.Y+lblToggleButton.Height+24 |
| ZIndex                 | 4                                           |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblSelectSpace

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value            |
| -------- | ---------------- |
| Live     | Live.Off         |
| Role     | TextRole.Default |
| Text     | varString.Space  |

### Design

| Property               | Value                                                                                                            |
| ---------------------- | ---------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Left                                                                                                       |
| BorderStyle            | BorderStyle.None                                                                                                 |
| BorderThickness        | 2                                                                                                                |
| DisplayMode            | DisplayMode.Edit                                                                                                 |
| FocusedBorderThickness | 4                                                                                                                |
| Font                   | Font.'Segoe UI'                                                                                                  |
| FontWeight             | FontWeight.Normal                                                                                                |
| Height                 | 32                                                                                                               |
| Italic                 | false                                                                                                            |
| LineHeight             | 1.2                                                                                                              |
| Overflow               | Overflow.Hidden                                                                                                  |
| PaddingBottom          | 5                                                                                                                |
| PaddingLeft            | 5                                                                                                                |
| PaddingRight           | 5                                                                                                                |
| PaddingTop             | 5                                                                                                                |
| Size                   | 18                                                                                                               |
| Strikethrough          | false                                                                                                            |
| Underline              | false                                                                                                            |
| VerticalAlign          | VerticalAlign.Middle                                                                                             |
| Width                  | 'lblSelectDate\/SpaceHeader'.Width                                                                               |
| X                      | 'lblSelectDate\/SpaceHeader'.X                                                                                   |
| Y                      | If(togDay.Value\=true,lblToggleButton.Y+lblToggleButton.Height+24,shpSlotSeparator.Y+shpSlotSeparator.Height+24) |
| ZIndex                 | 5                                                                                                                |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblSpaceName

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                               |
| -------- | ----------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                            |
| Role     | TextRole.Default                                                                    |
| Text     | ThisItem.SpaceName & " \| " & ThisItem.RequestDate & " (" & ThisItem.TimeSlot & ")" |

### Design

| Property      | Value                                      |
| ------------- | ------------------------------------------ |
| Align         | Align.Left                                 |
| BorderStyle   | BorderStyle.Solid                          |
| DisplayMode   | DisplayMode.Edit                           |
| Font          | Font.'Segoe UI'                            |
| FontWeight    | FontWeight.Normal                          |
| Height        | 40                                         |
| Overflow      | Overflow.Hidden                            |
| PaddingBottom | 0                                          |
| PaddingLeft   | 0                                          |
| PaddingRight  | 0                                          |
| PaddingTop    | 0                                          |
| Size          | 21                                         |
| VerticalAlign | VerticalAlign.Top                          |
| Width         | lblBuildingTitle.Width+90                  |
| X             | lblBuildingTitle.X                         |
| Y             | lblBuildingTitle.Y+lblBuildingTitle.Height |
| ZIndex        | 4                                          |

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
| Parent Control | glryReservedDate&Space |

## lblSpaceName2

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Data

| Property | Value                                                                                                                                                                                                                             |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                                                                                                                                                          |
| Role     | TextRole.Default                                                                                                                                                                                                                  |
| Text     | cmbSelectSpace.Selected.Title & " \| " & ThisItem.RequestDate & " (" & ThisItem.TimeSlot & ")"                                                                                                                                    |
| Tooltip  | \/\/cmbSelectSpace.Selected.Title & " \| " & If(togDay.Value,ThisItem.Value,ThisItem.RequestDate) & " (" & ThisItem.TimeSlot & ")" cmbSelectSpace.Selected.Title & " \| " & ThisItem.RequestDate & " (" & ThisItem.TimeSlot & ")" |

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
| Size          | 21                                           |
| VerticalAlign | VerticalAlign.Top                            |
| Width         | lblBuildingTitle2.Width+90                   |
| Wrap          | false                                        |
| X             | lblBuildingTitle2.X                          |
| Y             | lblBuildingTitle2.Y+lblBuildingTitle2.Height |
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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## lblStartDate

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                  |
| -------- | ---------------------- |
| Live     | Live.Off               |
| Role     | TextRole.Default       |
| Text     | varStringNew.StartDate |

### Design

| Property               | Value                                                                 |
| ---------------------- | --------------------------------------------------------------------- |
| Align                  | Align.Left                                                            |
| BorderStyle            | BorderStyle.None                                                      |
| BorderThickness        | 2                                                                     |
| DisplayMode            | DisplayMode.Edit                                                      |
| FocusedBorderThickness | 4                                                                     |
| Font                   | Font.'Segoe UI'                                                       |
| FontWeight             | FontWeight.Normal                                                     |
| Height                 | 32                                                                    |
| Italic                 | false                                                                 |
| LineHeight             | 1.2                                                                   |
| Overflow               | Overflow.Hidden                                                       |
| PaddingBottom          | 5                                                                     |
| PaddingLeft            | 5                                                                     |
| PaddingRight           | 5                                                                     |
| PaddingTop             | 5                                                                     |
| Size                   | 18                                                                    |
| Strikethrough          | false                                                                 |
| Underline              | false                                                                 |
| VerticalAlign          | VerticalAlign.Middle                                                  |
| Width                  | 'lblSelectDate\/SpaceHeader'.Width                                    |
| X                      | 'lblSelectDate\/SpaceHeader'.X                                        |
| Y                      | 'lblSelectDate\/SpaceHeader'.Y+'lblSelectDate\/SpaceHeader'.Height+24 |
| ZIndex                 | 2                                                                     |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblTimeSlotError

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                                                                                       |
| -------- | ------------------------------------------------------------------------------------------- |
| Live     | Live.Off                                                                                    |
| Role     | TextRole.Default                                                                            |
| Text     | Concatenate( varStringNew.TimeSlotErrorLbl, Text(Count(BAR\_TimeSlots.ID) \- 1), " items" ) |

### Design

| Property               | Value                                                                                                                   |
| ---------------------- | ----------------------------------------------------------------------------------------------------------------------- |
| Align                  | Align.Right                                                                                                             |
| BorderStyle            | BorderStyle.None                                                                                                        |
| BorderThickness        | 2                                                                                                                       |
| DisplayMode            | DisplayMode.Edit                                                                                                        |
| FocusedBorderThickness | 4                                                                                                                       |
| Font                   | Font.'Segoe UI'                                                                                                         |
| FontWeight             | FontWeight.Normal                                                                                                       |
| Height                 | lblSelectSlot.Height                                                                                                    |
| Italic                 | false                                                                                                                   |
| LineHeight             | 1.2                                                                                                                     |
| Overflow               | Overflow.Hidden                                                                                                         |
| PaddingBottom          | 5                                                                                                                       |
| PaddingLeft            | 5                                                                                                                       |
| PaddingRight           | 5                                                                                                                       |
| PaddingTop             | 5                                                                                                                       |
| Size                   | 18                                                                                                                      |
| Strikethrough          | false                                                                                                                   |
| Underline              | false                                                                                                                   |
| VerticalAlign          | VerticalAlign.Middle                                                                                                    |
| Visible                | If(Count(BAR\_TimeSlots.ID)\>0 && CountRows(cmbSelectSlot.SelectedItems) \> Count(BAR\_TimeSlots.ID) \- 1, true,false ) |
| Width                  | lblSelectSlot.Width                                                                                                     |
| X                      | lblSelectSlot.X                                                                                                         |
| Y                      | lblSelectSlot.Y                                                                                                         |
| ZIndex                 | 24                                                                                                                      |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(196, 49, 75, 1)</td></tr><tr><td style="background-color:#C4314B"></td></tr></table>   |
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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## lblTimeSlotExtra

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
| Height                 | 45                   |
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
| X                      | 560                  |
| Y                      | 40                   |
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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## lblTimeSlotValue

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
| Width                  | 0                    |
| X                      | 483                  |
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
| Parent Control | glryReservedDate&Space |

## lblToggleButton

| Property                      | Value       |
| ----------------------------- | ----------- |
| ![label](resources/label.png) | Type: label |

### Data

| Property | Value                   |
| -------- | ----------------------- |
| Live     | Live.Off                |
| Role     | TextRole.Default        |
| Text     | varStringNew.FullDayLbl |

### Design

| Property               | Value                                               |
| ---------------------- | --------------------------------------------------- |
| Align                  | Align.Left                                          |
| BorderStyle            | BorderStyle.None                                    |
| BorderThickness        | 2                                                   |
| DisplayMode            | DisplayMode.Edit                                    |
| FocusedBorderThickness | 4                                                   |
| Font                   | Font.'Segoe UI'                                     |
| FontWeight             | FontWeight.Normal                                   |
| Height                 | 32                                                  |
| Italic                 | false                                               |
| LineHeight             | 1.2                                                 |
| Overflow               | Overflow.Hidden                                     |
| PaddingBottom          | 5                                                   |
| PaddingLeft            | 5                                                   |
| PaddingRight           | 5                                                   |
| PaddingTop             | 5                                                   |
| Size                   | 18                                                  |
| Strikethrough          | false                                               |
| Underline              | false                                               |
| VerticalAlign          | VerticalAlign.Middle                                |
| Width                  | 100                                                 |
| X                      | 'lblSelectDate\/SpaceHeader'.X                      |
| Y                      | shpEndDateSeparator.Y+shpEndDateSeparator.Height+24 |
| ZIndex                 | 9                                                   |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## NavigationComponentSelectDateAndSpace

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![component](resources/component.png) | Type: component |

### Data

| Property           | Value                                                                                                              |
| ------------------ | ------------------------------------------------------------------------------------------------------------------ |
| CircleFill1        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill2        | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| CircleFill3        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| CircleFill4        | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| ConnectorLineWidth | If( Parent.Size\<\=ScreenSize.Small, 140, 328 )                                                                    |
| InnerCircle1       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle2       | <table border="0"><tr><td>RGBA(55,165,0,1)</td></tr><tr><td style="background-color:#37A500"></td></tr></table>    |
| InnerCircle3       | <table border="0"><tr><td>RGBA(248,210,42,1)</td></tr><tr><td style="background-color:#F8D22A"></td></tr></table>  |
| InnerCircle4       | <table border="0"><tr><td>RGBA(255,255,255,1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |

### Design

| Property | Value                                                              |
| -------- | ------------------------------------------------------------------ |
| Height   | 72                                                                 |
| Width    | If( Parent.Size\=ScreenSize.Small, App.Width, App.DesignWidth\*2 ) |
| X        | (Parent.Width\-Self.Width)\/2                                      |
| Y        | HeaderControlSelectDateAndSpace.Height+12                          |
| ZIndex   | 6                                                                  |

### Color Properties

| Property | Value                                                                                                           |
| -------- | --------------------------------------------------------------------------------------------------------------- |
| Fill     | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table> |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## shpAvailableDateGalleryRectangle

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property               | Value                 |
| ---------------------- | --------------------- |
| BorderStyle            | BorderStyle.Solid     |
| BorderThickness        | 1                     |
| DisplayMode            | DisplayMode.Edit      |
| FocusedBorderThickness | 4                     |
| Height                 | Parent.TemplateHeight |
| Width                  | Parent.TemplateWidth  |
| X                      | 0                     |
| Y                      | 0                     |
| ZIndex                 | 1                     |

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

| Property       | Value                   |
| -------------- | ----------------------- |
| Parent Control | glryAvailableDate&Space |

## shpEndDateSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                |
| ---------------------- | ------------------------------------ |
| BorderStyle            | BorderStyle.None                     |
| BorderThickness        | 0                                    |
| DisplayMode            | DisplayMode.Edit                     |
| FocusedBorderThickness | 4                                    |
| Height                 | 1                                    |
| Width                  | datepkEndDate.Width                  |
| X                      | datepkEndDate.X                      |
| Y                      | datepkEndDate.Y+datepkEndDate.Height |
| ZIndex                 | 14                                   |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## shpRectangleBackGround\_DateSpaceScreen

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## shpRectangleExtra

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                                                                                          |
| ---------------------- | -------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                               |
| BorderThickness        | 2                                                                                                              |
| DisplayMode            | DisplayMode.Edit                                                                                               |
| FocusedBorderThickness | 4                                                                                                              |
| Height                 | \/\/Parent.Height\-Self.Y\-'glryAvailableDate&Space'.Y\-'glryAvailableDate&Space'.Height Parent.Height\-Self.Y |
| Width                  | Parent.Width                                                                                                   |
| X                      | (Parent.Width\-Self.Width)\/2                                                                                  |
| Y                      | 'glryReservedDate&Space'.Y+'glryReservedDate&Space'.Height                                                     |
| ZIndex                 | 17                                                                                                             |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## shpReservedDateGalleryRectangle

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Behavior

| Property | Value          |
| -------- | -------------- |
| OnSelect | Select(Parent) |

### Design

| Property               | Value                 |
| ---------------------- | --------------------- |
| BorderStyle            | BorderStyle.Solid     |
| BorderThickness        | 1                     |
| DisplayMode            | DisplayMode.Edit      |
| FocusedBorderThickness | 4                     |
| Height                 | Parent.TemplateHeight |
| Width                  | Parent.TemplateWidth  |
| X                      | 0                     |
| Y                      | 0                     |
| ZIndex                 | 1                     |

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
| Parent Control | glryReservedDate&Space |

## shpSlotSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Data

| Property | Value |
| -------- | ----- |
| Tooltip  | ""    |

### Design

| Property               | Value                                |
| ---------------------- | ------------------------------------ |
| BorderStyle            | BorderStyle.None                     |
| BorderThickness        | 0                                    |
| DisplayMode            | DisplayMode.Edit                     |
| FocusedBorderThickness | 4                                    |
| Height                 | 1                                    |
| Visible                | If(togDay.Value\=false,true,false)   |
| Width                  | cmbSelectSlot.Width                  |
| X                      | cmbSelectSlot.X                      |
| Y                      | cmbSelectSlot.Y+cmbSelectSlot.Height |
| ZIndex                 | 15                                   |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## shpSpaceSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                  |
| ---------------------- | -------------------------------------- |
| BorderStyle            | BorderStyle.None                       |
| BorderThickness        | 0                                      |
| DisplayMode            | DisplayMode.Edit                       |
| FocusedBorderThickness | 4                                      |
| Height                 | 1                                      |
| Width                  | cmbSelectSpace.Width                   |
| X                      | cmbSelectSpace.X                       |
| Y                      | cmbSelectSpace.Y+cmbSelectSpace.Height |
| ZIndex                 | 16                                     |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## shpStartDateSeparator

| Property                              | Value           |
| ------------------------------------- | --------------- |
| ![rectangle](resources/rectangle.png) | Type: rectangle |

### Design

| Property               | Value                                    |
| ---------------------- | ---------------------------------------- |
| BorderStyle            | BorderStyle.None                         |
| BorderThickness        | 0                                        |
| DisplayMode            | DisplayMode.Edit                         |
| FocusedBorderThickness | 4                                        |
| Height                 | 1                                        |
| Width                  | datepkStartDate.Width                    |
| X                      | datepkStartDate.X                        |
| Y                      | datepkStartDate.Y+datepkStartDate.Height |
| ZIndex                 | 13                                       |

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |

## shpUpperRectangle\_DateSpaceScreen

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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | Date Space Screen |

## Title2

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
| Text     | ThisItem.RequestDate & " (" & ThisItem.TimeSlot & ")" |

### Design

| Property      | Value                |
| ------------- | -------------------- |
| Align         | Align.Left           |
| BorderStyle   | BorderStyle.Solid    |
| DisplayMode   | DisplayMode.Edit     |
| Font          | Font.'Segoe UI'      |
| FontWeight    | FontWeight.Semibold  |
| Height        | 40                   |
| Overflow      | Overflow.Hidden      |
| PaddingBottom | 5                    |
| PaddingLeft   | 5                    |
| PaddingRight  | 5                    |
| PaddingTop    | 5                    |
| Size          | 20                   |
| VerticalAlign | VerticalAlign.Top    |
| Width         | Parent.TemplateWidth |
| X             | 0                    |
| Y             | 0                    |
| ZIndex        | 2                    |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 1)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
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

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | glryReservedSpace |

## togDay

| Property                                    | Value              |
| ------------------------------------------- | ------------------ |
| ![toggleSwitch](resources/toggleSwitch.png) | Type: toggleSwitch |

### Data

| Property | Value |
| -------- | ----- |
| Default  | true  |

### Design

| Property               | Value                                                                                                                 |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderStyle            | BorderStyle.None                                                                                                      |
| BorderThickness        | 0                                                                                                                     |
| DisplayMode            | DisplayMode.Edit                                                                                                      |
| FalseFill              | <table border="0"><tr><td>RGBA(102, 102, 102, 1)</td></tr><tr><td style="background-color:#666666"></td></tr></table> |
| FalseHoverFill         | <table border="0"><tr><td>RGBA(33, 33, 33, 1)</td></tr><tr><td style="background-color:#212121"></td></tr></table>    |
| FocusedBorderThickness | 4                                                                                                                     |
| Font                   | Font.'Segoe UI'                                                                                                       |
| FontWeight             | FontWeight.Normal                                                                                                     |
| HandleFill             | <table border="0"><tr><td>RGBA(255, 255, 255, 1)</td></tr><tr><td style="background-color:#FFFFFF"></td></tr></table> |
| Height                 | 32                                                                                                                    |
| Size                   | 21                                                                                                                    |
| TextPosition           | TextPosition.Right                                                                                                    |
| TrueFill               | <table border="0"><tr><td>RGBA(98, 100, 167, 1)</td></tr><tr><td style="background-color:#6264A7"></td></tr></table>  |
| TrueHoverFill          | ColorFade(RGBA(135, 100, 184, 1), \-30%)                                                                              |
| Width                  | 110                                                                                                                   |
| X                      | lblToggleButton.X+lblToggleButton.Width+18                                                                            |
| Y                      | shpEndDateSeparator.Y+shpEndDateSeparator.Height+24                                                                   |
| ZIndex                 | 8                                                                                                                     |

### Color Properties

| Property            | Value                                                                                                                 |
| ------------------- | --------------------------------------------------------------------------------------------------------------------- |
| BorderColor         | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| Color               | <table border="0"><tr><td>RGBA(51, 51, 51, 1)</td></tr><tr><td style="background-color:#333333"></td></tr></table>    |
| DisabledBorderColor | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| DisabledColor       | <table border="0"><tr><td>RGBA(186, 186, 186, 1)</td></tr><tr><td style="background-color:#BABABA"></td></tr></table> |
| FocusedBorderColor  | Self.BorderColor                                                                                                      |
| HoverBorderColor    | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |
| PressedBorderColor  | <table border="0"><tr><td>RGBA(0, 0, 0, 0)</td></tr><tr><td style="background-color:#000000"></td></tr></table>       |

### Child & Parent Controls

| Property       | Value             |
| -------------- | ----------------- |
| Parent Control | DateSpaceDataCard |
