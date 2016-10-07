using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMImodule
{
   
    class Program
    {
        //public int size = 7;
        static void Main(string[] args)
        {
            string line;
            string[] words ;
            int size = 7;
            AdjacencyList adjacencyList = new AdjacencyList(size);

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\h\Desktop\MYBOY\Sneha\subchallenge1\test1.txt");
            while ((line = file.ReadLine()) != null)
            {
                // System.Console.WriteLine(line);
                words = line.Split('\t');
                // Console.WriteLine

                    int startVertex = int.Parse(words[0]);
                    int endVertex = int.Parse(words[1]);
                    double weight = double.Parse(words[2]);

                adjacencyList.addEdgeAtEnd(startVertex, endVertex, weight);
                adjacencyList.addEdgeAtEnd(endVertex, startVertex, weight);
                
            }
            adjacencyList.printAdjacencyList();
           // adjacencyList.PGRankarray();
            //adjacencyList.PGRankarrayW();
            //adjacencyList.Centrality();
            //adjacencyList.CentralityW();
            //adjacencyList.print();
            adjacencyList.cliques(2);
            adjacencyList.printcliques();
            Console.ReadLine();
        }
    }

    class AdjacencyList
    {
        LinkedList<Tuple<int, double>>[] adjacencyList;
        public int size;
        int count1 = 0;
        int count2 = 0;
        double[] PageRank ;
        double[] PageRankweights;
        double[] Centralitydegree;
        double[] Centralitydegreew;
        int[] nieghbour;
        public int N=0;
        LinkedList<int> Max_Clique = new LinkedList<int>();
        LinkedList<int> temp_clique = new LinkedList<int>();
        LinkedList<int> initial = new LinkedList<int>();

        // Constructor - creates an empty Adjacency List
        public AdjacencyList(int vertices)
        {
            size = vertices;
            adjacencyList = new LinkedList<Tuple<int, double>>[vertices];

            for (int i = 0; i < adjacencyList.Length; ++i)
            {
                adjacencyList[i] = new LinkedList<Tuple<int, double>>();
            }
        }

        // Appends a new Edge to the linked list
        public void addEdgeAtEnd(int startVertex, int endVertex, double weight)
        {
            adjacencyList[startVertex].AddLast(new Tuple<int, double>(endVertex, weight));
        }

        // Adds a new Edge to the linked list from the front
        public void addEdgeAtBegin(int startVertex, int endVertex, double weight)
        {
            adjacencyList[startVertex].AddFirst(new Tuple<int, double>(endVertex, weight));
        }

        // Returns number of vertices
        // Does not change for an object
        public int getNumberOfVertices()
        {
            return adjacencyList.Length;
        }

        // Returns a copy of the Linked List of outward edges from a vertex
        public LinkedList<Tuple<int, double>> this[int index]
        {
            get
            {
                LinkedList<Tuple<int, double>> edgeList
                               = new LinkedList<Tuple<int, double>>(adjacencyList[index]);

                return edgeList;
            }
        }

        // Prints the Adjacency List
        public void printAdjacencyList()
        {
            int i = 0;

            foreach (LinkedList<Tuple<int, double>> list in adjacencyList)
            {
                Console.Write("adjacencyList[" + i + "] -> ");

                foreach (Tuple<int, double> edge in list)
                {
                    Console.Write(edge.Item1 + "(" + edge.Item2 + ")");
                }

                ++i;
                Console.WriteLine();
            }
        }

        // Removes the first occurence of an edge and returns true
        // if there was any change in the collection, else false
        public int NoOfNieghbours(int i)
        {
            int count = 0;
            foreach(Tuple<int, double> edge in adjacencyList[i])
            {
                count++;
            }
            return count;
        }

        //Page Rank Array
        public void PGRankarray()
        {
            int N = size; ;
            PageRank = new double[N];
            for (int i =0; i<N; i++)
            {
                //PageRank[i] = new double();
                PageRank[i] = 1;
            }

            for (int j = 0; j <40; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    PageRank[i] = PageRankarray(i);
                  //  Console.Write(PageRank[i] + "\t");
                }
             //   Console.WriteLine();
            }
        }

        // Page Rank without weights
        public double PageRankarray(int X)
        {
            int N = size;
            double d = 0.85, sum = 0.0,Pagerank;
            foreach(Tuple<int,double> edge in adjacencyList[X])
            {
                sum += (PageRank[edge.Item1] / NoOfNieghbours(edge.Item1));
            }
            Pagerank = (1 - d) / N + (d * sum);
            return Pagerank;
        }


        //Page Rank with weights
        public void PGRankarrayW()
        {
            int N = size;
            PageRankweights = new double[N];
            for (int i = 0; i < N; i++)
            {
               // PageRankweights[i] = new double();
                PageRankweights[i] = 1;
            }

            for (int j = 0; j < 40; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    PageRankweights[i] = PageRankarrayweights(i);
                    Console.Write(PageRankweights[i] + "\t");
                }
                Console.WriteLine();
            }
        }

        public double PageRankarrayweights(int X)
        {
            int N = size;
            double d = 0.85, sum = 0.0, Pagerank;
            foreach (Tuple<int, double> edge in adjacencyList[X])
            {
                sum += (PageRankweights[edge.Item1] / NoOfNieghbours(edge.Item1));
            }
            Pagerank = (1 - d) / N + (d * sum);
            return Pagerank;
        }

        // Centrality with =out weights

        public void Centrality()
        {
            int N = size;
            
            Centralitydegree = new double[N];
            for ( int i =0; i< N; i++)
            {
                Centralitydegree[i] = 0;
                Centralitydegree[i] = Convert.ToDouble(NoOfNieghbours(i)) / Convert.ToDouble(N);
            }
        }
        public void CentralityW()
        {
            int N = size;
            double sum = 0;
            Centralitydegreew = new double[N];
            for (int i = 0; i < N; i++)
            {
                sum = 0;
                Centralitydegreew[i] = 0;
                foreach( Tuple<int,double> edge in adjacencyList[i])
                {
                    sum += edge.Item2;
                }
                Centralitydegreew[i] = sum /Convert.ToDouble(N);

            }
        }

        public void print()
        {
            for (int i=0; i < size;i++)
            {
                Console.Write(Centralitydegree[i]+"\t");
            }
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                Console.Write(Centralitydegreew[i] + "\t");
            }
        }

        public void cliques(int X)
        {
            int i = 0;
            nieghbour = new int[NoOfNieghbours(X)];
            foreach (Tuple<int,double> edge in adjacencyList[X])
            {
                nieghbour[i] = edge.Item1;
                i++;  
            }
            int N = nieghbour.Length;
            //Console.WriteLine(N);
            i = 0;
            //insert the first niegbour in array
            initial.AddFirst(X);
            temp_clique.AddFirst(X);
            
            insertvalue(i, N);
            
        // Console.WriteLine("smarttttmove");
           // printall(temp_clique);
            donotinsertvalue(temp_clique,i, N);
       
        }
    public void insertvalue(int index, int N)
        {
            //count1++;
            int i = index;
            addedgeclique(nieghbour[i]);
            i++;
            if(i!=N)
            {
                 //Console.WriteLine("hiii"+i);
                //Console.WriteLine(check(nieghbour[i], temp_clique));
               // Boolean temp = (check(nieghbour[i], temp_clique) && branchboundtest(i, N));
               // Console.WriteLine(temp);
                if (check(nieghbour[i], temp_clique) && branchboundtest(i,N))
                {
                    insertvalue(i, N);
         //           Console.WriteLine("donot"+i);
                  //  printall(temp_clique);
                    donotinsertvalue(temp_clique, i, N);
                        
                }
                else
                {
                    printall(temp_clique);
                    if (Max_Clique.Count() < temp_clique.Count())
                    {

                        //          Max_Clique = temp_clique;
                        assignMaxclique(temp_clique);
                        printcliques();
                    }
                    return;
                }
            }
           printall(temp_clique);
            if (Max_Clique.Count() < temp_clique.Count())
            {

                assignMaxclique(temp_clique);
                printcliques();
            }
            //  temp_clique = new LinkedList<int>();
            //temp_clique = initial;
            return ;
        }

        public void assignMaxclique( LinkedList<int> temp_clique)
        {
            Max_Clique = new LinkedList<int>();
            foreach( int value in temp_clique)
            {
                Max_Clique.AddLast(value);
            }
        }

        public void donotinsertvalue(LinkedList<int> temp,int index, int N)
        {
           // printcliques();
           // Console.WriteLine("hello" + index);
           // count2++;
            if (temp.Count > 0)
            {
                temp_clique.RemoveLast();
              
            }
            //printcliques();
            int i = index;
            i++;
            if (i != N)
            {
               
                if (check(nieghbour[i], temp_clique) && branchboundtest(i, N))
                {
                    insertvalue(i, N);
                    donotinsertvalue(temp_clique, i, N);

                    
                }
                else
                {
                    printcliques();
                    printall(temp_clique);
                    //Console.WriteLine(Max_Clique.Count() + "Temp value" + temp_clique.Count());
                    if (Max_Clique.Count() < temp_clique.Count())
                    {

                        assignMaxclique(temp_clique);
                      //  printcliques();
                    }
                    
                    return;
                }
            }
            printcliques();
             printall(temp_clique);
            if (Max_Clique.Count() < temp_clique.Count())
            {
                assignMaxclique(temp_clique);
            }
            // temp_clique = new LinkedList<int>();
            printcliques();
            return;
        }

        private bool branchboundtest(int index, int N)
        {
            Boolean flag = true;
            int temp_length = temp_clique.Count();
            int max_length = Max_Clique.Count();

            if (max_length >= (temp_length+N-index))
            {
                flag = false;          
            }
          return flag;
        }
 
        public bool check(int j, LinkedList<int> linkedList)
        {
            Boolean flag = false;
            foreach( int value in linkedList)
            {
                foreach( Tuple<int, double> item in adjacencyList[j])
                {
                    if(value == item.Item1)
                    {
                        flag = true;
                        break;  
                    }
                    else
                        flag = false;
                }
                if(flag == false)
                {
                    return flag;
                }
            }
            return flag;
        }

        public void addedgeclique( int endVertex)
        {
            temp_clique.AddLast(endVertex);
        }

        public void printcliques()
        { 
            
                Console.Write("Clique[ Maxium ]");
                foreach ( int value in Max_Clique)
                {

                    Console.Write(value + "\t");
                }
                Console.WriteLine();
            //Console.WriteLine(count1+"donot"+count2);
        }
        public void printall(LinkedList<int> temp)
        {
            Console.Write("Clique[  ]");
            foreach (int value in temp)
            {

                Console.Write(value + "\t");
            }
            Console.WriteLine();
           
        }
        
    }
}
