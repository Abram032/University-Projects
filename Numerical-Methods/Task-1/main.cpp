#include<stdio.h>
#include<stdlib.h>
#include<iostream>
#include<math.h>

#define c_n 10000

//N | S(a) | S(b) | (S(a) - S(dokl))/S(dokl) | (S(b) - S(dokl))/S(dokl)
//.     .      .            .                           .
//.     .      .            .                           .
//.     .      .            .                           .

template<class T>
T* SumForward(int n, int N, double * S_exact) 
{
    T * sum_arr = new T[N];
    T sum = 0;
    double sum_exact = 0;
    for(int i = n; i <= N; i++)
    {
        sum += 1 /(T)i;
        sum_exact += 1 /(double)i;
        sum_arr[i - 1] = sum;
        S_exact[i - 1] = sum_exact;
    }
    return sum_arr;
}

template<class T>
T* SumReversed(int n, int N, double * S_exact)
{
    T * sum_arr = new T[N];
    T sum = 0;
    double sum_exact = 0;
    for(int i = N; i >= n; i--)
    {
        sum += 1 /(T)i;
        sum_exact += 1 /(double)i;
        sum_arr[N - i] = sum;
        S_exact[N - i] = sum_exact;
    }
    return sum_arr;
}

float SumReversed_v2(int n, int N, double &S_exact)
{
    float sum = 0;
    for(int i = N+1; i >= n; i--)
    {
        sum += 1 /(float)i;
        S_exact += 1 /(double)i;
    }
    return sum;
}

int main()
{
    int n = 1, N = 1000000;

    double * Sa_exact_arr = new double[N];
    double * Sb_exact_arr = new double[N];
    float * Sa_arr = SumForward<float>(n, N, Sa_exact_arr);
    float * Sb_arr = SumReversed<float>(n, N, Sb_exact_arr);

    FILE * output;
    output = fopen("output.txt", "w");

    printf("i\t|S(a)\t|S(b)\t|S_e(a)\t|S_e(b)\n");
    for(int i = 1; i < N; i++)
    {
        if((i % c_n == 0) || (i == N - 1))
        {
            double Sb_exact = 0;
            double Sb = SumReversed_v2(1, i, Sb_exact);
            double Sa_diff = ((Sa_arr[i] - Sa_exact_arr[i]) / Sa_exact_arr[i]);
            double Sb_diff = ((Sb - Sb_exact) / Sb_exact);
            printf("%d\t%.20e\t%.20e\t%.20e\t%.20e\n", i, Sa_arr[i], Sb, Sa_diff, Sb_diff);
            fprintf(output, "%d\t%.20e\t%.20e\t%.20e\t%.20e\n", i, Sa_arr[i], Sb, Sa_diff, Sb_diff);
        }
    }
    return 0;
}