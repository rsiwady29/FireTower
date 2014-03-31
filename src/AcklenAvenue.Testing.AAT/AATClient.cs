using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using RestSharp;

namespace AcklenAvenue.Testing.AAT
{
    public class AATClient
    {
        readonly string _host;

        public AATClient(string host)
        {
            _host = host;
        }

        public IRestResponse Execute(string resource, Method method, object payload = null)
        {
            RestClient browser = GetRestClient();
            RestRequest request = BuildRestRequest(resource, method, payload);
            return browser.Execute(request);
        }

        public IRestResponse<T> Get<T>(string resource, object args)
            where T : new()
        {
            RestClient browser = GetRestClient();

            RestRequest request = BuildRestRequest(resource, Method.GET);
            foreach (PropertyInfo property in args.GetType().GetProperties())
            {
                request.AddParameter(property.Name, property.GetValue(args));
            }

            return ExecuteRequest<T>(browser, request);
        }


        public IRestResponse Post(string resource, object payload)
        {
            RestClient browser = GetRestClient();
            RestRequest request = BuildRestRequest(resource, Method.POST, payload);
            return ExecuteRequest(browser, request);
        }

        RestClient GetRestClient()
        {
            var restClient = new RestClient(_host);
            return restClient;
        }

        public IRestResponse Get(string resource, params KeyValuePair<string, string>[] keyValuePair)
        {
            RestClient browser = GetRestClient();

            RestRequest request = BuildRestRequest(resource, Method.GET);
            foreach (var valuePair in keyValuePair)
            {
                request.AddParameter(valuePair.Key, valuePair.Value);
            }

            return ExecuteRequest(browser, request);
        }

        static IRestResponse<T> ExecuteRequest<T>(RestClient browser, RestRequest request) where T : new()
        {
            IRestResponse<T> restResponse = browser.Execute<T>(request);
            if (restResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AATRestResponseException(HttpStatusCode.Unauthorized);
            }
            if (restResponse.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new AATRestResponseException(HttpStatusCode.Forbidden);
            }
            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }
            if (restResponse.Data == null)
            {
                throw new Exception("Response data is null.");
            }
            return restResponse;
        }

        static IRestResponse ExecuteRequest(RestClient browser, RestRequest request)
        {
            IRestResponse restResponse = browser.Execute(request);
            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }
            return restResponse;
        }

        public IRestResponse<T> Execute<T>(string resource, Method method, object payload = null)
            where T : new()
        {
            RestClient browser = GetRestClient();
            RestRequest request = BuildRestRequest(resource, method, payload);
            return ExecuteRequest<T>(browser, request);
        }

        static RestRequest BuildRestRequest(string resource, Method method, object payload = null)
        {
            var request = new RestRequest(resource, method)
                              {
                                  JsonSerializer = new RestSharpJsonNetSerializer(),
                                  RequestFormat = DataFormat.Json
                              };
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            if (payload != null)
                request.AddBody(payload);
            return request;
        }
    }
}