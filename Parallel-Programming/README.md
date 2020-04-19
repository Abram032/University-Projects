# Parallel-Programming

## Compiling
g++ -fopenmp main.cpp -o out.exe

## Clausules
#pragma omp parallel {}

#pragma omp parallel for
// No braces

#pragma omp parallel for private(x) shared(y)
// private - copied for every thread
// shared - shared between threads

#pragma omp critical
#pragma omp critical {}
// Critical section for semaphore

#pragma omp atomic
// Single atomic instruction

#pragma omp parallel for reduction(+:suma)