using Machine.Specifications;

namespace FireTower.IronMq.Specs.Integration
{
    public class when_subscribing_to_a_queue
    {
        static IIronMqSubscriber _subscriber;

        Establish context =
            () => { _subscriber = new IronMqSubscriber(); };

        Because of =
            () => _subscriber.Subscribe("test_queue", "http://requestb.in/o6h7axo6");

        It should_work =
            () => { };
    }
}