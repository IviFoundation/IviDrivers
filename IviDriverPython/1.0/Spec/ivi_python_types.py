# NOTES (JM 2026-01-(22-27)):
# - wrapped to 72
# - verified with Sphinx, very minor tweaks
# - composed doc strings
# - updated filename 
# - general rewrites for readability and clarity
# 
# SPEC ITEMS IN IMPLEMENTATION VS 1.0 DRAFT
# - instrument_serial_number, firmware version is not in the Core.  OK,
#   but why added?  Seems they need to be documented in spec if we are
#   adding them.  If we keep them should we specify null string if they
#   cannot be queried?
# - read_bytes does not need a length (that was in C for buffer sizing)
# - added a setter for io_timeout_ms

"""
===============================================================================
 IVI-Python Driver Types Module
===============================================================================

 This module defines types that are necessary for a driver to comply
 with the IVI-Python specification. This includes abstract base classes
 (ABCs) for the DriverUtility class and the IviDirectIo class as well as
 a data class for error queries (ErrorQueryResult). These classes
 outline the required methods and behaviors that are required by the
 IVI-Python specification.

 This version of the IVI Types is based on the IVI Foundation's 1.0
 release of IVI-Python.  For details see the `IVI-Python Specification
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

    Attributes:
        code (int): Error code returned by the instrument.

        message (str): Message provided by the instrument with the error
        code.
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
  Abstract base class for the required IVI-Python operations. IviUtility
  contains useful methods relevant for all IVI-Python drivers including
  methods to obtain driver and instrument identity information, error
  handling, and instrument reset.

  Implementations of this base class may provide additional related
  methods in their concrete IviUtility subclasses.
  
  This class is created by the driver when it is instantiated.
  """
  @property
  @abstractmethod
  def driver_version(self) -> str:
    """
    Driver version string. This is the driver version string optionally
    followed by a space and a descriptive string.

    Returns:
        str: Driver version string.
    """
    pass

  @property
  @abstractmethod
  def driver_vendor(self) -> str:
    """
    The name of the vendor that supplied this driver.
    
    Returns:
      str: Driver vendor name.
    """
    pass

  @property
  @abstractmethod
  def instrument_manufacturer(self) -> str:
    """
    Manufacturer of the instrument being controlled. The driver returns
    the value it queries from the instrument or a string indicating that
    it cannot query the instrument identity.
    
      Returns:
        str: Instrument manufacturer name.
    """
    pass

  @property
  @abstractmethod
  def instrument_model(self) -> str:
    """
    Model number or name of the currently connected instrument. The
    driver returns the value it queries from the instrument or a string
    indicating that it cannot query the instrument identity.
    
      Returns:
        str: Instrument model name.
    """
    pass

  @property
  @abstractmethod
  def instrument_serial_number(self) -> str:
    """
    Serial number of the currently connected instrument. The driver
    returns the value it queries from the instrument or a string
    indicating that it cannot query the instrument serial number.

      Returns:
        str: Instrument serial number as a string.
    """
    pass

  @property
  @abstractmethod
  def instrument_firmware(self) -> str:
    """
    Firmware version of the currently connected instrument. The driver
    returns the value it queries from the instrument or a string
    indicating that it cannot query the instrument firmware version.

      Returns:
        str: Instrument firmware version as a string.
    """
    pass

  # The query_instrument_status_enabled property although always
  # present, need only be implemented if it is meaningful for the
  # instrument being controlled. It controls whether the driver queries
  # the instrument status after each operation.
  @property
  @abstractmethod
  def query_instrument_status_enabled(self) -> bool:
    """
    Indicates if the driver queries the instrument status after each
    instrument operation. If the instrument can be queried for its
    status and Query Instrument Status Enabled is True, then the driver
    normally checks the instrument status at the end of every method
    that accesses the instrument and reports an error if the instrument
    has detected an error. If False, the driver does not query the
    instrument status at the end of each user operation.

    If the instrument status cannot be meaningfully queried after an
    operation, then this property has no effect on driver operation.

    This property is false when the driver is instantiated.

    Returns:
        bool: Indicates if Query Instrument Status is enabled.
    """
    pass

  @query_instrument_status_enabled.setter
  @abstractmethod
  def query_instrument_status_enabled(self, value: bool) -> None:
    pass

  @property
  @abstractmethod
  def simulation_enabled(self) -> bool:
    """
    Indicates whether the driver is in simulation mode. When in
    simulation mode, the driver performs no I/O to the instrument.

    This property is normally only set at driver instantiation and is
    read-only. However, drivers may allow changing this property at
    runtime.

    Returns:
        bool: Indicates if the driver is in simulation mode.
    """
    pass
  
  @abstractmethod
  def error_query(self) -> ErrorQueryResult | None:
    """
    Returns the most recent error in the instrument's error queue.

    For instruments that implement an error queue, Error Query extracts
    a single error from the queue and returns it.  If the queue is empty
    None is returned.

    For instruments that have status registers but no error queue, the
    IVI-Python driver returns an error consistent with instrument
    design.

    The operation of Error Query is independent of the opertion of Query
    Instrument Status Enabled.

    Returns:
        ErrorQueryResult: The most recent error or None if no error is
        present.
    """
    pass

  @abstractmethod
  def error_query_all(self) -> Collection[ErrorQueryResult]:
    """
    Returns all of the errors from the instrument's error queue and
    clears the instrument error queue. If the error queue is empty, an
    empty collection is returned.
    
    Returns:
        Collection[ErrorQueryResult]: A collection of all current
        errors.
    """
    return []

  @abstractmethod
  def raise_on_device_error(self) -> None:
    """
    Calls error_query_all() and raises an exception if any instrument
    errors are detected.
    """
    pass

  @abstractmethod
  def reset(self) -> None:
    """
    Resets the instrument to its default state and configures it as
    needed for normal operation with the driver.  The driver state is
    also reset to reflect the instrument's configuration.
    """
    pass

  @property
  @abstractmethod
  def supported_instrument_models(self) -> tuple[str, ...]:
    """
    Returns supported instrument models. The strings represent the
    instrument models as they would be reported by a connected
    instrument using the instrument_model property.

      Returns:
        tuple[str, ...]: A tuple of supported instrument model strings.
    """
    pass
  

