using System;
using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Moq.ExpectedObjects;
using BlingBag;
using FireTower.Domain.Events;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace FireTower.ViewStore.Specs
{
    public class after_creating_a_new_disaster
    {
        static IBlingHandler<NewDisasterCreated> _eventHandler;
        static IViewModelRepository _viewModelRepository;

        static readonly NewDisasterCreated NewDisasterCreated =
            new NewDisasterCreated(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now, "Santa Ana", 1234.43, 12321.43);

        Establish context =
            () =>
                {
                    _viewModelRepository = Mock.Of<IViewModelRepository>();
                    _eventHandler = new DisasterViewModelCreator(_viewModelRepository);
                };

        Because of =
            () => _eventHandler.Handle(NewDisasterCreated);

        It should_add_the_disaster_to_the_view_model_store =
            () => Mock.Get(_viewModelRepository)
                      .Verify(x =>
                              x.Create(
                                  WithExpected.Object(
                                      new DisasterViewModel(NewDisasterCreated.DisasterId,
                                                            NewDisasterCreated.CreatedDate,
                                                            NewDisasterCreated.LocationDescription,
                                                            NewDisasterCreated.Latitude,
                                                            NewDisasterCreated.Longitude))));
    }

    public class after_creating_adding_an_image_to_an_existing_disaster
    {
        static IBlingHandler<NewImageAddedToDisaster> _eventHandler;
        static IViewModelRepository _viewModelRepository;

        static readonly NewImageAddedToDisaster NewImageAddedToDisaster =
            new NewImageAddedToDisaster(Guid.NewGuid(), Guid.NewGuid(), "url");

        static DisasterViewModel _disasterViewModel;
        static DisasterViewModel _expectedDisaster;

        Establish context =
            () =>
                {
                    _viewModelRepository = Mock.Of<IViewModelRepository>();
                    _eventHandler = new DisasterViewModelImageAdder(_viewModelRepository);

                    _disasterViewModel = new DisasterViewModel
                                             {
                                                 DisasterId = NewImageAddedToDisaster.DisasterId,
                                                 Images = new[] {"existingImage"}
                                             };
                    Mock.Get(_viewModelRepository).Setup(x => x.FindOne(ThatHas.AnExpressionFor<DisasterViewModel>()
                                                                            .ThatMatches(_disasterViewModel)
                                                                            .ThatDoesNotMatch(new DisasterViewModel())
                                                                            .Build()))
                        .Returns(_disasterViewModel);

                    _expectedDisaster = new DisasterViewModel
                                            {
                                                DisasterId = NewImageAddedToDisaster.DisasterId,
                                                Images = new[] {"existingImage", NewImageAddedToDisaster.ImageUrl}
                                            };
                };

        Because of =
            () => _eventHandler.Handle(NewImageAddedToDisaster);

        It should_add_the_disaster_to_the_view_model_store =
            () => Mock.Get(_viewModelRepository)
                      .Verify(x =>
                              x.Update(
                                  WithExpected.Object(_expectedDisaster)));
    }
}