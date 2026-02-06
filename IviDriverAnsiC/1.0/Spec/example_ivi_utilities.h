/************************************************************
 Example IVI ANSI-C Driver Prototypes
*************************************************************

This is an example C include file that contains examples of the
prototypes required for an IVI-ANSI-C driver. 

The prototypes here are for the required utilities and the IviDriverIo
functions. The IviDriverIo functions are only required for certain
drivers, the specification has details of those requirements.

This file uses the following example identifiers:

    Driver Identifier: AcmeM3456 (that is manufacturer Acme, model M3456)

    IO Prefix: AcmeM3456_utilities_io  (that is the 
      Driver Identifier + _Utilities_IO_, implying a hierarchy of 
      Utilities -> IO)

For this example, the type of the IO session is void*. The driver author
should consider a more specific type to provide stronger type checking.

To adapt this example to an actual driver, the Driver Identifier, the
prefix attached to the IO functions, the type of the IO session, and the
filename must be updated for the actual driver.

This example is based on the IVI Foundation's 1.0 release of IVI-ANSI-C.
For details see the `IVI-ANSI-C Specification' at:

<https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverAnsiC/1.0/Spec>

 Author: IVI Foundation

 Version: 1.0
*********************************************************/


#ifndef EXAMPLE_IVI_UTILITIES_H
#define EXAMPLE_IVI_UTILITIES_H

#include <stdint.h>
#include <stdbool.h>
#include <stddef.h>

/* an opaque, strongly typed typedef for the driver session, the driver
implementation will use a private type for this */
typedef void* AcmeM3456Session;

/* Initialization functions */
int32_t AcmeM3456_init(const char *resource_name, bool id_query,  bool reset, AcmeM3456Session* session_out);
int32_t AcmeM3456_init_with_options(const char* resource_name, bool id_query, bool reset, const char* options, AcmeM3456Session* session_out);

/* Functions specified in the IVI Core specification */
int32_t AcmeM3456_driver_version_get(AcmeM3456Session session, size_t size, char* version_out, size_t* size_required);
int32_t AcmeM3456_driver_vendor_get(AcmeM3456Session session, size_t size, char* vendor_out,  size_t* size_required);
int32_t AcmeM3456_error_query(AcmeM3456Session session, int32_t* error_code_out, size_t size, char* error_message_out, size_t* size_required);
int32_t AcmeM3456_instrument_manufacturer_get(AcmeM3456Session session, size_t size, char* manufacturer_out, size_t* size_required);
int32_t AcmeM3456_instrument_model_get(AcmeM3456Session session, char* model_out);
int32_t AcmeM3456_query_instrument_status_enabled_get(AcmeM3456Session session, bool* instrument_status_enabled_out);
int32_t AcmeM3456_query_instrument_status_enabled_set(AcmeM3456Session session, bool instrument_status_enabled);
int32_t AcmeM3456_reset(AcmeM3456Session session);
int32_t AcmeM3456_simulate_get(AcmeM3456Session session, bool* simulate_out);
int32_t AcmeM3456_supported_instrument_models_get(AcmeM3456Session session, size_t size, char* supported_instrument_models_out, size_t* size_required);

/* Additional IVI-ANSI-C functions required for driver error management */
int32_t AcmeM3456_error_message(int32_t error, size_t size, char *error_message, size_t* size_required);
int32_t AcmeM3456_last_error_message(AcmeM3456Session session, size_t size, char *error_message, size_t* size_required);
int32_t AcmeM3456_clear_last_error(AcmeM3456Session session);
int32_t AcmeM3456_read_and_clear_error_queue(AcmeM3456Session session, size_t size, char *error_queue);


/* IVI Direct IO functions only required for certain drivers */
int32_t AcmeM3456_utility_io_timeout_milliseconds_set(const void* session, const long);
int32_t AcmeM3456_utility_io_timeout_milliseconds_get(const void* session, long* timeout_milliseconds_out);
int32_t AcmeM3456_utility_io_read_bytes(const void* session, const long size, uint8_t *);
int32_t AcmeM3456_utility_io_read_string(const void* session,const long size, char *);
int32_t AcmeM3456_utility_io_write_bytes(const void* session, const long size, const uint8_t *);
int32_t AcmeM3456_utility_io_write_string (const void* session, const char *);

/* Optional Direct IO function to retrieve the underlying IO Session */
int32_t AcmeM3456_utility_io_iosession_get(const void* session, void **iosession);

#endif /* EXAMPLE_IVI_UTILITIES_H */