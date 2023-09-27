using System.Diagnostics;
using VehicleFinder;

var appTimer = Stopwatch.StartNew();

var positions = GetPositions();

var fileTimer = Stopwatch.StartNew();
var vehicles = DataFileReader.Read();
fileTimer.Stop();

var finderTimer = Stopwatch.StartNew();
var results = await VehicleFinder.VehicleFinder.FindAsync(positions, vehicles); // Asynchronous finder.
//var results = VehicleFinder.VehicleFinder.Find(positions, vehicles);  // Synchronous finder.
finderTimer.Stop();

appTimer.Stop();


Console.WriteLine();
Console.WriteLine("Results :");
Console.WriteLine($"  File read time :             {fileTimer.ElapsedMilliseconds} ms");
Console.WriteLine($"  Finder time :                {finderTimer.ElapsedMilliseconds} ms");
Console.WriteLine($"  Application execution time : {appTimer.ElapsedMilliseconds} ms");
Console.WriteLine();

var counter = 0;
results = results.OrderBy(x => Array.IndexOf(positions, x.Key)).ToDictionary(x => x.Key, x => x.Value);
foreach (var result in results.Values)
{
    Console.Write($"position {counter + 1,2} ({positions[counter].Latitude,9:0.000000} , {positions[counter].Longitude,11:0.000000})");
    Console.Write($" | vehicle : [{result.VehicleId,7}]");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write($" {result.VehicleRegistration,11}");
    Console.ResetColor();
    //Console.Write($" {result.RecordedTimeUTC}");
    Console.Write($" ({result.Latitude,9:0.000000} , {result.Longitude,11:0.000000})");
    Console.WriteLine();

    counter++;
}
Console.WriteLine();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

Position[] GetPositions()
{
    return new Position[]
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
}
