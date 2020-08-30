using NUnit.Framework;
using Unity.Entities;

namespace Timespawn.Core.Tests
{
    [TestFixture]
    public abstract class ECSTestFixture
    {
        protected World ActiveWorld { get; private set; }
        protected EntityManager ActiveEntityManager { get; private set; }

        [SetUp]
        protected virtual void SetUp()
        {
            ActiveWorld = new World("TestWorld");
            ActiveEntityManager = ActiveWorld.EntityManager;

            World.DefaultGameObjectInjectionWorld = ActiveWorld;
        }

        [TearDown]
        protected virtual void TearDown()
        {
            ActiveWorld.Dispose();

            ActiveWorld = null;
            ActiveEntityManager = default;

            World.DefaultGameObjectInjectionWorld = null;
        }
    }
}