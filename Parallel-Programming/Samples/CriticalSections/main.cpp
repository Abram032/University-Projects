#include <omp.h>
#include <iostream>
#include <stdlib.h>
#include <stdio.h>

int main() 
{
    int thread_num = omp_get_num_threads();

    int cn = 0;
    #pragma omp parallel for shared(cn)
    for(int i = 1; i < 10; i++) {
        #pragma omp critical
        cn += i;
    }

    int an = 0;
    #pragma omp parallel for shared(an)
    for(int i = 1; i < 10; i++) {
        #pragma omp atomic update
        an += i;
    }

    int rn = 0;
    #pragma omp parallel for reduction(+:rn)
    for(int i = 1; i < 10; i++) {
        rn += i;
    }

    int en = 0;
    for(int th = 0; th < thread_num; th++) {
        for(int i = 1; i < 10; i++) {
            en += i;
        }
    }
    
    printf("Threads amount: %d\n", thread_num);
    printf("Critical n: %d\n", cn);
    printf("Atomic n: %d\n", an);
    printf("Reduction n: %d\n", rn);
    printf("Expected n: %d\n", en);
    return 0;
}