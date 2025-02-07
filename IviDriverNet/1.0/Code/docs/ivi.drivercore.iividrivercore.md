# IIviDriverCore

Namespace: Ivi.DriverCore

Interface for IVI Driver Core. This interface defines the essential properties and methods that IVI drivers must implement.

```csharp
public interface IIviDriverCore
```

Attributes [NullableContextAttribute](./system.runtime.compilerservices.nullablecontextattribute.md)

## Properties

### **DriverVersion**

Gets the version of the driver.

```csharp
public abstract string DriverVersion { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

The version string should follow the format "MajorVersion.MinorVersion.PatchVersion".

### **DriverVendor**

Gets the name of the driver vendor.

```csharp
public abstract string DriverVendor { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

Example: "Keysight Technologies".

### **DriverSetup**

Provides setup information for the driver.

```csharp
public abstract string DriverSetup { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

This could be configuration or initialization parameters passed when initializing the driver.
 Driver setup is empty if the driver is not initialized.

### **InstrumentManufacturer**

Gets the name of the instrument's manufacturer.

```csharp
public abstract string InstrumentManufacturer { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

Example: "Keysight Technologies".

### **InstrumentModel**

Gets the model number or name of the instrument.

```csharp
public abstract string InstrumentModel { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

Example: "34410A".

### **QueryInstrumentStatus**

Gets or sets a value indicating whether the instrument's status should be queried after each operation.

```csharp
public abstract bool QueryInstrumentStatus { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

**Remarks:**

When set to true, the driver queries the instrument's status after every method call to check for errors.

### **Simulate**

Gets a value indicating whether the driver is operating in simulation mode.

```csharp
public abstract bool Simulate { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

**Remarks:**

In simulation mode, the driver does not perform actual I/O with the instrument and generates simulated output.

## Methods

### **ErrorQuery()**

Queries the instrument for any error information.

```csharp
ErrorQueryResult ErrorQuery()
```

#### Returns

[ErrorQueryResult](./ivi.drivercore.errorqueryresult.md)<br>
Returns an error result that provides details of the instrument's error state.

**Remarks:**

The error query result should reflect the status or errors from the instrument based on its error queue or registers.

### **Reset()**

Resets the instrument to a known state.

```csharp
void Reset()
```

**Remarks:**

Typically, this sends a reset command (e.g., "*RST") to the instrument to ensure it is in a known state.

### **GetSupportInstrumentModels()**

Retrieves the list of supported instrument models compatible with this driver.

```csharp
String[] GetSupportInstrumentModels()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
An array of strings, each representing a model of the instrument that this driver can control.

**Remarks:**

The returned models should be consistent with the instrument models supported by the driver.
