# Notes for work on IVI Python Spec

This document has notes for running discussions and action items.

Table of Contents:

- [Notes for work on IVI Python Spec](#notes-for-work-on-ivi-python-spec)
  - [Action Items](#action-items)
  - [Issue List](#issue-list)
  - [Process Plan](#process-plan)
  - [July 10](#july-10)
    - [Discussion](#discussion)


## Action Items

1. Get the repository setup and put the initial document in it.
2. Propose updates to the document consistent with discussion in June at triannual meeting. (PR)

## Issue List

1. The newest document Version 0.3 has changes in chapters order. Do we revert to the original order to keep it aligned with the ANSI-C spec?
2. Markdown files style: lines wrapping - do we prefer manual wrapping or auto-wrap in editors.
3. Meta-names format: <DistributionPackageName> or <dist-pckg-name> or <dist_pckg_name> or <distribution-package-name> ...
4. Distribution package name composition in relation to the <DriverIdentifier>.

## Process Plan

- During meetings, Milo will take notes in the spec or in the notes.md file.  After meeting, Milo will force a push into the repo to capture notes and corrections in both documents.  Every meeting should contain a meeting summary in the notes.md.
 
## July 10

Agenda

- Review process and state of repo
- Review PR that should align current draft with discussions at triannual IVI meeting.
- Collect issue list (things we need to discuss)

### Discussion

- Decision on the meta-names. Expected name should mirror the meta-names casing.
- One driver per package or just one? What should be shall or should?
- We should let the vendor decide the internal structure, following PEP-8
- Typing hints? Requirement?
- Zen of Python - do not have duplicated features. If there is a way to get a good code-completion without the `marker_item` methods, we should not require them. Example of the code to see the auto-completion.
- Collections naming (recommendation): should they be plural?
  - marker_item = io.measurement.marker[]
  or
  - marker_item = io.measurement.markers[]
- should we ask for a specific classifier for the IVI drivers?
- for the packaging add the note: This is an example.