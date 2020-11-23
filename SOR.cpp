#include<iostream>
#include<algorithm>
using namespace std;
int arr[10005];
int main()
{
	long long int n;
    cin>>n;
    for(long long int i=0;i<n;i++)
    {
    	cin>>arr[i];
    }
    sort(arr,arr+n);
    for(long long int i=0;i<n;i++)
    {
    	cout<<arr[i]<<" "; 
    }
}
        