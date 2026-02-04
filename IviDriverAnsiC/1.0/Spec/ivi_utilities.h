/************************************************************
 IVI ANSI-C Driver Prototypes
*************************************************************

 This module defines prototype that are necessary for a driver to comply
 with the IVI-ANSI-C specification. This includes prototypes for
 required utilities and prototypes for the IviDriverIo functions which
 are only required for certain drivers.

 Use of this file is optional.  It is only provided for convenience.  If a
 driver does not use this file, it must still provide the functions.

 To use this file, the define the macro "IVI_DRIVER_IDENTIFIER" which is
 used in the definition of the function names.

 This version of the IVI Types is based on the IVI Foundation's 1.0
 release of IVI-ANSI-C.  For details see the `IVI-Python Specification
 <https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverPython/1.0/Spec>.

 Author: IVI Foundation

 Version: 1.0
*********************************************************/


#ifndef IVI_UTILITIES_H
#define IVI_UTILITIES_H

#include <stdint.h>
#include <stdbool.h>
#include <stddef.h>


/*
Questions:
1. Is the complexity of writing a generic file worthwhile, or should we omit entirely, or should we do an example?
    - If we go for a generic file, how should we pick up the prefixes?  From another file?

    DISCUSSION:
        - Generally although good technique would be to have clever macros, just an example is probably better for clarity.
        - For driver Identifier use: AcmeM3456 (manufacturer + model)
        - For IO Prefix use: AcmeM3456_Utilities_IO_

2. What do we do about "byte" type.  Seems to not be define in C**, should we use it or change to uint8_t?
    - Update the spec
    - typedef it in the example

    DISCUSSION:
        - Either approach should work. Using uint8_t is probably better since it avoids confusion.

4. filename selection - perhaps put in a note at the top.  If the file is an example, that should be clear from the filename.

*/

#define DUMMY   


// Beginning of first style:
#ifndef DUMMY // First style

// ************************************************************
// The following two macros and typedef should probably be in a separate
// file and #included. The actual identifiers must be updated to be
// appropriate for the driver.

#ifndef IVI_DRIVER_IDENTIFIER
#define IVI_DRIVER_IDENTIFIER VendorModel
#endif

#ifndef IVI_IO_PREFIX
#define IVI_IO_PREFIX_STRING VendorModel_hierarchy
#endif

typedef void* IVI_DRIVER_SESSION;   // Opaque session handle type

// End of the driver-specific included content
// ************************************************************


// JM: THESE MAY NOT WORK WITH ALL COMPILERS, NEED TO TEST IF WE KEEP THEM (ISSUE IF "x" IS ITSELF A MACRO)

#define IVI_PREFIX(x) IVI_DRIVER_IDENTIFIER##_##x
#define IVI_IO_PREFIX(x) IVI_IO_PREFIX_STRING##_##x


/* Initialization functions */
int32_t IVI_PREFIX(init)(const char *resource_name, bool id_query,  bool reset, IVI_DRIVER_SESSION session_out);

int32_t IVI_PREFIX(init_with_options)(const char* resource_name, bool id_query, bool reset, const char* options, IVI_DRIVER_SESSION session_out);

/* Functions specified in the base spec */
int32_t IVI_PREFIX(driver_version_get)(IVI_DRIVER_SESSION session, size_t size, char* version_out, size_t* size_required);
int32_t IVI_PREFIX(driver_vendor_get)(IVI_DRIVER_SESSION session, size_t size, char* vendor_out,  size_t* size_required);
int32_t IVI_PREFIX(error_query)(IVI_DRIVER_SESSION session, int32_t* error_code_out, size_t size, char* error_message_out, size_t* size_required);
int32_t IVI_PREFIX(instrument_manufacturer_get)(IVI_DRIVER_SESSION session, size_t size, char* manufacturer_out, size_t* size_required);
int32_t IVI_PREFIX(instrument_model_get)(IVI_DRIVER_SESSION session, char* model_out);
int32_t IVI_PREFIX(query_instrument_status_enabled_get)(IVI_DRIVER_SESSION session, bool* instrument_status_enabled_out);
int32_t IVI_PREFIX(query_instrument_status_enabled_set)(IVI_DRIVER_SESSION session, bool instrument_status_enabled);
int32_t IVI_PREFIX(reset)(IVI_DRIVER_SESSION session);
int32_t IVI_PREFIX(simulate_get)(IVI_DRIVER_SESSION session, bool* simulate_out);
int32_t IVI_PREFIX(supported_instrument_models_get)(IVI_DRIVER_SESSION session, size_t size, char* supported_instrument_models_out, size_t* size_required);

