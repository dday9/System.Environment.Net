# System.Environment.Net
A Visual Basic .NET to load and parse environment variables

To use the file simply add it to your project and call the `System.Environment.Load` or `System.Environment.Parse` method.

## Syntax
### Load
`Public Function Load(path As String) As Dictionary(Of String, String)`

**Parameters**
- *path*
  - Type: System.String
  - The location of the .env file

- Return Value
  - Type: System.Collections.Generic.Dictionary(Of String, String)
  - A Dictionary populated from the file that contains environment variables.

**Remarks**
The method uses IO.File.ReadAllLines to read the file, therefore the file extension does necessarily need to be .env.

**Exceptions**
- System.ArgumentNullException
  - Thrown when the *path* argument is null or whitespace
- System.ArgumentException
  - Thrown when the *path* does not reference a file that exists

### Parse
**Overloads**
- Parse(variables As IEnumerable(Of String))
- Parse(variable As String)

#### Parse(variables As IEnumerable(Of String))
`Public Shared Function Parse(variables As IEnumerable(Of String)) As Dictionary(Of String, String)`

**Parameters**
- *variables*
  - Type: System.Collections.Generic.IEnumerable(Of String)
  - A collection of KeyValuePairs in the format: Key=Value

- Return Value
  - Type: System.Collections.Generic.Dictionary(Of String, String)
  - A Dictionary populated from the *variables* that contains environment variables.

**Exceptions**
- System.Exception
  - Thrown when there are multiple environment variables with the same key

#### Parse(variable As String)
`Public Shared Function Parse(variable As String) As KeyValuePair(Of String, String)`

**Parameters**
- *variable*
  - Type: System.String
  - KeyValuePair in the format: Key=Value

- Return Value
  - Type: System.Collections.Generic.KeyValuePair(Of String, String)
  - A KeyValuePair populated from the *variable* that contain the environment variable.

**Exceptions**
- System.ArgumentNullException
  - Thrown when the *variable* argument is null or whitespace
- System.Exception
  - Thrown when the *variable* is not in a valid format

## Example
``` vb.net
Imports System
Imports System.Collections.Generic
Public Module Module1
    Public Sub Main()
        Dim singleVariable = System.Environment.Parse("Key1=Value1")
        Console.WriteLine("Single Variable")
        Console.WriteLine("{0}, {1}", singleVariable.Key, singleVariable.Value)
        Console.WriteLine()

        Dim multipleValues = System.Environment.Parse({"Key2=Value2", "Key3=Value3", "Key4=Value4"})
        Console.WriteLine("Multiple Variables")
        For Each kvp In multipleValues
            Console.WriteLine("{0}, {1}", kvp.Key, kvp.Value)
        Next
        Console.WriteLine()

        IO.File.WriteAllLines("test.env", {"Key5=Value5", "Key6=Value6", "Key7=Value7"})
        Dim byFile = System.Environment.Load("test.env")
        Console.WriteLine("Loaded by File")
        For Each kvp In byFile
            Console.WriteLine("{0}, {1}", kvp.Key, kvp.Value)
        Next
        Console.WriteLine()
    End Sub
End Module
```
Fiddle: https://dotnetfiddle.net/vRrxW7
