// See https://aka.ms/new-console-template for more information

var fruit = "Apple";
var fruits = new List<string> { "Apple", "Banana", "Cherry", "Banana", "Mango"};
Console.WriteLine("Hello, World!");
Console.WriteLine(fruit);
Console.WriteLine(string.Join(", ", fruits));

var mangoCount = fruits.Count(f => f == "Mango");
var bananaCount = fruits.Count(f => f == "Banana");
Console.WriteLine($"Mango count: {mangoCount}");
Console.WriteLine($"Banana count: {bananaCount}");

var mangoSelected = fruits.Where(f => f == "Mango").Select(f => f);
var bananaSelected = fruits.Where(f => f == "Banana").Select(f => f);


var mangoSelectedList = mangoSelected.ToList();
var bananaSelectedList = bananaSelected.ToList();


Console.WriteLine($"Mango selected: {string.Join(", ", mangoSelectedList)}");
Console.WriteLine($"Banana selected: {string.Join(", ", bananaSelectedList)}");

var mangoList = 
fruits.Where(f => f.StartsWith("M")).ToList();
Console.WriteLine($"Fruits starting with 'M': {string.Join(", ", mangoList)}");