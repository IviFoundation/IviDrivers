# IVI Shared Component Version Management

This document describes the process and organization used by the IVI Foundation to manage the driver shared components that are in the IviFoundation/InstrumentDriverSpecs repository.

## Goals

The key goals of the process and file organization are:

- **Content**
  - Various standards (e.g., Core, .NET, Python, Ruby, ...)
  - Various versions of those standards (e.g., 1.0, 1.1, 2.0)
  - Shared components associated with each of those standards (with necessary DevOps)
  - Other documentation such as this document
  - Example drivers to help developers get started

- **Availability**
  
  - Versions of shared components associated with a version of the driver standards need to be easily available to consumers (driver suppliers, customers?).  For instance, need the 1.1 shared component and the 1.1 standard.
  
  - Since shared components will presumably need occasionaly updates independent of the standard itself, the latest version of the shared components should be trivially available.
  
- **Version Tracking**
  
  Clarifying definitions: a *version* of a specification is *Major.Minor*.  However these versions typically have editorial updates, and a version always has an editorial date associated with it.
  
  Shared components also have versions, but a version of the shared components is associated with a version of the specification.  For this analysis it is not too important how the *Major.Minor* of the shared components is managed.
  
  - Need to contain arbitrary versions of the specifications, each with its editorial updates.  The latest editorial update to a specification should be trivially available and managed separately from the version.  That is, generally an editorial update replaces its predecessor, but a version update does not replace a predecessor (since the predecessor was a deployed specification).

  - Independent development and versioning of different standards/shared component tuples. (e.g., Python/.NET)

  - Prefer to facilitate simultaneous access to different semantic versions since multiple versions will be in deployed at the same time. (e.g., 1.0 and 2.0 versions of the .NET Spec and its components).

  - Shared components need to be tightly bound to the associated specification version.  However both the specification and the shared components need to permit bug-fixes and editorially updates.
  
  - The organization needs to support revising shared components associated with various versions of the standard.  For instance, if we have version 1.0 and 2.0 of the standard, each standard version needs to have associated shared components.  Each version-specific shared components package needs to be individually revisable to address defect etc.

- **Intuitive Organization** (admittedly in the eyes of the beholder)

  - Easily associate a version of the shared components with a version of the specification.

  - Keep documents ancillary to the specifications with this specification such that versions are held between them.  For instance, the *ExampleComplianceDocument* needs to be tightly associated with a specific specification version. Generally, duplicating it is better than attemptiong to manage independent versions (although some organizations could avoid either).

  - Need to independently manage shared components and standards for numerous standards.  For instance, .NET, Python, Ruby standards and shared components need to fit into the repo.

## Proposed Organization

This organization is based on tags and directories.  It makes no organizational use of branches for versions of either specs or shared components.

The basic approach here is:

- Create a root directory for each specification type
- That is followed by a directory for each RELEASED version (e.g., 1.0)
- That is followed by directories for Spec and Code (Shared Components)
- These directories contain whatever is needed.
- Use tags to identify editorial updates to specs
- Use tags to identify updates to shared components

Some basic attributes of this organization:

- The repo image has all of the released specs and corresponding shared components.  This includes versions of the specs, and the family of specs.
- Able to contain all of the potential types of specifications with independent management of versions of the specifications and their shared components.
- Very simple, self-documenting, the basic root trunc always has the latest released.
- Puts an image of everything in every instance of the repo, whereas a branch-based scheme would allow only acquiring the slice you are interested in.
- Potentially appears dated to users of git for branch-based software releases.

~~~
IviDriverCore/
    1.0/    # spec version
        Spec/        # use tags for editorial changes (need a tag naming
                       convention that identifies the last commit on 
                       this editorial version of the spec, independent of 
                       others, e.g.: 'IviDriverCoreSpec-2024-01-10')

                       'main' should have the latest
            IviDriverCore.md
            Example.md
            …
        Code/       # part of the pattern, but none for Core
    2.0/
    3.0/
    
IviDriverNet/
    1.0/  (spec version)
        Spec/
            IviDriverNet.md
            Example.md
            …
        Code/    # tags or branches for multiple release versions
                   actual organization of subdirectories depends on
                   code organization convenience.

                   Need to support complete build.
    1.1/
    2.0/
~~~
