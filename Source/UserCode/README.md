# UserCode

Place your own Delphi source files in this directory if you want to translate them with D2CSharp.

Files in this directory are intended for local use only and are ignored by Git. This makes it possible to work with private or proprietary Delphi sources without accidentally adding them to the D2CSharp repository.

During conversion, the corresponding generated C# files are written to:

```text
TargetGenerated/UserCode
```

The working versions are maintained in:

```text
TargetWorking/UserCode
```

Files that differ between the generated and working versions may be preserved in:

```text
TargetReworked/UserCode
```

Do not place D2CSharp test files here. Repository tests belong in `Source/Tests`.
