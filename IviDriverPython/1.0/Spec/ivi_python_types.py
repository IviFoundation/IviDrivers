"""
===============================================================================
 IVI-Python Driver Types Module
===============================================================================

 This module defines types that are necessary for a driver to comply
 with the IVI-Python specification. This includes abstract base classes
 (ABCs) for the IVI-Python DriverUtility class and the IviDirectIo class
 as well as a data class for ErrorQueryResult. These classes include the
 methods and behaviors that are required by the IVI-Python
 specification.

 This version of the IVI Types is based on the IVI Foundation's 1.0
 release of IVI-Python. For details see the `IVI-Python Specification
 <https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverPython/1.0/Spec>`_.

 Usage:

    - Inherit from these abstract classes to ensure IVI conformant APIs.
    - Implement all abstract methods in subclasses.
    - Refer to project documentation for design guidelines and examples.

 Author: IVI Foundation

 Version: 1.0
===============================================================================
"""


from typing import Collection, Any
from abc import ABC, abstractmethod


class ErrorQueryResult:
  """
  A class representing the result of an error query from an instrument.

  Instances of ErrorQueryResult are immutable.
  """

  def __init__(self, code: int, message: str) -> None:
    self._code = code
    self._message = message
    
  @property
  def code(self) -> int:
    """Error code returned by the instrument."""
    return self._code
    
  @property
  def message(self) -> str:
    """Message provided by the instrument with the error code."""
    return self._message


class IviUtility(ABC):
  """
  Abstract base class for the required IVI-Python operations. 
  
  IviUtility contains useful methods relevant for all IVI-Python drivers
  including methods to obtain identity information for the driver and
  instrument, error handling, and instrument reset.

  Implementations of this base class may provide additional related
  methods in their concrete IviUtility subclasses.
  
  This class is instantiated by the driver.
  """

  @property
  @abstractmethod
  def driver_version(self) -> str:
    """
    Driver version string.
    
    This is the driver version string optionally followed by a space and
    a descriptive string.
    """
    pass

  @property
  @abstractmethod
  def driver_vendor(self) -> str:
    """
    The name of the vendor that supplied this driver.
    """
    pass

  @property
  @abstractmethod
  def instrument_manufacturer(self) -> str:
    """
    Manufacturer of the instrument being controlled. 
    
    The driver returns the value it queried from the instrument or a
    string indicating that it cannot query the instrument identity.
    """
    pass

  @property
  @abstractmethod
  def instrument_model(self) -> str:
    """
    Model number or name of the currently connected instrument.
    
    The driver returns the value it queried from the instrument or a
    string indicating that it cannot query the instrument identity.
    """
    pass

  # Although the query_instrument_status_enabled property is always
  # present, the underlying function of querying the instrument status
  # only needs to be implemented if it is meaningful for the instrument
  # being controlled.
  @property
  @abstractmethod
  def query_instrument_status_enabled(self) -> bool:
    """
    Indicates if the driver queries the instrument status after each instrument operation.
    
    If the instrument can be queried for its status and Query Instrument
    Status Enabled is True, then the driver normally checks the
    instrument status at the end of every method or property that
    accesses the instrument and reports an error if the instrument has
    detected an error. If False, the driver does not automatically query
    the instrument status at the end of each user operation.

    If the instrument status cannot be meaningfully queried after an
    operation, then this property has no effect on driver operation.

    This property is false when the driver is instantiated.
    """  # noqa: W505 - doc line too long
    pass

  @query_instrument_status_enabled.setter
  @abstractmethod
  def query_instrument_status_enabled(self, value: bool) -> None:
    pass

  @property
  @abstractmethod
  def simulation_enabled(self) -> bool:
    """
    Indicates whether the driver is in simulation mode. 
    
    When in simulation mode, the driver performs no I/O to the
    instrument.

    This property is normally only set at driver instantiation and is
    read-only. However, drivers may allow changing this property at
    runtime.
    """
    pass
  
  @abstractmethod
  def error_query(self) -> ErrorQueryResult | None:
    """
    Query the most recent error from the instrument.

    For instruments that implement an error queue, Error Query extracts
    a single error from the queue and returns it.  If the queue is empty
    None is returned.

    For instruments that indicate errors with status registers instead
    of an error queue, an error consistent with the instrument design is
    returned.

    The operation of Error Query is independent of the operation of Query
    Instrument Status Enabled.

    Returns:
      ErrorQueryResult: The most recent error from the instrument error
        queue.
    """
    pass

  @abstractmethod
  def error_query_all(self) -> Collection[ErrorQueryResult]:
    """
    Query all of the errors from the instrument's error queue and clear the instrument error queue. 
    
    If the error queue is empty, an empty collection is returned.

    Returns:
      Collection[ErrorQueryResult]: All of the errors from the 
        instrument error queue.
    """  # noqa: W505 - doc line too long
    pass

  @abstractmethod
  def raise_on_device_error(self) -> None:
    """
    Call error_query_all() and raise an exception if any instrument errors are detected.
    """  # noqa: W505 - doc line too long
    pass

  @abstractmethod
  def reset(self) -> None:
    """
    Reset the instrument to its default state and configure it as needed for normal operation with the driver.  
    
    The driver state is also reset to reflect the instrument's
    configuration.
    """  # noqa: W505 - doc line too long
    pass

  @property
  @abstractmethod
  def supported_instrument_models(self) -> tuple[str, ...]:
    """
    A tuple of supported instrument models. 
    
    The strings represent the instrument models as they would be
    reported by a connected instrument using the instrument_model
    property.
    """
    pass
  

