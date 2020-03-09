#include<stdio.h>
#include<stdlib.h>
#include<iostream>
#include<math.h>

int main()
{
    float f_eps = 1.0f, f_U = 1.0f;
    double d_eps = 1.0f, d_U = 1.0f;

    printf("Float:\n");
    int f_bits = 0;
    while((1.0f + f_eps) > 1.0f)
    {
        f_eps /= 2;
        f_bits++;
        printf("%d\t%.20e\t%.20f\n", f_bits, f_eps, 1.0f + f_eps);
    }

    printf("\nBits: %d\nEpsilon: %.20f\n\n", f_bits, f_eps * 2);

    printf("Double:\n");
    int d_bits = 0;
    while((1.0f + d_eps) > 1.0f)
    {
        d_eps /= 2;
        d_bits++;
        printf("%d\t%.20e\t%.20f\n", d_bits, d_eps, 1.0f + d_eps);
    }
    printf("\nBits: %d\nEpsilon: %.20f\n\n", d_bits, d_eps * 2);

    printf("Float Upper bound:\n");
    int f_U_bits = 0;
    while (!isinf(f_U))
    {
        if(isinf(f_U * 2))
        {
            break;
        }
        f_U *= 2;
        f_U_bits++;
        printf("%d\t%.20e\n", f_U_bits, f_U);
    }

    printf("Double Upper bound:\n");
    int d_U_bits = 0;
    while (!isinf(d_U))
    {
        if(isinf(d_U * 2))
        {
            break;
        }
        d_U *= 2;
        d_U_bits++;
        printf("%d\t%.20e\n", d_U_bits, d_U);
    }


}
