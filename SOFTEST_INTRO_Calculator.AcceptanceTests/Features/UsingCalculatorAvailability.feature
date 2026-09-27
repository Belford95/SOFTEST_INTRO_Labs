@Availability
Feature: UsingCalculatorAvailability
  In order to calculate MTBF and Availability
  As someone who struggles with maths
  I want to be able to use my calculator to do this

  Scenario: Calculating MTBF
    Given I have a calculator
    When I have entered 1000 and 4 into the calculator and press MTBF
    Then the result should be 250

  Scenario: Calculating Availability
    Given I have a calculator
    When I have entered 200 and 50 into the calculator and press Availability
    Then the result should be 0.8

  Scenario: Availability is perfect when repairs take no time
    Given I have a calculator
    When I have entered 100 and 0 into the calculator and press Availability
    Then the result should be 1

  Scenario Outline: Reject MTBF with unusable measurements
    Given I have a calculator
    When I have entered <operating_time> and <failures> into the calculator and press MTBF
    Then the calculation should be rejected

    Examples:
      | operating_time | failures |
      | 1000           | 0        |
      | 0              | 4        |
      | -100           | 4        |

  Scenario Outline: Reject Availability with unusable values
    Given I have a calculator
    When I have entered <mtbf> and <mttr> into the calculator and press Availability
    Then the calculation should be rejected

    Examples:
      | mtbf | mttr |
      | 0    | 0    |
      | -10  | 5    |
      | 10   | -5   |

   Scenario: Calculating Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      | 90   | 10   |
    When I calculate Availability from these values
    Then the result should be 0.9
