#include <iostream>
#include <cstdlib>
#include <omp.h>

int main(int argc, char* argv[])
{
    const long int N=100000000;
    double step = 1.0 / N;
    double sum = 0.0;

    double start = omp_get_wtime(); 
    // #pragma omp parallel for reduction(+:sum)
    // for(int i = 0; i < N; i++) {
    //     double x = (i + 0.5) * step;
    //     sum += 4.0/(1.0 + x*x);
    // }

    #pragma omp parallel
    {
        double sum_priv = 0.0;
        #pragma omp for
        for(int i = 0; i < N; i++) {
            double x = (i + 0.5) * step;
            sum += 4.0/(1.0 + x*x);
        }
        #pragma omp atomic
        sum += sum_priv;
    }
    double end = omp_get_wtime(); 

    std::cout.precision(15);
    std::cout << "PI = " << sum * step << std::endl;
    std::cout << "Time = " << end - start << std::endl;
    return EXIT_SUCCESS;
}