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


IEnumerable<string> GetFruits()
{
    yield return "Apple";
    yield return "Banana";
    yield return "Cherry";
    yield return "Banana";
    yield return "Mango";
}
// LINQ to Objects allows you to query any IEnumerable<T> collection, including those produced by iterator methods like GetFruits.
var fruitsFromMethod = GetFruits().Where(f => f.StartsWith("B")).ToArray();

IQueryable<string> queryableFruits = fruits.AsQueryable();
var queryableBananas = queryableFruits.Where(f => f == "Banana").ToList();
Console.WriteLine($"Bananas from IQueryable: {string.Join(", ", queryableBananas)}");

// Note: In this example, since we're using an in-memory collection, the behavior of IQueryable and IEnumerable will be similar. However, in a real-world scenario with a database provider (like Entity Framework), IQueryable would translate the query into SQL and execute it on the database server, while IEnumerable would execute the query in memory after retrieving all data.
// This code demonstrates the use of LINQ to query and manipulate collections in C#. It shows how to filter, project, and materialize results using methods like Where, Select, ToList, and ToArray. It also illustrates the difference between IEnumerable and IQueryable in the context of LINQ queries.
// The output of this program will be:
// Hello, World!    
// Apple
// Apple, Banana, Cherry, Banana, Mango
// Mango count: 1
// Banana count: 2  
// Mango selected: Mango
// Banana selected: Banana, Banana  
// Fruits starting with 'M': Mango
// Bananas from IQueryable: Banana, Banana     

// Diference between IEnumerable and IQueryable:
// IEnumerable<T> is an interface that represents a sequence of elements that can be enumerated.
// It is typically used for in-memory collections and supports deferred execution. When you call a LINQ method on an IEnumerable<T>, the query is executed in memory, and the results are returned as an IEnumerable<T>.
// IQueryable<T> is an interface that represents a queryable data source. It is typically used for querying data from external sources, such as databases. When you call a LINQ method on an IQueryable<T>, the query is not executed immediately. Instead, it builds an expression tree that represents the query, which can be translated into a query language (like SQL) and executed on the data source. 
// This allows for more efficient querying, as only the necessary data is retrieved from the source. 
// In summary, IEnumerable<T> is used for in-memory collections and executes queries in memory, while IQueryable<T> is used for querying external data sources and allows for query translation and execution on the data source.
// In this example, since we're using an in-memory collection, the behavior of IQueryable and IEnumerable will be similar. However, in a real-world scenario with a database provider (like Entity Framework), IQueryable would translate the query into SQL and execute it on the database server, while IEnumerable would execute the query in memory after retrieving all data.

Examples.IEnumerableVsIQueryableDemo.Run();