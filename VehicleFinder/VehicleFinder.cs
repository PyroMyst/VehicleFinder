namespace VehicleFinder;

public static class VehicleFinder
{
    private const double _range = .25d;
    private const double _modifier = .1d;

    public static Dictionary<Position, Vehicle> Find(Position[] positions, IEnumerable<Vehicle> vehicles)
    {
        if (vehicles is null || !vehicles.Any())
            return new();

        var results = new Dictionary<Position, Vehicle>();
        foreach (var position in positions)
        {
            results[position] = FindNearestVehicle(position, vehicles, _range, _modifier).Result;
        }
        return results;
    }

    public static async Task<Dictionary<Position, Vehicle>> FindAsync(Position[] positions, IEnumerable<Vehicle> vehicles)
    {
        if (vehicles is null || !vehicles.Any())
            return new();

        var tasks = new List<Task<(Position Position, Vehicle Vehicle)>>();
            foreach (var position in positions)
            {
                tasks.Add(Task.Run(async () => await FindNearestVehicle(position, vehicles)));
            }
            var results = (await Task.WhenAll(tasks))
                .ToDictionary(x => x.Position, x => x.Vehicle);
            return results;
    }

    private static async Task<(Position, Vehicle)> FindNearestVehicle(Position position, IEnumerable<Vehicle> vehicles)
    {


        var vehicle = await FindNearestVehicle(position, vehicles, _range, _modifier);
        return (position, vehicle);
    }

    private static async Task<Vehicle> FindNearestVehicle(Position position, IEnumerable<Vehicle> vehicles, double range, double modifier)
    {
        var latitudeMin = position.Latitude - range;
        var latitudeMax = position.Latitude + range;
        var longitudeMin = position.Longitude - range;
        var longitudeMax = position.Longitude + range;

        var subset = vehicles.Where(x =>
            x.Latitude >= latitudeMin && x.Latitude <= latitudeMax
            && x.Longitude >= longitudeMin && x.Longitude <= longitudeMax
        ).ToList();

        switch (subset.Count)
        {
            case 1:
                return subset.Single();

            case 0:
                range += modifier;
                return await FindNearestVehicle(position, vehicles, range, modifier);

            case <= 5:
                Vehicle? nearest = null;
                var nearestDistance = double.MaxValue;
                foreach (var vehicle2 in subset)
                {
                    //var dist = Math.Sqrt(Math.Pow(position.Latitude - vehicle2.Latitude, 2) + Math.Pow(position.Longitude - vehicle2.Longitude, 2));
                    var x = position.Latitude - vehicle2.Latitude;
                    var y = position.Longitude - vehicle2.Longitude;
                    var dist = x * x + y * y;
                    if (dist < nearestDistance)
                    {
                        nearestDistance = dist;
                        nearest = vehicle2;
                    }
                }
                return nearest!;

            default:
                range -= modifier;
                modifier *= .75;
                //modifier /= 2;
                return await FindNearestVehicle(position, subset, range, modifier);
        }
    }
}
