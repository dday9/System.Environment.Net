Namespace System
    
    Public Class Environment

        ''' <summary>
        ''' Parses a file with zero or more environment variables
        ''' </summary>
        ''' <param name="path">The location of the .env file</param>
        ''' <returns><see cref="Dictionary(Of TKey, TValue)"/></returns>
        ''' <remarks>The method uses <see cref="IO.File.ReadAllLines"/> to read the file, therefore the file extension does necessarily need to be .env.</remarks>
        Public Shared Function Load(path As String) As Dictionary(Of String, String)
            If (String.IsNullOrWhiteSpace(path)) Then
                Throw New ArgumentNullException("path")
            End If
            If (Not IO.File.Exists(path)) Then
                Throw New ArgumentException("The file does not exist.")
            End If

            Dim lines = IO.File.ReadAllLines(path)
            
            Return Parse(lines)
        End Function

        ''' <summary>
        ''' Parses zero or more environment variables
        ''' </summary>
        ''' <param name="variables">A collection of KeyValuePairs in the format: Key=Value</param>
        ''' <returns><see cref="Dictionary(Of TKey, TValue)"/></returns>
        Public Shared Function Parse(variables As IEnumerable(Of String)) As Dictionary(Of String, String)
            Dim parsedVariables = New Dictionary(Of String, String)

            For Each variable In variables
                Dim parsedVariable = Parse(variable)
                If (parsedVariables.ContainsKey(parsedVariable.Key)) Then
                    Throw New Exception($"Multiple variables withe the key: {parsedVariable.Key}")
                End If

                parsedVariables.Add(parsedVariable.Key, parsedVariable.Value)
            Next

            Return parsedVariables
        End Function

        ''' <summary>
        ''' Parses an individual environment variable
        ''' </summary>
        ''' <param name="variable">KeyValuePair in the format: Key=Value</param>
        ''' <returns><see cref="KeyValuePair(Of TKey, TValue)"/></returns>
        Public Shared Function Parse(variable As String) As KeyValuePair(Of String, String)
            If (String.IsNullOrWhiteSpace(variable)) Then
                Throw New ArgumentNullException(NameOf(variable))
            End If

            Dim separator = variable.IndexOf("=")
            If (separator < 0 OrElse separator = variable.Length - 1) Then
                Throw New Exception($"The variable is not in a valid format: {variable}")
            End If

            Dim key = variable.Substring(0, separator)
            Dim value = variable.Substring(separator + 1)
            
            Return New KeyValuePair(Of String, String)(key, value)
        End Function

    End Class

End Namespace
