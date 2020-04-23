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
            printf("%.8e ", matrix[i][j]);
        }
        printf("\n");
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

    //Initial setup for L
    for(int i = 0; i < n; i++) {
        L[i][i] = 1;
    }

    //LU decomposition (Doolittle method)
    for(int k = 0; k < n; k++) {
        //Rows for U
        for(int j = k; j < n; j++) {
            double sum = 0;
            for(int p = 0; p < k; p++) {
                sum += L[k][p] * U[p][j];
            }
            double result = A[k][j] - sum;
            U[k][j] = result;
        }
        //Columns for L
        for(int i = k; i < n; i++) {
            if(k != i) {
                double sum = 0;
                for(int p = 0; p < k; p++) {
                    sum += L[i][p] * U[p][k];
                }
                double result = (A[i][k] - sum) / U[k][k];
                L[i][k] = result;
            }           
        }
    }

    PrintMatrix(L, n, n, "L");
    PrintMatrix(U, n, n, "U");

    double** LU = Multiply(L, U, n);
    PrintMatrix(LU, n, n, "A = LU");
    
    //! PART 2 - LU factorization with partial pivoting

    PrintMatrix(A, n, n, "A");

    L = new double* [n];
    U = new double* [n];
    P = new double* [n];
    for(int i = 0; i < n; i++) {
        L[i] = new double[n];
        U[i] = new double[n];
        P[i] = new double[n];
    }

    //Setup for P
    for(int i = 0; i < n; i++) {
        P[i][i] = 1;
    }

    //Initial setup for L
    for(int i = 0; i < n; i++) {
        L[i][i] = 1;
    }

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

    PrintMatrix(L, n, n, "L");
    PrintMatrix(U, n, n, "U");
    PrintMatrix(P, n, n, "P");

    LU = Multiply(L, U, n);
    //Transposing P
    P = Transpose(P, n);
    double** LUP = Multiply(LU, P, n);
    PrintMatrix(LUP, n, n, "A = LUP");
    //Release resources
    for(int i = 0; i < n; i++) {
        delete[] A[i];
        delete[] L[i];
        delete[] U[i];
        delete[] P[i];
        delete[] LU[i];
        delete[] LUP[i];
    }
    delete[] A;
    delete[] L;
    delete[] U;
    delete[] P;
    delete[] LU;
    delete[] LUP;

    return 0;
}