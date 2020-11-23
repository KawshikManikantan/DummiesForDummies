#include<iostream>
#include<vector>
#include<algorithm>
#include<queue>
#include<utility>
using namespace std;
vector<long long int > adj_list[100005];
bool visited[100005];
long long int dist[100005];
long long int source=0;
long long int destination=0;
long long int answer=0;
int dfs(long long int node,long long int parent)
{
    visited[node]=1;
    if(parent!=-1)
    {
        dist[node]=dist[parent]+1;
    }
    for(long long int i=0;i<adj_list[node].size();i++)
    {
        if(!visited[adj_list[node][i]])
        {
            // cout<<"Node visited: "<<adj_list[node][i]<<endl;
            dfs(adj_list[node][i],node);
        }
    }
    return 0;
}
int main()
{
    long long int num_edges,num_vertices;
    cin>>num_vertices;
    long long int vertex1,vertex2;
    
    for(long long int i=0;i<num_vertices;i++)
    {
        visited[i]=0;
        dist[i]=0;
    }

    for(long long int i=0;i<num_vertices-1;i++)
    {
        cin>>vertex1>>vertex2;
        adj_list[vertex1].push_back(vertex2);
        adj_list[vertex2].push_back(vertex1);
    }

    dfs(0,-1);
    // cout<<"What happened?"<<endl;
    // for(int i=0;i<num_vertices;i++)
    // {
    //     cout<<dist[i]<<" ";
    // }
    // cout<<endl;
    source=max_element(dist,dist+num_vertices)-dist;
    // cout<<source<<endl;
    for(long long int i=0;i<num_vertices;i++)
    {
        visited[i]=0;
        dist[i]=0;
    }
    dfs(source,-1);
    // for(int i=0;i<num_vertices;i++)
    // {
    //     cout<<dist[i]<<" ";
    // }
    // cout<<endl;
    answer=*max_element(dist,dist+num_vertices);
    cout<<answer<<endl;
}   