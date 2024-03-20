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
            return (Dictionary<string, string>) dic;

        }
        catch (Exception e)
        {
            throw new LegitException("Load properties file failed!" + e);
        }
    }
}