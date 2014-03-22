using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.DataAnnotations;
using Ploeh.AutoFixture.Dsl;
using Ploeh.AutoFixture.Kernel;

namespace Uncas.GraphiteAlerts.Tests
{
    public class TestObjectFactory : IFixture
    {
        private readonly IFixture _fixture;

        public TestObjectFactory()
        {
            ISpecimenBuilder[] la = new DefaultEngineParts().ToArray();
            _fixture = new Fixture(
                new DefaultEngineParts(
                    la.Where(
                        x =>
                            !(x is RangeAttributeRelay) &&
                            !(x is RegularExpressionAttributeRelay))))
                .Customize(new MultipleCustomization())
                .Customize(new AutoMoqCustomization());

            ISpecimenBuilderTransformation specimenBuilderTransformations =
                _fixture.Behaviors.Single(x => x is ThrowingRecursionBehavior);
            _fixture.Behaviors.Remove(specimenBuilderTransformations);
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _fixture.Customizations.Add(new StableFiniteSequenceRelay());
        }

        #region IFixture Members

        public ICustomizationComposer<T> Build<T>()
        {
            return _fixture.Build<T>();
        }

        public IFixture Customize(ICustomization customization)
        {
            return _fixture.Customize(customization);
        }

        public void Customize<T>(
            Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation)
        {
            _fixture.Customize(composerTransformation);
        }

        public IList<ISpecimenBuilderTransformation> Behaviors
        {
            get { return _fixture.Behaviors; }
        }

        public IList<ISpecimenBuilder> Customizations
        {
            get { return _fixture.Customizations; }
        }

        public bool OmitAutoProperties
        {
            get { return _fixture.OmitAutoProperties; }
            set { _fixture.OmitAutoProperties = value; }
        }

        public int RepeatCount
        {
            get { return _fixture.RepeatCount; }
            set { _fixture.RepeatCount = value; }
        }

        public IList<ISpecimenBuilder> ResidueCollectors
        {
            get { return _fixture.ResidueCollectors; }
        }

        [DebuggerStepThrough]
        public ISpecimenBuilder Compose()
        {
            return _fixture;
        }

        #endregion

        public object Create(object request, ISpecimenContext context)
        {
            return _fixture.Create(request, context);
        }

        public static TestObjectFactory SetupFixture(
            bool withAutoFixtureLegacySupport = true)
        {
            return new TestObjectFactory();
        }
    }
}