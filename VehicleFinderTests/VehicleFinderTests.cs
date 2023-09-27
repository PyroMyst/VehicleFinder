using System.Text.Json;
using VehicleFinder;

namespace VehicleFinderTests
{
    [TestFixture]
    internal class VehicleFinderTests
    {
        [Test]
        [TestCaseSource(typeof(TestData), nameof(TestData.FindVehicleData))]
        public async Task<Vehicle[]> FindReturnsNearestVehicleToPosition(Position[] positions , Vehicle[] vehicles)
        {
            return (await VehicleFinder.VehicleFinder.FindAsync(positions, vehicles)).Values.ToArray();
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(9)]
        public void FindReturnsNearestVehicleToPositionWithActualData(int positionIndex)
        {
            // Arrange
            var positions = TestData.Positions[positionIndex..(positionIndex + 1)];

            var expectedVehicleJson = JsonSerializer.Serialize( TestData.Vehicles[positionIndex]);
            var vehicles = DataFileReader.Read();

            // Act
            var result = VehicleFinder.VehicleFinder.FindAsync(positions, vehicles).Result;

            // Assert
            var actualJson = JsonSerializer.Serialize(result.First().Value);
            Assert.That(actualJson, Is.EqualTo(expectedVehicleJson));
        }
    }
}
