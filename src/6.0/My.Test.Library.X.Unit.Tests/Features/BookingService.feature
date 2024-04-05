Feature: BookingService
Basic booking service that takes a booking request, validates it for correctness, and then attempts to secure the number of tickets against the registered event.

    @validation
    Scenario: Invalid request

    Background: an invalid response is returned from the (mocked) request validator interface
        Given the request has a validation failure of "Something failed validation"
        When the booking request is made
        Then the booking request should fail
        And the response message should be "Error - Something failed validation"

    @tickets
    @event
    Scenario: Event cancelled
        Given the request is valid
        And the request asks for 2 tickets
        And the requested event is "Cancelled"
        When the booking request is made
        Then the booking request should fail
        And the response message should be "Error - Event is cancelled"

    @tickets
    Scenario: No tickets left
        Given the request is valid
        And the request asks for 2 tickets
        And the requested event has 0 tickets left
        When the booking request is made
        Then the booking request should fail
        And the response message should be "Error - Event is fully booked"

    @tickets
    Scenario: Some tickets left
        Given the request is valid
        And the request asks for 20 tickets
        And the requested event has 10 tickets left
        When the booking request is made
        Then the booking request should succeed
        And the response should indicate 10 tickets were purchased
        And the response message should be "Partial Success - 10 tickets were purchased"

    @tickets
    @happyPath
    Scenario: Tickets purchased
        Given the request is valid
        And the request asks for 5 tickets
        And the requested event has 10 tickets left
        When the booking request is made
        Then the booking request should succeed
        And the response should indicate 5 tickets were purchased
        And the response message should be "Success - 5 tickets were purchased"

    @event
    Scenario: An inconclusive test
        Given the request is valid
        When the booking request is made
        Then the booking test should be inconclusive