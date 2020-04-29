#include <string>
#include <cmath>
#include <omp.h>
#include <vector>
#include <cassert>
#include <iostream>
#include <fstream>

using namespace std;

template<typename M>
bool MatrixMarket(M& m,size_t& N, const string& fName) {
  fstream file(fName.c_str());
  if (!file.is_open()) {
    cerr << "Can't find " << fName << endl;
    return false;
  }
  string header;
  getline(file,header);
  cout << header << endl;
  if (header!="%%MatrixMarket matrix coordinate real symmetric") {
    cerr << "Wrong matrix format" << endl;
    return false;
  }

  size_t nonzero;
  {
     size_t w,k;
     file >> w >> k >> nonzero >> ws;
     cout << w << " " << k << " " << nonzero << endl;
     if (w!=k) { cerr << " w!=k " << endl; return false;}
     N=w;
     assert(w==k && w==N);
     m.resize(N);
  }

  assert(m.size()==N);
  for (size_t i=0;i!=m.size();++i) {
    m[i].resize(N);
  }

  for (size_t i=0; i<N;++i) {
    for (size_t j=0; j<N;++j) {
        m[i][j]=0.0;
    }
  }

  double frob=0.0;
  for (size_t linia=0;linia<nonzero;++linia) {
    if (file.eof()) { cerr << "End of file - " <<  fName << " corrupted " << endl; return false; }
    size_t i,j;
    double value;
    file >> i >> j >> value >> ws;
    //cout << i << " " << j << " " << value << " " << linia << endl;
    i--;j--;
    assert(i>=0 && j>=0 && i<N && j<N);
    m[i][j]=value;
    if (i!=j) m[j][i]=value;
    assert(m[i][j]==m[j][i]);
    if (i!=j)
      frob+=2*value*value;
    else
      frob+=value*value;
  }
  std::cout << " Matrix N= " << N << " nonzero elements = " << nonzero << " Frobenius norm " << sqrt(frob) << " above diagonal " << nonzero-N << endl;
  return true;
}

//tutaj jest ta funkcja....


//tutaj jest ta funkcja....


template<typename V>
double Dot(const V& v1,const V& v2, int N) {
  double temp=0.0;
#pragma omp parallel for reduction(+:temp)
  for (int i=0; i<N;++i) {
    temp+=v1[i]*v2[i];
  }
  return temp;
}

template<typename V>
void Normalize(V& v,int N) {
  double temp=sqrt(Dot(v,v,N));
  assert(temp>1e-20);
  temp=1.0/temp;
#pragma omp parallel for
  for (int i=0; i<N;++i) {
    v[i]*=temp;
  }
}

//w=M*v
//|1 2||3|=|1*3+2*4|=|11|
//|2 1||4| |2*3+1*4|=|10|
  
template<typename V,typename M>
void MatVec(V& w,const M& m,const V& v, int N)
{
#pragma omp parallel for
  for (int i=0; i<N;++i)
  {
    double temp=0.0;
    for (int j=0; j<N;++j)
    {
      temp+=m[i][j]*v[j];
    }
    w[i]=temp;
  }
}

const int maxIter=1000;

int main(int argc,char* argv[]) {
  if (argc!=2) { cerr << "podaj nazwe macierzy" << endl; return -1; }
  vector< vector<double> > m;

  size_t N;
  if (!MatrixMarket(m,N,argv[1])){ return -1; }
  vector<double> v(N),w(N);
  
  for (size_t i=0; i<N;++i) {
    v[i]=i+1;
    w[i]=0.0;
  }

  double start=omp_get_wtime();

  for (int iter=0;iter<maxIter;++iter) {

    if (iter%2==0) { MatVec(w,m,v,N); Normalize(w,N); }
      else
                   { MatVec(v,m,w,N); Normalize(v,N); }
  }
  
  cout.precision(15);
  // for (int i=0; i<N;++i) {
  //   cout << w[i] << " ";
  // }
  // cout << endl;

  MatVec(v,m,w,N);
  cout << "maxIter " << maxIter << " size " << N << " w*M*w = " << Dot(v,w,N);
  double stop=omp_get_wtime();
  cout << " time " << stop-start << endl;

  return 0;
}
