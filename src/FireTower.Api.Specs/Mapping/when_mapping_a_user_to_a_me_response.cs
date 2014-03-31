using AutoMapper;
using FizzWare.NBuilder;
using Machine.Specifications;
using FireTower.Domain.Entities;
using FireTower.Presentation;
using FireTower.Presentation.Responses;

namespace FireTower.Api.Specs.Mapping
{
    public class when_mapping_a_user_to_a_me_response
    {
        static User _user;
        static MeResponse _expected;
        static MeResponse _result;

        Establish context =
            () =>
                {
                    new ConfigureAutomapperMappings().Task(null);

                    _user = Builder<User>.CreateNew().Build();

                    _expected = new MeResponse
                                    {
                                        Activated = _user.Activated,
                                        Email = _user.Email,
                                    };
                };

        Because of =
            () => _result = Mapper.Map<User, MeResponse>(_user);

        It should_return_the_expected_me_response =
            () => _result.ShouldBeLike(_expected);
    }

    
}