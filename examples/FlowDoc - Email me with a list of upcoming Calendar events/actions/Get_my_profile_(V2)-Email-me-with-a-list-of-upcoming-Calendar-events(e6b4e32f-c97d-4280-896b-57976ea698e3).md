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

## Get\_my\_profile\_(V2)

| Property   | Value                                                                                                               |
| ---------- | ------------------------------------------------------------------------------------------------------------------- |
| Name       | Get\_my\_profile\_(V2)                                                                                              |
| Type       | ApiConnection                                                                                                       |
| Connection | [![office365users](../office365users32.png) Office 365 Users](https://docs.microsoft.com/connectors/office365users) |

### Inputs

| Property       | Value                                                                                                                                                                                                                                                                                                                                  |
| -------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| test           | test                                                                                                                                                                                                                                                                                                                                   |
| host           | <table><tr><td>api</td><td><table><tr><td>runtimeUrl</td><td>https://flow-apim-europe-001-northeurope-01.azure-apim.net/apim/office365users</td></tr></table></td></tr><tr><td>connection</td><td><table><tr><td>name</td><td>@parameters('$connections')['shared_office365users']['connectionId']</td></tr></table></td></tr></table> |
| method         | get                                                                                                                                                                                                                                                                                                                                    |
| path           | /codeless/v1.0/me                                                                                                                                                                                                                                                                                                                      |
| queries        | <table><tr><td>$select</td><td>Mail</td></tr></table>                                                                                                                                                                                                                                                                                  |
| authentication | @parameters('$authentication')                                                                                                                                                                                                                                                                                                         |

### Next Action(s) Conditions

| Next Action                                                                                                                              |
| ---------------------------------------------------------------------------------------------------------------------------------------- |
| [Send\_an\_email \[Succeeded\]](Send_an_email-Email-me-with-a-list-of-upcoming-Calendar-events(e6b4e32f-c97d-4280-896b-57976ea698e3).md) |
