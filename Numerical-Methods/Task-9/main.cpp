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
            printf("%.4f ", matrix[i][j]);
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
            double value = matrix1[i][j] - matrix2[i][j];
            result[i][j] = value;
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

double VectorLength(double** vector, int n) {
    double sum = 0;
    for(int i = 0; i < n; i++) {
        sum += pow(vector[i][0], 2);
    }
    return sqrt(sum);
}

double** NormalizeVector(double** vector, int n) {
    double len = VectorLength(vector, n);
    double** result = CreateMatrix(n, 1);

    for(int i = 0; i < n; i++) {
        result[i][0] = vector[i][0] / len;
    }

    return result;
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
    double** I = CreateMatrix(n, n);
    double** lambda = CreateMatrix(n, 1);

    //?Testing
    A[0] = new double[4] {2, 13, -14, 3};
    A[1] = new double[4] {-2, 25, -22, 4};  
    A[2] = new double[4] {-3, 31, -27, 5};
    A[2] = new double[4] {-2, 34, -32, 7};
    //A[0] = new double[3] {1,1,1};
    //A[1] = new double[3] {2,-2,2};
    //A[2] = new double[3] {3,3,-3};

   // lambda[0] = 2.76300328661375;
   // lambda[1] = -1.723685894982085;
    //lambda[2] = -5.03931739163167;
    lambda[0][0] = 3;
    lambda[1][0] = 2;
    lambda[2][0] = 1;
    lambda[3][0] = 1;

    for(int i = 0; i < n; i++) {
        I[i][i] = 1;
    }

    PrintMatrix(A, n, n, "A");
    PrintMatrix(I, n, n, "I");

    // double** A = InsertMatrix(n, "A");
    // std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(lambda, n, 1, "lambda");  

    for(int i = 0; i < n; i++)
    {
        printf("============> i = %d <============\n\n", i);
        //Calculating λ * I
        double** I_lambda = Multiply(I, n, n, lambda[i][0]);
        //Subtracting A - Iλ
        double** A_lambda = Subtract(A, I_lambda, n, n);

        PrintMatrix(I_lambda, n, n, "I_lambda");
        PrintMatrix(A_lambda, n, n, "A_lambda");

        double** L = CreateMatrix(n, n);
        double** U = CreateMatrix(n, n);
        double** P = CreateMatrix(n, n);
        
        //? Setting up L and P
        for(int j = 0; j < n; j++)
        {
            L[j][j] = 1;
            P[j][j] = 1;
        }

        DooLittleDecomposition(n, A_lambda, L, U, P);

        PrintMatrix(L, n, n, "L");
        PrintMatrix(U, n, n, "U");
        PrintMatrix(P, n, n, "P");

        //Solving Ux'_i = 0
        double** x_prim = CreateMatrix(n, 1);
        //Setting last value to 1
        x_prim[n-1][0] = 1;

        for(int j = n - 2; j >= 0; j--)
        {
            double sum = 0;
            for(int k = j + 1; k < n; k++)
            {
                sum += U[j][k]*x_prim[k][0];
            }
            x_prim[j][0] = (0 - sum)/U[j][j];
        }
        
        //Getting final vector
        PrintMatrix(x_prim, n, 1, "x_prim_i");
        //double** P_Tr = Transpose(P, n, n);
        double** x = Multiply(P, n, n, x_prim, n, 1);
        PrintMatrix(x, n, 1, "x_i");
        //Normalization
        double** x_norm = NormalizeVector(x, n);
        PrintMatrix(x_norm, n, 1, "x_norm");

        //A*xi
        double** A_xi = Multiply(A, n, n, x, n, 1);
        PrintMatrix(A_xi, n, 1, "A_xi");
        //λ*xi
        double** lambda_xi = Multiply(lambda, n, 1, x, n, 1);
        PrintMatrix(lambda_xi, n, 1, "lambda_xi");

        Dispose(n, A_lambda);
        Dispose(n, I_lambda);
        Dispose(n, L);
        Dispose(n, U);
        Dispose(n, P);
        Dispose(n, x_prim);
        Dispose(n, x);
        Dispose(n, x_norm);
        Dispose(n, A_xi);
        Dispose(n, lambda_xi);
    }

    Dispose(n, A);
    Dispose(n, lambda);

    return 0;
}