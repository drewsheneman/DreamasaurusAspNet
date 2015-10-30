using Dreamasaurus.Services;
using Moq;
using NUnit.Framework;

namespace Dreamasaurus.Tests.Services
{
    [TestFixture]
    public class DreamsServiceTests
    {
        public IDreamsService DreamsService;

        //Mock<>
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void ShouldDeleteAValidDream()
        {

            Assert.IsTrue(true);
        }
    }
}