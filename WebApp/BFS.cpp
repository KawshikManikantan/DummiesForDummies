#include<bits/stdc++.h>
#define pb push_back
#define pi pair<int, int>
#define mp make_pair
using namespace std;
int Time[1001][1001];
int n, m, k;
queue<pi>q;
void BFS()
{
    int r,c;
    while(!q.empty())
    {
        r = q.front().first, c = q.front().second;
        q.pop();
        if(r+1<n)
            if(Time[r+1][c]==-1) Time[r+1][c] = 1 + Time[r][c], q.push(mp(r+1, c));
        if(c+1<m)
            if(Time[r][c+1]==-1) Time[r][c+1] = 1 + Time[r][c], q.push(mp(r, c+1));
        if(r-1>=0)
            if(Time[r-1][c]==-1) Time[r-1][c] = 1 + Time[r][c], q.push(mp(r-1, c));
        if(c-1>=0)
            if(Time[r][c-1]==-1) Time[r][c-1] = 1 + Time[r][c], q.push(mp(r, c-1));
    }
}
int main()
{
    cin>>n>>m>>k;
    long long int mod = 1000000000+7;
    for(int i=0;i<n;i++) for(int j=0;j<m;j++) Time[i][j] = -1;
    for(int i=0;i<k;i++)
    {
        int x,y;
        cin>>x>>y;
        Time[x][y] = 0;
        q.push(mp(x, y));
    }
    BFS();
    long long int ans = 0;
    for(int i=0;i<n;i++)
        for(int j=0;j<m;j++)
        {
            long long int term = (i+1) * (j+1) * Time[i][j] % mod;
            ans = (ans + term) % mod;
        }
    cout<<ans<<"\n";
    return 0;
}
        
        