using System;
using System.Threading.Tasks;
using My.Test.Library.Domain.Definition;
using My.Test.Library.Domain.Services;
using NUnit.Framework;

namespace My.Test.Library.N.Unit.Tests.Steps
{
    [Binding]
    public class RequestValidatorSteps
    {
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
                    Referrer = ReferrerEnum.NotSpecified,
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
    }
}