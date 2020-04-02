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
    
}