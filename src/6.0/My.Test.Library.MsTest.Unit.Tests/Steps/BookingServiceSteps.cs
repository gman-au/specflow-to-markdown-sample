using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using My.Test.Library.Domain.Definition;
using My.Test.Library.Domain.Services;

namespace My.Test.Library.MsTest.Unit.Tests.Steps
{
    [Binding]
    public class BookingServiceSteps
    {
        private readonly Mock<IEventService> _eventService;
        private readonly IFixture _fixture;
        private readonly Mock<IRequestValidator> _requestValidator;
        private readonly MyBookingService _sut;
        private readonly Event _returnedEvent;
        private BookingRequest _request;
        private BookingResponse _response;

        public BookingServiceSteps()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _fixture
                .Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture
                .Behaviors
                .Add(new OmitOnRecursionBehavior());

            _eventService = new Mock<IEventService>();
            _requestValidator = new Mock<IRequestValidator>();

            _returnedEvent = new Event
            {
                EventCode = Guid.NewGuid(),
                StartTime = DateTime.Now.AddHours(10),
                EndTime = DateTime.Now.AddHours(11),
                Status = EventStatusEnum.Upcoming,
                TicketsRemaining = 100
            };

            _sut =
                new MyBookingService(
                    _eventService.Object,
                    _requestValidator.Object
                );
        }

        [Given(@"the request is valid")]
        public void GivenTheRequestIsValid()
        {
            _request =
                _fixture
                    .Create<BookingRequest>();

            _requestValidator
                .Setup(o => o.Validate(It.IsAny<BookingRequest>()))
                .Returns(
                    new ValidationResponse
                    {
                        IsValid = true,
                        Message = null
                    }
                );
        }

        [Given(@"the request has a validation failure of ""(.*)""")]
        public void GivenTheRequestHasAValidationFailureOf(string errorMessage)
        {
            _request =
                _fixture
                    .Create<BookingRequest>();

            _requestValidator
                .Setup(o => o.Validate(It.IsAny<BookingRequest>()))
                .Returns(
                    new ValidationResponse
                    {
                        IsValid = false,
                        Message = errorMessage
                    }
                );
        }

        [Given(@"the request asks for (.*) tickets")]
        public void GivenTheRequestAsksForTickets(int tickets)
        {
            _request.NumberOfTickets = tickets;
        }

        [Given(@"the requested event has (.*) tickets left")]
        public void GivenTheRequestedEventHasTicketsLeft(int tickets)
        {
            _returnedEvent.TicketsRemaining = tickets;
        }

        [When(@"the booking request is made")]
        public async Task WhenTheBookingRequestIsMade()
        {
            _eventService
                .Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_returnedEvent);

            _response =
                await
                    _sut
                        .ProcessAsync(_request);
        }

        [Then(@"the booking request should succeed")]
        public void ThenTheBookingRequestShouldSucceed()
        {
            Assert.IsTrue(_response.IsSuccess);
        }

        [Then(@"the booking request should fail")]
        public void ThenTheBookingRequestShouldFail()
        {
            Assert.IsFalse(_response.IsSuccess);
        }

        [Then(@"the response message should be ""(.*)""")]
        public void ThenTheResponseMessageShouldBe(string expectedErrorMessage)
        {
            Assert.AreEqual(
                expectedErrorMessage,
                _response.ResponseMessage
            );
        }

        [Then(@"the response should indicate (.*) tickets were purchased")]
        public void ThenTheResponseShouldIndicateTicketsWerePurchased(int tickets)
        {
            Assert.AreEqual(
                tickets,
                _response.TicketsPurchased
            );
        }

        [Given(@"the requested event is ""(.*)""")]
        public void GivenTheRequestedEventIs(string eventStatus)
        {
            var statusValue =
                Enum
                    .Parse<EventStatusEnum>(
                        eventStatus
                    );

            _returnedEvent.Status = statusValue;
        }

        [Then(@"the booking test should be inconclusive")]
        public void ThenTheBookingTestShouldBeInconclusive()
        {
            ScenarioContext.StepIsPending();
        }
    }
}