            #include<iostream>
using namespace std;
int main()
{
    int n, mini=-1;
    cin>>n;
    for(int i=0;i<n;i++)
    {
    	int temp;
        cin>>temp;
        if(mini==-1)
        	mini = temp;
        if(temp<mini)
        	mini = temp;
    }
    cout<<mini<<"\n";
    return 0;
}
        