# IVI-Python drivers are required to implement the IviDirectIo class if
# the controlled instruments support an ASCII command set such as SCPI.
class IviDirectIo(ABC):
    """
    Abstract base class for direct I/O operations. These operations
    permit directly communicating with the instrument by sending and
    receiving raw data such as SCPI commands.
    """

    # The session property is optional and may be implemented in subclasses to 
    # return the underlying resource/session object.
    @property
    @abstractmethod
    def session(self) -> Any:
      """
      Returns the active instance of the underlying class that performs
      IO to the instrument. Such as a PyVISA Resource object.

        Returns:
          Any: The underlying session/resource object.
      """
      raise NotImplementedError("Optional session property not implemented.")
    
    @property
    @abstractmethod
    def io_timeout_ms(self) -> int:
      """
      The I/O timeout value in milliseconds. This is the minimum time
      the driver waits for an IO operation with the instrument to be
      completed.
      
        Returns:
          int: I/O timeout in milliseconds.
      """
      pass
 
    @io_timeout_ms.setter
    @abstractmethod
    def io_timeout_ms(self, timeout_ms: int) -> None:
      pass

    @abstractmethod
    def read_bytes(self) -> bytes:
      """
      Reads a complete response from the instrument into an array of
      bytes. The response message terminator is not included in the
      array.

        Returns:
          bytes: The response from the instrument.
      """
      pass
 
    @abstractmethod
    def read_string(self) -> str:
      """
      Reads a complete response from the instrument into a string. The
      response message terminator is not included in the string.

        Returns:
          str: The response from the instrument.
      """
      pass
 
    @abstractmethod
    def write_bytes(self, data: bytes) -> None:
      """
      Writes an array of bytes to the instrument followed by the normal
      message termination sequence. For IEEE 488.2 instruments the
      termination sequence is typically a line feed character with END
      asserted.

      The array of bytes must include a complete instrument message. The
      driver adds the termination sequence.

      Args:
          data (bytes): Data to write to the instrument as a bytes
          object. 
      """
      pass
 
    @abstractmethod
    def write_string(self, data: str) -> None:
      """
      Writes a string to the instrument followed by the normal message
      termination sequence. For IEEE 488.2 instruments the termination
      sequence is typically a line feed character with END asserted.

      The array of byte must include a complete instrument message. The
      driver adds the termination sequence.

      Args:
          data (str): String to write to the instrument.
      """
      pass
