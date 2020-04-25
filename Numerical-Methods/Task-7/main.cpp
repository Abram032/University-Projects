#include<stdio.h>
#include<stdlib.h>
#include<bitset>
#include<iostream>
#include<iomanip>
#include<math.h>
#include<string.h>

#pragma region Array/Matrix Functions

double** CreateMatrix(int n)
{
    double** matrix = new double* [n];
    for(int i = 0; i < n; i++) {
        matrix[i] = new double[n];
    }
    for(int i = 0; i < n; i++) {
        for(int j = 0; i < n; i++) {
            matrix[i][j] = 0;
        }
    }
    return matrix;
}

double** CreateMatrix(int n, int m)
{
    double** matrix = new double* [n];
    for(int i = 0; i < n; i++) {
        matrix[i] = new double[m];
    }
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < m; j++) {
            matrix[i][j] = 0;
        }
    }
    return matrix;
}

double* CreateArray(int n)
{
    double* array = new double[n];
    for(int i = 0; i < n; i++) {
        array[i] = 0;
    }
    return array;
}

double** CopyMatrix(int n, double** matrix)
{
    double** matrix_copy = CreateMatrix(n);
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++)
        {
            matrix_copy[i][j] = matrix[i][j];
        }
    }
    return matrix_copy;
}

double* CopyArray(int n, double* array)
{
    double* array_copy = CreateArray(n);
    for(int i = 0; i < n; i++) {
        array_copy[i] = array[i];
    }
    return array_copy;
}

void Dispose(int n, double** matrix)
{
    for(int i = 0; i < n; i++) {
        delete[] matrix[i];
    }
    delete[] matrix;
}

void Dispose(double* array)
{
    delete[] array;
}

double** InsertMatrix(int n, std::string name = "Matrix")
{
    double** matrix = CreateMatrix(n);
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            double value;
            std::cout << "Put value for " << name << "[" << i << "]" << "[" << j << "]: ";  
            std::cin >> value;
            matrix[i][j] = value;
        }
    }
    return matrix;
}

double* InsertArray(int n, std::string name = "Array")
{
    double* array = CreateArray(n);
    for(int i = 0; i < n; i++) {
        double value;
        std::cout << "Put value for " << name << "[" << i << "]: ";
        std::cin >> value;
        array[i] = value;
    }
    return array;
}

void PrintMatrix(double** matrix, int n, int m, std::string name = "")
{
    std::cout << name << ":" << std::endl;
    for(int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++)
        {
            printf("%.8e ", matrix[i][j]);
        }
        printf("\n");
    }
    printf("\n");
}

void PrintMatrix(double* matrix, int n, std::string name = "")
{
    std::cout << name << ":" << std::endl;
    for(int i = 0; i < n; i++)
    {
        printf("%.8e\n", matrix[i]);
    }
    printf("\n");
}

double** Multiply(double** matrix1, double** matrix2, int n)
{
    double** result = CreateMatrix(n);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            for(int k = 0; k < n; k++) {
                result[i][j] += matrix1[i][k] * matrix2[k][j];
            }
        }
    }
    return result;
}

double* Multiply(double** matrix1, double* matrix2, int n)
{
    double* result = CreateArray(n);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            for(int k = 0; k < n; k++) {
                result[i] += matrix1[i][k] * matrix2[k];
            }
        }
    }
    return result;
}

double* Multiply(double** matrix1, double n, double m, double* matrix2)
{
    double* result = CreateArray(n);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < m; j++) {
            result[i] += matrix1[i][j] * matrix2[j];
        }   
    }
        
    return result;
}

double** Multiply(double** matrix1, double n1, double m1, double** matrix2, double n2, double m2)
{
    double** result = CreateMatrix(n1, m2);

    for(int i = 0; i < n1; ++i)
    {
        for(int j = 0; j < m2; ++j)
        {
            for(int k = 0; k < m1; ++k)
            {
                result[i][j] += matrix1[i][k] * matrix2[k][j];
            }
        }   
    }
        
    return result;
}

void SwapRow(double** matrix, int row1, int row2, int n) 
{   
    for(int i = 0; i < n; i++)
    {
        double temp = matrix[row1][i];
        matrix[row1][i] = matrix[row2][i];
        matrix[row2][i] = temp;
    }
}

void SwapColumn(double** matrix, int col1, int col2, int n)
{
    for(int i = 0; i < n; i++)
    {
        double temp = matrix[i][col1];
        matrix[i][col1] = matrix[i][col2];
        matrix[i][col2] = temp;
    }
}

#pragma endregion

#pragma region Array/Matrix Math Functions

double** Transpose(double** matrix, int n)
{
    double** result = CreateMatrix(n);

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            result[i][j] = matrix[j][i];
        }  
    }
    return result;
}

double** Transpose(double** matrix, int n, int m)
{
    double** result = CreateMatrix(m, n);

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < m; j++) {
            result[j][i] = matrix[i][j];
        }  
    }
    return result;
}

