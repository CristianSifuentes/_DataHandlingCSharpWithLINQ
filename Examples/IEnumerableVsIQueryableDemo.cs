using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Examples
{
    /// <summary>
    /// Hands-on demo exploring the practical differences between IEnumerable<T> and IQueryable<T>.
    /// Run `Examples.IEnumerableVsIQueryableDemo.Run()` from your application to observe output.
    /// The demo shows deferred execution, expression trees, provider information and how enumeration behaves
    /// when queries are built against IEnumerable vs IQueryable. This is intentionally verbose for learning.
    /// </summary>
    public static class IEnumerableVsIQueryableDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== IEnumerable<T> vs IQueryable<T> Demo ===");
            Console.WriteLine();

            // Create a small sample dataset for clarity in output (easy to inspect).
            var seed = Enumerable.Range(1, 20)
                .Select(i => new Fruit { Id = i, Name = (i % 5 == 0 ? "Banana" : $"Fruit{i}") })
                .ToList();

            // Wrap the list in a logging enumerable so we can observe how many times the sequence is enumerated.
            var logging = new LoggingEnumerable<Fruit>(seed);

            // --- Scenario A: Method returns IEnumerable<T> (commonly what people do)
            Console.WriteLine("-- Scenario A: method returns IEnumerable<T> --");
            IEnumerable<Fruit> enumerableFromMethod = GetFruitsAsEnumerable(logging);

            // At this point no enumeration has occurred: LINQ calls are deferred until you enumerate.
            var clientFilter = enumerableFromMethod.Where(f => f.Name == "Banana");
            Console.WriteLine("Deferred LINQ on IEnumerable<T> using a delegate (Func<T,bool>). No expression tree is available.");

            // Materialize the results: enumeration happens here; LoggingEnumerable will show items being iterated.
            var bananas1 = clientFilter.ToList();
            Console.WriteLine($"Found {bananas1.Count} bananas (IEnumerable path). Enumerations: {logging.Enumerations}");
            Console.WriteLine();

            // --- Scenario B: Queryable built from the same data source
            Console.WriteLine("-- Scenario B: build IQueryable<T> from the same data --");
            var queryable = seed.AsQueryable();
            var q = queryable.Where(f => f.Name == "Banana");

            // IQueryable carries an Expression tree that providers can translate (e.g., to SQL).
            Console.WriteLine("IQueryable.Expression: ");
            Console.WriteLine(q.Expression);
            Console.WriteLine($"IQueryable.Provider: {q.Provider.GetType().FullName}");

            // Materialize the IQueryable. For in-memory data this will enumerate the sequence once.
            var bananas2 = q.ToList();
            Console.WriteLine($"Found {bananas2.Count} bananas (IQueryable path). Logging enumerable not used here so no extra enumerations.");
            Console.WriteLine();

            // --- Scenario C: What goes wrong when a DB method returns IEnumerable<T>
            Console.WriteLine("-- Scenario C: simulate a database method that returns IEnumerable<T> --");
            // Simulate a 'database' provider by exposing an IQueryable but returning it as IEnumerable.
            IEnumerable<Fruit> dbLikeEnumerable = seed.AsQueryable().Where(f => f.Id > 0).AsEnumerable();

            // From the caller perspective it's IEnumerable: subsequent filters run client-side.
            // This demonstrates why returning IEnumerable from data access can cause unnecessary loads.
            // We'll wrap the sequence with logging to show enumeration behavior.
            var loggedDbEnumerable = new LoggingEnumerable<Fruit>(dbLikeEnumerable);
            var clientSideFiltered = loggedDbEnumerable.Where(f => f.Name == "Banana");
            var resultClientSide = clientSideFiltered.ToList();
            Console.WriteLine($"Simulated DB returned as IEnumerable -> client-side filter found {resultClientSide.Count}. Enumerations: {loggedDbEnumerable.Enumerations}");
            Console.WriteLine();

            // --- Inspect Expression trees vs delegates
            Console.WriteLine("-- Expression trees vs delegates --");
            // When using IQueryable, Where receives an Expression<Func<T,bool>> which can be examined or translated.
            Expression<Func<Fruit, bool>> expr = f => f.Name == "Banana";
            Console.WriteLine("Expression<Func<Fruit,bool>> example: " + expr);

            // When using IEnumerable LINQ extension methods, the predicate is a Func<T,bool> (compiled delegate)
            Func<Fruit, bool> del = f => f.Name == "Banana";
            Console.WriteLine("Func<Fruit,bool> delegate example: compiled delegate cannot be translated.");
            Console.WriteLine();

            // --- Practical guidance printed for learners
            Console.WriteLine("Practical guidance:");
            Console.WriteLine("- Use IEnumerable<T> for local, in-memory collections and simple client-side processing.");
            Console.WriteLine("- Use IQueryable<T> for remote/back-end providers (Entity Framework, remote query engines) so expressions can be translated to SQL.");
            Console.WriteLine("- Avoid returning IEnumerable<T> from repository methods if callers should apply server-side filters.");
            Console.WriteLine();

            Console.WriteLine("Demo complete.");
        }

        // Simulate a method that returns an iterator-based IEnumerable<T> (common pattern using yield)
        private static IEnumerable<Fruit> GetFruitsAsEnumerable(IEnumerable<Fruit> source)
        {
            foreach (var s in source)
            {
                // The act of yielding will enumerate the underlying source when the caller iterates.
                yield return s;
            }
        }

        private sealed class Fruit
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public override string ToString() => $"{Id}:{Name}";
        }

        /// <summary>
        /// Minimal wrapper that logs each enumeration and counts how many times GetEnumerator was called.
        /// Use this to observe deferred execution and how often sequences are iterated.
        /// </summary>
        private sealed class LoggingEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _source;
            public int Enumerations { get; private set; }

            public LoggingEnumerable(IEnumerable<T> source)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public IEnumerator<T> GetEnumerator()
            {
                Enumerations++;
                foreach (var item in _source)
                {
                    // Show the item being returned so learners can see what is iterated.
                    Console.WriteLine($"Enumerating item: {item}");
                    yield return item;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
