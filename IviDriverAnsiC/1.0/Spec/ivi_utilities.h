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


        MACRO Scheme -- gets messy -- e.g., each driver defines IVI_PREFIX or whatever, need to undef it and fiddle around.  So the custmomer code cannot use this file.

        General agreement that we prefer going with the example.

2. What do we do about "byte" type.  Seems to not be define in C**, should we use it or change to uint8_t?
    - Update the spec to uint8_t  
    - typedef it in the example  (likely to cause conflicts)

    DISCUSSION:
        - Update the spec to uint8_t is probably better since it avoids confusion.
            - Treat this as editorial - Sounds fine.

4. filename selection - perhaps put in a note at the top.  If the file is an example, that should be clear from the filename.

*/



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