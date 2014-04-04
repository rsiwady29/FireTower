using System;
using System.Linq;
using AcklenAvenue.Testing.AAT;
using FireTower.Domain.Commands;
using FireTower.Presentation.Responses;
using Machine.Specifications;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_creating_a_new_disaster : given_an_api_server_context<CurrentlyDeveloping>
    {
        static Guid _token;
        static IRestResponse _result;
        static string _url;

        Establish context =
            () =>
                {
                    RegisterUser();

                    _token = Login().Token;

                    _url = string.Format("http://{0}.url.com", new Random().Next(9999999));
                };

        Because of =
            () => _result = Client.Post("/disasters", new CreateNewDisaster(DateTime.Now, "Santa Ana", 123.45, 456.32, _url, 1), _token);

        It should_return_ok =
            () => _result.ShouldBeOk();

        It should_put_a_disaster_in_the_view_model_store =
            () =>
                {
                    
                };
    }
}