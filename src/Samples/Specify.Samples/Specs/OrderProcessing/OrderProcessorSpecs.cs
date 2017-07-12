﻿using Serilog;
using NSubstitute;
using Shouldly;
using Specify.Samples.Domain.OrderProcessing;

namespace Specify.Samples.Specs.OrderProcessing
{
    public class given
    {
        public abstract class the_item_is_available : ScenarioFor<OrderProcessor>
        {
            protected void Given_the_item_is_available()
            {
                The<IInventory>().IsQuantityAvailable("TestPart", 10).Returns(true);
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
            The<IInventory>().Received().IsQuantityAvailable("TestPart", 10);
        }

        public void AndThen_it_raises_an_order_submitted_event()
        {
            The<Publisher>().Received().Publish(Arg.Is<OrderSubmitted>(x => x.OrderNumber == _result.OrderNumber));
        }

        public void AndThen_it_logs_the_order_part_number()
        {
            The<ILogger>().Received().Information(Arg.Is<string>(x => x == "TestPart"));
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
            Container.Get<Publisher>().DidNotReceive().Publish(Arg.Any<OrderSubmitted>());
        }

        public void AndThen_it_logs_the_order_part_number()
        {
            The<ILogger>().Received().Information(Arg.Is<string>(x => x == "TestPart"));
        }
    }
}
