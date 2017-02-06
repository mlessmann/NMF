# Migration to .NET Standard / .NET Core

This is the progress report for the migration effort of NMF towards .NET Standard.

## Aims

We aim to make all libraries target .NET Standard 1.4 and all executables and test assemblies target .NET Core 1.0. The version numbers are subject to change, as retaining features takes priority over targeting old platforms.

As the MSTest framework in not available for .NET Core, we use the MSTest v2 unit test framework, which is currently in beta.

## Status

Last updated 06. Feb. 2017

You need Visual Studio 2017 RC to compile the code. Alternatively, the dotnet CLI works too. Important: the solution file contains all projects, including the ones that do not yet compile (see below). Make sure to disable them first.

### What works

The following projects (and their respective test projects) compile and pass all unit tests:

* Analysis
* Benchmarks
* Collections
* Connectivity
* Expressions
* Expressions.Linq
* Expressions.Utilities
* Layering
* Optimizations
* Serialization
* Tests
* Transformations
* Transformations.Core
* Transformations.Parallel
* Transformations.Sample
* Transformations.Trace
* Utilities

### What doesn't compile

* CodeGen & CodeGen.Test
* Ecore2Code
* Incerator
* Models.MetaTransformations
* Models.Transformations

Increator is missing the GAF nuget package, which is only available for .NET Framework, not for .NET Standard.

CodeGen is missing the CodeDom API, which will be included in .NET Standard 2.0 (as seen by milestone tag [here](https://github.com/dotnet/corefx/issues?utf8=%E2%9C%93&q=label%3Aarea-System.CodeDom%20)). The other projects depend on CodeDom and therefore don't compile either.

### What fails their tests

* EcoreInterop
* Expressions.Configuration
* Expressions.Models
* Models
* Synchronizations

Currently, 98 unit tests are failing in total. All of them are due to missing automatic metamodel discovery in the `MetaRepository` and `ModelRepository` classes. This functionality is missing because the AppDomain concept was dropped in .NET Core.

### Minor missing features and temporary workarounds

* The XML and XMI Serializers no longer call the methods of `ISupportInitialize` during deserialization. The interface was removed in .NET Core.
* During unit test execution the working directoy is set wrongly. Instead of the directory of the testassemly, it defaults to sthe Visual Studio installation path. [This is a known bug](https://github.com/Microsoft/vstest/issues/311) which has already been fixed, but not deployed. To fix this issue temporarily, there are 2 TODOs in the ModelRepository class, which set the working directory manually.
* The Benchmarks library targets .NET Standard 1.5 instead of 1.4, because it depends on the CommandLineParser nuget package, which targets 1.5.

## TODO

* Find a solution for the missing metamodel discovery. We need two functions: get a list of all currently loaded assemblies (a replacement for `AppDomain.CurrentDomain.GetAssemblies()`) and some mechanism that lets us know when a new assemly has been loaded (a replacement for the `AppDomain.CurrentDomain.AssemblyLoad` event). Possible approaches are the nuget packages containing `AssemblyLoadContext` or `DependencyModel`.
* Wait for CodeDom to be released for .NET Standard (probably in 2.0), then port the depending libraries to it.
* Find a replacement for the Generic Algorithm Framework used by Incerator.
* Update the nuget package generation.
* Update the CI.
