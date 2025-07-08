# Notes for work on IVI ANSI-C Spec

This document has notes for running discussions and action items.

Table of Contents:

- [Notes for work on IVI ANSI-C Spec](#notes-for-work-on-ivi-ansi-c-spec)
  - [Action Items](#action-items)
  - [Issue List](#issue-list)
  - [Process Plan](#process-plan)
  - [July 10](#july-10)
    - [Discussion](#discussion)


## Action Items

1. Get the repository setup and put the initial document in it.
2. Propose updates to the document consistent with discussion in June at triannual meeting. (PR)

## Issue List

1. Are there special consideration to ensure an IVI-C and an IVI-AnsiC Core driver can work together? (exist on same machine, be used in same application)
2. What do we want to do, if anything  to facilitate permitting driver vendors to share source between IVI-C and IVI-AnsiC?
3. Do we want to pursue driver meta-data that would support same use-cases as .SUB and .FP files? 
4. Do we want to consider extending IVI-AnsiC to common instrument classes (like IVI-C)?  Are there provisions we should make to enable that?
5. Do we need a IVI-AnsiC Core Driver Shared Component?

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

