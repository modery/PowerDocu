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

## Get\_calendar\_view\_of\_events\_(V2)

| Property   | Value                                                                                                  |
| ---------- | ------------------------------------------------------------------------------------------------------ |
| Name       | Get\_calendar\_view\_of\_events\_(V2)                                                                  |
| Type       | ApiConnection                                                                                          |
| Connection | [![office365](../office36532.png) Office 365 Outlook](https://docs.microsoft.com/connectors/office365) |

### Inputs

| Property       | Value                                                                                                                                                                                                                                                                                                                                   |
| -------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| test           | test                                                                                                                                                                                                                                                                                                                                    |
| host           | <table><tr><td>api</td><td><table><tr><td>runtimeUrl</td><td>https://flow-apim-europe-001-northeurope-01.azure-apim.net/apim/office365</td></tr></table></td></tr><tr><td>connection</td><td><table><tr><td>name</td><td>@parameters('$connections')['shared_office365']['connectionId']</td></tr></table></td></tr></table>            |
| method         | get                                                                                                                                                                                                                                                                                                                                     |
| path           | /datasets/calendars/v2/tables/items/calendarview                                                                                                                                                                                                                                                                                        |
| queries        | <table><tr><td>calendarId</td><td>AAMkAGUxMmMwNDYxLTE2MWUtNDg4NS1hZTViLTQ0OGRiYTYxNjMxYQBGAAAAAAA9OrE7guxLRZawE4FUVzEDBwA85BmDcoplRqBtm4DAEgO5AAAAAAEGAAA85BmDcoplRqBtm4DAEgO5AAAAf_AzAAA=</td></tr><tr><td>startDateTimeOffset</td><td>@{utcNow()}</td></tr><tr><td>endDateTimeOffset</td><td>@{addDays(utcNow(),6)}</td></tr></table> |
| authentication | @parameters('$authentication')                                                                                                                                                                                                                                                                                                          |

### Next Action(s) Conditions

| Next Action                                                                                                                              |
| ---------------------------------------------------------------------------------------------------------------------------------------- |
| [Apply\_to\_each \[Succeeded\]](Apply_to_each-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
