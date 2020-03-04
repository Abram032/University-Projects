#include <omp.h>
#include <iostream>

int main() 
{
    std::cout << "Hello from thread: " << std::endl;
    omp_set_num_threads(4);
    #pragma omp parallel
    {
        std::string temp = std::string("[") << omp_get_thread_num() << std::string("] ");
        std::cout << temp;
        std::string temp2 = std::string("Threads: ") << omp_get_num_threads() << std::endl;
        std::cout << temp2;
    }
    std::cout << std::endl << "Going back" << std::endl;
}