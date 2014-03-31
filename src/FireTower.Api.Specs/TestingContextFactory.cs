using Nancy;

namespace FireTower.Api.Specs
{
    public class TestingContextFactory : INancyContextFactory
    {
        readonly NancyContext _nancyContext;

        public TestingContextFactory(NancyContext nancyContext)
        {
            _nancyContext = nancyContext;
        }

        public NancyContext Create(Request request)
        {
            _nancyContext.Request = request;
            return _nancyContext;
        }
    }
}