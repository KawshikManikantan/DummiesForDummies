#include<iostream>
#include<vector>
#include<algorithm>
#include<queue>
#include<utility>
using namespace std;
typedef pair< long long int, pair<long long int,long long int> > ppl;
ppl edges[100000];
// priority_queue< ppl, vector< ppl >, greater< ppl > > edgeweights;
vector < ppl > edgeweights;
vector < ppl > mst;
ppl compare;
long long int parent_node[100000];
long long int size_component[100000];
long long int count_now=0;
long long int min_cost=0;
long long int num_edges,num_vertices;
long long int sigg=99999999999999;
void joining_component(long long int parent1,long long int parent2)
{
    if(size_component[parent1]>=size_component[parent2])
    {
        if(size_component[parent1]==size_component[parent2])
        {
            size_component[parent1]++;
        }
        parent_node[parent2]=parent1;
    }
    else
    {
        parent_node[parent2]=parent1;
    }
    
}

int finding_parent(long long int vertex1)
{
    while(parent_node[vertex1]!=vertex1)
    {
        vertex1=parent_node[vertex1];
    }
    return vertex1; 
}

long long int kruskalalgo(ppl edge_ignore)
{
    int flag=0;
    for(long long int i=0;i<edgeweights.size();i++)
    {
        ppl present_min=edgeweights[i];
        // cout<<"Present Minimum:"<<present_min.first<<" "<<present_min.second.first<<endl;
        if(edge_ignore==present_min)
        {
            // cout<<"Enterred"<<endl;
            continue;
        }
        if(finding_parent(present_min.second.first)==finding_parent(present_min.second.second))
        {
            // cout<<"Enterred1"<<endl;
            continue;
        }
        count_now++;
        if(edge_ignore.first==-1)
        {
            mst.push_back(present_min);
        }
        joining_component(finding_parent(present_min.second.first),finding_parent(present_min.second.second));
        min_cost+=present_min.first;
        if(count_now==num_vertices-1)
        {
            flag=1;
            break;
        }
    }
    
    if(flag==1)
    {
        return min_cost;
    }

    else
    {
        return sigg;
    }
    
}
int main()
{
    compare.first=-1;
    compare.second.first=-1;
    compare.second.second=-1;
    cin>>num_vertices>>num_edges;
    long long int vertex1,vertex2,edge_length;

    for(long long int i=0;i<=num_vertices;i++)
    {
        parent_node[i]=i;
        size_component[i]=1;
    }

    for(long long int i=0;i<num_edges;i++)
    {
        cin>>vertex1>>vertex2>>edge_length;
        edges[i].first=edge_length;
        edges[i].second.first=vertex1;
        edges[i].second.second=vertex2;
        edgeweights.push_back(edges[i]);
    }
    sort(edgeweights.begin(),edgeweights.end());
    long long int evilflag=0;
    long long int totalcost=0;
    evilflag=kruskalalgo(compare);
    // cout<<min_cost<<endl;
    // if(evilflag==sigg)
    // {
    //     cout<<"1000000007"<<endl;
    //     return 0;
    // }
    totalcost+=min_cost;
    // cout << totalcost << endl;
    long long secondcost=sigg;
    for(long long i=0;i<mst.size();i++)
    {
        count_now=0;
        min_cost=0;
        for(long long j=0;j<=num_vertices;j++)
        {
            parent_node[j]=j;
            size_component[j]=1;
        }
        secondcost=min(secondcost,kruskalalgo(mst[i]));
    }
    if(secondcost==sigg)
    {
        cout<<"1000000007"<<endl;
        return 0;
    }
    totalcost+=secondcost;
    cout<<totalcost<<endl;
}            
        