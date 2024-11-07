//using NUnit.Framework;
//using Moq;
//using System.Web.Mvc;
//using CapaPresentacionTienda.Controllers;
//using CapaNegocio;
//using CapaEntidad;



//namespace MiProyecto.Tests
//{
//    [TestFixture]
//    public class TiendaControllerTests
//    {
//        private TiendaController _controller;
//        private Mock<CN_Producto> _mockProductoNegocio;

//        [SetUp]
//        public void Setup()
//        {
//            // Mock del negocio de productos
//            _mockProductoNegocio = new Mock<CN_Producto>();
//            // Instancia del controlador con la dependencia mockeada
//            _controller = new TiendaController(_mockProductoNegocio.Object);
//        }

//        [Test]
//        public void DetalleProducto_Returns_View_With_Product_When_Product_Exists()
//        {
//            // Arreglar
//            int testProductId = 1;
//            var expectedProduct = new Producto { IdProducto = testProductId, Nombre = "Producto Test" };
//            _mockProductoNegocio.Setup(p => p.Listar()).Returns(new List<Producto> { expectedProduct });

//            // Actuar
//            var result = _controller.DetalleProducto(testProductId) as ViewResult;

//            // Afirmar
//            Assert.IsNotNull(result);
//            var model = result.Model as Producto;
//            Assert.IsNotNull(model);
//            Assert.AreEqual(testProductId, model.IdProducto);
//        }
//    }
//}