# ErrorQueryResult

Namespace: Ivi.DriverCore

Result of an error query operation.

```csharp
public struct ErrorQueryResult
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [ErrorQueryResult](./ivi.drivercore.errorqueryresult.md)<br>
Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md), [NullableAttribute](./system.runtime.compilerservices.nullableattribute.md)

## Properties

### **Code**

Instrument error code.

```csharp
public int Code { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Message**

Instrument error message.

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ErrorQueryResult(Int32, String)**

Constructor.

```csharp
ErrorQueryResult(int code, string message)
```

#### Parameters

`code` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Instrument error code.

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Instrument error message.

## Methods

### **Equals(Object)**

Compares two  instances for equality.

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The  instance to compare with the current instance.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the two instances represent the same result; otherwise, false.

### **GetHashCode()**

Returns the hash code for the result.

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32) containing the hash value generated for this result.
