"""
===============================================================================
 IVI-Python Driver Abstract Base Classes Module
===============================================================================

 This module defines abstract base classes (ABCs) intended to serve as 
  interfaces for developers. These classes outline the required 
 methods and behaviors that IVI-Python drivers must provide.

 This version of the abstract base classe is aligned with IVI Foundation's 1.0 release of IVI-Python.  For details see: https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverPython/1.0/Spec

 Usage:
    - Inherit from these abstract classes to ensure consistent APIs.
    - Implement all abstract methods in subclasses.
    - Refer to project documentation for design guidelines and examples.

 Author: IVI Foundation
 Created: 2026-01-20
 Version: 1.0
===============================================================================
"""

"""Things to discuss:
- should we put doc strings in from the Core classes to explain semantics?
- pull in doc strings regarding parameters and return values from the IVI specs?
"""

from typing import Any, Generic, Iterable, Optional, TypeVar, List, Tuple
from abc import ABC, abstractmethod


class ErrorQueryResult:
      
  def __init__(self, code: int, message: str) -> None:
    self._code = code
    self._message = message
    
  @property
  def code(self) -> int:
    return self._code
    
  @property
  def message(self) -> str:
    return self._message


class IviUtility(ABC):
  
  @property
  @abstractmethod
  def driver_version(self) -> str:
    """Provides a driver version string. This is a version optionally followed by a descriptive string."""

    """Provides a driver version string. This is a version optionally followed by a descriptive string.

    The format of the version shall follow the rules for FileVersion defined in File Versioning followed by an optional string. If the string is present, a space shall separate the version from the string. The string contains additional driver specific version information. Multi-byte characters are not allowed in the string that this property returns. String characters shall be in the range of \x20 - \x7E."""
    pass

  @property
  @abstractmethod
  def driver_vendor(self) -> str:
    """Returns the name of the vendor that supplies the IVI Core driver."""
    pass

  @property
  @abstractmethod
  def instrument_manufacturer(self) -> str:
    """Returns the name of the manufacturer of the instrument. The IVI driver returns the value it queries from the instrument or a string indicating that it cannot query the instrument identity. For instance, "Cannot query from instrument"."""
    pass

  @property
  @abstractmethod
  def instrument_model(self) -> str:
    """Returns the model number or name of the instrument. The IVI driver returns the value it queries from the instrument or a string indicating that it cannot query the instrument identity. For instance, "Cannot query from instrument"."""
    pass

  @property
  @abstractmethod
  def instrument_serial_number(self) -> str:
    pass

  @property
  @abstractmethod
  def instrument_firmware(self) -> str:
    pass

  # The query_instrument_status_enabled property although always preset, need only be implemented if it is meaningful for the instrument being controlled. It controls whether the driver queries the instrument status after each operation.
  @property
  @abstractmethod
  def query_instrument_status_enabled(self) -> bool:
    """IVI drivers shall implement Query Instrument Status Enabled if possible. At initialization Query Instrument Status Enabled shall be false, unless initialization options have been used to enable it.

    If the instrument can be queried for its status and Query Instrument Status Enabled is True, then the driver checks the instrument status at the end of every call by the user to a method that accesses the instrument and reports an error if the instrument has detected an error. If False, the driver does not query the instrument status at the end of each user operation.

    If the instrument status cannot be meaningfully queried after an operation, then this property has no effect on driver operation."""
    pass

  @property
  @abstractmethod
  def simulation_enabled(self) -> bool:
    pass
  
  @abstractmethod
  def error_query(self) -> ErrorQueryResult | None:
    """Returns the last error in the instrument's error queue.
    Returns None if no error is present."""
    """The Error Query member queries the instrument and returns instrument specific error information.

    For instruments that implement an error queue, Error Query extracts a single error from the queue.

    For instruments that have status registers but no error queue, the IVI driver returns an error consistent with instrument design.

    The operation of Error Query is not affected by the value of Query Instrument Status Enabled."""
    pass

  @abstractmethod
  def error_query_all(self) -> Collection[ErrorQueryResult]:
    """Returns all the errors currently reported in the instrument's error queue.
      If no error is present, the method returns an empty collection."""
    return []

  @abstractmethod
  def raise_on_device_error(self) -> None:
    """Calls error_query_all() and raises an exception if any instrument errors were detected."""
    pass

  @abstractmethod
  def reset(self) -> None:
    pass

  @property
  @abstractmethod
  def supported_instrument_models(self) -> Tuple[str, ...]:
    """Returns supported models, one per element."""
    pass
  

class IviDirectIo(ABC):
    """An abstract base class for IVI Direct I/O operations."""

    # The session property is optional and may be implemented in subclasses to 
    # return the underlying resource/session object.
    # @property
    # def session(self) -> Resource:
    #   return self._io
 
    @property
    @abstractmethod
    def io_timeout_ms(self) -> int:
        pass
 
    @io_timeout_ms.setter
    @abstractmethod
    def io_timeout_ms(self, timeout_ms: int) -> None:
        pass

    @abstractmethod
    def read_bytes(self, count: int) -> bytes:
        pass
 
    @abstractmethod
    def read_string(self) -> str:
        pass
 
    @abstractmethod
    def write_bytes(self, data: bytes) -> None:
      pass
 
    @abstractmethod
    def write_string(self, data: str) -> None:
      pass

