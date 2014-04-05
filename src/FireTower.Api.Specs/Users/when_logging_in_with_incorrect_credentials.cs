using System;
using AcklenAvenue.Testing.Moq;
using AcklenAvenue.Testing.Nancy;
using FireTower.Data;
using FireTower.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_logging_in_with_incorrect_credentials : given_a_login_module_context
    {
        static Exception _exception;

        Establish context =
            () => Mock.Get(ReadOnlyRepository).Setup(x => x.First(ThatHas.AnExpressionFor<User>().Build()))
                      .Throws(new ItemNotFoundException<User>());

        Because of =
            () =>
            _exception =
            Catch.Exception(
                () => Browser.PostSecureJson("/login", new {email = "incorrect@email.com", password = "incorrect"}));

        It should_return_an_unauthorized_response =
            () => _exception.InnerException.InnerException.ShouldBeOfExactType<UnauthorizedAccessException>();
    }
}