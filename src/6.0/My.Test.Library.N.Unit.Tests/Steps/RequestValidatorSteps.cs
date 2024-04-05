using System;
using My.Test.Library.Domain.Definition;
using My.Test.Library.Domain.Services;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace My.Test.Library.N.Unit.Tests.Steps
{
    [Binding]
    public class RequestValidatorSteps
    {
        private const string SkipForTheEmperor = "Emperor";

        private readonly MyRequestValidator _sut;
        private BookingRequest _request;
        private ValidationResponse _response;

        public RequestValidatorSteps()
        {
            _sut = new MyRequestValidator();
            _request = new BookingRequest();
        }

        [Given(@"a request has been created")]
        public void GivenARequestHasBeenCreated()
        {
            _request =
                new BookingRequest
                {
                    FirstName = "John",
                    LastName = "Smith",
                    EventCode = Guid.NewGuid(),
                    NumberOfTickets = 2
                };
        }

        [When(@"the validation request is made")]
        public void WhenTheValidationRequestIsMade()
        {
            _response =
                _sut
                    .Validate(_request);
        }

        [Then(@"the validation request should fail")]
        public void ThenTheValidationRequestShouldFail()
        {
            Assert.False(_response.IsValid);
        }

        [Then(@"the validation request should succeed")]
        public void ThenTheValidationRequestShouldSucceed()
        {
            Assert.True(_response.IsValid);
        }

        [Then(@"the error message should be ""(.*)""")]
        public void ThenTheErrorMessageShouldBe(string expectedErrorMessage)
        {
            Assert
                .AreEqual(
                    expectedErrorMessage,
                    _response.Message
                );
        }

        [Given(@"the request has an invalid first name")]
        public void GivenTheRequestHasAnInvalidFirstName()
        {
            _request.FirstName = null;
        }

        [Given(@"the request contains (.*) tickets")]
        public void GivenTheRequestContainsTickets(int tickets)
        {
            _request.NumberOfTickets = tickets;
        }

        [Given(@"a request has been created as follows:")]
        public void GivenARequestHasBeenCreatedAsFollows(TableRow table)
        {
            var request =
                table
                    .CreateInstance<BookingRequest>();

            _request =
                new BookingRequest
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EventCode = Guid.NewGuid(),
                    NumberOfTickets = request.NumberOfTickets
                };
        }

        [Then(@"there should be no error message")]
        public void ThenThereShouldBeNoErrorMessage()
        {
            Assert
                .Null(_response.Message);
        }

        [Given(@"the first name is set to (.*)")]
        public void GivenTheFirstNameIsSetTo(string value) => _request.FirstName = value;

        [Given(@"the last name is set to (.*)")]
        public void GivenTheLastNameIsSetTo(string value)
        {
            if (value == SkipForTheEmperor)
                ScenarioContext.StepIsPending();
            
            _request.LastName = value;
        }

        [Given(@"the tickets requested are set to (.*)")]
        public void GivenTheTicketsRequestedAreSetTo(int  value) => _request.NumberOfTickets = value;

        [Then(@"the validation test should be inconclusive")]
        public void ThenTheValidationTestShouldBeInconclusive()
        {
            ScenarioContext.StepIsPending();
        }
    }
}