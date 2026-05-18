# [Address Scaling Concerns in To-Do Application]

---

| Field | Value |
|---|---|
| **Type** | `Story` |
| **Priority** | `High` |
| **Status** | `In Progress` |
| **Assignee** | `Christopher Louderback` |
| **Reporter** | `Fake Namesson` |
| **Parent epic** | N/A |
| **Story points** | `3` |
| **Labels** | `to-do-app`, `quartz`, `scaling` |
| **Sprint** | N/A |
| **Due date** | `5/18/2026` |

---

## Description

Quartz is currently configured with "UseInMemoryStore", which uses process memory. Horizontal scaling is contingent on this being changed, as with this configuration, a job will fire once per backend instance, leading to excess copies. This can be fixed by changing from the use of "UseInMemoryStore" to "UsePersistentStore" with "UseClustering" enabled. Quartz should then automatically coordinate jobs, ensuring each one fires only once.

Beyond addressing the horizontal scaling, there are several security issues that warrant fixing, as they have an impact on the basic functionality of the codebase. These issues include problems with the password policy, CORS, data isolation, access controls, and an exposed information. Initial fixes can include updating the password policy, adjusting the setup of CORS, adding authorization to endpoints, setting up data isolation, implementing exception handling, implementing some sort of user id system to allow for filtering, and updating the configuration. Copilot can be used for the inital build out of these features, but there are always risks with AI use on security-related issues, and there should be human review.

There is also a bug with the weekly email reports. This can be addressed by properly calling "SendEmailAsync".

---

## Acceptance criteria

- [ ] "UseInMemoryStore" replaced with "UsePersistentStore" with "UseClustering" enabled and applied to the relevant connection string(s).
- [ ] With more than 1 backend instance, any given scheduled job fires only once.
- [ ] With more than 1 backend instance, any given manually triggered job fires only once.
- [ ] Backend instances being stopped/started or restarted does not lead to duplicates or missed jobs.
- [ ] Password policy is updated to modern standards (minimum length, different character types, account lockouts).
- [ ] CORS updated to use a whitelist.
- [ ] Authorization added to all endpoints.
- [ ] Unique identifier added to each user (e.g. a UserID).
- [ ] Ownership check implemented using unique identifier.
- [ ] Exception handling implemented.
- [ ] DB schema updated.
- [ ] Weekly email reports functional.

---

## Notes & links

- Docs / references: https://github.com/jin3107/TodoApp_BasicToModern, https://github.com/quartznet/quartznet
- Additional context: N/A
