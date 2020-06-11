
<div align=center>
 <img alt="CakeLang" src="assets/logo-shadow.png" width="30%">
 <br>
 <img alt="Nuget" src="https://img.shields.io/nuget/v/CakeLang">
 <!--<img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/WilliamRagstad/CakeLang">-->
 <img alt="Nuget" src="https://img.shields.io/nuget/dt/CakeLang">
 <img alt="Last Commit" src="https://img.shields.io/github/last-commit/WilliamRagstad/CakeLang">
</div>

# CakeLang
CakeLang is an open source cross-platform Minecraft data pack development framework in C# *.NET Core* with a built in support for custom data pack development languages and tools, which includes a CakeLang scripting language transpiler!

<div align=center>
 <p>Start enjoying data pack development now 🎂🎉</p>
</div>

> ### Install
> Add CakeLang to your C# project by installing the NuGet package from the link below.
>
> <a href="https://www.nuget.org/packages/CakeLang"><img src="https://docs.microsoft.com/en-us/dotnet/standard/library-guidance/media/nuget/nuget-logo.png" target="_blank" height="40px"></a>





<div align=center>
    <img src="assets/avatar1.png" width=20%>
    <h4>Interview with a developer</h4>
    <p>
        <em>
            "What I love about CakeLang is that it simplifies and collects the code base for one or multiple data packs in one place and one project. This makes it super easy to reuse code, make major changes with just a few keystrokes and generate fast and optimized commands.<br>
CakeLang also provides a huge amount of power when I can mix regular C# code and NuGet packages with my data pack source code to let me create anything imaginable!"
        </em>
	- Dotch
    </p>
</div>








# Get Started!



## Setup

CakeLang is a .NET Core 3.1 NuGet package which supports *every platform* and *every program environment*, be it web, software or app development. The only requirement is a .NET Core runtime.

This makes CakeLang the ultimate choice when developing an online IDE for data pack development in Vanilla / CakeLang (or a language based on CakeLang's SDK), an AR app for a new kind of minecraft data pack development 🤯, or simply plain data pack development in any of the languages that CakeLang (or a CakeLang plugin) offers. **Yes, you can easily program in different languages** like Vanilla, CakeLang (provided with package), or any other language built upon CakeLang's SDK.

Some creamy examples of things that are exceptionally good with CakeLang are: Dynamic data packs, standalone installation programs with customizable UIs for data packs, Plugins to CakeLang or regular data pack development in C #, the benefit of programming in a GPL, ...



## Examples



### 1. Basic Setup

In this example, we have a console program with the CakeLang package installed. The code below creates a new data pack with name, description and associated namespace, and only compiles it into a pack which is directly injected into a Minecraft world.

Source code:

```c#
using CakeLang;

namespace CakeLangDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataPack demoPack = new DataPack(
                "Demo Pack",
                "A demonstration of CakeLang!",
                "demopack"
            );
            
            // Code ...
            
            demoPack.CompileAndInject("CakeLang Demo");
        }
    }
}
```
