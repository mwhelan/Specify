using FluentAssertions;
using NSubstitute;
using Serilog;

namespace Specs.Unit.ApiTemplate.Samples
{
    public class OrderProcessingWithoutAutoMocking : ScenarioFor<OrderProcessor>
    {
        private OrderResult _result;

        private ILogger _logger;
        private IInventory _inventory;
        private Publisher _publisher;
        private Auditer _auditer;

        public void Setup()
        {
            _inventory = Substitute.For<IInventory>();
            _publisher = Substitute.For<Publisher>();
            _logger = Substitute.For<ILogger>();
            _auditer = new Auditer(_logger);

            SUT = new OrderProcessor(_inventory, _publisher, _auditer);
        }

        protected void Given_the_item_is_available()
        {
            _inventory
                .IsQuantityAvailable("TestPart", 10)
                .Returns(true);
        }

        public void When_processing_an_order()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = 10 });
        }

        public void Then_the_order_is_accepted()
        {
            _result.WasAccepted.Should().Be(true);
        }

        public void AndThen_it_checks_the_inventory()
        {
            _inventory
                .Received().IsQuantityAvailable("TestPart", 10);
        }

        public void AndThen_it_raises_an_order_submitted_event()
        {
            _publisher
                .Received().Publish(Arg.Is<OrderSubmitted>(x => x.OrderNumber == _result.OrderNumber));
        }

        public void AndThen_it_logs_the_order_part_number()
        {
            _logger
                .Received().Information(Arg.Is<string>(x => x == "TestPart"));
        }
    }

    public class OrdersWithANegativeQuantityWithoutAutoMocking : ScenarioFor<OrderProcessor>
    {
        private OrderResult _result;

        private ILogger _logger;
        private IInventory _inventory;
        private Publisher _publisher;
        private Auditer _auditer;

        public void Setup()
        {
            _inventory = Substitute.For<IInventory>();
            _publisher = Substitute.For<Publisher>();
            _logger = Substitute.For<ILogger>();
            _auditer = new Auditer(_logger);

            SUT = new OrderProcessor(_inventory, _publisher, _auditer);
        }

        protected void Given_the_item_is_available()
        {
            _inventory
                .IsQuantityAvailable("TestPart", 10)
                .Returns(true);
        }

        protected void When_processing_an_order_with_a_negative_quantity()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = -1 });
        }

        public void Then_the_order_is_rejected()
        {
            _result.WasAccepted.Should().Be(false);
        }

        public void AndThen_it_does_not_check_the_inventory()
        {
            _inventory
                .DidNotReceive().IsQuantityAvailable("TestPart", -1);
        }

        public void AndThen_it_does_not_raise_an_order_submitted_event()
        {
            _publisher
                .DidNotReceive().Publish(Arg.Any<OrderSubmitted>());
        }

        public void AndThen_it_logs_the_order_part_number()
        {
            _logger
                .Received().Information(Arg.Is<string>(x => x == "TestPart"));
        }
    }
}