/* Additional functions required for driver error management */
int32_t IVI_PREFIX(error_message)(int32_t error, size_t size, char *error_message, size_t* size_required);
int32_t IVI_PREFIX(last_error_message)(IVI_DRIVER_SESSION session, size_t size, char *error_message, size_t* size_required);
int32_t IVI_PREFIX(clear_last_error)(IVI_DRIVER_SESSION session);

/* Additional function for working with the instrument error queue */
int32_t IVI_PREFIX(read_and_clear_error_queue)(IVI_DRIVER_SESSION session, size_t size, char *error_queue);


/* IVI Direct IO functions, these are only required for certain drivers */
/* Drivers are permitted to provide a typedef for the IO Session instead
of using void* as shown here, in which case it should be a pointer type. */
typedef uint8_t byte;  // implicit in specification

int32_t IVI_IO_PREFIX(timeout_milliseconds_set)(const void* session, const long);
int32_t IVI_IO_PREFIX(timeout_milliseconds_get)(const void* session, long* timeout_milliseconds_out);
int32_t IVI_IO_PREFIX(iosession_get)(const void* session, void **iosession);    // Optional
int32_t IVI_IO_PREFIX(read_bytes)(const void* session, const long size, byte *);
int32_t IVI_IO_PREFIX(read_string)(const void* session,const long size, char *);
int32_t IVI_IO_PREFIX(write_bytes)(const void* session, const long size, const byte *);
int32_t IVI_IO_PREFIX(write_string) (const void* session, const char *);

#endif // first style

#ifdef DUMMY // Second style

// The following is an example prototype for file an IVI-ANSI-C driver. It uses the following example identifiers:
//    Driver Identifier: AcmeM3456 (that is manufacturer Acme, model M3456)
//    IO Prefix: AcmeM3456_utilities_io  (that is the Driver Identifier + _Utilities_IO_, implying a hierarchy of Utilities -> IO)
//
// For this example, the type of the IO session is void*. The driver author can use a more specific type if they choose.


typedef void* AcmeM3456Session;   // Opaque session handle type, update to the appropriate for the driver


/* Initialization functions */
int32_t AcmeM3456_init(const char *resource_name, bool id_query,  bool reset, AcmeM3456Session* session_out);
int32_t AcmeM3456_init_with_options(const char* resource_name, bool id_query, bool reset, const char* options, AcmeM3456Session* session_out);

/* Functions specified in the base spec */
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

/* Additional functions required for driver error management */
int32_t AcmeM3456_error_message(int32_t error, size_t size, char *error_message, size_t* size_required);
int32_t AcmeM3456_last_error_message(AcmeM3456Session session, size_t size, char *error_message, size_t* size_required);
int32_t AcmeM3456_clear_last_error(AcmeM3456Session session);

/* Additional function for working with the instrument error queue */
int32_t AcmeM3456_read_and_clear_error_queue(AcmeM3456Session session, size_t size, char *error_queue);


/* IVI Direct IO functions only required for certain drivers */
typedef uint8_t byte;    // implicit in specification

int32_t AcmeM3456_utility_io_timeout_milliseconds_set(const void* session, const long);
int32_t AcmeM3456_utility_io_timeout_milliseconds_get(const void* session, long* timeout_milliseconds_out);
int32_t AcmeM3456_utility_io_iosession_get(const void* session, void **iosession);    // Optional
int32_t AcmeM3456_utility_io_read_bytes(const void* session, const long size, byte *); //NOTE CHANGE
int32_t AcmeM3456_utility_io_read_string(const void* session,const long size, char *);
int32_t AcmeM3456_utility_io_write_bytes(const void* session, const long size, const byte *); //NOTE CHANGE
int32_t AcmeM3456_utility_io_write_string (const void* session, const char *);


#endif // second style

#endif /* IVI_UTILITIES_H */