void DooLittleDecomposition(int n, double** A, double** L, double** U, double** P)
{
    //Doolittle
    for(int k = 0; k < n; k++) {
        //Rows for U
        for(int j = k; j < n; j++) {
            double sum = 0;
            for(int p = 0; p < k; p++) {
                sum += L[k][p] * U[p][j];
            }
            U[k][j] = A[k][j] - sum;
        }
        //Search for max Ukj
        double ukj_max = 0;
        int j_max = 0;
        for(int j = k; j < n; j++) {
            if(abs(U[k][j]) > abs(ukj_max)) {
                ukj_max = U[k][j];
                j_max = j;
            }
        }
        if(k != j_max)
        {
            //Swapping columns
            SwapColumn(A, k, j_max, n);
            SwapColumn(U, k, j_max, n);
            SwapColumn(P, k, j_max, n);
        }
        
        //Columns for L
        for(int i = k; i < n; i++) {
            if(k != i) {
                double sum = 0;
                for(int p = 0; p < k; p++) {
                    sum += L[i][p] * U[p][k];
                }
                L[i][k] = (A[i][k] - sum) / U[k][k];
            }           
        }
    }
}

void SolveLinearEquation(int n, double** L, double** U, double** P, double* B, double* y, double* x, double* x_prim)
{
    //Forward substitution
    for(int i = 0; i < n; i++)
    {
        double sum = 0;
        for(int j = 0; j < i; j++)
        {
            sum += L[i][j]*y[j];
        }
        y[i] = (B[i] - sum)/L[i][i];
    }

    //Back substitution
    for(int i = n - 1; i >= 0; i--)
    {
        double sum = 0;
        for(int j = i + 1; j < n; j++)
        {
            sum += U[i][j]*x_prim[j];
        }
        x_prim[i] = (y[i] - sum)/U[i][i];
    }

    //Matrix multiplication for final x
    for(int i = 0; i < n; i++)
    {
        double sum = 0;
        for(int j = 0; j < n; j++)
        {
            sum += P[i][j] * x_prim[j];
        }
        x[i] = sum;
    }
}

double FindMax(int n, double* array)
{
    double max = -__DBL_MAX__;
    for(int i = 0; i < n; i++) {
        if(max < array[i]) {
            max = array[i];
        }
    }
    return max;
}

void ScaleMatrix(int n, double** A, double* B)
{
    for(int i = 0; i < n; i++)
    {
        double max = FindMax(n, A[i]);
        for(int j = 0; j < n; j++)
        {
            A[i][j] /= max;
        }
        B[i] /= max;
    }
}

double** InverseL(int n, double** L)
{
    double** L_inv = CreateMatrix(n);

    for(int k = 0; k < n; k++)
    {
        double* e = CreateArray(n);
        double* y = CreateArray(n);
        e[k] = 1;
        //Forward substitution
        for(int i = 0; i < n; i++)
        {
            double sum = 0;
            for(int j = 0; j < i; j++)
            {
                sum += L[i][j]*y[j];
            }
            y[i] = (e[i] - sum)/L[i][i];
        }

        //Assigning column to L
        for(int i = 0; i < n; i++)
        {
            L_inv[i][k] = y[i];
        }

        delete[] e;
        delete[] y;
    }
    
    return L_inv;
}

double** InverseU(int n, double** U)
{
    double** U_inv = CreateMatrix(n);

    for(int k = 0; k < n; k++)
    {
        double* e = CreateArray(n);
        double* z = CreateArray(n);
        e[k] = 1;
        //Back substitution
        for(int i = n - 1; i >= 0; i--)
        {
            double sum = 0;
            for(int j = i + 1; j < n; j++)
            {
                sum += U[i][j]*z[j];
            }
            z[i] = (e[i] - sum)/U[i][i];
        }

        //Assigning column to U
        for(int i = 0; i < n; i++)
        {
            U_inv[i][k] = z[i];
        }

        delete[] e;
        delete[] z;
    }

    return U_inv;
}

double Matrix1Form(int n, double** matrix)
{
    double max = 0;
    for(int i = 0; i < n; i++) {
        double sum = 0;
        for(int j = 0; j < n; j++) {
            sum += abs(matrix[j][i]);
        }
        if(sum > max) {
            max = sum;
        }    
    }
    return max;
}

double MatrixInfForm(int n, double** matrix)
{
    double max = 0;
    for(int i = 0; i < n; i++) {
        double sum = 0;
        for(int j = 0; j < n; j++) {
            sum += abs(matrix[i][j]);
        }
        if(sum > max) {
            max = sum;
        }    
    }
    return max;
}

double Array1Form(int n, double* array)
{
    double sum = 0;
    for(int i = 0; i < n; i++) {
        sum += abs(array[i]);
    }
    return sum;
}

