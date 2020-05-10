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

double* Multiply(double** matrix1, int n, int m, double* matrix2)
{
    double* result = CreateArray(n);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < m; j++) {
            result[i] += matrix1[i][j] * matrix2[j];
        }   
    }
        
    return result;
}

double** Multiply(double** matrix1, int n1, int m1, double** matrix2, int n2, int m2)
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

double** Multiply(double** matrix, int n, int m, double scalar)
{
    double** result = CreateMatrix(n,m);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            result[i][j] = matrix[i][j] * scalar;
        }
    }

    return result;
}

double** Subtract(double** matrix1, double** matrix2, int n, int m) 
{
    double** result = CreateMatrix(n,m);

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            result[i][j] = matrix1[i][j] - matrix2[i][j];
        }
    }

    return result;
}

bool Equals(double a, double b)
{
    return (abs(a) - abs(b)) < __DBL_EPSILON__;
}

bool Equals(double** matrix1, double** matrix2, int n, int m)
{
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            if(Equals(matrix1[i][j], matrix2[i][j]) == false) {
                return false;
            }
        }
    }
    return true;
}

void Clear(double** matrix, int n, int m)
{
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            matrix[i][j] = 0;
        }
    }
}

void Clear(double* array, int n)
{
    for(int i = 0; i < n; i++)
    {
        array[i] = 0;
    }
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

int sign(double value) {
    if(value > 0) {
        return 1;
    } else if(value < 0) {
        return -1;
    } else {
        return 0;
    }
}

int main()
{
    int n;
    // std::cout << "Enter n: ";
    // std::cin >> n;

    //? Testing
    n = 4;

    double** A = CreateMatrix(n, n);

    //?Testing
    A[0] = new double[4] {1, 2, 3, 5};
    A[1] = new double[4] {2, 3, 4, 5};  
    A[2] = new double[4] {3, 4, 5, 6};  
    A[3] = new double[4] {5, 5, 6, 8}; 

    // double** A = InsertMatrix(n, "A");
    // std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(A, n, n, "A");

    double** Q = CreateMatrix(n);
    double** R = CreateMatrix(n);
    double** Ak = CopyMatrix(n, A);
    double** Ak1 = CreateMatrix(n);
    double** I = CreateMatrix(n);

    for(int i = 0; i < n; i++) {
        I[i][i] = 1;
    }
 
    int k = 1;
    for(int k = 1; k < n; k++)
    {
        printf("Iteration k = %d\n\n", k);

        double** x = CreateMatrix(n, 1);
        double sk = 0;
        for(int i = k; i < n; i++)
        {
            sk += pow(Ak[i][k], 2);
        }
        sk = sqrt(sk);
        Clear(x, 1, n);
        if(sk != 0) {
            double value = sqrt(1.0/2.0 * (1.0 + (Ak[k][k]/sk)));
            x[k][0] = value;
            for(int i = k + 1; i < n; i++)
            {
                x[i][0] = (sign(A[k][k]) / (2 * sk * x[k][0])) * A[i][k];
            }
        }

        PrintMatrix(x, n, 1, "x");
        
        double** x_Tr = Transpose(x, n, 1);
        double** x_xTr = Multiply(x, n, 1, x_Tr, 1, n);
        double** s_xxTr = Multiply(x_xTr, n, n, 2);
        double** Qk = Subtract(I, s_xxTr, n, n);
        double** Qk_Tr = Transpose(Qk, n, n);
        double** Rk = Multiply(Qk_Tr, n, n, Ak, n, n);
        Ak1 = Multiply(Rk, n, n, Qk, n, n);
        double** Ak_check = Multiply(Qk, n, n, Rk, n, n);
        PrintMatrix(x, n, 1, "x");
        PrintMatrix(x_Tr, 1, n, "x_Tr");
        PrintMatrix(x_xTr, n, n, "x_xTr");
        PrintMatrix(s_xxTr, n, n, "s_xxTr");
        PrintMatrix(Ak, n, n, "Ak");
        PrintMatrix(Ak_check, n, n, "Ak_check");
        PrintMatrix(Qk, n, n, "Qk");
        PrintMatrix(Qk_Tr, n, n, "Qk_Tr");
        PrintMatrix(Rk, n, n, "Rk");
        PrintMatrix(Ak1, n, n, "Ak1");

        if(Equals(Ak, Ak_check, n, n)) {
        printf("matrixes are equal\n");
        } else {
            printf("matrixes are not equal\n");
        }

        Ak = CopyMatrix(n, Ak1);

        Dispose(1, x_Tr);
        Dispose(n, x_xTr);
        Dispose(n, s_xxTr);
    }
    
    Dispose(n, A);
    Dispose(n, Ak);
    Dispose(n, Ak1);
    Dispose(n, Q);
    Dispose(n, R);

    return 0;
}