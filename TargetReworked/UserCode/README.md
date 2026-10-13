# UserCode

This directory is used to preserve manually reworked UserCode files.

It contains only files whose working versions differ from the corresponding files in:

```text
TargetGenerated/UserCode
```

The directory is maintained as part of the D2CSharp workflow so that manual corrections can survive regeneration of the translated C# files.

Normally, files should be edited in:

```text
TargetWorking/UserCode
```

rather than directly in this directory.

UserCode files are intended for local use only and are ignored by Git.
