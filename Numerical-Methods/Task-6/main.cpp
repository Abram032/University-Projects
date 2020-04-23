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

int main()
{
    int n;
    std::cout << "Enter n of the matrix: ";
    std::cin >> n;

    //? Testing
    n = 5;

    //Creating matrixes
    double** L = CreateMatrix(n);
    double** U = CreateMatrix(n);
    double** P = CreateMatrix(n);
    double* y = CreateArray(n);
    double* x = CreateArray(n);
    double* x_prim = CreateArray(n);

    //? Testing
    double** A = new double*[n];
    A[0] = new double[5] {2.1000000000e+01, 0.0000000000e+00, 7.7000000000e+02, 0.0000000000e+00, 5.0666000000e+04};
    A[1] = new double[5] {0.0000000000e+00, 7.7000000000e+02, 0.0000000000e+00, 5.0666000000e+04, 0.0000000000e+00};  
    A[2] = new double[5] {7.7000000000e+02, 0.0000000000e+00, 5.0666000000e+04, 0.0000000000e+00, 3.9568100000e+06};  
    A[3] = new double[5] {0.0000000000e+00, 5.0666000000e+04, 0.0000000000e+00, 3.9568100000e+06, 0.0000000000e+00}; 
    A[4] = new double[5] {5.0666000000e+04, 0.0000000000e+00, 3.9568100000e+06, 0.0000000000e+00, 3.3546266600e+08};

    // double** A = InsertMatrix(n, "A");
    // std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(A, n, n, "A");

    //? Testing
    double* B = new double[5] {1.5278900000e+05, 1.0210200000e+05, 1.1921866000e+07, 7.9642860000e+06, 1.0103954740e+09};
    
    //double* B = InsertArray(n, "B");
    //std::cout << "Inserted matrix:" << std::endl;
    PrintMatrix(B, n, "B");

    //Initial setup for L and P
    for(int i = 0; i < n; i++) {
        L[i][i] = 1;
        P[i][i] = 1;
    }

    //! Matrix scaling
    double** A_scaled = CopyMatrix(n, A);
    double* B_scaled = CopyArray(n, B);
    double** L_scaled = CopyMatrix(n, L);
    double** U_scaled = CopyMatrix(n, U);
    double** P_scaled = CopyMatrix(n, P);
    double* y_scaled = CreateArray(n);
    double* x_scaled = CreateArray(n);
    double* x_scaled_prim = CreateArray(n);

    ScaleMatrix(n, A_scaled, B_scaled);

    PrintMatrix(A_scaled, n, n, "A_scaled");
    PrintMatrix(B_scaled, n, "B_scaled");

    //!Original matrix

    DooLittleDecomposition(n, A, L, U, P);

    P = Transpose(P, n);
    A = Multiply(A, P, n);

    SolveLinearEquation(n, L, U, P, B, y, x, x_prim);

    double** L_inv = InverseL(n, L);
    double** U_inv = InverseU(n, U);
    double** P_inv = Transpose(P, n);
    double** PU_inv = Multiply(P_inv, U_inv, n);
    double** A_inv = Multiply(PU_inv, L_inv, n);
    double** AA_inv = Multiply(A, A_inv, n);

    //Matrix cond by definition
    double A_1Form = Matrix1Form(n, A);
    double A_infForm = MatrixInfForm(n, A);
    double A_inv_1Form = Matrix1Form(n, A_inv);
    double A_inv_infForm = MatrixInfForm(n, A_inv);
    //Matrix cond by estimation
    double x_1Form = Array1Form(n, x);
    double B_1Form = Array1Form(n, B);
    double x_infForm = ArrayInfForm(n, x);
    double B_infForm = ArrayInfForm(n, B);

    PrintMatrix(A_inv, n, n, "A^(-1)");
    PrintMatrix(AA_inv, n, n, "A*A^(-1)");
    PrintMatrix(L, n, n, "L");
    PrintMatrix(L_inv, n, n, "L^(-1)");
    PrintMatrix(U, n, n, "U");
    PrintMatrix(U_inv, n, n, "U^(-1)");
    PrintMatrix(P, n, n, "P^T");
    PrintMatrix(y, n, "y");
    PrintMatrix(x_prim, n, "x\'");
    PrintMatrix(x, n, "x");
    // printf("A 1-Form: %.8f\n", A_1Form);
    // printf("A^(-1) 1-Form: %.8f\n", A_inv_1Form);
    // printf("A Inf-Form: %.8f\n", A_infForm);
    // printf("A^(-1) Inf-Form: %.8f\n", A_inv_infForm);
    printf("cond(A) (1): %.8f\n", A_1Form * A_inv_1Form);
    printf("cond(A) (Inf): %.8f\n", A_infForm * A_inv_infForm);
    printf("Estimated cond(A) (1): %.8f\n", (A_1Form * x_1Form) / B_1Form);
    printf("Estimated cond(A) (Inf): %.8f\n", (A_infForm * x_infForm) / B_infForm);

    printf("\n\n");
    //!Scaled matrix

    DooLittleDecomposition(n, A_scaled, L_scaled, U_scaled, P_scaled);

    P = Transpose(P_scaled, n);
    A_scaled = Multiply(A_scaled, P_scaled, n);

    SolveLinearEquation(n, L_scaled, U_scaled, P_scaled, B_scaled, y_scaled, x_scaled, x_scaled_prim);

    double** L_scaled_inv = InverseL(n, L_scaled);
    double** U_scaled_inv = InverseU(n, U_scaled);
    double** P_scaled_inv = Transpose(P_scaled, n);
    double** PU_scaled_inv = Multiply(P_scaled_inv, U_scaled_inv, n);
    double** A_scaled_inv = Multiply(PU_scaled_inv, L_scaled_inv, n);
    double** AA_scaled_inv = Multiply(A_scaled, A_scaled_inv, n);

    //Matrix cond by definition
    double A_scaled_1Form = Matrix1Form(n, A_scaled);
    double A_scaled_infForm = MatrixInfForm(n, A_scaled);
    double A_scaled_inv_1Form = Matrix1Form(n, A_scaled_inv);
    double A_scaled_inv_infForm = MatrixInfForm(n, A_scaled_inv);

    //Matrix cond by estimation
    double x_scaled_1Form = Array1Form(n, x_scaled);
    double B_scaled_1Form = Array1Form(n, B_scaled);
    double x_scaled_infForm = ArrayInfForm(n, x_scaled);
    double B_scaled_infForm = ArrayInfForm(n, B_scaled);

    PrintMatrix(A_scaled_inv, n, n, "A^(-1) (Scaled)");
    PrintMatrix(AA_scaled_inv, n, n, "A*A^(-1) (Scaled)");
    PrintMatrix(L_scaled, n, n, "L (Scaled)");
    PrintMatrix(L_scaled_inv, n, n, "L^(-1) (Scaled)");
    PrintMatrix(U_scaled, n, n, "U (Scaled)");
    PrintMatrix(U_scaled_inv, n, n, "U^(-1) (Scaled)");
    PrintMatrix(P_scaled, n, n, "P^T (Scaled)");
    PrintMatrix(y_scaled, n, "y (Scaled)");
    PrintMatrix(x_scaled_prim, n, "x\' (Scaled)");
    PrintMatrix(x_scaled, n, "x (Scaled)");
    // printf("A (Scaled) 1-Form: %.8f\n", A_scaled_1Form);
    // printf("A^(-1) (Scaled) 1-Form: %.8f\n", A_scaled_inv_1Form);
    // printf("A (Scaled) Inf-Form: %.8f\n", A_scaled_infForm);
    // printf("A^(-1) (Scaled) Inf-Form: %.8f\n", A_scaled_inv_infForm);
    printf("cond(A (Scaled)) (1): %.8f\n", A_scaled_1Form * A_scaled_inv_1Form);
    printf("cond(A (Scaled)) (Inf): %.8f\n", A_scaled_infForm * A_scaled_inv_infForm);
    printf("Estimated cond(A (Scaled)) (1): %.8f\n", (A_scaled_1Form * x_scaled_1Form) / B_scaled_1Form);
    printf("Estimated cond(A (Scaled)) (Inf): %.8f\n", (A_scaled_infForm * x_scaled_infForm) / B_scaled_infForm);

    //!Structures disposal

    Dispose(n, A);
    Dispose(n, L);
    Dispose(n, L_inv);
    Dispose(n, U);
    Dispose(n, U_inv);
    Dispose(n, P);
    Dispose(B);
    Dispose(x);
    Dispose(x_prim);
    Dispose(y);

    Dispose(n, A_scaled);
    Dispose(n, L_scaled);
    Dispose(n, L_scaled_inv);
    Dispose(n, U_scaled);
    Dispose(n, U_scaled_inv);
    Dispose(n, P_scaled);
    Dispose(B_scaled);
    Dispose(x_scaled);
    Dispose(x_scaled_prim);
    Dispose(y_scaled);

    return 0;
}