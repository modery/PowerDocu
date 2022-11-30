# Flow Documentation \- Email me with a list of upcoming Calendar events

| Flow Name                  | Email me with a list of upcoming Calendar events |
| -------------------------- | ------------------------------------------------ |
| Flow Name                  | Email me with a list of upcoming Calendar events |
| Flow ID                    | e6b4e32f\-c97d\-4280\-896b\-57976ea698e3         |
| Documentation generated at | Wednesday, 30 November 2022 10:33 am             |
| Number of Variables        | 5                                                |
| Number of Actions          | 28                                               |

- [Overview](../index-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Connection References](../connections-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Variables](../variables-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)
- [Triggers & Actions](../triggersactions-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)

## Set\_CalendarEvents\_variable

| Property | Value                         |
| -------- | ----------------------------- |
| Name     | Set\_CalendarEvents\_variable |
| Type     | SetVariable                   |

### Inputs

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| test     | test                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| name     | CalendarEvents                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           |
| value    | <table><tr><td>Subject</td><td>@{items('Apply_to_each')?['Subject']}</td></tr><tr><td>StartTime</td><td>@{formatDateTime(items('Apply_to_each')['Start'],'yyyy-MM-ddThh:mm:ss')}</td></tr><tr><td>EndTime</td><td>@{formatDateTime(items('Apply_to_each')['End'],'yyyy-MM-ddThh:mm:ss')}</td></tr><tr><td>Location</td><td>@{items('Apply_to_each')?['Location']}</td></tr><tr><td>CurrentWeather</td><td></td></tr><tr><td>Hotel</td><td>@{variables('Hotel')}</td></tr><tr><td>Flight</td><td>@{variables('Flight')}</td></tr></table> |

### Next Action(s) Conditions

| Next Action                                                                                                                                                     |
| --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Append\_to\_array\_variable \[Succeeded\]](Append_to_array_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
