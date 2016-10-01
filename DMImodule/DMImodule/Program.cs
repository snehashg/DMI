using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMImodule
{
   
    class Program
    {
        public int size = 5;
        static void Main(string[] args)
        {
            string line;
            string[] words ;
            AdjacencyList adjacencyList = new AdjacencyList(5);

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\h\Desktop\MYBOY\Sneha\sample.txt");
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
            adjacencyList.cliques(0);
            adjacencyList.printcliques();
            Console.ReadLine();
        }
    }

    class AdjacencyList
    {
        LinkedList<Tuple<int, double>>[] adjacencyList;
        double[] PageRank ;
        double[] PageRankweights;
        double[] Centralitydegree;
        double[] Centralitydegreew;
        public int N=0;
        LinkedList<Tuple<int>>[] Clique;

        // Constructor - creates an empty Adjacency List
        public AdjacencyList(int vertices)
        {
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
            int N = 5;
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
            int N = 5;
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
            int N = 5;
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
            int N = 5;
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
            int N = 5;
            
            Centralitydegree = new double[N];
            for ( int i =0; i< 5; i++)
            {
                Centralitydegree[i] = 0;
                Centralitydegree[i] = Convert.ToDouble(NoOfNieghbours(i)) / Convert.ToDouble(N);
            }
        }
        public void CentralityW()
        {
            int N = 5;
            double sum = 0;
            Centralitydegreew = new double[N];
            for (int i = 0; i < 5; i++)
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
            for (int i=0; i < 5;i++)
            {
                Console.Write(Centralitydegree[i]+"\t");
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.Write(Centralitydegreew[i] + "\t");
            }
        }

        public void cliques(int X)
        {
            int i=0,j=0;
            int[] nieghbour = new int[NoOfNieghbours(X)];
            foreach (Tuple<int,double> edge in adjacencyList[X])
            {
                nieghbour[i] = edge.Item1;
                i++;  
            }
             N = Convert.ToInt32(Math.Pow(2, i));
            Clique = new LinkedList<Tuple<int>>[N];
            for( i =0; i< N; i++)
            {
                Clique[i] = new LinkedList<Tuple<int>>();
               // addedgeclique(i, X);
            }
            i = 0;
            int index = 0;
            while (i!= NoOfNieghbours(X))
            {
                Console.WriteLine("hellooo" + i);
                j = 0;
                addedgeclique(index, X);
                addedgeclique(index, nieghbour[i]);
                while(j< NoOfNieghbours(X))
                {
                    Console.WriteLine("next" + j);
                    Console.WriteLine(exists(nieghbour[j], Clique[index]));
                    if (exists(nieghbour[j], Clique[index]))
                    {
                        j++;
                    }
                    else
                    {
                        if (check(nieghbour[j], Clique[index]))
                        {
                            addedgeclique(index, nieghbour[j]);
                        }
                        else
                        {
                            index++;
                        }
                        j++;
                    }
                }
                index++;
                i++;

            }
          

        }

        private bool exists(int j, LinkedList<Tuple<int>> linkedList)
        {
            Boolean flag = false;
            foreach(Tuple<int> item in linkedList)
            {
                if (j == item.Item1)
                {
                    flag = true;
                    return flag;
                }
                else
                    flag = false;
            }
            return flag;
        }

        public bool check(int j, LinkedList<Tuple<int>> linkedList)
        {
            Boolean flag = false;
            foreach( Tuple<int> value in linkedList)
            {
                foreach( Tuple<int, double> item in adjacencyList[j])
                {
                    if(value.Item1 == item.Item1)
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

        public void addedgeclique(int startVertex, int endVertex)
        {
            Clique[startVertex].AddLast(new Tuple<int>(endVertex));
        }

        public void printcliques()
        {
            int i = 0;

            while(i != N)
            {
                Console.Write("Clique[" + i + "]");
                foreach ( Tuple<int> value in Clique[i])
                {

                    Console.Write(value.Item1 + "\t");
                }
                Console.WriteLine();
                i++;
            }
        }
    }
}
