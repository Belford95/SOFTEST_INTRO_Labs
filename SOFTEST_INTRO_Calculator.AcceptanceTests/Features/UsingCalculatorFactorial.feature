@Factorial
Feature: UsingCalculatorFactorial
  In order to count the ways to arrange items
  As a calculator user
  I want to be told the factorial of a whole number

  Scenario: Factorial of a normal number
    Given I have a calculator
    When I have entered 5 into the calculator and press factorial
    Then the factorial result should be 120

  Scenario: Factorial of zero is one
    Given I have a calculator
    When I have entered 0 into the calculator and press factorial
    Then the factorial result should be 1

  Scenario Outline: Reject unsupported factorial values
    Given I have a calculator
    When I have entered <value> into the calculator and press factorial
    Then factorial should be rejected

    Examples:
      | value |
      | -1    |
      | 21    |