using System;
using AcklenAvenue.Testing.Nancy;
using FireTower.Domain;
using FireTower.Domain.Entities;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace FireTower.Api.Specs.Users
{
    public class when_sending_disaster_email : given_a_Disaster_module_context
    {
        const string email = "user@domain.com";
        static readonly DateTime date = DateTime.Today;
        static double latitude = 234.23;
        private static double longitude = 234.234;
        static string locationDescription= "location description";
        protected static IEmailSender _EmailSender = Mock.Of<IEmailSender>();
        static BrowserResponse _result;

        

        Establish context =
            () =>
                {
                    Disaster _model = new Disaster(date,
                                                   locationDescription,
                                                   latitude,
                                                   longitude);
                    Mock.Get(_EmailSender).Setup(x => x.Send(_model, email));

                };

        

        Because of =
            () => _result = Browser.PostSecureJson("/SendDisasterByEmail", new { Email = email, CreatedDate = date, LocationDescription = locationDescription, Latitude = latitude, Longitude = longitude });

        It should_send_the_email =
            () => _result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }
}