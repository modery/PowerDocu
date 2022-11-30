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

## Compose\_BodyToSend

| Property            | Value                            |
| ------------------- | -------------------------------- |
| Name                | Compose\_BodyToSend              |
| Type                | Compose                          |
| Description \/ Note | Replace &lt; &gt; with \< and \> |

### Inputs

| Property | Value                                                                  |
| -------- | ---------------------------------------------------------------------- |
| Value    | @replace(replace(body('Create\_HTML\_table'),'&lt;','\<'),'&gt;','\>') |

### Next Action(s) Conditions

| Next Action                                                                                                                                           |
| ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Get\_my\_profile\_(V2) \[Succeeded\]](Get_my_profile_(V2)-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
