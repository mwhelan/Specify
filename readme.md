# Specify

## What is it?
Specify is a testing library that builds on top of BDDfy from [TestStack](http://teststack.net/). While BDDfy is primarily intended for BDD testing, it is beautifully designed to be very easy to customize and extend.

When I first started using BDDfy for acceptance testing, I would use a different framework for unit testing, but I didn't like the context switching between frameworks and testing styles (Arrange Act Assert vs Given When Then). The goal of Specify is to provide a consistent experience for all types of tests. Why not have the fantastic BDDfy reports for all of your different test types?

## Overview of Features
* Tests use a context-specification style, with a class per test
* Supports automocking containers for unit tests and IoC containers for larger tests
* Tests can be resolved from your IoC container

## Context-Specification Style
It follows the Given When Then, as opposed o It uses an automocking container for unit tests and will use a full IoC container for functional tests.
You can read more about the unit testing approach [here](http://michael-whelan.net/bddfy-in-action/using-bddfy-for-unit-tests):

## Containers
### Automocking Containers


### Inversion of Control Containers



