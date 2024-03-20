
using PlaywrightTests.Exceptions;

namespace PlaywrightTests.Steps;

public static class Utils
{
    public static Dictionary<string, string> ReadPropertiesFile(string path)
    {
        try
        {
            var file = string.Join("", File.ReadAllLines(path));
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(file);
            if (dic is null)
                throw new LegitException();
            return (Dictionary<string, string>)dic;
        }
        catch (Exception e)
        {
            throw new LegitException("Load properties file failed!" + e);
        }
    }

    // public static Order GenerateOrderData(int numberOfProducts)
    // {
    //     var r = new Random();
    //     var order = new Order("", $"Ramat Gan, Bialik {r.Next(1, 1500)}, Israel", new List<Product>());
    //
    //     for (var i = 0; i < numberOfProducts; i++)
    //     {
    //         order.Products.Add(new Product(r.Next(1, 5), 20, r.Next(1, 5)));
    //     }
    //
    //     Console.WriteLine($"The following order data has been generated:\n" +
    //                       $"{JsonConvert.SerializeObject(order, Formatting.Indented)}");
    //     return order;
    // }
}