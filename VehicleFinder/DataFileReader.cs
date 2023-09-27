using System.Reflection;
using System.Text;

namespace VehicleFinder;

public static class DataFileReader
{
    private const string filename = "VehiclePositions.dat";

    private static string GetFilePath()
    {
        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (directory == null)
            return string.Empty;
        var path = Path.Combine(directory, filename);
        return path;
    }

    public static IEnumerable<Vehicle> Read()
    {
        var path = GetFilePath();
        if (File.Exists(path))
            return ReadFile(path);

        Console.WriteLine("File not found!");
        return Enumerable.Empty<Vehicle>();
    }

    private static List<Vehicle> ReadFile(string path)
    {
        var vehicles = new List<Vehicle>();
        var buffer = new Buffer { data = File.ReadAllBytes(path) };
        var length = buffer.data.Length;

        while (buffer.offset < length)
            vehicles.Add(ConvertBytes(buffer));

        return vehicles;
    }

    private static Vehicle ConvertBytes(Buffer buffer)
    {
        var vehicleId = BitConverter.ToInt32(buffer.data, buffer.offset);
        buffer.offset += 4;

        var length = 0; // Registration number lengths in the dataset are between 9 and 11 characters. We could set this to 9 and save an extra 200ms.
        while (buffer.data[buffer.offset + length] != 0)
            length++;
        var vehicleReg = Encoding.Default.GetString(buffer.data, buffer.offset, length);
        buffer.offset += (length + 1);

        var latitude = BitConverter.ToSingle(buffer.data, buffer.offset);
        buffer.offset += 4;

        var longitude = BitConverter.ToSingle(buffer.data, buffer.offset);
        buffer.offset += 4;

        ulong uint64 = BitConverter.ToUInt64(buffer.data, buffer.offset);
        var recordedTimeUtc = DateTime.UnixEpoch.AddSeconds(uint64);
        buffer.offset += 8;

        return new Vehicle(vehicleId, vehicleReg, latitude, longitude, recordedTimeUtc);
    }

    private class Buffer
    {
        public byte[] data = Array.Empty<byte>();
        public int offset = 0;
    }
}
