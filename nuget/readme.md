# R/WinRT for Windows App SDK

R/WinRT is ease-to-use resources access for the MRTCore resource files.

[Source code][source] | [Package (NuGet)][package]

## Getting started

### Install the package

Install the R/WinRT with [NuGet][nuget]:

```cmd
nuget install Mntone.RWinRT
```

## Examples

### [C#/WinRT] Get the resource

Define "Hello, World!" as the key "HelloWorld" in Resources.lang-en-US.resw.

```cs
using System;

namespace RWinRTCsTest
{
  class Program
  {
    static void Main(string[] args)
    {
      var text = R.HelloWorld.Value;
      Console.WriteLine(text); // [Output] Hello, World!
    }
  }
}
```

### [C#/WinRT] Get the formatted resource

Define "I have {0} apples!" as the key "IHaveNApples" in Resources.lang-en-US.resw.

```cs
using System;

namespace RWinRTCsTest
{
  class Program
  {
    static void Main(string[] args)
    {
      var count = 4;
      var fmtText = R.IHaveNApples.Format(4);
      Console.WriteLine(fmtText); // [Output] I have 4 apples!
    }
  }
}
```

### [C#/WinRT] Change current language

Define "日本語 (Japanese)" as the key "CurrentLanguage" in Resources.lang-ja-JP.resw.

```cs
using System;

namespace RWinRTCsTest
{
  class Program
  {
    static void Main(string[] args)
    {
      RWinRT.ResourceManager.ChangeLanguage("ja-JP");

      var language = R.CurrentLanguage.Value;
      Console.WriteLine(language); // [Output] 日本語 (Japanese)
    }
  }
}
```

### [C++/WinRT] Get the resource

Define "Hello, World!" as the key "HelloWorld" in Resources.lang-en-US.resw.

```cpp
#include <iostream>

#include "res.g.h"

int main() {
  ResourceManager<R> manager;
  ResourceContext<R> context { manager.Context() };
  winrt::hstring text { context.Value<R::HelloWorld>() }; // [Output] Hello, World!
  std::cout << text << std::endl;
  return 0;
}
```

### [C++/WinRT] Get the formatted resource

Define "I have %d apples!" as the key "IHaveNApples" in Resources.lang-en-US.resw.

```cpp
#include <iostream>

#include "res.g.h"

int main() {
  ResourceManager<R> manager;
  ResourceContext<R> context { manager.Context() };
  int count { 4 };
  winrt::hstring fmtText { context.Format<R::IHaveNApples>(count) }; // [Output] I have 4 apples!
  std::cout << fmtText << std::endl;
  return 0;
}
```

### [C++/WinRT] Change current language

Define "日本語 (Japanese)" as the key "CurrentLanguage" in Resources.lang-ja-JP.resw.

```cpp
#include <iostream>

#include "res.g.h"

int main() {
  ResourceManager<R> manager;
  ResourceContext<R> context { manager.Context(L"ja-JP") };
  winrt::hstring text { context.Value<R::CurrentLanguage>() }; // [Output] 日本語 (Japanese)
  std::cout << text << std::endl;
  return 0;
}
```

[source]: https://github.com/mntone/RWinRT/tree/main/src
[package]: https://www.nuget.org/packages/Mntone.RWinRT
[nuget]: https://www.nuget.org/
