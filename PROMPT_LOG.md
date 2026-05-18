# Prompt Log

## Copilot
### Audit this codebase for security vulnerabilities, bugs, and architectural issues. Give me a prioritized list.
After taking an initial lookover of the repository, I was concerned as it had some hallmarks of unreviewed ai-generated work (e.g. having dock-compose-yml and docker-compose.yml.example present in the same location and identical in contents). I immediately have quality concerns with anything that used ai-generation seemingly without proper review afterwards, so pulled down the code into Visual Studio and ran a generic prompt with Copilot (using Claude in this case) to gain an understanding of possible issues. Copilot found a barrage of security problems along with many smaller issues, which I began working through using recommended solutions.

### Implement fixes for the identified issues.
I didn't want to divert too much focus from the main focus of horizontal scaling, but felt leaving these issues unaddressed would be blatantly negative. I had Copilot implement fixes, manually reviewing and tweaking where neccesary, while denying where unneccesary or too much outside of scope.

## Claude (instance 1)
### Give me an in-depth overview of Jira-style tickets, their creation, and best practices. Assume no prior exposure.
Having not worked with Jira-style tickets before, I wanted to reinforce the understanding that I gained through my own research with a tool that could understand the nuances in my query. I've found Claude to be helpful in providing complete overviews that might note some details that a source like a basic tutorial online or a YouTube video might miss.

### Explain story points.
A follow-up to better understand the style of time measurement in Agile development with Jira tickets, which would be helpful when creating my own ticket and assigning it story points.

### Generate a Jira-style ticket template as a .md file where I could manually fill in necessary information.
Using Claude to manage some of the less design-focused work allowed for better time usage for addressing the main issues of the assessment. Claude can generally be trusted to follow best practices and have knowledge of how things like a ticket might look, so by letting it handle this part there was more time to focus on the things more important than modifying header size on a markdown file.

### What are the typical use cases for the different priorities on a Jira ticket? Provide examples.
As with my question about story points, the goal here was to gain a better understanding of Agile development. I wanted to ensure that I properly understood the different priorities that were assignable through Jira tickets, and what they communicated about the complexity, importance, and scale of tasks.

## Claude (instance 2)
### There is a scaling concern with this codebase: https://github.com/jin3107/TodoApp_BasicToModern. Review it for scaling issues with a focus on being able to scale Quartz jobs horizontally.
The issues identified with Copilot notwithstanding, I wanted to keep my main focus on the task of scaling out Quartz jobs. I used this prompt to be able to quickly identify the pain points before going into the code and manually looking over what Claude found.

## Claude (instance 3)
### What are the usecases for implementing HSTS?
Some of the Copilot-generated changes were inappropriate for the context, like this one where it tried to add in HSTS, which in my understanding is not always proper for a development environment. I commented it out and left a message to add it in before deployment.

### Give me an overview on the usage of UseJsonSerializer versus the usage of UseNewtonsoftJsonSerializer when using Quartz.
Visual Studio was yelling at me about UseJsonSerializer being obsolete. I referenced Claude for an overview of the advantages and disadvantages of each. I used this information to make the call to leave it as UseJsonSerializer for now, with a comment above to note that it's worth revisiting switching them out in the future or if issues arise.