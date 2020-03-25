#include<stdio.h>
#include<stdlib.h>
#include<bitset>
#include<iostream>
#include<iomanip>
#include<math.h>
#include<string.h>
//? Gowno copy pasta

void getCofactor(double** A, double** temp, int p, int q, int n) 
{ 
    int i = 0, j = 0; 
  
    // Looping for each element of the matrix 
    for (int row = 0; row < n; row++) 
    { 
        for (int col = 0; col < n; col++) 
        { 
            //  Copying into temporary matrix only those element 
            //  which are not in given row and column 
            if (row != p && col != q) 
            { 
                temp[i][j++] = A[row][col]; 
  
                // Row is filled, so increase row index and 
                // reset col index 
                if (j == n - 1) 
                { 
                    j = 0; 
                    i++; 
                } 
            } 
        } 
    } 
} 
  
/* Recursive function for finding determinant of matrix. 
   n is current dimension of A[][]. */
int determinant(double** A, int n) 
{ 
    int D = 0; // Initialize result 
  
    //  Base case : if matrix contains single element 
    if (n == 1) 
        return A[0][0]; 
  
    double** temp = new double*[n];
    for(int i = 0; i < n; i++) {
        temp[i] = new double[n];
    }

    int sign = 1;  // To store sign multiplier 
  
     // Iterate for each element of first row 
    for (int f = 0; f < n; f++) 
    { 
        // Getting Cofactor of A[0][f] 
        getCofactor(A, temp, 0, f, n); 
        D += sign * A[0][f] * determinant(temp, n - 1); 
  
        // terms are to be added with alternate sign 
        sign = -sign; 
    } 
  
    return D; 
} 
  
// Function to get adjoint of A[N][N] in adj[N][N]. 
void adjoint(double** A, double** adj, int n) 
{ 
    if (n == 1) 
    { 
        adj[0][0] = 1; 
        return; 
    } 
  
    // temp is used to store cofactors of A[][] 
    double sign = 1;
    double** temp = new double*[n];
    for(int i = 0; i < n; i++) {
        temp[i] = new double[n];
    }
  
    for (int i=0; i<n; i++) 
    { 
        for (int j=0; j<n; j++) 
        { 
            // Get cofactor of A[i][j] 
            getCofactor(A, temp, i, j, n); 
  
            // sign of adj[j][i] positive if sum of row 
            // and column indexes is even. 
            sign = ((i+j)%2==0)? 1: -1; 
  
            // Interchanging rows and columns to get the 
            // transpose of the cofactor matrix 
            adj[j][i] = (sign)*(determinant(temp, n-1)); 
        } 
    } 
} 
  
// Function to calculate and store inverse, returns false if 
// matrix is singular 
bool inverse(double** A, double** inverse, int n) 
{
    // Find determinant of A[][] 
    int det = determinant(A, n); 
    if (det == 0) 
    { 
        return false; 
    } 
  
    // Find adjoint 
    double** adj = new double*[n];
    for(int i = 0; i < n; i++) {
        adj[i] = new double[n];
    }
    adjoint(A, adj, n); 
  
    // Find Inverse using formula "inverse(A) = adj(A)/det(A)" 
    for (int i=0; i<n; i++) 
        for (int j=0; j<n; j++) 
            inverse[i][j] = adj[i][j]/double(det); 
  
    return true; 
} 

//? koniec gonwo copy pasty

void PrintMatrix(double** matrix, int n, int m, std::string name = "")
{
    std::cout << name << ":" << std::endl;
    for(int i = 0; i < n; i++)
    {
        for(int j = 0; j < m; j++)
        {
            printf("%.2f ", matrix[i][j]);
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
    //Swapping one of the rows
    int row1, row2;
    std::cout << "Which row to swap: ";
    std::cin >> row1;
    std::cout << "Target row to swap with: ";
    std::cin >> row2;
    SwapRow(A, 1, 2, n);
    PrintMatrix(A, n, n, "A' (Swapped row)");

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
        //Swapping columns
        SwapColumn(A, k, j_max, n);
        SwapColumn(U, k, j_max, n);
        SwapColumn(P, k, j_max, n);

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
    PrintMatrix(U, n, n, "U'");
    PrintMatrix(P, n, n, "P");

    LU = Multiply(L, U, n);
    PrintMatrix(LU, n, n, "A = LU'");
    // double** LUP = Multiply(LU, P, n);
    // PrintMatrix(LUP, n, n, "A = LU'P");

    double** UP = Multiply(U, P, n);
    PrintMatrix(UP, n, n, "U (?)");
    double** LUP = Multiply(L, UP, n);
    PrintMatrix(LUP, n, n, "A = LUP (?)");

    double** inverse_P = new double* [n];
    for(int i = 0; i < n; i++) {
        inverse_P[i] = new double[n];
    }
    inverse(P, inverse_P, n);
    PrintMatrix(P, n, n, "P^(-1) (?)");

    double** real_U = Multiply(U, inverse_P, n);
    PrintMatrix(real_U, n, n, "U (?)");

    LUP = Multiply(L, real_U, n);
    PrintMatrix(LU, n, n, "A = LUP (?)");

    //Release resources
    for(int i = 0; i < n; i++) {
        delete[] A[i];
        delete[] L[i];
        delete[] U[i];
        delete[] P[i];
        delete[] LU[i];
    }
    delete[] A;
    delete[] L;
    delete[] U;
    delete[] P;
    delete[] LU;

    return 0;
}