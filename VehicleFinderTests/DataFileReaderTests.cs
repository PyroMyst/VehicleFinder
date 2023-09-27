using VehicleFinder;

namespace VehicleFinderTests
{
    internal class DataFileReaderTests
    {
        [Test]
        public void DataFileReaderReturnsExpectedNumberOfRecords()
        {
            var vehicles=  DataFileReader.Read();

            Assert.That(vehicles.Count, Is.EqualTo(2000001));
        }
    }
}
