# Algebra
 Linear Algebra

## Requirement
 .NET 6.0
 
## Install
[Download DLL](https://github.com/tk-yoshimura/Algebra/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.algebra/)  

- Import DoubleDouble(https://github.com/tk-yoshimura/DoubleDouble)
- To install, just import the DLL.
- This library does not change the environment at all.

## Usage

```csharp
// solve for v: Av=x
Matrix a = new double[,] { { 1, 2 }, { 3, 4 } };
Vector x = (4, 3);

Vector v = Matrix.Solve(a, x);
```

## Licence
[MIT](https://github.com/tk-yoshimura/Algebra/blob/main/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
