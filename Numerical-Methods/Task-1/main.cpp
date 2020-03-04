#include<stdio.h>
#include<stdlib.h>
#include<iostream>

//N | S(a) | S(b) | (S(a) - S(dokl))/S(dokl) | (S(b) - S(dokl))/S(dokl)
//.     .      .            .                           .
//.     .      .            .                           .
//.     .      .            .                           .

template<class T>
T* SumForward(int n, int N, T * S_exact, int print_n) 
{
    T * sum_arr = new T[print_n];
    T sum = 0;
    for(int i = n, print_i = 0; i <= N; i++)
    {
        sum += 1 /(T)i;
        if(i % 10000 == 0)
        {
            sum_arr[print_i] = sum;
            S_exact[print_i] = sum;
            print_i++;
        }
    }
    return sum_arr;
}

template<class T>
T* SumReversed(int n, int N, T * S_exact, int print_n)
{
    T * sum_arr = new T[print_n];
    T sum = 0;
    for(int i = N, print_i = 0; i >= n; i--)
    {
        sum += 1 / (T)i;
        if(i % 10000 == 0)
        {
            sum_arr[print_i] = sum;
            S_exact[print_i] = sum;
            print_i++;
        }
    }
    return sum_arr;
}

int main()
{
    int n = 1, N = 1000000;
    int print_n = N / 10000;
    float Sa_f, Sb_f, Se_f;
    double Sa_d, Sb_d, Se_d;

    // double * S_exact_arr = new double[N + 1];
    // double * Sa_arr = SumForward_D(n, N, S_exact_arr);
    // for(int i = n; i <= N; i++)
    // {
    //     printf("%d: %.20f\t%.20f\n", i, Sa_arr[i - 1], S_exact_arr[i - 1]);
    // }

    double * Sa_exact_arr = new double[print_n];
    double * Sa_arr = SumForward<double>(n, N, Sa_exact_arr, print_n);
    for(int i = 0; i <= print_n; i++)
    {
        printf("%d: %.20f\t%.20f\n", i * 10000, Sa_arr[i - 1], Sa_exact_arr[i - 1]);
    }

    double * Sb_exact_arr = new double[print_n];
    double * Sb_arr = SumReversed<double>(n, N, Sb_exact_arr, print_n);
    for(int i = 0; i <= print_n; i++)
    {
        printf("%d: %.20f\t%.20f\n", i * 10000, Sb_arr[i - 1], Sb_exact_arr[i - 1]);
    }


    // printf("Sa_f\n");
    // Sa_f = SumForward_F(n, N);
    // printf("Sb_f\n");
    // Sb_f = SumReversed_F(n, N);
    // printf("Sa_f = %.20f\n", Sa_f);
    // printf("Sb_f = %.20f\n", Sb_f);


    // printf("Sa_d\n");
    // Sa_d = SumForward_D(n, N);
    // printf("Sb_d\n");
    // Sb_d = SumReversed_D(n, N);
    // printf("Sa_d = %.20f\n", Sa_d);
    // printf("Sa_d = %.20f\n", Sb_d);

    return 0;
}