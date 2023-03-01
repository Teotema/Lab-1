// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;

Console.WriteLine("Input currency");



var client = new HttpClient();
var vuluev = Console.ReadLine();

var dateTime = DateTime.Now;
var dtformat = dateTime.ToString("yyyyMMdd");
var resr = await client.GetAsync($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date={dtformat}");

var xml = System.Xml.Linq.XElement.Parse(await resr.Content.ReadAsStringAsync());

var n = xml.Nodes().Select(x => (XElement)x).Select(x => new Сurrency()
{
    Name = x.Element("cc").Value,
    Value = x.Element("rate").Value
}).ToArray();

var res = n.FirstOrDefault(x => x.Name == vuluev);

Console.WriteLine(res?.Value);
Console.ReadLine();

public class Сurrency
{
    public string Name { get; set; }
    public string Value { get; set; }
}