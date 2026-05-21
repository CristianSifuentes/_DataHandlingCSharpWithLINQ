# _DataHandlingCSharpWithLINQ
Manipulate data in C# collections using LINQ. Learn operators for filtering, grouping, and averaging. Practice by creating a book project, applying advanced techniques, and improving code performance.

## Table of Contents
- [Introduction](#introduction)
- [Video Summary](#video-summary)
- [What is LINQ?](#what-is-linq)
- [LINQ Providers](#linq-providers)
- [LINQ Syntax](#linq-syntax)
  - [Query Expression](#query-expression)
  - [Extension Methods](#extension-methods)
- [Step 1: Getting Started](#step-1-getting-started)
- [Project Evolution](#project-evolution)
- [Resources](#resources)

## Introduction
This repository is designed to learn and demonstrate the use of LINQ in .NET. LINQ is a language that integrates into C# and enables collection manipulation with a fluent, expressive query model. It supports two main implementation styles: query expressions and extension methods, and it is available through the `System.Linq` namespace.

LINQ enables querying data from different sources in a language-integrated way, leveraging C# power to transform, filter, and order information.

## Video Summary
LINQ is a set of technologies in .NET derived from the term "Language Integrated Query" and it is used to query data from different sources.

Common data sources:
- Object collections
- Relational databases
- DataSet and DataTable
- XML documents

LINQ has different providers for each data source. It is possible to create a custom provider by implementing the `IQueryProvider` and `IQueryable` interfaces.

## What is LINQ?
LINQ is a .NET feature used to work with queries over collections and data sources. It is not a separate programming language; rather, it is a metalanguage built on top of existing .NET languages such as C#. This means LINQ is understood by the compiler and works with the language you are already using.

- It is not a programming language.
- It is not an SQL component.
- It is not a database component.
- It is not a third-party library.
- It is a metalanguage that is compatible with many .NET languages.

## LINQ Providers
LINQ works with providers that adapt queries to each data source. Some of the most common providers are:
- LINQ to Objects
- LINQ to SQL
- Entity Framework / LINQ to Entities
- LINQ to XML
- LINQ to DataSet

## LINQ Syntax
LINQ provides two main syntax styles:

### Query Expression
Query expressions use a SQL-like syntax inside C#.
```csharp
var result = from l in list
             where l > 10
             select l;
```

### Extension Methods
Extension methods appear on collections and provide filtering and transformation functions. These methods come from the `System.Linq` namespace.
```csharp
var result = list.Where(x => x > 10);
```

## Step 1: Getting Started
1. Review the basic LINQ concepts and its providers.
2. Create simple examples with C# collections using `Where`, `Select`, `OrderBy`, and `GroupBy`.
3. Add a console app or tests to validate the results.

> Recommended first step: implement a list of objects in C# and perform basic queries with LINQ to Objects.

## Project Evolution
This README is prepared for evolutionary changes. Here are some items that can be added next:
- Documentation for each LINQ operator.
- Practical examples with collections, XML, and DataSet.
- Guide for creating a custom provider with `IQueryProvider` and `IQueryable`.
- Comparisons between query expression syntax and extension methods.
- Best practices and query optimization section.

## Resources
- https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
- https://linqexamples.com/intro/
- https://dotnettutorials.net/lesson/introduction-to-linq/
- https://refactoring.guru/es/design-patterns/csharp
