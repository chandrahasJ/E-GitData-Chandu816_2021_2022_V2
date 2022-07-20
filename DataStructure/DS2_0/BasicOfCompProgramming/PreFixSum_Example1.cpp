/*
    Given array of N integers.
    Given Q queries and in each queries given L and R 
    print sum of array elements from L to R(L, R included)

    Constraints
    1 <= N <= 10e5;
    1 <= a[i] <= 10e9;
    1 <= Q <= 10^5;
    1 <= L, R <= N
*/

#include<bits/stdc++.h>
using namespace std;
const long long M = 10000010;
int N = 100005+10;
long long prefix[M];

void normal(){    
    int arraySize = 0;
    cin>>arraySize;
    int array[arraySize];
    
    for(int i = 1;i <= arraySize;++i){
        cin>>array[i];
    }
    int q = 0;
    cin>>q;
    while (q--)
    {
        int L,R;
        cin>> L>>R;
        long long sum=0;
        for (int i = L; i <= R; i++)
        {
            sum += array[i];
        }
        cout<< sum << endl;        
    }
    
}

int main(){
    normal();
    return 0;
}