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

## Compose\_Weather

| Property | Value            |
| -------- | ---------------- |
| Name     | Compose\_Weather |
| Type     | Compose          |

### Inputs

| Property | Value                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| -------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Value    | Conditions: @{body('Get\_current\_weather')?\['responses'\]?\['weather'\]?\['current'\]?\['cap'\]}\<br\> Humidity: @{body('Get\_current\_weather')?\['responses'\]?\['weather'\]?\['current'\]?\['rh'\]}\<br\> Temperature: @{body('Get\_current\_weather')?\['responses'\]?\['weather'\]?\['current'\]?\['temp'\]} @{body('Get\_current\_weather')?\['units'\]?\['temperature'\]}\<br\> Wind Speed: @{body('Get\_current\_weather')?\['responses'\]?\['weather'\]?\['current'\]?\['windSpd'\]} @{body('Get\_current\_weather')?\['units'\]?\['speed'\]}\<br\> |

### Next Action(s) Conditions

| Next Action                                                                                                                                       |
| ------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Set\_CalendarEvents \[Succeeded\]](Set_CalendarEvents-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
