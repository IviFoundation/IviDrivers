# IVI Instrument Driver Specs Repo Organization

This document describes the process and organization used by the IVI Foundation to manage the driver shared components that are in the IviFoundation/InstrumentDriverSpecs repository.

## Goals

The key goals of the process and file organization are:

- **Content**
  
  The repository needs to contain:
  - Various standards (e.g., Core, .NET, Python, Ruby, ...)
  - Various versions of those standards (e.g., 1.0, 1.1, 2.0)
  - Shared components associated with each of those standards (with necessary DevOps)
  - Other documentation such as this document
  - Example drivers to help developers get started

- **Availability**
  
  - Versions of shared components associated with a version of the driver standards need to be easily available to consumers (driver suppliers, customers?).  For instance, need the 1.1 shared component and the 1.1 standard.
  
  - Since shared components will presumably need occasionaly updates independent of the standard itself, the latest version of the shared components should be trivially available.
  
- **Version Tracking**
  
  Specification version management: a *version* of a specification is *Major.Minor*.  However a given versions will typically have editorial updates that are indicated with a date.  Therefore, a version always has an editorial date associated with it.
  
  Shared Component version management: Shared components are the common software components used in the implementation and/or use of  the standard.  The shared component verfsion is associated with a version of the specification, but also has multiple released independent of the standard version. These releases are asynchronous to the standard versions and may have enhancements or bug fixes.  The details of the assignment of version numbers to the shared components is beyond the scope of this document.

  The rep organization needs to support:
  
  - Arbitrary versions of the specifications, each with its editorial updates.  The latest editorial update to a specification should be trivially available and managed separately from the version.  That is, generally an editorial update replaces its predecessor, but a version update does not replace a predecessor (since the predecessor was a deployed specification).

  - Independent development and versioning of different standards and the shared component associated with the standard. (for example, Python and .NET standards each have independent versions and their shared components also have independent versions).

  - Would prefer an organization that facilitates simultaneous access to different specification *Major.Minor* versions and their most current shared components since multiple specification *Major.Minor* versions will be in deployed at the same time. (e.g., 1.0 and 2.0 versions of the .NET Spec and its components are both deployed simultaneously).

  - Shared components need to be tightly bound to the associated specification *Major.Minor* version.  However the specification must permit editorial updates and the shared components need to permit bug-fixes.
  
  - The organization needs to support revising shared components associated with various versions of the standard.  For instance, if we have version 1.0 and 2.0 of the standard, each standard version needs to have associated shared components.  Each version-specific shared components package needs to be individually revisable to address defect etc.

- **Intuitive Organization** (admittedly in the eyes of the beholder)

  - We would like the organization to easily associate a version of the shared components with a version of the specification.

  - Keep documents ancillary to the specifications with this specification such that versions are held between them.  For instance, the *ExampleComplianceDocument* needs to be tightly associated with a specific specification version. Generally, duplicating it is better than attemptiong to manage independent versions (although some organizations could avoid either).

  - Need to independently manage shared components and standards for numerous standards.  For instance, .NET, Python, Ruby standards and shared components need to fit into the repo.

## Organization

This organization is based on tags and directories.  It makes no organizational use of branches for versions of either specs or shared components.  However, we anticipate utilizing branches for the standards processes for updates and new documents.

The organization is as follows:

- There is a root directory for each specification type
- The specification directories have subdirectories for each RELEASED *Major.Minor* specification version (e.g., 1.0)
- The specification version directory has subdirectories for Specifications (*Spec*) and shared components (*Code*).  However, it seems likely that a single version of the code may in some cases support newer versions of the specification. In these cases, no new code directory is created.
- These *Spec* and *Code* directories contain whatever is deemed appropriate by the working group. So a *Spec* directory may contain specs, examples and other explanatory information and a *Code* directory contains whatever is appropriate for the language (or languages) and tool chain.

The IVI Foundation assigns tags to:

- identify editorial updates to specs
- identify updates to shared components

Some basic attributes of this organization:

- The repo image has all of the released specs and corresponding shared components.  This includes versions of the specs, and the family of specs.
- the repo contains all of the potential types of specifications with independent management of versions of the specifications and their shared components.
- Very simple, self-documenting, the basic root trunk always has the latest released versions of the specs and shared components
- Puts an image of everything in every instance of the repo, whereas a branch-based scheme would allow only acquiring the slice you are interested in.
- The repository directories containing specification should also contain pdf files of the associated version of the spec. Since the pdf files will be used beyond their  location in the directory, each is tagged with its version number. so the name of the pdf file is: *\<specification identifier>-\<major>.\<minor>.pdf*.  A single pdf file independent of the editorial revision should be in the repository (presumably there is not need to keep historical versions around that only have editorial differences).
- Note that users desiring editorial versions will have to delve into the git history as the editorial revisions to the markdown and pdf replace the originals.

We realize that this organization may appear dated to users of git for branch-based software releases.  We have found this organization to be effective for team utilization of documents and the associated code.

The following is an example organization:
~~~
IviDriverCore/       # this identifies and particular specification
    1.0/             # spec Major.Minor version
        Spec/        # use tags for editorial changes (see below for
                     # tag naming convention.
            IviDriverCore.md
            Example.md
            IviDriverCore-1.0.pdf
            <other files>
        Code/        # part of the pattern, but none since Core has no shared components
    2.0/
    3.0/
    
IviDriverNet/
    1.0/             # spec Major.Minor version
        Spec/
            IviDriverNet.md
            Example.md
            …
        Code/       # tags or branches for multiple release versions
                    # actual organization of subdirectories depends on
                    # code organization convenience.

                    # Need to support complete build.
    1.1/
    2.0/
~~~

## Tags

When tagging a commit to identify a specification releases light-weight git tags are used with the following syntax:

`<specification identifier>-<major.minor>-<ISO date>`

Where:

- *specification identifier* is the directory the specification is in.
- *major.minor* is the major and minor versions of the spec
- *ISO date* is the ISO 8601 standard date format, for example: 1941-12-07

Therefore, the following are valid tags:

- IviDriverCore-1.0-2025-01-21
- IviDriverNet-1.0-2025-01-21
