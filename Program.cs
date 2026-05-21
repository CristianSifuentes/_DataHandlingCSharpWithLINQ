// See https://aka.ms/new-console-template for more information

var fruit = "Apple";
var fruits = new List<string> { "Apple", "Banana", "Cherry", "Banana", "Mango" };
Console.WriteLine("Hello, World!");
Console.WriteLine(fruit);
Console.WriteLine(string.Join(", ", fruits));

var mangoCount = fruits.Count(f => f == "Mango");
var bananaCount = fruits.Count(f => f == "Banana");
Console.WriteLine($"Mango count: {mangoCount}");
Console.WriteLine($"Banana count: {bananaCount}");

// Use Where to filter the collection by a predicate.
// This produces an IEnumerable<string> of items that match the condition.
var mangoSelected = fruits.Where(f => f == "Mango").Select(f => f);
var bananaSelected = fruits.Where(f => f == "Banana").Select(f => f);

// Select projects each matching element into the result sequence.
// In this case we simply return the same value, but Select can also transform items.

// ToList forces execution of the query and stores the results in a List<string>.
// This is useful when you want to enumerate once and keep the results in memory.
var mangoSelectedList = mangoSelected.ToList();
var bananaSelectedList = bananaSelected.ToList();

Console.WriteLine($"Mango selected: {string.Join(", ", mangoSelectedList)}");
Console.WriteLine($"Banana selected: {string.Join(", ", bananaSelectedList)}");

// Another example: filter by prefix and materialize the final result into a list.
var mangoList = fruits.Where(f => f.StartsWith("M")).ToList();
Console.WriteLine($"Fruits starting with 'M': {string.Join(", ", mangoList)}");