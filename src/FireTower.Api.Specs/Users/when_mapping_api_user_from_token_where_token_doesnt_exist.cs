using System;
using AcklenAvenue.Testing.Moq;
using Machine.Specifications;
using Moq;
using FireTower.Data;
using FireTower.Domain.Entities;
using FireTower.Domain.Exceptions;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_mapping_api_user_from_token_where_token_doesnt_exist : given_an_api_user_mapper_context
    {
        static readonly Guid AccessToken = Guid.NewGuid();

        static Exception _exception;

        Establish context =
            () =>
                {
                    DateTime now = DateTime.Now;
                    Mock.Get(_timeProvider).Setup(x => x.Now()).Returns(now);

                    Mock.Get(_readOnlyRepo).Setup(
                        x => x.First(ThatHas.AnExpressionFor<UserSession>()
                                         .Build()))
                        .Throws(new ItemNotFoundException<UserSession>());
                };

        Because of =
            () => _exception = Catch.Exception(() => _mapper.GetUserFromAccessToken(AccessToken));

        It should_throw_an_exception =
            () => _exception.ShouldBeOfExactType<TokenDoesNotExistException>();
    }
}