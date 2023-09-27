using System.Collections;
using VehicleFinder;

namespace VehicleFinderTests
{
    internal static class TestData
    {
        internal static Position[] Positions = new Position[]
        {
            new Position(34.544909f, -102.100843f),
            new Position(32.345544f, -99.123124f),
            new Position(33.234235f, -100.214124f),
            new Position(35.195739f, -95.348899f),
            new Position(31.895839f, -97.789573f),
            new Position(32.895839f, -101.789573f),
            new Position(34.115839f, -100.225732f),
            new Position(32.335839f, -99.992232f),
            new Position(33.535339f, -94.792232f),
            new Position(32.234235f, -100.222222f)
        };

        internal static Vehicle[] Vehicles = new Vehicle[]
        {
        new Vehicle(239701, "K2-080 CT", 34.54235f, -102.10086f, DateTime.Parse("2022-11-17T13:29:44").ToUniversalTime()),
        new Vehicle(864907, "3I-388 XE", 32.344444f, -99.12403f, DateTime.Parse("2022-11-17T13:29:45").ToUniversalTime()),
        new Vehicle(835420, "77-0126 BV", 33.23224f, -100.21374f, DateTime.Parse("2022-11-17T13:30:15").ToUniversalTime()),
        new Vehicle(1590684, "65-31272 KE", 35.195534f, -95.34728f, DateTime.Parse("2022-11-17T13:29:44").ToUniversalTime()),
        new Vehicle(729854, "1Y-805 OU", 31.89555f, -97.78699f, DateTime.Parse("2022-11-17T13:29:58").ToUniversalTime()),
        new Vehicle(304750, "81-01772 GK", 32.898594f, -101.78807f, DateTime.Parse("2022-11-17T13:29:57").ToUniversalTime()),
        new Vehicle(1629527, "R6-2507 JP", 34.117325f, -100.2253f, DateTime.Parse("2022-11-17T13:29:39").ToUniversalTime()),
        new Vehicle(1065091, "1I-6674 HJ", 32.335518f, -99.992004f, DateTime.Parse("2022-11-17T13:29:26").ToUniversalTime()),
        new Vehicle(302648, "KS-35442 DD", 33.533287f, -94.79153f, DateTime.Parse("2022-11-17T13:29:22").ToUniversalTime()),
        new Vehicle(686758, "7A-4445 DH", 32.23328f, -100.22275f, DateTime.Parse("2022-11-17T13:30:29").ToUniversalTime())
        };

        internal static IEnumerable FindVehicleData
        {
            get
            {
                yield return new TestCaseData(new[] { Positions[0], Positions[2] }, Vehicles).Returns(new[] { Vehicles[0], Vehicles[2] });
                yield return new TestCaseData(new[] { Positions[4] }, Vehicles).Returns(new[] { Vehicles[4] });
            }
        }
    }
}