# IVI-Python drivers are required to implement the IviDirectIo class if
# the controlled instruments support an ASCII command set such as SCPI.
class IviDirectIo(ABC):
  """
  Abstract base class for direct I/O operations. These operations permit
  directly communicating with the instrument by sending and receiving
  raw data such as SCPI commands.
  """

  # The session property is not required by the IVI-Python specification
  # and may be implemented in subclasses to return the underlying
  # resource/session object.
  @property
  @abstractmethod
  def session(self) -> Any:
    """
    Returns the active instance of the underlying class that performs IO to the instrument. 
  
    session is typically a PyVISA Resource object.
    """  # noqa: W505 - doc line too long
    raise NotImplementedError("Optional session property not implemented.")
    
  @property
  @abstractmethod
  def io_timeout_ms(self) -> int:
    """
    The I/O timeout value in milliseconds. 
    
    This is the minimum time the driver waits for an IO operation with
    the instrument to be completed.
    """
    pass
 
  @io_timeout_ms.setter
  @abstractmethod
  def io_timeout_ms(self, timeout_ms: int) -> None:
    pass

  @abstractmethod
  def read_bytes(self) -> bytes:
    """
    Read a complete response from the instrument as bytes.
    
    The response message terminator is not included in the bytes object.

    Returns:
      bytes: The response from the instrument.
    """  # noqa: W505 - doc line too long
    pass
 
  @abstractmethod
  def read_string(self) -> str:
    """
    Read a complete response from the instrument as a string.
    
    The response message terminator is not included in the string.

    Returns:
      str: The response from the instrument.
    """
    pass
 
  @abstractmethod
  def write_bytes(self, data: bytes) -> None:
    """
    Write bytes to the instrument followed by the normal message termination sequence.
    
    For IEEE 488.2 instruments the termination sequence is typically a
    line feed character with END asserted.

    The bytes must include a complete instrument message. The driver
    adds the termination sequence.
    
    Args:
      data (bytes): Data to write to the instrument. A program message
        terminator will be appended by the driver.
    """  # noqa: W505 - doc line too long
    pass
 
  @abstractmethod
  def write_string(self, data: str) -> None:
    """
    Write a string to the instrument followed by the normal message termination sequence.
    
    For IEEE 488.2 instruments the termination sequence is typically a
    line feed character with END asserted.

    The string must include a complete instrument message. The
    driver adds the termination sequence.

    Args:
      data (str): Data to write to the instrument. A program message
        terminator will be appended by the driver.
    """  # noqa: W505 - doc line too long
    pass
