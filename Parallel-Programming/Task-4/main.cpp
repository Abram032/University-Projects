// #include <iostream>
// using namespace std;
// #include <omp.h>
 
// int main(int argc, char *argv[])
// {
//   int th_id, nthreads;
//   #pragma omp parallel private(th_id) shared(nthreads)
//   {
//     th_id = omp_get_thread_num();
//     #pragma omp single nowait
//     {
//       cout << "Hello World from thread " << th_id << '\n';
//     }
//     //#pragma omp barrier
 
//     #pragma omp single
//     {
//       nthreads = omp_get_num_threads();
//       cout << "There are " << nthreads << " threads" << '\n';
//       cout << "Hello World 2 from thread " << th_id << '\n';
//     }
//   }
//   return 0;
// }

#include <iostream>
using namespace std;
#include <omp.h>
 
int main(int argc, char *argv[])
{
#pragma omp parallel for
  for(int i=0;i<10;i++) {
    #pragma omp critical
    cout << i << endl;
  }
  return 0;
}





