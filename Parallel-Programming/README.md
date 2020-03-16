# Parallel-Programming

## Compiling
g++ -fopenmp main.cpp -o out.exe

## Homework
100 / 6 (CPU) - dividing and ranges per CPU

Create long tasks (4-40 seconds) and compare how much running code in parallel improves performance (Is 2 cpu's faster 2x faster than with 1 cpu etc.)

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