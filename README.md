[![download](https://img.shields.io/badge/Download-Installer-blue.svg)](https://aosoftware.blob.core.windows.net/install/ZingerSetup.exe)

This is a C# WinForms app that helps you generate C# POCO classes to encapsulate results of SQL queries as well as to test dynamic query parameters and preview results. For example, given a query like this:

```sql
SELECT [Whatever], [DjangoValue], [MatriculationVector], [OscilliscopeRiskDate]
FROM [dbo].[Ablative]
WHERE blah blah blah
```
Zinger creates a C# class patterned after results like this:

```csharp
public class MyQueryResult
{
    public string Whatever { get; set; }
    public int DjangoValue { get; set; }
    public string MatriculationVector { get; set; }
    public DateTime OscilliscopeRiskDate { get; set; }
}
```
Zinger infers the property types from the underlying table column types in the backend database. SQL Server, MySQL, and OleDb data sources are supported.

In addition to generating POCO result classes, you can also use Zinger to debug and create [Dapper.QX Query](https://github.com/adamfoneil/Dapper.QX) instances. This is how I handle inline SQL in my applications -- allowing me to balance the productivity and convenience of inline SQL with its inherent liabilities.

("Zinger" was kind of a code name I was using for this during development. It's not one of my better code names, though, and it never stuck. Some of the source assets use this name, and it's too much trouble to rename it. "Postulate" refers to an [old ORM project](https://github.com/adamfoneil/Postulate.Lite) of mine. That's no longer relevant to Zinger, but renaming things is a little awkward like I said, so I just left it.)

## Downloads
The latest release is now available [here](https://aosoftware.blob.core.windows.net/install/ZingerSetup.exe).

There's also a NuGet package for the C# generation features:

[![Nuget](https://img.shields.io/nuget/v/AO.Zinger.CSharp)](https://www.nuget.org/packages/AO.Zinger.CSharp/)

built from the [Zinger.CSharp project](https://github.com/adamfoneil/Postulate.Zinger/tree/master/Zinger.CSharp). This powers a [SqlChartify](https://sqlchartify.azurewebsites.net/) feature.

I'm not going to maintain GitHub releases anymore unless sporadically, so please use the installer download link above.

## How to use
The desktop icon looks like this:

![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/icon.png)

 When you run it for first time, you'll need to create one or more connection strings. (Zinger encrypts any connection strings you create.) Click File > Connections:
 
![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/connections-menu.png)

I have a few connections that I toggle among:

![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/connections-dialog.png)

Next, use the Query Editor form to select a connection you created and enter a valid SQL query. If you're going to use a dynamic where clause use **{where}** to indicate where the generated WHERE clause will be inserted. In this example, I have two parameters with related expressions and test values entered. To omit a parameter from the query, clear its Value.

![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/query-example.png)

Run the query to see that it give expected results. In my example, I got two records returned in 545ms. (The F5 key runs the query.)

![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/query-executed.png)

Next, click the **C#** tab to see the C# outputs. Here you set the **Query Name** on which the result class and query class names are based. In my example I just have "WhateverQuery" -- the name should be a valid C# identifier. The result class has properties that map to every column in the query result.

![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/result-class.png)

Click the **Query Class** tab to see the class generated that's compatible with the Dapper.QX Query&lt;T&gt; type. For more info on this, see the [Dapper.QX project](https://github.com/adamfoneil/Dapper.QX).
  
![img](https://github.com/adamosoftware/Postulate.Zinger/blob/master/query-class.png)


