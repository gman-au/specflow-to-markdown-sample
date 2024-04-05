Feature: RequestValidator
	Simple calculator for adding two numbers

    @validation
    Scenario: Invalid booking name
        Given a request has been created
        And the request has an invalid first name
        When the validation request is made
        Then the validation request should fail
        And the error message should be "Please supply a valid first name"    
        
    @validation @tickets
    Scenario: Invalid tickets requested
        Given a request has been created
        And the request contains 0 tickets
        When the validation request is made
        Then the validation request should fail
        And the error message should be "Please specify a number of tickets greater than zero"