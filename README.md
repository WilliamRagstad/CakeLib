
<div align=center>
 <img alt="CakeLang" src="assets/logo-shadow.png" width="30%">
 <br>
 <img alt="Nuget" src="https://img.shields.io/nuget/v/CakeLang">
 <!--<img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/WilliamRagstad/CakeLang">-->
 <img alt="Nuget" src="https://img.shields.io/nuget/dt/CakeLang">
</div>

# CakeLang
A Minecraft data pack development framework.

# Usage

## Examples

### 1. Basic Setup

Source code:

```c#
using CakeLang;

namespace CakeLangDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataPack demoPack = new DataPack("Demo Pack", "A demonstration of CakeLang!", "demopack");
            // Code ...
            demoPack.CompileAndInject("CakeLang Demo");
        }
    }
}
```
