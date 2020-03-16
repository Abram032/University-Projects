#include <omp.h>
#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#define N 10

int main() 
{
    // //omp_set_num_threads(2);
    // #pragma omp parallel
    // {
    //     int this_thread = omp_get_thread_num();
    //     int num_threads = omp_get_num_threads();

    //     int my_start = (this_thread) * N / num_threads;
    //     int my_end = (this_thread + 1) * N / num_threads;

    //     // // for(int n = my_start; n < my_end; ++n) {
    //     // //     //printf("[%d]: %d\n", this_thread,n);
    //     // //     std::cout << this_thread << "|" << n << std::endl;
    //     // // }
    //     // if(this_thread == 0)
    //     // {
    //     //     //std::cout << "[" << this_thread << "] : " << my_start << " - " << my_end << std::endl;
    //     //     printf("[%d] : %d - %d", this_thread, my_start, my_end);
    //     // }

    //     #pragma omp for
    //     for(int i = 0; i < N; ++i) {
    //         printf("[%d] ", this_thread);
    //         //std::cout << this_thread << " ";
    //     }
    // }

    // #pragma omp parallel for
    // for(int i = 0; i < N; ++i) {
    //     if(omp_get_thread_num() == 2)
    //         printf("%d ", i);
    // }

    int threads = 100;
    int id = 100;

    #pragma omp parallel
    {
        threads = omp_get_num_threads();
        id = omp_get_thread_num();
        std::cout << "Hello from thread: ", id << "out of ";
        std::cout << threads << "\n";
    }

    printf("\n");
    return 0;
}