# What is it?
Specify is a small opinionated testing library for writing different kinds of tests in .Net. It is built on top of several other libraries:

* BDDfy
* NSubstitute
* Autofac
* Your choice of testing framework (NUnit, xunit, MsTest....)

Its goal is to provide a consistent experience for unit testing and acceptance testing (full system functional tests). It uses an automocking container for unit tests and will use a full IoC container for functional tests.

It will also provide support for subcutaneous acceptance tests in ASP.Net MVC and Selenium Functional Tests using Seleno.

You can read more about the unit testing approach [here](http://michael-whelan.net/bddfy-in-action/using-bddfy-for-unit-tests):
