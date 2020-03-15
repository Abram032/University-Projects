#include<stdio.h>
#include<stdlib.h>
#include<iostream>
#include<math.h>

int main()
{
    float f_eps = 1.0f;
    double d_eps = 1.0f;

    printf("Float:\n");
    int f_bits = 0;
    while((1.0f + f_eps) > 1.0f)
    {
        if(1.0f + f_eps / 2 == 1.0f)
        {
            break;
        }
        f_eps /= 2.0f;
        f_bits++;
        //printf("%d\t%.20e\t%.20f\n", f_bits, f_eps, 1.0f + f_eps);
    }
    printf("Bits: %d\nEpsilon: %.20f\n\n", f_bits, f_eps);

    printf("Double:\n");
    int d_bits = 0;
    while((1.0f + d_eps) > 1.0f)
    {
        if(1.0f + d_eps / 2 == 1.0f)
        {
            break;
        }
        d_eps /= 2.0f;
        d_bits++;
        //printf("%d\t%.20e\t%.20f\n", d_bits, d_eps, 1.0f + d_eps);
    }
    printf("Bits: %d\nEpsilon: %.20f\n\n", d_bits, d_eps);

    printf("Float Upper bound:\n");
    int f_U_bits = 0;
    float f_U = 1.0f - f_eps;
    while (!isinf(f_U))
    {
        if(isinf(f_U * 2.0f))
        {
            break;
        }
        f_U *= 2.0f;
        f_U_bits++;
        //printf("%d\t%.20e\n", f_U_bits, f_U);
    }
    printf("%d\t%.20e\n\n", f_U_bits, f_U);

    printf("Double Upper bound:\n");
    int d_U_bits = 0;
    double d_U = 1.0f - d_eps;
    while (!isinf(d_U))
    {
        if(isinf(d_U * 2.0f))
        {
            break;
        }
        d_U *= 2.0f;
        d_U_bits++;
        //printf("%d\t%.20e\n", d_U_bits, d_U);
    }
    printf("%d\t%.20e\n\n", d_U_bits, d_U);

    printf("Float Lower bound:\n");
    int f_L_bits = 0;
    float f_L = 1.0f + f_eps;
    float f_L_temp = 2.0f;
    while(f_L > 0)
    {   
        if(f_L * 2.0f == f_L_temp)
        {
            break;
        }
        f_L_temp /= 2.0f;
        f_L /= 2.0f;
        f_L_bits++;
        //printf("%d\t%.20e\t%.20e\n", f_L_bits, f_L_temp, f_L);
    }
    f_L = f_L_temp;
    printf("%d\t%.20e\n\n", f_L_bits, f_L);

    printf("Double Lower bound:\n");
    int d_L_bits = 0;
    double d_L = 1.0f + d_eps;
    double d_L_temp = 2.0f;
    while(d_L > 0)
    {   
        if(d_L * 2.0f == d_L_temp)
        {
            break;
        }
        d_L_temp /= 2.0f;
        d_L /= 2.0f;
        d_L_bits++;
        //printf("%d\t%.20e\t%.20e\n", f_L_bits, f_L_temp, f_L);
    }
    d_L = d_L_temp;
    printf("%d\t%.20e\n", d_L_bits, d_L_temp);
}
