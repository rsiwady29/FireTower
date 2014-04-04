using System;
using System.Collections.Generic;
using System.Linq;
using AcklenAvenue.Testing.AAT;
using FireTower.Domain.Commands;
using FireTower.ViewStore;
using Machine.Specifications;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using RestSharp;

namespace FireTower.API.AAT
{
    public class when_voting_on_the_severity_of_a_fire : given_an_api_server_context<CurrentlyDeveloping>
    {
        static Guid _disasterId;
        static IRestResponse _result;
        static Guid _token;
        static string _url;
        static MongoCollection<DisasterViewModel> _disasterViewModelCollection;
        static DisasterViewModel _disaster;

        Establish context =
            () =>
                {
                    _token = Login().Token;
                    _url = string.Format("http://{0}.url.com", new Random().Next(9999999));
                    Client.Post("/disasters", new CreateNewDisaster(DateTime.Now, "Santa Ana2", 123.45, 456.32, _url, 1),
                                _token);

                    var uri =
                        new MongoUrl(
                            @"mongodb://client:password@ds045137.mongolab.com:45137/appharbor_ab50c767-930d-4b7d-9571-dd2a0b62d5a9");

                    MongoServer server = new MongoClient(uri).GetServer();

                    MongoDatabase db = server.GetDatabase(uri.DatabaseName);
                    _disasterViewModelCollection = db.GetCollection<DisasterViewModel>("DisasterViewModel");
                    _disaster = _disasterViewModelCollection.AsQueryable().FirstOrDefault(x => x.Images.Contains(_url));
                    _disasterId = _disaster != null ? _disaster.DisasterId : Guid.Empty;
                };

        Because of =
            () => _result = Client.Post("votes/severity", new {disasterId = _disasterId, severity = 3}, _token);

        It should_add_the_severity_vote_to_the_severity_votes_list =
            () =>
                {
                    var disasterViewModel =
                        _disasterViewModelCollection.AsQueryable().FirstOrDefault(x => x.Images.Contains(_url));
                    disasterViewModel.SeverityVotes.Count().ShouldEqual(2);
                };

        It should_return_ok =
            () => _result.ShouldBeOk();

        Cleanup when_finished =
            () =>
                {
                    /*if (_disaster != null)
                    {
                        _disasterViewModelCollection.Remove(Query<DisasterViewModel>.EQ(x => x.DisasterId, _disaster.DisasterId));
                    }*/
                };
    }

    public class cosa
    {
        public DisasterId DisasterId { get; set; }
        public string[] Images { get; set; }
    }

    public class DisasterId
    {
        public Guid uuid { get; set; }
    }
}