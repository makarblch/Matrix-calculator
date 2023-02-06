using System;
using System.Linq;
using System.Text;

public class Matrix
{
    private int _n;
    private int _m;

    private int[,] _meow;

    public int this[int ind1, int ind2]
    {
        get => _meow[ind1, ind2];
        set => _meow[ind1, ind2] = value;
    }
    
    public Matrix(int n, int m)
    {
        _n = n;
        _m = m;
        _meow = new int[_n, _m];
    }

    public (int, int) GetDimension() => (_n, _m);

    public static int Meow(int i) => i % 2 == 0 ? 1 : -1;

    public void ReadMatrix()
    {
        for (int i = 0; i < _n; i++)
        {
            int[] temp = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
            for (int j = 0; j < _m; j++)
            {
                _meow[i, j] = temp[j];
            }
        }
    }

    public static Matrix Slice(Matrix a, int index)
    {
        int n = a.GetDimension().Item1;
        int m = a.GetDimension().Item2;
        Matrix pizda = new Matrix(n - 1, m - 1);
        for (int i = 1; i < n; i++)
        {
            int boo = 0;
            for (int j = 0; j < m; j++)
            {
                if (j == index)
                {
                    boo++;
                    continue;
                }
                pizda[i - 1, j - boo] = a[i, j];
            }
        }

        return pizda;
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.GetDimension() != b.GetDimension()) throw new ArithmeticException("Impossible");
        Matrix res = new Matrix(a.GetDimension().Item1, a.GetDimension().Item2);
        for (int i = 0; i < a.GetDimension().Item1; i++)
        {
            for (int j = 0; j < a.GetDimension().Item2; j++)
            {
                res[i, j] = a[i, j] + b[i, j];
            }
        }

        return res;
    }
    
    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.GetDimension() != b.GetDimension()) throw new ArithmeticException("Impossible");
        Matrix res = new Matrix(a.GetDimension().Item1, a.GetDimension().Item2);
        for (int i = 0; i < a.GetDimension().Item1; i++)
        {
            for (int j = 0; j < a.GetDimension().Item2; j++)
            {
                res[i, j] = a[i, j] - b[i, j];
            }
        }

        return res;
    }
    
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.GetDimension().Item2 != b.GetDimension().Item1) throw new ArithmeticException("Impossible");

        Matrix pizda = new Matrix(a.GetDimension().Item1, b.GetDimension().Item2);
        for (int i = 0; i < a.GetDimension().Item1; i++)
        {
            for (int j = 0; j < b.GetDimension().Item2; j++)
            {
                pizda[i, j] = 0;
                for (int k = 0; k < a.GetDimension().Item2; k++)
                {
                    pizda[i, j] += a[i, k] * b[k, j];
                }
                
            }
        }

        return pizda;
    }
    
    public static Matrix operator ^(Matrix a, int n)
    {
        if (a.GetDimension().Item1 != a.GetDimension().Item2) throw new ArithmeticException("Impossible");
        if (n < 0) throw new ArithmeticException("Impossible");
        Matrix pizda = new Matrix(a.GetDimension().Item1, a.GetDimension().Item1);
        
        if (n != 0)
        {
            pizda._meow = a._meow;
            for (int i = 0; i < n - 1; i++)
            {
                pizda *= a;
            }
        }
        
        else
        {
            for (int i = 0; i < a.GetDimension().Item1; i++)
            {
                for (int j = 0; j < a.GetDimension().Item1; j++)
                {
                    if (i == j)
                    {
                        pizda[i, j] = 1;
                    }
                    else pizda[i, j] = 0;
                }
            }
        }

       
        return pizda;
    }
    
    public static Matrix operator ~(Matrix a)
    {
        Matrix pizda = new Matrix(a.GetDimension().Item2, a.GetDimension().Item1);
        for (int i = 0; i < a.GetDimension().Item1; i++)
        {
            for (int j = 0; j < a.GetDimension().Item2; j++)
            {
                pizda[j, i] = a[i, j];
            }
        }

        return pizda;
    }
    
    public static int operator !(Matrix a)
    {
        if (a.GetDimension().Item1 != a.GetDimension().Item2) throw new ArithmeticException("Impossible");
        int determinant = 0;
        
        if (a.GetDimension().Item1 == 1) return a[0, 0];
        
        for (int i = 0; i < a.GetDimension().Item2; i++)
        {
            int temp = a[0, i] * Meow(i) * !Slice(a, i);
            determinant += temp;
            // Console.WriteLine(a[0, i] + " " + Meow(i) + " " + !Slice(a, i));
        }

        return determinant;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _m; j++)
            {
                sb.Append(_meow[i, j] + " ");
            }

            sb.Append("\n");
        }

        return sb.ToString();
    }
    
    
}

