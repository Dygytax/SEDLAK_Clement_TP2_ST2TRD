using System.Text.Json;

static async Task<Root> getWeather(string customRequest)
{
    var client = new HttpClient();
    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri =
            new Uri(String.Format("https://api.openweathermap.org/data/2.5/weather?{0}&units=metric&appid=f729f099b81280e7124a6719d2184417", customRequest))
    };
    using (var response = await client.SendAsync(request))
    {
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Root myDeserializedClass = JsonSerializer.Deserialize<Root>(body);
        return myDeserializedClass;
    }
}

Root result2 = await getWeather("q=Morocco");
Console.WriteLine("The weather in Morocco is : " + result2.weather[0].main);

Root result3 = await getWeather("q=Oslo");
DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
dateTime = dateTime.AddSeconds(result3.sys.sunrise).ToLocalTime();
Console.WriteLine("The sun in Oslo rise at : " + dateTime);

DateTime dateOslo = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
dateOslo = dateOslo.AddSeconds(result3.sys.sunset).ToLocalTime();
Console.WriteLine("The sun in Oslo set at : " + dateOslo);

Root result4 = await getWeather("q=Jakarta");
Console.WriteLine("The temperature in Jakarta is : " + result4.main.temp + " degrees");

Root NewYork = await getWeather("q=New%20York");
Root Tokyo = await getWeather("q=Tokyo");
Root Paris = await getWeather("q=Paris");

if (NewYork.wind.speed >= Tokyo.wind.speed && NewYork.wind.speed >= Paris.wind.speed)
{
    Console.WriteLine("The most windy city is New-York with a wind speed of : " + NewYork.wind.speed);
}
else if (Tokyo.wind.speed >= NewYork.wind.speed && Tokyo.wind.speed >= Paris.wind.speed)
{
    Console.WriteLine("The most windy city is Tokyo with a wind speed of : " + Tokyo.wind.speed);
}
else
{
    Console.WriteLine("The most windy city is Paris with a wind speed of : " + Paris.wind.speed);
}

Root result5 = await getWeather("q=Kiev");
Console.WriteLine("The humidity in Kiev is : " + result5.main.humidity + " % and the pressure is : " + result5.main.pressure + " hPa");

Root result6 = await getWeather("q=Moscow");
Console.WriteLine("The humidity in Moscow is : " + result6.main.humidity + " % and the pressure is : " + result6.main.pressure + " hPa");

Root result7 = await getWeather("q=Berlin");
Console.WriteLine("The humidity in Berlin is : " + result7.main.humidity + " % and the pressure is : " + result7.main.pressure + " hPa");

public class Clouds
{
    public int all { get; set; }
}

public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
    public int sea_level { get; set; }
    public int grnd_level { get; set; }
}

public class Root
{
    public Coord coord { get; set; }
    public List<Weather> weather { get; set; }
    public string @base { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public int deg { get; set; }
    public double gust { get; set; }
}


