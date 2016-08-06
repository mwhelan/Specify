# Context-Specification
With context-specification you have a class per scenario, with each step having its own method and state being shared between methods in fields. This means that the setup and execution only happen once (the context), and then each `Then` method is a specification that asserts against the result of the execution.

## Scenarios
With functional tests, user stories can be seen as placeholders for conversations that are purposely kept terse while in the backlog (Desirements). 

Scenarios document the conversation that took place with the customer (Requirements). They are the product owner’s acceptance criteria for the user story. 

## Scenarios are Specifications
Functional test scenarios are high-level specifications of what the system should do. They document the conversation with the business. 

Unit test scenarios are low-level specifications of how the system works. They are a conversation amongst the developers. 

## Scenarios should focus on business rules
All the steps should reflect the business rules (the what), not the technical implementation (the how). 

One suggestion is to [pretend there is no UI](http://itsadeliverything.com/declarative-vs-imperative-gherkin-scenarios-for-cucumber).