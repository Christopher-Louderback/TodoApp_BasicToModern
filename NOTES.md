## Notes
Would've liked to break away things like security issues and email bug into their own ticket(s), but the assessment description said ticket not tickets, so worked with that.

Worked with assumption that database changes would be handled, but that it wouldn't be neccesary for me to do here.

## Descriptions & Reasoning - Manual Changes

Manually tweaked various implementations of Copilot/Claude to avoid unnecessarily complex changes or changes that were too far outside the scope of the project.

Manually addressed some identified issues, for example limiting the range for PageSize in TodoApp.Server/src/MayNghien.Infrastructures/MayNghien.Infrastructures/Models/Requests/SearchRequest.cs and aspects of the Quartz scaling. Done where more personal control was wanted or Copilot missed the problem.

Manually wrote the descriptions and criteria in TICKET.md, the explanations in PROMPT_LOG.md, and this file. Done to maintain proper control over explanations and details (and to avoid totally soulless writing).