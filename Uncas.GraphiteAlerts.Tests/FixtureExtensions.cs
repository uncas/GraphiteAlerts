using System;
using System.Linq.Expressions;
using Moq;
using Ploeh.AutoFixture;

namespace Uncas.GraphiteAlerts.Tests
{
    public static class FixtureExtensions
    {
        public static TValue FreezeResult<TMock, TValue>(this IFixture fixture)
            where TMock : class
        {
            var value = fixture.Freeze<TValue>();
            return fixture.FreezeResult<TMock, TValue>(value);
        }

        public static TValue FreezeResult<TMock, TValue>(this IFixture fixture,
            TValue value) where TMock : class
        {
            fixture.FreezeMock<TMock>().SetReturnsDefault(value);
            return value;
        }

        public static Mock<TMock> FreezeMock<TMock, TValue>(this IFixture fixture)
            where TMock : class
        {
            Mock<TMock> mock = fixture.FreezeMock<TMock>();
            mock.SetReturnsDefault(fixture.Freeze<TValue>());
            return mock;
        }

        public static void FreezeResult<TMock, TValue, TValue2>(this IFixture fixture,
            TValue value, TValue2 value2) where TMock : class
        {
            Mock<TMock> freezeMock = fixture.FreezeMock<TMock>();
            freezeMock.SetReturnsDefault(value);
            freezeMock.SetReturnsDefault(value2);
        }

        public static Mock<T> FreezeMock<T>(this IFixture fixture) where T : class
        {
            var freeze = fixture.Freeze<Mock<T>>();
            return freeze;
        }

        public static Mock<T> RegisterMock<T>(this IFixture fixture,
            Expression<Func<T, bool>> setup)
            where T : class
        {
            T mock = Mock.Of(setup);
            fixture.Register(() => mock);
            return Mock.Get(mock);
        }
    }
}