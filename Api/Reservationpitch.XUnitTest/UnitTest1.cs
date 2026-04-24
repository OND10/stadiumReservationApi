using NetArchTest.Rules;

namespace Reservationpitch.XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherLayers()
        {
            //Arrange
            var assembly = typeof(Reservationpitch.Domain.AssemblyReference).Assembly;

            //Act
            var result = Types.InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("Reservationpitch.Application")
                .And()
                .NotHaveDependencyOn("Reservationpitch.Infustracture")
                .GetResult();

            //Assert
            Assert.True(result.IsSuccessful);

        }
    }
}