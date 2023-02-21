
<div align="center">
    <img alt="NLog logo" src="https://raw.githubusercontent.com/NLog/NLog.github.io/master/images/NLog-logo-only_small.png" height="64" />
    :heavy_plus_sign:
    <img alt="Ntfy logo" src="https://github.com/binwiederhier/ntfy/raw/main/web/public/static/img/ntfy.png" height="64" />
</div>
<h1 align="center">
    NLog target and layout renderer for <a href="https://ntfy.sh/">ntfy.sh</a>
</h1>

<div align="center">
    <a href="https://www.nuget.org/packages/MichelMichels.NLog.Targets.Ntfy">
        <img src="https://img.shields.io/nuget/v/MichelMichels.NLog.Targets.Ntfy"/>
    </a>
</div>

<br />

<div align="center">
    This repository contains a library containing the code for the NLog target and layout renderer, and also a test and demo console library.
</div>
<br />
<br />

<details open="open">
<summary>Table of Contents</summary>

- [Prerequisites](#prerequisites)
- [Building](#building)
- [Installation](#installation)
- [Getting started](#getting-started)
  - [1. Add extension](#1-add-extension)
  - [2. Add target](#2-add-target)
  - [3. Add or edit rule](#3-add-or-edit-rule)
- [Credits](#credits)

</details>

---

## Prerequisites
- [.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [NLog 5.1.2+](https://github.com/NLog/NLog)
- [Ntfy.sh](https://ntfy.sh)

## Building

Use [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) to build the project. 

## Installation

Get the NuGet package from [nuget.org](https://www.nuget.org/packages/MichelMichels.NLog.Targets.Ntfy/) or search for `MichelMichels.NLog.Targets.Ntfy` in the GUI package manager in Visual Studio.

You can also use the cli of the package manager with following command:

```cli
Install-Package MichelMichels.NLog.Targets.Ntfy
```

## Getting started

> :warning: This assumes you already have a `nlog.config` file setup. For more information to setup NLog, see the [NLog wiki](https://github.com/NLog/NLog/wiki).

These instructions will add information to your NLog configuration file to make our target available. In this setup, the default nlog.config is used and nothing is removed. 

:information_source: For brevity, only the added or edited elements are displayed in these code snippets.


### 1. Add extension

```xml
<extensions>
    <add assembly="MichelMichels.NLog.Targets.Ntfy" />
</extensions>
```

### 2. Add target

```xml
<targets>
    <target xsi:type="Ntfy" name="logntfy" />
</targets>
```

### 3. Add or edit rule

In this snippet, `logntfy` (the name of the target configured above) is added to the `writeTo` attribute. The others are default and are not necessary for this target to work.

```xml
<rules>
    <logger name="*" minlevel="Trace" writeTo="logfile,logconsole,logntfy" />
</rules>
```

## Credits

Written by [Michel Michels](https://github.com/MichelMichels).