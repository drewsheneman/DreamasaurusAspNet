using System.Web.Mvc;
using Dreamasaurus.Controllers;
using NUnit.Framework;
using Moq;
using Dreamasaurus.DAL;

namespace Dreamasaurus.Tests.Controllers
{
    [TestFixture]
    public class DreamsControllerTests
    {
        Mock<IDreamsDbContext> _dbContextMock;

        //Mock<>
        [SetUp]
        public void SetUp()
        {
            this._dbContextMock = new Mock<IDreamsDbContext>();
        }

        [Test]
        public void Index()
        {
            DreamsController controller = new DreamsController();
 
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}