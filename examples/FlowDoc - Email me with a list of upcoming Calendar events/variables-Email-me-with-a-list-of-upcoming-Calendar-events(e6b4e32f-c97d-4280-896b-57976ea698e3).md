# Flow Documentation \- Email me with a list of upcoming Calendar events

| Flow Name                  | Email me with a list of upcoming Calendar events |
| -------------------------- | ------------------------------------------------ |
| Flow Name                  | Email me with a list of upcoming Calendar events |
| Flow ID                    | e6b4e32f\-c97d\-4280\-896b\-57976ea698e3         |
| Documentation generated at | Wednesday, 30 November 2022 10:33 am             |
| Number of Variables        | 5                                                |
| Number of Actions          | 28                                               |

- [Overview](index-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Connection References](connections-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Variables](variables-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Triggers & Actions](triggersactions-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)

## Variables

### BodyToSend

| Property | Value      |
| -------- | ---------- |
| Name     | BodyToSend |
| Type     | Array      |

| Variable Used In                                                                                                                                                     |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Create\_HTML\_table](actions/Create_HTML_table-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                           |
| [Initialize\_BodyToSend\_variable](actions/Initialize_BodyToSend_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### CalendarEvents

| Property | Value          |
| -------- | -------------- |
| Name     | CalendarEvents |
| Type     | Object         |

| Variable Property | Initial Value |
| ----------------- | ------------- |
| Subject           |               |
| StartTime         |               |
| EndTime           |               |
| Location          |               |
| Hotel             |               |
| Flight            |               |
| CurrentWeather    |               |

| Variable Used In                                                                                                                                                                         |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Append\_to\_array\_variable](actions/Append_to_array_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                |
| [Append\_to\_CalendarEvents\_array](actions/Append_to_CalendarEvents_array-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                    |
| [Append\_to\_CalendarEvents\_array\_variable](actions/Append_to_CalendarEvents_array_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
| [Initialize\_CalendarEvents\_variable](actions/Initialize_CalendarEvents_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)             |
| [Set\_CalendarEvents](actions/Set_CalendarEvents-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                              |
| [Set\_CalendarEvents\_Values](actions/Set_CalendarEvents_Values-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                               |
| [Set\_CalendarEvents\_variable](actions/Set_CalendarEvents_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                           |

### Empty

| Property | Value  |
| -------- | ------ |
| Name     | Empty  |
| Type     | String |

| Variable Used In                                                                                                                                           |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Empty\_Flight\_variable](actions/Empty_Flight_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)         |
| [Empty\_Hotel\_variable](actions/Empty_Hotel_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)           |
| [Initialize\_Empty\_variable](actions/Initialize_Empty_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### Flight

| Property | Value  |
| -------- | ------ |
| Name     | Flight |
| Type     | String |

| Variable Used In                                                                                                                                             |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| [Empty\_Flight\_variable](actions/Empty_Flight_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)           |
| [Initialize\_Flight\_variable](actions/Initialize_Flight_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
| [Set\_Flight\_variable](actions/Set_Flight_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)               |

### Hotel

| Property | Value  |
| -------- | ------ |
| Name     | Hotel  |
| Type     | String |

| Variable Used In                                                                                                                                           |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Empty\_Hotel\_variable](actions/Empty_Hotel_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)           |
| [Initialize\_Hotel\_variable](actions/Initialize_Hotel_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
| [Set\_Hotel\_variable](actions/Set_Hotel_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)               |
