#include<stdio.h>
#include<stdlib.h>
#include<bitset>
#include<iostream>
#include<iomanip>
#include<math.h>
#include<string.h>

void PrintMatrix(double** matrix, int n, int m, std::string name = "")
{
    std::cout << name << ":" << std::endl;
    for(int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++)
        {
            printf("%.8f ", matrix[i][j]);
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
        printf("%.8f\n", matrix[i]);
    }
    printf("\n");
}

double** Multiply(double** matrix1, double** matrix2, int n)
{
    double** result = new double*[n];
    for(int i = 0; i < n; i++) {
        result[i] = new double[n];
    }

    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            for(int k = 0; k < n; k++) {
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

double** Transpose(double** matrix, int n)
{
    double** result = new double* [n];
    for(int i = 0; i < n; i++) {
        result[i] = new double[n];
    }

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            result[i][j] = matrix[j][i];
        }  
    }
    return result;
}

int main()
{
    int n;
    std::cout << "Enter n of the matrix: ";
    std::cin >> n;

    //Creating matrixes
    double** A = new double* [n];
    for(int i = 0; i < n; i++) {
        A[i] = new double[n];
    }
    double* B = new double[n];
    double* y = new double[n];
    double* x = new double[n];

    //! PART 1 - LU Decomposition
    double** L = new double* [n];
    double** U = new double* [n];
    double** P = new double* [n];
    for(int i = 0; i < n; i++) {
        L[i] = new double[n];
        U[i] = new double[n];
        P[i] = new double[n];
    }

    //Inserting data
    for(int i = 0; i < n; i++) {
        for(int j = 0; j < n; j++) {
            double value;
            printf("Put value for A[%d][%d]: ", i, j);
            std::cin >> value;
            A[i][j] = value;
        }
    }

    std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(A, n, n, "A");

    for(int i = 0; i < n; i++) {
        double value;
        printf("Put value for B[%d]: ", i);
        std::cin >> value;
        B[i] = value;
    }

    std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(B, n, "B");

    //Initial setup for L
    for(int i = 0; i < n; i++) {
        L[i][i] = 1;
    }

    //Setup for P
    for(int i = 0; i < n; i++) {
        P[i][i] = 1;
    }

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

    P = Transpose(P, n);

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
    double* x_prim = new double[n];
    for(int i = n - 1; i >= 0; i--)
    {
        double sum = 0;
        for(int j = i + 1; j < n; j++)
        {
            sum += U[i][j]*x_prim[j];
        }
        x_prim[i] = (y[i] - sum)/U[i][i];
    }

    PrintMatrix(L, n, n, "L");
    PrintMatrix(U, n, n, "U");
    PrintMatrix(P, n, n, "P^T");

    PrintMatrix(y, n, "y");
    PrintMatrix(x_prim, n, "x\'");

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

    PrintMatrix(x, n, "x");

    for(int i = 0; i < n; i++) {
        delete[] A[i];
        delete[] L[i];
        delete[] U[i];
        delete[] P[i];
    }
    delete[] A;
    delete[] B;
    delete[] L;
    delete[] U;
    delete[] P;
    delete[] x;
    delete[] x_prim;
    delete[] y;

    return 0;
}