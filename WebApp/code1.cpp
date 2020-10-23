#include <iostream>
#include <algorithm>
#include<vector>

int arr[100];
int main()
{
	int n;
    cin>>n;
    for(int i=0;i<n;i++)
    {
        cin>>arr[i];
    }

    cout<<*min_element(arr,arr+n)<<endl;
}

           
        
        
        
        
        
        
        
        