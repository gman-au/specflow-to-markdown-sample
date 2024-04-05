Feature: RequestValidator
Simple calculator for adding two numbers

    @validation
    Scenario: Invalid booking name
        Given a request has been created
        And the request has an invalid first name
        When the validation request is made
        Then the validation request should fail
        And the error message should be "Please supply a valid first name"

    @validation
    @tickets
    Scenario: Invalid tickets requested
        Given a request has been created
        And the request contains 0 tickets
        When the validation request is made
        Then the validation request should fail
        And the error message should be "Please specify a number of tickets greater than zero"

    Scenario: Various request scenarios
        Given a request has been created
        And the first name is set to <first_name>
        And the last name is set to <last_name>
        And the tickets requested are set to <number_of_tickets>
        When the validation request is made
        Then the validation request should succeed
        And there should be no error message

    Examples:
      | first_name | last_name | number_of_tickets |
      | John       | Smith     | 10                |
      | Paul       | Jones     | 15                |
      | Mary       | Sue       | 1                 |
      | Alex       | McCain    | 1                 |
      | Mary       | Jones     | 100               |
      | The        | Emperor   | 1                 |
      | Cole       | Stevens   | 42                |
      | Jack       | Smith     | 5                 |

    @tickets
    Scenario: An inconclusive test
        Given a request has been created
        When the validation request is made
        Then the validation test should be inconclusive