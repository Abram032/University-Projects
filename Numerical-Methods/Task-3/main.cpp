#include<stdio.h>
#include<stdlib.h>
#include<bitset>
#include<iostream>
#include<iomanip>
#include<math.h>
#include<string.h>

std::string byteToString(unsigned char byte, int lo = 0, int hi = 7) 
{
    std::string result = "";
    for(int i = hi; i >= lo; i--) {
        result += std::to_string(((byte >> i) & 1));
    }
    return result;
}

int main()
{
    printf("Enter value: ");
    float value;
    std::cin >> value;
    printf("Entered value: %.23f\n", value);
    //!Default representation on my device is little endian
    
    //!byte_array is saved as little endian
    unsigned char * byte_array = (unsigned char *)(&value);

    //!bit_array is saved as big endian
    bool* bit_array = new bool[32];
    for(int i = 3, bit = 0; i >= 0; i--) {
        for(int j = 7; j >= 0; j--, bit++) {
            bit_array[bit] = (byte_array[i] >> j) & 1;
        }
    }

    //Binary
    std::cout << "Binary (little endian): " 
        << byteToString(byte_array[0]) << " "
        << byteToString(byte_array[1]) << " "
        << byteToString(byte_array[2]) << " "
        << byteToString(byte_array[3]) << " "
        << std::endl;
    std::cout << "Binary (big endian): " 
        << byteToString(byte_array[3]) << " "
        << byteToString(byte_array[2]) << " "
        << byteToString(byte_array[1]) << " "
        << byteToString(byte_array[0]) << " "
        << std::endl;

    //Sign
    bool sign = bit_array[0];
    std::cout << "Sign: " << sign << std::endl;

    //Exponent
    //TODO: Fix exponent
    std::string exp_str = (byteToString(byte_array[3], 0, 6) + byteToString(byte_array[2], 7, 7));
    unsigned char exponent = static_cast<unsigned char>(std::stoi(exp_str, nullptr, 2));
    std::cout << "Exponent: "
        << byteToString(byte_array[3], 0, 6) << " "
        << byteToString(byte_array[2], 7, 7) << "."
        << std::endl;
    std::cout << "Exponent (e): " << +exponent << std::endl;

    //Bias
    int bias = 127;
    if(exponent == 0) {
        bias = 126;
    }
    std::cout << "Bias: " << bias << std::endl;

    //f0
    bool f0 = 1;
    if(exponent == 0) {
        f0 = 0;
    }
    std::cout << "f0: " << f0 << std::endl;

    //Mantissa
    double mantissa = 0;
    std::cout << "Mantissa: " 
        << "." << byteToString(byte_array[2], 0, 6) << " " 
        << byteToString(byte_array[1]) << " "
        << byteToString(byte_array[0]) << " "
        << std::endl;
    for(int i = 9, bit = 1; i < 32; i++, bit++) {
        mantissa += (bit_array[i]/(pow(2.0f, bit)));
    }
    mantissa += f0;
    std::cout << "Mantissa (m): " << mantissa << std::endl;

    //Decimal value
    double dec_value = pow(-1, sign) * mantissa * pow(2, exponent - bias);
    std::cout << "Decimal value: " << dec_value << std::endl;
    return 0;
}