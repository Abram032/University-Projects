## Usage

```
Usage:
dotnet OM.Console.dll [Path]
```

If no path is given, `[Path]` will default to ./graphs. Program will load all files within the given directory. Any file placed under ./resources/graphs in the root folder, will be copied on build to ./graphs directory in output directory.

Data format:
```
#DIGRAPH
[Bool (info if graph is directed or not)]
#EDGES
[Integer (Starting vertex name) Integer (End vertex name) Integer (Edge weight)]

Example:
#DIGRAPH
false
#EDGES
1 2 1
1 3 2
1 4 1
2 3 1
```