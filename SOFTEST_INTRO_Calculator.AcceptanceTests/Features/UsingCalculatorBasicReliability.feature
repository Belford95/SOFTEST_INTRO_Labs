@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate the Basic Musa model's failures and intensities
  As a Software Quality Metric enthusiast
  I want to use my calculator to do this
  (Execution time is in hours and failure intensity in failures per hour.)

  # Stage 1: current failure intensity, lambda(tau)

  Scenario: Failure intensity at the start of testing equals the initial intensity
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total number of failures is 100
    And the accumulated execution time is 0 hours
    When I calculate the current failure intensity
    Then the result should be 10

  Scenario: Failure intensity falls as testing time accumulates
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total number of failures is 100
    And the accumulated execution time is 10 hours
    When I calculate the current failure intensity
    Then the result should be approximately 3.6788

  # Stage 2: expected cumulative failures, mu(tau)

  Scenario: No failures are expected before any testing
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total number of failures is 100
    And the accumulated execution time is 0 hours
    When I calculate the expected cumulative failures
    Then the result should be 0

  Scenario: Expected failures grow as testing time accumulates
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total number of failures is 100
    And the accumulated execution time is 10 hours
    When I calculate the expected cumulative failures
    Then the result should be approximately 63.2121

  # Rejected parameters

  Scenario Outline: Reject invalid parameters when calculating failure intensity
    Given I have a calculator
    And the initial failure intensity is <initial_intensity> failures per hour
    And the expected total number of failures is <total_failures>
    And the accumulated execution time is <time> hours
    When I calculate the current failure intensity
    Then the calculation should be rejected

    Examples:
      | initial_intensity | total_failures | time |
      | 0                 | 100            | 10   |
      | 10                | -5             | 10   |
      | 10                | 100            | -1   |

  Scenario: Reject a negative execution time when calculating cumulative failures
    Given I have a calculator
    And the initial failure intensity is 10 failures per hour
    And the expected total number of failures is 100
    And the accumulated execution time is -1 hours
    When I calculate the expected cumulative failures
    Then the calculation should be rejected