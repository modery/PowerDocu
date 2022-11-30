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

## Check\_if\_its\_Flight

| Property   | Value                                                            |
| ---------- | ---------------------------------------------------------------- |
| Name       | Check\_if\_its\_Flight                                           |
| Type       | If                                                               |
| Expression | @contains(toLower(outputs('Compose_Flight_or_Hotel')), 'flight') |

### Subactions

| Action                                                                                                                                 |
| -------------------------------------------------------------------------------------------------------------------------------------- |
| [Set\_Flight\_variable](Set_Flight_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### Elseactions

| Elseactions                                                                                                                           |
| ------------------------------------------------------------------------------------------------------------------------------------- |
| [Check\_if\_its\_Hotel](Check_if_its_Hotel-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### Next Action(s) Conditions

| Next Action                                                                                                                                                          |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Check\_if\_location\_is\_empty \[Succeeded\]](Check_if_location_is_empty-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