double ArrayInfForm(int n, double* array)
{
    return FindMax(n, array);
}

#pragma endregion

double Func(double x, double k)
{
    return pow(x, k);
}

double CalculateChiSquared(int n, int m, double* a, double* x, double* y, double* sigma)
{
    double chisq = 0;
    for(int i = 0; i < n; i++)
    {
        double sum = 0;
        for(int k = 0; k < m; k++) 
        {
            sum += a[k] * Func(x[i], k);
        }
        chisq += pow(((y[i] - sum)/sigma[i]), 2);
    }
    return chisq;
}

int main()
{
    int n, m;
    // std::cout << "Enter n: ";
    // std::cin >> n;
    // std::cout << "Enter m: ";
    // std::cin >> m;

    //? Testing
    n = 20;
    m = 5;

    double* x = CreateArray(n);
    double* y = CreateArray(n);
    double* sigma = CreateArray(n);
    double* noise = new double[20] {  
        -0.282474,
         0.573539,
         0.061420,
        -1.888663,
        -0.411312,
         0.852528,
         0.841401,
        -1.072427,
        -0.586583,
         2.835644,
        -0.684718,
        -0.160293,
        -0.167703,
        -0.365004,
        -1.124204,
         0.189771,
        -0.750554,
        -0.725392,
        -1.591409,
        -0.971162 
    };
    
    double** A = CreateMatrix(n, m);
    double** A_T = CreateMatrix(m, n);
    double* b = CreateArray(n);
    double** alpha = CreateMatrix(m);
    double* beta = CreateArray(m);

    double** L = CreateMatrix(m);
    double** U = CreateMatrix(m);
    double** P = CreateMatrix(m);

    double* z = CreateArray(m);
    double* a = CreateArray(m);
    double* a_prim = CreateArray(m);

    //? Setting up x, y, sigma and b
    for(int i = 0; i < n; i++)
    {
        sigma[i] = 0.5;
        x[i] = i+1;
        y[i] = 4 + (3 * x[i]) + (2 * pow(x[i], 2)) + (pow(x[i], 3)) + noise[i];
        b[i] = y[i]/sigma[i];
    }
    //? Setting up L and P
    for(int i = 0; i < m; i++)
    {
        L[i][i] = 1;
        P[i][i] = 1;
    }

    //? Setting up A
    for(int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++)
        {
            A[i][j] = Func(x[i], j)/sigma[i];
        }
    }

    A_T = Transpose(A, n, m);
    alpha = Multiply(A_T, m, n, A, n, m);
    beta = Multiply(A_T, m, n, b);

    ScaleMatrix(m, alpha, beta);

    // PrintMatrix(sigma, n, "sigma");
    // PrintMatrix(x, n, "x");
    // PrintMatrix(y, n, "y");
    // PrintMatrix(A, n, m, "A");
    // PrintMatrix(A_T, m, n, "A^T");
    // PrintMatrix(b, n, "b");
    PrintMatrix(alpha, m, m, "alpha");
    PrintMatrix(beta, m, "beta");

    DooLittleDecomposition(m, alpha, L, U, P);

    P = Transpose(P, m);
    alpha = Multiply(alpha, P, m);

    SolveLinearEquation(m, L, U, P, beta, z, a, a_prim);

    // PrintMatrix(L, m, m, "L");
    // PrintMatrix(U, m, m, "U");
    // PrintMatrix(P, m, m, "P");
    PrintMatrix(a, m, "a");

    double** L_inv = InverseL(m, L);
    double** U_inv = InverseU(m, U);
    double** P_inv = Transpose(P, m);
    double** PU_inv = Multiply(P_inv, U_inv, m);
    double** alpha_inv = Multiply(PU_inv, L_inv, m);
    double** alpha_alpha_inv = Multiply(alpha, alpha_inv, m);

    //Matrix cond by definition
    double alpha_1Form = Matrix1Form(m, alpha);
    double alpha_infForm = MatrixInfForm(m, alpha);
    double alpha_inv_1Form = Matrix1Form(m, alpha_inv);
    double alpha_inv_infForm = MatrixInfForm(m, alpha_inv);

    // printf("norm alpha: %.8e\n", alpha_infForm);
    // printf("norm alpha^-1: %.8e\n", alpha_inv_infForm);
    printf("cond(alpha) (1): %.8e\n", alpha_1Form * alpha_inv_1Form);
    printf("cond(alpha) (Inf): %.8e\n\n", alpha_infForm * alpha_inv_infForm);

    printf("Chi^2: %.8e\n\n", CalculateChiSquared(n, m, a, x, y, sigma));
    PrintMatrix(alpha_inv, m, m, "Covariant matrix (alpha)");

    // double* check = Multiply(alpha, m, m, a);
    // PrintMatrix(check, m, "check");
    // PrintMatrix(beta, m, "beta");
    // PrintMatrix(alpha, m, m, "alpha");

    return 0;
}