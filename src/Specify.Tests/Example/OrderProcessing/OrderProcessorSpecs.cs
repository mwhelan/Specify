using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Specify.Tests.Example.OrderProcessing
{
    public class given
    {
        public abstract class the_item_is_available : SpecificationFor<OrderProcessor>
        {
            protected void Given_the_item_is_available()
            {
                SubFor<IInventory>().IsQuantityAvailable("TestPart", 10).Returns(true);
            }
        }
    }

    public class processing_an_order : given.the_item_is_available
    {
        private OrderResult _result;

        public void when_processing_an_order()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = 10 });
        }

        [Test]
        public void then_the_order_is_accepted()
        {
            _result.WasAccepted.Should().BeTrue();
        }

        [Test]
        public void then_it_checks_the_inventory()
        {
            SubFor<IInventory>().Received().IsQuantityAvailable("TestPart", 10);
        }

        [Test]
        public void then_it_raises_an_order_submitted_event()
        {
            SubFor<IPublisher>().Received().Publish(Arg.Is<OrderSubmitted>(x => x.OrderNumber == _result.OrderNumber));
        }
    }
    
    public class orders_with_a_negative_quantity : given.the_item_is_available
    {
        private OrderResult _result;

        protected void when_processing_an_order_with_a_negative_quantity()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = -1 });
        }

        [Test]
        public void then_the_order_is_rejected()
        {
            _result.WasAccepted.Should().BeFalse();
        }

        [Test]
        public void then_it_does_not_check_the_inventory()
        {
            SubFor<IInventory>().DidNotReceive().IsQuantityAvailable("TestPart", -1);
        }

        [Test]
        public void then_it_does_not_raise_an_order_submitted_event()
        {
            SubFor<IPublisher>().DidNotReceive().Publish(Arg.Any<OrderSubmitted>());
        }
    }
}
