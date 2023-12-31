![Logo](Icon.png)

# Raylib Sharp

Raylib sharp is a safe partially autogenerated binding to raylib

## Features

-   XML documentation for intellisense

## Wasm Support

Run using these commands

```shell
dotnet publish
dotnet serve --directory "./bin/Debug/net8.0/browser-wasm/AppBundle/" -p 42069
```

## Icon Support

RaylibSharp supports a very simple way of adding icons to the application

Add an EmbeddedResource to use a custom Icon, if none is specified the default RaylibSharp [Icon](Icon.png) is used

```xml
<ItemGroup>
    <EmbeddedResource Include="./Icon.png" />
</ItemGroup>
```

## Examples

Examples generated from raylib git version `70286c7cdc6d972c63704ad957c18065f6a44cfe`

### References

-   [DotnetRaylibWasm](https://github.com/disketteman/DotnetRaylibWasm/)
-   [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)
-   [Raylib-cs](https://github.com/ChrisDill/Raylib-cs)
