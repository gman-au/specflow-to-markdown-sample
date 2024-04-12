Feature: RequestValidator
**This is feature text** which supports markdown
You can find some additional SpecFlow markdown documentation [here](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/Generating/Markdown-and-Embedding-Images.html)
![Image](https://cdn.britannica.com/87/189187-050-C6C16A3B/Smithsonian-Institution-building-Castle-1855.jpg?w=400&h=300&c=crop)

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

    @validation @isolated
    Scenario: Various request scenarios
        Given a request has been created
        And the first name is set to <first_name> with neither <unused_variable_1> nor <unused_variable_2> being used
        And the last name is set to <last_name>
        And the tickets requested are set to <number_of_tickets>
        When the validation request is made
        Then the validation request should succeed
        And there should be no error message

    Examples:
      | first_name | last_name | number_of_tickets | unused_variable_1 | unused_variable_2 |
      | John       | Smith     | 10                | 12                | 1                 |
      | Paul       | Jones     | 15                | 13                | 1                 |
      | Mary       | Sue       | 1                 | 14                | 1                 |
      | Alex       | McCain    | 1                 | 15                | 1                 |
      | Mary       | Jones     | 100               | 43                | 1                 |
      | The        | Emperor   | 1                 | 42                | 1                 |
      | Cole       | Stevens   | 42                | 41                | 1                 |
      | Jack       | Smith     | 5                 | 40                | 1                 |

    @tickets
    Scenario: An inconclusive test
        Given a request has been created
        When the validation request is made
        Then the validation test should be inconclusive