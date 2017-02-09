# Migration to .NET Standard / .NET Core

This is the progress report for the migration effort of NMF towards .NET Standard.

## Aims

We aim to make all libraries target .NET Standard 1.4 and all executables and test assemblies target .NET Core 1.0. The version numbers are subject to change, as retaining features takes priority over targeting old platforms.

As the MSTest framework in not available for .NET Core, we use the MSTest v2 unit test framework, which is currently in beta.

## Status

Last updated 09. Feb. 2017

You need Visual Studio 2017 RC to compile the code. Alternatively, the dotnet CLI works too. Important: the solution file contains all projects, including the ones that do not yet compile (see below). Make sure to disable them first.

### What works

The following projects (and their respective test projects) compile and pass all unit tests:

* Analysis
* Benchmarks
* Collections
* Connectivity
* EcoreInterop
* Expressions
* Expressions.Configuration
* Expressions.Models
* Expressions.Linq
* Expressions.Utilities
* Layering
* Models
* Optimizations
* Serialization
* Synchronizations
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

CodeGen is missing the CodeDom API, which will be included in .NET Standard 2.0 (as seen by milestone tag [here](https://github.com/dotnet/corefx/issues?utf8=%E2%9C%93&q=label%3Aarea-System.CodeDom%20)). The other projects depend on CodeGen and therefore don't compile either.

### Feature differences

* The metamodel discovery in `MetaRepository` has been completely revamped. Since the AppDomain concept was discontinued in .NET Core, we can no longer react dynamically whenever a new assembly is loaded. Instead, we take advantage of the new `*.deps.json` file using the `Microsoft.Extensions.DependencyModel` package. The file contains the dependencies between all assemblies of the program at compile time. We simply walk the dependency graph and check all assemblies for metamodels. Therefore we only have to trigger the metamodel discovery once. Since dynamically loaded assemblies (i.e. via LoadBytes()) are not supported by this method, the user can now trigger metamodel discovery on a specific assembly manually.
* The XML and XMI Serializers no longer call the methods of `ISupportInitialize` during deserialization. The interface was removed in .NET Core.
* During unit test execution, the working directoy is set wrongly. Instead of the directory of the test assembly, it defaults to the Visual Studio installation path. [This is a known bug in MSTest v2](https://github.com/Microsoft/vstest/issues/311) which has already been fixed, but not deployed. To fix this issue temporarily, there are 2 TODOs in the ModelRepository class, which set the working directory manually.
* The Benchmarks library targets .NET Standard 1.5 instead of 1.4, because it depends on the CommandLineParser nuget package, which targets 1.5.

## TODO

* Wait for CodeDom to be released for .NET Standard (probably in 2.0), then port the depending libraries to it.
* Find a replacement for the Genetic Algorithm Framework used by Incerator.
* Update the nuget package generation.
* Update the CI.
