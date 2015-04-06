using NSubstitute;
using Shouldly;

namespace Specify.IntegrationTests.App.OrderProcessing
{
    public class given
    {
        public abstract class the_item_is_available : NScenarioFor<OrderProcessor>
        {
            protected void Given_the_item_is_available()
            {
                Container.Get<IInventory>().IsQuantityAvailable("TestPart", 10).Returns(true);
            }
        }
    }

    public class processing_an_order : given.the_item_is_available
    {
        private OrderResult _result;

        public void When_processing_an_order()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = 10 });
        }

        public void Then_the_order_is_accepted()
        {
            _result.WasAccepted.ShouldBe(true);
        }

        public void AndThen_it_checks_the_inventory()
        {
            Container.Get<IInventory>().Received().IsQuantityAvailable("TestPart", 10);
        }

        public void AndThen_it_raises_an_order_submitted_event()
        {
            Container.Get<IPublisher>().Received().Publish(Arg.Is<OrderSubmitted>(x => x.OrderNumber == _result.OrderNumber));
        }
    }
    
    public class orders_with_a_negative_quantity : given.the_item_is_available
    {
        private OrderResult _result;

        protected void When_processing_an_order_with_a_negative_quantity()
        {
            _result = SUT.Process(new Order { PartNumber = "TestPart", Quantity = -1 });
        }

        public void Then_the_order_is_rejected()
        {
            _result.WasAccepted.ShouldBe(false);
        }

        public void AndThen_it_does_not_check_the_inventory()
        {
            Container.Get<IInventory>().DidNotReceive().IsQuantityAvailable("TestPart", -1);
        }

        public void AndThen_it_does_not_raise_an_order_submitted_event()
        {
            Container.Get<IPublisher>().DidNotReceive().Publish(Arg.Any<OrderSubmitted>());
        }
    }
}
