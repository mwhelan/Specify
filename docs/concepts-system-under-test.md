This is a term from the book [xUnit Patterns](http://xunitpatterns.com/), by Gerard Meszaros.

> The "system under test". It is short for "whatever thing we are testing" and is always defined from the perspective of the test. When we are writing unit tests the system under test (SUT) is whatever class (a.k.a. CUT), object (a.k.a. OUT) or method(s) (a.k.a. MUT) we are testing; when we are writing customer tests, the SUT is probably the entire application (a.k.a. AUT) or at least a major subsystem of it. The parts of the application that we are not verifying in this particular test may still be involved as a depended-on component (DOC).

Also known as 

- Application Under Test (AUT)
- Method Under Test (MUT)
- Class Under Test (CUT)

Erik Dietrich describes the SUT as [Target](http://www.daedtech.com/test-readability-best-of-all-worlds/).