using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Uncas.GraphiteAlerts.Tests
{
    public abstract class WithFixture : AssertionHelper
    {
        private Lazy<IFixture> _fixture;

        public IFixture Fixture
        {
            get { return _fixture.Value; }
        }

        protected virtual IFixture CreateFixture()
        {
            return TestObjectFactory.SetupFixture();
        }

        [SetUp]
        public void SetupFixture()
        {
            _fixture = new Lazy<IFixture>(CreateFixture);
        }

        public T A<T>()
        {
            return Fixture.Create<T>();
        }

        public IEnumerable<T> Many<T>(int count = 3)
        {
            return Fixture.CreateMany<T>(count);
        }

        protected Mock<TMock> FreezeMock<TMock>() where TMock : class
        {
            return Fixture.FreezeMock<TMock>();
        }
    }

    public abstract class WithFixture<T> : WithFixture
    {
        private Lazy<T> _sut;

        public T Sut
        {
            get { return _sut.Value; }
        }

        protected SetupFor<T> Setup
        {
            get { return new SetupFor<T>(this); }
        }

        [SetUp]
        public void SetupSut()
        {
            _sut = new Lazy<T>(Fixture.Create<T>);
        }
    }

    public class SetupFor
    {
        protected SetupFor(WithFixture withFixture)
        {
            WithFixture = withFixture;
        }

        public WithFixture WithFixture { get; set; }
    }

    public class SetupFor<T> : SetupFor
    {
        public SetupFor(WithFixture<T> withFixture) : base(withFixture)
        {
            WithFixture = withFixture;
        }

        public new WithFixture<T> WithFixture { get; private set; }
    }
}