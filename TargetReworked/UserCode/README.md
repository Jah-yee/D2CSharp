# UserCode

This directory is part of the local UserCode workflow for translating your own
Delphi source files with D2CSharp.

The corresponding Delphi sources are placed in:

```text
Source/UserCode
```

During the conversion workflow, C# files may be generated, copied, preserved,
or restored between the following directories:

```text
TargetGenerated/UserCode
TargetWorking/UserCode
TargetReworked/UserCode
```

These three directories intentionally contain the same `README.md` file so
that the README itself does not appear as a manually reworked file.

UserCode files are intended for local use only and are ignored by Git. This
allows private or proprietary Delphi sources to be translated without
accidentally adding them to the D2CSharp repository.

Do not place repository test files here. D2CSharp tests belong in:

```text
Source/Tests
```
