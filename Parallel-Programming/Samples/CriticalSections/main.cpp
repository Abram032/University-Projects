#include <omp.h>
#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#define N 101

int main() 
{
    char* env = std::getenv("OMP_NUM_THREADS");
    int thread_num = std::stoi(env);
    int i = 0;

    int un = 0;
    #pragma omp parallel for private(i) shared(un)
        for(i = 1; i < N; i++) {
            un += i;
        }

    int cn = 0;
    #pragma omp parallel for private(i) shared(cn)
    for(i = 1; i < N; i++) {
        #pragma omp critical
        cn += i;
    }

    int an = 0;
    #pragma omp parallel for private(i) shared(an)
    for(i = 1; i < N; i++) {
        #pragma omp atomic update
        an += i;
    }

    int rn = 0;
    #pragma omp parallel for private(i) reduction(+:rn)
    for(i = 1; i < N; i++) {
        rn += i;
    }

    int en = 0;
    for(i = 1; i < N; i++) {
        en += i;
    }
    
    printf("Threads amount: %d\n", thread_num);
    printf("Unsafe n: %d\n", un);
    printf("Critical n: %d\n", cn);
    printf("Atomic n: %d\n", an);
    printf("Reduction n: %d\n", rn);
    printf("Expected n: %d\n", en);
    return 0;
}