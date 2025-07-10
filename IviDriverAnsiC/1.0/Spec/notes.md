# Notes for work on IVI ANSI-C Spec

This document has notes for running discussions and action items.

Table of Contents:

- [Notes for work on IVI ANSI-C Spec](#notes-for-work-on-ivi-ansi-c-spec)
  - [Action Items](#action-items)
  - [Issue List](#issue-list)
      - [Things we need to discuss before digging into the spec:](#things-we-need-to-discuss-before-digging-into-the-spec)
      - [Things to mesh with spec review:](#things-to-mesh-with-spec-review)
  - [Process Plan](#process-plan)
  - [July 10](#july-10)
    - [Discussion](#discussion)

## Action Items

1. Get the repository setup and put the initial document in it.
2. Propose updates to the document consistent with discussion in June at triannual meeting. (PR)

## Issue List

#### Things we need to discuss before digging into the spec:

1. Are there special consideration to ensure an IVI-C and an IVI-AnsiC Core driver can work together? (exist on same machine, be used in same application)

  - If we think about 2014 and 2026 coexisting.  Would a 2014 driver still comply since it is more specific requirement?  How do the specs relate? Expectation that a 2014 driver would still comply.  Expect them to coexist.

2. Do we want to pursue driver meta-data that would support same use-cases as .SUB and .FP files? (may not be complete before spec review starts, but needs some discussion at least).
3. Do we need a IVI-AnsiC Core Driver Shared Component? (needs some air time, may not resolve till later).
4. What provisions do we need to include in the spec for ABI compatibility?  Especially regarding using and permitting use of *int* and *enum*
5. Need to discuss typing.  Do we want to permit stronger typing by:
  - Defining some common types such as "ResultType" for errors/warnings
  - We could specify a type for session, or we could  (as written here) either suggest or require driver-defined types.

NOTE Joe - Put starter discussions in Discussion area for these 5 areas

#### Things to mesh with spec review:

1. What do we want to do, if anything  to facilitate permitting driver vendors to share source between IVI-C and IVI-AnsiC?
2. Do we want to consider extending IVI-AnsiC to common instrument classes (like IVI-C)?  Are there provisions we should make to enable that?
3. Memory management - need to know sizes of things (e.g., arrays etc).  Do you do the "IVI Dance" to call with a null pointer then get the size?  Can we define a way to do this consistently.  Perhaps return the size instead of the error code?
4. Good practice for libraries to have global initialize/finalize function so you can join worker threads you have in the background.  NI sees a need for this a lot.  Perhaps it could be optional but called out (important when loading the drivers in the background - need a way to clean it up before the process unloads).  Seems like, at least for Windows, these should be called in DLL_PROCESS_ATTACH/DETACH??  would we make that a requirement?

## Process Plan

- During meetings, Joe will take notes in the spec or in the notes.md file.  After meeting, Joe will force a push into the repo to capture notes and corrections in both documents.  Every meeting should contain a meeting summary in the notes.md.
  - In general, Joe will force merges that clean-up or facilitate process.  The history is there for everyone to see :).
- Weekly we will work through the issue list, and/or work through the document beginning to end (possibly pushing things into issue list), and/or review PRs.
  - We will make some sort of ad hoc decision regarding putting content in a [!NOTE] or putting it in the issue list.
- We will always entertain PRs from members, but ideally they should mesh into discussions.  Numerous outstanding PRs with heavy discussions could be nightmarish to merge.  Perhaps PRs should be created that add issues and propose solutions in a way that they can be merged without coming to a complete conclusion??
- When the document is fully reviewed and the issues are all closed, do another complete review.

## July 10

Agenda

- Review process and state of repo
- Review PR that should align current draft with discussions at triannual IVI meeting.
- Collect issue list (things we need to discuss)

### Discussion

