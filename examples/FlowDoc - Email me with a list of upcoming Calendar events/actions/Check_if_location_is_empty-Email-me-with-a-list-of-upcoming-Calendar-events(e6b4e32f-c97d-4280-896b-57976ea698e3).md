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

## Check\_if\_location\_is\_empty

| Property   | Value                                            |
| ---------- | ------------------------------------------------ |
| Name       | Check\_if\_location\_is\_empty                   |
| Type       | If                                               |
| Expression | @not(empty(items('Apply_to_each')?['Location'])) |

### Subactions

| Action                                                                                                                                                                           |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Get\_current\_weather](Get_current_weather-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                           |
| [Set\_CalendarEvents\_variable](Set_CalendarEvents_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                           |
| [Compose\_Weather](Compose_Weather-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                                    |
| [Set\_CalendarEvents](Set_CalendarEvents-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                              |
| [Append\_to\_array\_variable](Append_to_array_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)                                |
| [Append\_to\_CalendarEvents\_array\_variable](Append_to_CalendarEvents_array_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### Elseactions

| Elseactions                                                                                                                                                   |
| ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Set\_CalendarEvents\_Values](Set_CalendarEvents_Values-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md)            |
| [Append\_to\_CalendarEvents\_array](Append_to_CalendarEvents_array-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |

### Next Action(s) Conditions

| Next Action                                                                                                                                            |
| ------------------------------------------------------------------------------------------------------------------------------------------------------ |
| [Empty\_Hotel\_variable \[Succeeded\]](Empty_Hotel_variable-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
