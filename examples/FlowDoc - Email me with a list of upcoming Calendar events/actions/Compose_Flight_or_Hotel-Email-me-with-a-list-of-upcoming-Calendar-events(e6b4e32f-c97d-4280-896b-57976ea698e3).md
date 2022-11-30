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

## Compose\_Flight\_or\_Hotel

| Property            | Value                                                             |
| ------------------- | ----------------------------------------------------------------- |
| Name                | Compose\_Flight\_or\_Hotel                                        |
| Type                | Compose                                                           |
| Description \/ Note | Expecting the subject in format "Name \- Flight or Hotel details" |

### Inputs

| Property | Value                                                                                                                                                                                                                          |
| -------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Value    | @substring(items('Apply\_to\_each')?\['Subject'\],add(indexOf(items('Apply\_to\_each')?\['Subject'\],'\-'),1),sub(sub(length(items('Apply\_to\_each')?\['Subject'\]),indexOf(items('Apply\_to\_each')?\['Subject'\],'\-')),1)) |

### Next Action(s) Conditions

| Next Action                                                                                                                                           |
| ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Check\_if\_its\_Flight \[Succeeded\]](Check_if_its_Flight-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
