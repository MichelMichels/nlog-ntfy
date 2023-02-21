<div align="center">
    <img alt="NLog logo" src="https://raw.githubusercontent.com/NLog/NLog.github.io/master/images/NLog-logo-only_small.png" height="64" />
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
- [Configuration](#configuration)
  - [Changing host](#changing-host)
  - [Changing notification title](#changing-notification-title)
  - [Changing topic](#changing-topic)
  - [Changing title icons](#changing-title-icons)
  - [Defaults](#defaults)
- [LayoutRenderer](#layoutrenderer)
  - [Disable date output](#disable-date-output)
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
<br />
<hr>

## Getting started

> :warning: This assumes you already have a `nlog.config` file. For more information to setup NLog, see the [NLog wiki](https://github.com/NLog/NLog/wiki).

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

<br />
<hr>

## Configuration

### Changing host

> :information_source: The default value for the host is `https://ntfy.sh/`.

If you want to use another ntfy-server, you can change the `host` attribute on the target.

Example:
```xml
<target xsi:type="Ntfy" name="logntfy" host="https://other.ntfy.server/"  />
```

### Changing notification title

> :information_source: The default value for the title is `NLog`.

The notification title contains 3 parts:
  
* A symbol linked to the LogLevel
* The string-value of the LogLevel (f.e. 'Debug')
* The value in the 'Title' attribute (f.e. your app name)

The notification title of your Ntfy notification for a debug log message with default configuration would be:
```
:computer: Debug - NLog
```
In previous example, the `NLog` part will be changed when setting the `Title` attribute on the target.

### Changing topic

> :information_source: The default value for the topic is `nlog-ntfy`.

> :warning: You should change the topic if you don't want other people to read your logging.

Set the `topic` attribute on the target to change to a ntfy topic of your choice.

### Changing title icons

You can change the title icons (or 'tags') by setting the `*Tags` attributes. See the [emoji shortcodes](https://docs.ntfy.sh/emojis/) on ntfy.sh for supported emoji-tags. See the [defaults table](#defaults) for the default values.

### Defaults

These are the default values when nothing is changed.

| Setting | Value |
|---------|-------|
| Host    | `https://ntfy.sh/` |
| Topic    | `nlog-ntfy` |
| Title     | `NLog` |
| TraceTags | empty |
| DebugTags | :computer:, `computer` |
| InformationTags | :information_source:, `information_source` |
| WarnTags | :warning:, `warning` |
| ErrorTags | :exclamation:, `exclamation` |
| FatalTags | :skull:, `skull` |
| DefaultTags (fallback) | empty |

<br />
<hr>


## LayoutRenderer

Any layout renderer can be used, but I also included one. The body of the notification will be the render output.
To use the renderer included in this package you can add following attribute and value to the Ntfy target:

```xml
layout="${ntfy}"
```

This produces following output:
```
Date: dd/mm/yyyy
Time: hh:mm

{logMessage}

Exception: {exception.ToString()}

StackTrace: {stackTrace}
```

The exception and stacktrace output is only enabled if this info is present in the log event.

### Disable date output

Date and time output can be disabled by using the `isdaterendered` option on the layoutrenderer.

```xml
layout="${ntfy:isdaterendered=false}"
```


## Credits

Written by [Michel Michels](https://github.com/MichelMichels).