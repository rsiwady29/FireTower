using AutoMapper;
using Moq;

namespace FireTower.Api.Specs
{
    public static class MoqMappingExtensions
    {
        public static void SetupMapping<TIn, TOut>(this Mock<IMappingEngine> mock, TIn typeIn, TOut typeOut)
        {
            mock.Setup(x => x.Map<TIn, TOut>(typeIn)).Returns(typeOut);
        }
    }
}